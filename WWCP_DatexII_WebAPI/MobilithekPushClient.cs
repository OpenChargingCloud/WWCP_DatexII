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

using System.Text;
using System.IO.Compression;
using System.Net.Security;
using System.Threading.Channels;
using System.Security.Cryptography.X509Certificates;

using Newtonsoft.Json;

using org.GraphDefined.Vanaheimr.Hermod;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.POI3;
using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.WebAPI
{

    /// <summary>
    /// The result of a push to the Mobilithek.
    /// </summary>
    /// <param name="IsSuccess">Whether the Mobilithek took the publication.</param>
    /// <param name="StatusCode">The HTTP status code of the response, if there was one.</param>
    /// <param name="Description">What went wrong, if anything.</param>
    public sealed record MobilithekPushResult(Boolean  IsSuccess,
                                              UInt32?  StatusCode,
                                              String?  Description);

    /// <summary>
    /// A publication was pushed to the Mobilithek, or the push failed.
    /// </summary>
    /// <param name="Sender">The push client.</param>
    /// <param name="PublicationId">The publication of the Mobilithek pushed to.</param>
    /// <param name="Publication">The table or status publication.</param>
    /// <param name="Result">The result.</param>
    public delegate void OnMobilithekPushDelegate(MobilithekPushClient  Sender,
                                                  UInt64                PublicationId,
                                                  APayloadPublication   Publication,
                                                  MobilithekPushResult  Result);


    /// <summary>
    /// Pushes DATEX II Energy Infrastructure publications to the German national access point, the Mobilithek, as its
    /// guideline for AFIR recharging offers wants them: two publications of the data provider, a static one taking the
    /// table publications and a dynamic one taking the status publications, both pushed over HTTPS, as the JSON of the
    /// Mobilithek profile 01-00-00. The status is in its message container, as a snapshotPush or, for a delta, a
    /// deltaPush. Each push is a POST to the publication URL followed by the publication identification, authenticated
    /// by the machine certificate of the data provider's organisation (mutual TLS), gzip compressed by default.
    /// </summary>
    /// <remarks>
    /// Attached to a POI3ToDatexIIPublisher, it pushes its publications one after the other, in their order. A table
    /// that could not be pushed is pushed again before the next status publication. After a status publication could not
    /// be pushed, the Mobilithek misses a change, so the next status publication pushed is the full status, a snapshot,
    /// rather than a delta. For the Mobilithek profile, create the publisher with its delta interval and snapshot rules
    /// (MobilithekStatusDeltaInterval, MobilithekSnapshotInterval and MobilithekMaxDeltasPerSnapshot).
    /// </remarks>
    public sealed class MobilithekPushClient : IAsyncDisposable
    {

        #region Data

        /// <summary>
        /// The publication URL of the Mobilithek: a publication identification is appended to it.
        /// </summary>
        public static readonly URL DefaultPublicationURL = URL.Parse("https://mobilithek.info:8443/mobilithek/api/v1.0/publication/");

        private readonly HTTPClient                        httpClient;
        private readonly Channel<Func<Task>>               work           = Channel.CreateUnbounded<Func<Task>>(new UnboundedChannelOptions { SingleReader = true });
        private          Task?                             worker;
        private          POI3ToDatexIIPublisher?           publisher;

        // Only the worker touches them.
        private          EnergyInfrastructureTablePublication?  pendingTable;
        private          Boolean                                snapshotNeeded;

        #endregion

        #region Properties

        /// <summary>
        /// The publication of the Mobilithek taking the table publications (static data, without deltas).
        /// </summary>
        public UInt64   StaticPublicationId     { get; }

        /// <summary>
        /// The publication of the Mobilithek taking the status publications (dynamic data, with deltas).
        /// </summary>
        public UInt64   DynamicPublicationId    { get; }

        /// <summary>
        /// The publication URL: a publication identification is appended to it.
        /// </summary>
        public URL      PublicationURL          { get; }

        /// <summary>
        /// Whether the publications are pushed gzip compressed.
        /// </summary>
        public Boolean  Compress                { get; }

        /// <summary>
        /// The longest time a push may take.
        /// </summary>
        public TimeSpan RequestTimeout          { get; }

        #endregion

        #region Events

        /// <summary>
        /// A publication was pushed, or the push failed.
        /// </summary>
        public event OnMobilithekPushDelegate?  OnPush;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Push to the given publications of the Mobilithek.
        /// </summary>
        /// <param name="StaticPublicationId">The publication of the Mobilithek taking the table publications.</param>
        /// <param name="DynamicPublicationId">The publication of the Mobilithek taking the status publications.</param>
        /// <param name="MachineCertificate">The machine certificate of the data provider's organisation, with its private key, as the Mobilithek issued it.</param>
        /// <param name="TrustedRootCertificates">The root certificates the certificate of the Mobilithek is checked against, e.g. its certificate chain from mobilithek.info/help/download; else the operating system checks it.</param>
        /// <param name="PublicationURL">The publication URL; that of the Mobilithek by default.</param>
        /// <param name="Compress">Whether to push gzip compressed; true by default.</param>
        /// <param name="RequestTimeout">The longest time a push may take; 60 seconds by default.</param>
        public MobilithekPushClient(UInt64                           StaticPublicationId,
                                    UInt64                           DynamicPublicationId,
                                    X509Certificate2?                MachineCertificate        = null,
                                    IEnumerable<X509Certificate2>?   TrustedRootCertificates   = null,
                                    URL?                             PublicationURL            = null,
                                    Boolean                          Compress                  = true,
                                    TimeSpan?                        RequestTimeout            = null)
        {

            this.StaticPublicationId   = StaticPublicationId;
            this.DynamicPublicationId  = DynamicPublicationId;
            this.PublicationURL        = PublicationURL ?? DefaultPublicationURL;
            this.Compress              = Compress;
            this.RequestTimeout        = RequestTimeout ?? TimeSpan.FromSeconds(60);

            var roots                  = TrustedRootCertificates?.ToArray();

            this.httpClient            = new HTTPClient(this.PublicationURL,
                                                        RemoteCertificateValidator:  roots is { Length: > 0 }
                                                                                         ? ValidateBy(roots)
                                                                                         : null,
                                                        ClientCertificates:          MachineCertificate is not null
                                                                                         ? [ MachineCertificate ]
                                                                                         : null);

            this.worker                = Task.Run(WorkAsync);

        }

        #endregion


        #region Attach(Publisher)

        /// <summary>
        /// Push every publication of the given publisher from now on, one after the other. Attach it before it starts,
        /// to push its first table and full status as well.
        /// </summary>
        /// <param name="Publisher">A publisher.</param>
        public void Attach(POI3ToDatexIIPublisher Publisher)
        {

            if (publisher is not null)
                throw new InvalidOperationException("A publisher is attached already!");

            publisher = Publisher;

            Publisher.OnTablePublication   += TablePublished;
            Publisher.OnStatusPublication  += StatusPublished;

        }

        private Task TablePublished(POI3ToDatexIIPublisher                Sender,
                                    EnergyInfrastructureTablePublication  Publication)
        {
            work.Writer.TryWrite(async () => {
                pendingTable = Publication;
                await PushPendingTableAsync();
            });
            return Task.CompletedTask;
        }

        private Task StatusPublished(POI3ToDatexIIPublisher                 Sender,
                                     EnergyInfrastructureStatusPublication  Publication,
                                     Boolean                                IsDelta)
        {
            work.Writer.TryWrite(async () => {

                // A status refers to its table.
                await PushPendingTableAsync();

                // After a failed push the Mobilithek misses a change: the full status rather than a delta.
                var status  = IsDelta && snapshotNeeded && Sender.CurrentStatusPublication is EnergyInfrastructureStatusPublication full
                                  ? full.AsDelta(false)
                                  : Publication;

                var result  = await PushStatusAsync(status);

                snapshotNeeded = !result.IsSuccess || (snapshotNeeded && status.IsDelta);

            });
            return Task.CompletedTask;
        }

        private async Task PushPendingTableAsync()
        {

            if (pendingTable is null)
                return;

            if ((await PushTableAsync(pendingTable)).IsSuccess)
                pendingTable = null;

        }

        #endregion

        #region PushTableAsync (Publication, CancellationToken = default)

        /// <summary>
        /// Push the given table publication to the static publication.
        /// </summary>
        /// <param name="Publication">A table publication.</param>
        /// <param name="CancellationToken">A cancellation token.</param>
        public Task<MobilithekPushResult> PushTableAsync(EnergyInfrastructureTablePublication  Publication,
                                                         CancellationToken                     CancellationToken   = default)

            => PushAsync(StaticPublicationId,
                         Publication,
                         Publication.ToJSON(DatexIIJSONDialect.Mobilithek_01_00_00).ToString(Formatting.None),
                         CancellationToken);

        #endregion

        #region PushStatusAsync(Publication, CancellationToken = default)

        /// <summary>
        /// Push the given status publication to the dynamic publication: a snapshotPush, or a deltaPush when it is a delta.
        /// </summary>
        /// <param name="Publication">A status publication.</param>
        /// <param name="CancellationToken">A cancellation token.</param>
        public Task<MobilithekPushResult> PushStatusAsync(EnergyInfrastructureStatusPublication  Publication,
                                                          CancellationToken                      CancellationToken   = default)

            => PushAsync(DynamicPublicationId,
                         Publication,
                         Publication.ToJSON(DatexIIJSONDialect.Mobilithek_01_00_00).ToString(Formatting.None),
                         CancellationToken);

        #endregion


        #region (private) PushAsync(PublicationId, Publication, JSON, CancellationToken)

        private async Task<MobilithekPushResult> PushAsync(UInt64               PublicationId,
                                                           APayloadPublication  Publication,
                                                           String               JSON,
                                                           CancellationToken    CancellationToken)
        {

            MobilithekPushResult result;

            try
            {

                var content   = Encoding.UTF8.GetBytes(JSON);

                if (Compress)
                    content = GZip(content);

                var response  = await httpClient.POST(PublicationURL.Path + PublicationId.ToString(),
                                                      content,
                                                      HTTPContentType.Application.JSON_UTF8,
                                                      RequestTimeout:     RequestTimeout,
                                                      RequestBuilder:     builder => {
                                                                              if (Compress)
                                                                                  builder.SetContentEncoding("gzip");
                                                                          },
                                                      CancellationToken:  CancellationToken);

                var code      = response.HTTPStatusCode.Code;

                result        = response.HTTPStatusCode.IsSuccessful
                                    ? new MobilithekPushResult(true, code, null)
                                    : new MobilithekPushResult(false, code, code switch {
                                          400  => "The publication identification is invalid!",
                                          403  => "The machine certificate is not authorised for the publication, or the offer does not take HTTPS pushes!",
                                          404  => "The publication is unknown or expired!",
                                          422  => "The offer takes deltas, but the publication has no valid exchange protocol or an invalid syntax!",
                                          _    => $"The Mobilithek answered {code}!"
                                      });

            }
            catch (Exception e)
            {
                result = new MobilithekPushResult(false, null, $"The push failed: {e.Message}");
            }

            try
            {
                OnPush?.Invoke(this, PublicationId, Publication, result);
            }
            catch
            { }

            return result;

        }

        #endregion

        #region (private) WorkAsync()

        private async Task WorkAsync()
        {
            await foreach (var job in work.Reader.ReadAllAsync())
            {
                try
                {
                    await job();
                }
                catch
                { }
            }
        }

        #endregion

        #region (private static) GZip(Content) / ValidateBy(RootCertificates)

        private static Byte[] GZip(Byte[] Content)
        {

            using var compressed = new MemoryStream();

            using (var gzip = new GZipStream(compressed, CompressionLevel.Optimal, leaveOpen: true))
                gzip.Write(Content);

            return compressed.ToArray();

        }

        /// <summary>
        /// Check the certificate of the Mobilithek against the given root certificates rather than those of the
        /// operating system: the Mobilithek signs its server certificate itself.
        /// </summary>
        private static RemoteTLSServerCertificateValidationHandler<IHTTPClient> ValidateBy(IReadOnlyList<X509Certificate2> RootCertificates)

            => (sender, certificate, chain, client, policyErrors) => {

                   if (certificate is null)
                       return TLSValidationResult.Failed("The server sent no certificate!");

                   if (policyErrors.HasFlag(SslPolicyErrors.RemoteCertificateNameMismatch))
                       return TLSValidationResult.Failed("The certificate of the server is not for its name!");

                   using var x509Chain = new X509Chain();

                   x509Chain.ChainPolicy.TrustMode          = X509ChainTrustMode.CustomRootTrust;
                   x509Chain.ChainPolicy.RevocationMode     = X509RevocationMode.NoCheck;
                   x509Chain.ChainPolicy.CustomTrustStore.AddRange(RootCertificates.ToArray());

                   if (chain is not null)
                       foreach (var element in chain.ChainElements)
                           x509Chain.ChainPolicy.ExtraStore.Add(element.Certificate);

                   var isValid = x509Chain.Build(certificate);

                   return TLSValidationResult.From(isValid,
                                                   x509Chain.ChainStatus.Select(status => status.StatusInformation));

               };

        #endregion

        #region DisposeAsync()

        /// <summary>
        /// Stop pushing, after what is waiting.
        /// </summary>
        public async ValueTask DisposeAsync()
        {

            if (publisher is not null)
            {
                publisher.OnTablePublication   -= TablePublished;
                publisher.OnStatusPublication  -= StatusPublished;
            }

            work.Writer.TryComplete();

            if (worker is not null)
                await worker;

            await httpClient.DisposeAsync();

        }

        #endregion

    }

}
