/*
 * Copyright (c) 2014-2026 GraphDefined GmbH <achim.friedland@graphdefined.com>
 * This file is part of WWCP DatexII <https://github.com/OpenChargingCloud/WWCP_DatexII>
 *
 * Licensed under the Affero GPL license, Version 3.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.gnu.org/licenses/agpl.html
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System.Net;
using System.Text;
using System.IO.Compression;
using System.Net.Http.Headers;

using NUnit.Framework;

using Newtonsoft.Json.Linq;

using org.GraphDefined.Vanaheimr.Aegir;
using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.POI3;
using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.WebAPI;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;
using cloud.charging.open.protocols.WWCP.POI.tests;

using POI = cloud.charging.open.protocols.WWCP.POI;

#endregion

namespace cloud.charging.open.protocols.DatexII.Tests.WebAPI
{

    /// <summary>
    /// Publications delivered: grouped status deltas and snapshots of the publisher, served for a pull, and pushed to
    /// the Mobilithek.
    /// </summary>
    [TestFixture]
    public class TransportTests : AXMLSchemaValidation
    {

        #region Data

        private static readonly DateTimeOffset                  Created  = DateTimeOffset.Parse("2026-01-01T00:00:00Z");
        private static readonly POI.ChargingStationOperator_Id  GEF      = POI.ChargingStationOperator_Id.Parse("DE*GEF");
        private static readonly POI.EVSE_Id                     E1       = POI.EVSE_Id.Parse("DE*GEF*E1*1");
        private static readonly InternationalIdentifier         Creator  = new (Country.Germany, "DE-NAP-GEF");

        #endregion

        #region (private static) ANetwork() / SetStatus(History, Status) / Eventually(Condition)

        private static POI.RoamingNetwork ANetwork()

            => new POI.RoamingNetwork(POI.RoamingNetwork_Id.Parse("rn1"),
                                      Created:                   Created,
                                      ChargingStationOperators:  [ new POI.ChargingStationOperator(GEF,
                                                                                                   Name:           I18NString.Create(Languages.en, "GraphDefined"),
                                                                                                   ChargingPools:  [ new POI.ChargingPool(POI.ChargingPool_Id.Parse("DE*GEF*P1"),
                                                                                                                                          I18NString.Create(Languages.en, "Biberweg"),
                                                                                                                                          GeoLocation:       GeoCoordinate.Parse(50.92, 11.58),
                                                                                                                                          ChargingStations:  [ new POI.ChargingStation(POI.ChargingStation_Id.Parse("DE*GEF*S1"),
                                                                                                                                                                                       EVSEs:    [ new POI.EVSE(E1,
                                                                                                                                                                                                                CurrentTypes:        POI.CurrentTypes.AC_ThreePhases,
                                                                                                                                                                                                                MaxPower:            Watt.FromW(22000),
                                                                                                                                                                                                                ChargingConnectors:  [ new POI.ChargingConnector(POI.ChargingConnector_Id.Parse("1"), POI.ChargingConnectorType.IEC_62196_T2) ],
                                                                                                                                                                                                                Created:             Created) ],
                                                                                                                                                                                       Created:  Created) ],
                                                                                                                                          Created:           Created) ],
                                                                                                   Created:        Created) ]).WithKeys();

        private static void SetStatus(POI.RoamingNetworkHistory  History,
                                      String                     Status)

            => History.RuntimeStatus.TryUpdate(POI.POIRuntimeStatusKind.Status,
                                               nameof(POI.EVSE),
                                               E1.ToString(),
                                               Status,
                                               Timestamp.Now,
                                               out _,
                                               out _);

        private static async Task<Boolean> Eventually(Func<Boolean> Condition)
        {

            for (var i = 0; i < 200; i++)
            {
                if (Condition())
                    return true;
                await Task.Delay(25);
            }

            return Condition();

        }

        #endregion


        #region StatusDeltasAreGroupedPerInterval()

        /// <summary>
        /// With a status delta interval, the first change is published at once, and the changes until the interval is
        /// over are grouped into one delta, as they are then.
        /// </summary>
        [Test]
        public async Task StatusDeltasAreGroupedPerInterval()
        {

            var history  = POI.RoamingNetworkHistory.Start(ANetwork());
            var deltas   = new List<EnergyInfrastructureStatusPublication>();

            await using var publisher = new POI3ToDatexIIPublisher(history, Creator, Languages.de, StatusDeltaInterval: TimeSpan.FromMilliseconds(600));

            publisher.OnStatusPublication += (sender, publication, isDelta) => { if (isDelta) lock (deltas) deltas.Add(publication); return Task.CompletedTask; };

            await publisher.StartAsync();

            SetStatus(history, "charging");

            Assert.That(await Eventually(() => { lock (deltas) return deltas.Count == 1; }), Is.True, "The first change was not published at once.");

            SetStatus(history, "available");
            SetStatus(history, "occupied");

            Assert.That(await Eventually(() => { lock (deltas) return deltas.Count == 2; }), Is.True, "The grouped changes were not published.");

            await Task.Delay(800);

            lock (deltas)
            {
                Assert.That(deltas.Count, Is.EqualTo(2));
                Assert.That(deltas[1].EnergyInfrastructureSiteStatus.Single().EnergyInfrastructureStationStatus.Single().RefillPointStatus.Single().Status.ToString(), Is.EqualTo("occupied"));
            }

        }

        #endregion

        #region TheFullStatusFollowsAfterTheMostDeltasAndTheSnapshotInterval()

        /// <summary>
        /// After the most deltas per snapshot, and when the snapshot interval is over, the full status is published again.
        /// </summary>
        [Test]
        public async Task TheFullStatusFollowsAfterTheMostDeltasAndTheSnapshotInterval()
        {

            var history     = POI.RoamingNetworkHistory.Start(ANetwork());
            var published   = new List<Boolean>();

            await using var publisher = new POI3ToDatexIIPublisher(history, Creator, Languages.de, MaxDeltasPerSnapshot: 2, SnapshotInterval: TimeSpan.FromSeconds(3));

            publisher.OnStatusPublication += (sender, publication, isDelta) => { lock (published) published.Add(isDelta); return Task.CompletedTask; };

            await publisher.StartAsync();

            // The full status at the start, two deltas, and the full status again.
            SetStatus(history, "charging");
            Assert.That(await Eventually(() => { lock (published) return published.Count >= 2; }), Is.True);

            SetStatus(history, "available");
            Assert.That(await Eventually(() => { lock (published) return published.Count >= 4; }), Is.True, () => { lock (published) return String.Join(", ", published); });

            lock (published)
                Assert.That(published, Is.EqualTo(new[] { false, true, true, false }));

            // And again when the snapshot interval is over.
            Assert.That(await Eventually(() => { lock (published) return published.Count == 5; }), Is.True, "No snapshot after the snapshot interval.");

            lock (published)
                Assert.That(published[4], Is.False);

        }

        #endregion

        #region ThePublicationsAreServedForAPull()

        /// <summary>
        /// The current table and full status, served as XML, as DATEX II 3.7 JSON and as Mobilithek JSON, with
        /// Last-Modified and 304 Not Modified; 503 before the first publication.
        /// </summary>
        [Test]
        public async Task ThePublicationsAreServedForAPull()
        {

            var history  = POI.RoamingNetworkHistory.Start(ANetwork());

            await using var publisher = new POI3ToDatexIIPublisher(history, Creator, Languages.de, StationIdBNetzA: station => "BNetzA-4711");

            await using var httpServer = new HTTPServer(IPAddress:  IPv4Address.Localhost,
                                                        TCPPort:    IPPort.Zero,
                                                        AutoStart:  true);

            _ = new DatexIIHTTPAPI(httpServer,
                                         () => publisher.CurrentTablePublication,
                                         () => publisher.CurrentStatusPublication,
                                         HTTPPath.Parse("/datex"));

            using var httpClient = new HttpClient { BaseAddress = new Uri($"http://127.0.0.1:{httpServer.TCPPort}/datex/") };

            // Nothing published yet.
            Assert.That((await httpClient.GetAsync("table.xml")).StatusCode, Is.EqualTo(HttpStatusCode.ServiceUnavailable));

            await publisher.StartAsync();

            var tableXML = await httpClient.GetAsync("table.xml");

            Assert.Multiple(() => {
                Assert.That(tableXML.StatusCode,                           Is.EqualTo(HttpStatusCode.OK));
                Assert.That(tableXML.Content.Headers.ContentType?.MediaType, Is.EqualTo("text/xml"));
                Assert.That(tableXML.Content.Headers.LastModified,         Is.Not.Null);
            });

            Assert.That(ValidateTableSchema(await tableXML.Content.ReadAsStringAsync(), out _, out var errors), Is.True, String.Join(Environment.NewLine, errors));

            // Not modified since.
            var conditional = new HttpRequestMessage(HttpMethod.Get, "table.xml");
            conditional.Headers.IfModifiedSince = tableXML.Content.Headers.LastModified;

            Assert.That((await httpClient.SendAsync(conditional)).StatusCode, Is.EqualTo(HttpStatusCode.NotModified));

            // DATEX II 3.7 JSON and Mobilithek JSON.
            var statusXML         = await httpClient.GetAsync("status.xml");
            var statusJSON        = JObject.Parse(await httpClient.GetStringAsync("status.json"));
            var mobilithekTable   = JObject.Parse(await httpClient.GetStringAsync("mobilithek/table.json"));
            var mobilithekStatus  = JObject.Parse(await httpClient.GetStringAsync("mobilithek/status.json"));

            Assert.That(ValidateStatusSchema(await statusXML.Content.ReadAsStringAsync(), out _, out errors),       Is.True, String.Join(Environment.NewLine, errors));
            Assert.That(JSONSchemas.Validate(JSONSchemas.DATEXII_3_7_Payload,      statusJSON,       out var jsonErrors),  Is.True, String.Join(Environment.NewLine, jsonErrors));
            Assert.That(JSONSchemas.Validate(JSONSchemas.Mobilithek_Table,         mobilithekTable,  out jsonErrors),      Is.True, String.Join(Environment.NewLine, jsonErrors));
            Assert.That(JSONSchemas.Validate(JSONSchemas.Mobilithek_StatusMessage, mobilithekStatus, out jsonErrors),      Is.True, String.Join(Environment.NewLine, jsonErrors));

            // The full status: a snapshot.
            Assert.That(mobilithekStatus.SelectToken("$..codedExchangeProtocol.value")?.Value<String>(), Is.EqualTo("snapshotPush"));

        }

        #endregion

        #region ThePublicationsArePushedToTheMobilithek()

        /// <summary>
        /// Attached to a publisher, the push client pushes its table to the static publication and its status to the
        /// dynamic one, as Mobilithek JSON, gzip compressed. After a failed delta, the next push is a snapshot.
        /// </summary>
        [Test]
        public async Task ThePublicationsArePushedToTheMobilithek()
        {

            var pushes      = new List<(String PublicationId, JObject JSON)>();
            var failNext    = false;

            await using var httpServer = new HTTPServer(IPAddress:  IPv4Address.Localhost,
                                                        TCPPort:    IPPort.Zero,
                                                        AutoStart:  true);

            // The Mobilithek, as far as a data provider sees it.
            var mobilithek = new HTTPAPI(httpServer);

            mobilithek.AddHandler(HTTPPath.Parse("/mobilithek/api/v1.0/publication/{publicationId}"),
                                  HTTPMethod:    HTTPMethod.POST,
                                  HTTPDelegate:  request => {

                                      var body = request.HTTPBody ?? [];

                                      // gzip, unless the server decoded it already.
                                      if (body.Length > 2 && body[0] == 0x1f && body[1] == 0x8b)
                                      {
                                          using var gzip     = new GZipStream(new MemoryStream(body), CompressionMode.Decompress);
                                          using var decoded  = new MemoryStream();
                                          gzip.CopyTo(decoded);
                                          body = decoded.ToArray();
                                      }

                                      lock (pushes)
                                      {

                                          if (failNext)
                                          {
                                              failNext = false;
                                              return Task.FromResult(new HTTPResponse.Builder(request) { HTTPStatusCode = HTTPStatusCode.InternalServerError }.AsImmutable);
                                          }

                                          pushes.Add((request.ParsedURLParameters[0], JObject.Parse(Encoding.UTF8.GetString(body))));

                                      }

                                      return Task.FromResult(new HTTPResponse.Builder(request) { HTTPStatusCode = HTTPStatusCode.OK }.AsImmutable);

                                  });

            var history  = POI.RoamingNetworkHistory.Start(ANetwork());

            await using var publisher  = new POI3ToDatexIIPublisher(history, Creator, Languages.de, StationIdBNetzA: station => "BNetzA-4711");
            await using var pusher     = new MobilithekPushClient(11, 22, PublicationURL: URL.Parse($"http://127.0.0.1:{httpServer.TCPPort}/mobilithek/api/v1.0/publication/"));

            var results  = new List<MobilithekPushResult>();
            pusher.OnPush += (sender, publicationId, publication, result) => { lock (results) results.Add(result); };

            pusher.Attach(publisher);

            await publisher.StartAsync();

            Assert.That(await Eventually(() => { lock (pushes) return pushes.Count == 2; }), Is.True, "The table and the full status were not pushed.");

            String? ProtocolOf(Int32 Index)
            {
                lock (pushes)
                    return pushes[Index].JSON.SelectToken("$..codedExchangeProtocol.value")?.Value<String>();
            }

            lock (pushes)
            {
                Assert.That(pushes[0].PublicationId, Is.EqualTo("11"));
                Assert.That(pushes[1].PublicationId, Is.EqualTo("22"));
                Assert.That(JSONSchemas.Validate(JSONSchemas.Mobilithek_Table,         pushes[0].JSON, out var errors), Is.True, String.Join(Environment.NewLine, errors));
                Assert.That(JSONSchemas.Validate(JSONSchemas.Mobilithek_StatusMessage, pushes[1].JSON, out errors),     Is.True, String.Join(Environment.NewLine, errors));
            }

            Assert.That(ProtocolOf(1), Is.EqualTo("snapshotPush"));

            // A delta.
            SetStatus(history, "charging");

            Assert.That(await Eventually(() => { lock (pushes) return pushes.Count == 3; }), Is.True);
            Assert.That(ProtocolOf(2), Is.EqualTo("deltaPush"));

            // A failed delta: the next push is the full status.
            lock (pushes)
                failNext = true;

            SetStatus(history, "available");

            Assert.That(await Eventually(() => { lock (results) return results.Count(result => !result.IsSuccess) == 1; }), Is.True);

            SetStatus(history, "occupied");

            Assert.That(await Eventually(() => { lock (pushes) return pushes.Count == 4; }), Is.True);
            Assert.That(ProtocolOf(3), Is.EqualTo("snapshotPush"));

            // And deltas again.
            SetStatus(history, "charging");

            Assert.That(await Eventually(() => { lock (pushes) return pushes.Count == 5; }), Is.True);
            Assert.That(ProtocolOf(4), Is.EqualTo("deltaPush"));

        }

        #endregion

        #region (private static) AMobilithek(HTTPServer, Answer, Pushes)

        /// <summary>
        /// The Mobilithek as far as a data provider sees it: every push is counted, and answered as the given delegate
        /// tells for the publication and the number of pushes to it so far.
        /// </summary>
        private static void AMobilithek(HTTPServer                                   HTTPServer,
                                        Func<String, Int32, HTTPStatusCode>          Answer,
                                        List<(String PublicationId, JObject JSON)>   Pushes)
        {

            var tries = new Dictionary<String, Int32>();

            new HTTPAPI(HTTPServer).AddHandler(HTTPPath.Parse("/mobilithek/api/v1.0/publication/{publicationId}"),
                                               HTTPMethod:    HTTPMethod.POST,
                                               HTTPDelegate:  request => {

                                                   var publicationId  = request.ParsedURLParameters[0];
                                                   var body           = request.HTTPBody ?? [];

                                                   if (body.Length > 2 && body[0] == 0x1f && body[1] == 0x8b)
                                                   {
                                                       using var gzip     = new GZipStream(new MemoryStream(body), CompressionMode.Decompress);
                                                       using var decoded  = new MemoryStream();
                                                       gzip.CopyTo(decoded);
                                                       body = decoded.ToArray();
                                                   }

                                                   HTTPStatusCode statusCode;

                                                   lock (Pushes)
                                                   {
                                                       tries[publicationId] = tries.GetValueOrDefault(publicationId) + 1;
                                                       statusCode = Answer(publicationId, tries[publicationId]);
                                                       Pushes.Add((publicationId, JObject.Parse(Encoding.UTF8.GetString(body))));
                                                   }

                                                   return Task.FromResult(new HTTPResponse.Builder(request) { HTTPStatusCode = statusCode }.AsImmutable);

                                               });

        }

        #endregion

        #region AFailedPushIsTriedAgainWithBackoff()

        /// <summary>
        /// A status push the Mobilithek could not take for a passing reason is tried again without a new publication:
        /// after the retry delay, then twice as long, as the full status.
        /// </summary>
        [Test]
        public async Task AFailedPushIsTriedAgainWithBackoff()
        {

            var pushes = new List<(String PublicationId, JObject JSON)>();

            await using var httpServer = new HTTPServer(IPAddress: IPv4Address.Localhost, TCPPort: IPPort.Zero, AutoStart: true);

            // The first two pushes of the status are answered 503 Service Unavailable.
            AMobilithek(httpServer,
                        (publicationId, tries) => publicationId == "22" && tries <= 2 ? HTTPStatusCode.ServiceUnavailable : HTTPStatusCode.OK,
                        pushes);

            var history  = POI.RoamingNetworkHistory.Start(ANetwork());

            await using var publisher  = new POI3ToDatexIIPublisher(history, Creator, Languages.de, StationIdBNetzA: station => "BNetzA-4711");
            await using var pusher     = new MobilithekPushClient(11, 22,
                                                                  PublicationURL:  URL.Parse($"http://127.0.0.1:{httpServer.TCPPort}/mobilithek/api/v1.0/publication/"),
                                                                  RetryDelay:      TimeSpan.FromMilliseconds(300),
                                                                  MaxRetryDelay:   TimeSpan.FromSeconds(1));

            var results  = new List<(DateTimeOffset At, MobilithekPushResult Result)>();
            pusher.OnPush += (sender, publicationId, publication, result) => { if (publicationId == 22) lock (results) results.Add((Timestamp.Now, result)); };

            pusher.Attach(publisher);

            await publisher.StartAsync();

            // The status: failed, failed, and taken, with nothing published in between.
            Assert.That(await Eventually(() => { lock (results) return results.Count == 3; }), Is.True, () => { lock (results) return $"{results.Count} pushes of the status"; });

            lock (results)
            {
                Assert.That(results.Select(result => result.Result.IsSuccess), Is.EqualTo(new[] { false, false, true }));
                // 300 ms, then 600 ms.
                Assert.That(results[1].At - results[0].At, Is.GreaterThanOrEqualTo(TimeSpan.FromMilliseconds(250)));
                Assert.That(results[2].At - results[1].At, Is.GreaterThanOrEqualTo(TimeSpan.FromMilliseconds(550)));
            }

            lock (pushes)
                Assert.That(pushes.Last().JSON.SelectToken("$..codedExchangeProtocol.value")?.Value<String>(), Is.EqualTo("snapshotPush"));

            // And no more tries once taken.
            await Task.Delay(1500);

            lock (results)
                Assert.That(results, Has.Count.EqualTo(3));

        }

        #endregion

        #region AWrongConfigurationIsNotTriedAgain()

        /// <summary>
        /// 404, an unknown publication, tells a wrong configuration: it is not tried again until the next publication.
        /// </summary>
        [Test]
        public async Task AWrongConfigurationIsNotTriedAgain()
        {

            var pushes = new List<(String PublicationId, JObject JSON)>();

            await using var httpServer = new HTTPServer(IPAddress: IPv4Address.Localhost, TCPPort: IPPort.Zero, AutoStart: true);

            AMobilithek(httpServer,
                        (publicationId, tries) => publicationId == "22" ? HTTPStatusCode.NotFound : HTTPStatusCode.OK,
                        pushes);

            var history  = POI.RoamingNetworkHistory.Start(ANetwork());

            await using var publisher  = new POI3ToDatexIIPublisher(history, Creator, Languages.de, StationIdBNetzA: station => "BNetzA-4711");
            await using var pusher     = new MobilithekPushClient(11, 22,
                                                                  PublicationURL:  URL.Parse($"http://127.0.0.1:{httpServer.TCPPort}/mobilithek/api/v1.0/publication/"),
                                                                  RetryDelay:      TimeSpan.FromMilliseconds(100));

            var results  = new List<MobilithekPushResult>();
            pusher.OnPush += (sender, publicationId, publication, result) => { if (publicationId == 22) lock (results) results.Add(result); };

            pusher.Attach(publisher);

            await publisher.StartAsync();

            Assert.That(await Eventually(() => { lock (results) return results.Count == 1; }), Is.True);

            await Task.Delay(1000);

            lock (results)
            {
                Assert.That(results,                 Has.Count.EqualTo(1));
                Assert.That(results[0].StatusCode,   Is.EqualTo(404));
                Assert.That(results[0].Description,  Does.Contain("unknown or expired"));
            }

            // The next publication tries again.
            SetStatus(history, "charging");

            Assert.That(await Eventually(() => { lock (results) return results.Count == 2; }), Is.True);

        }

        #endregion

    }

}
