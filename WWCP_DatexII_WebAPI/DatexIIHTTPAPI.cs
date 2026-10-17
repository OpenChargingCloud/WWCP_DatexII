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
using System.Xml.Linq;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Collections.Concurrent;

using Newtonsoft.Json;

using org.GraphDefined.Vanaheimr.Hermod.HTTP;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.WebAPI
{

    /// <summary>
    /// Serves the current DATEX II Energy Infrastructure table and status publications for a pull, e.g. by a national
    /// access point or another subscriber, as DATEX II Exchange 2020 describes a snapshot pull from a simple HTTP server:
    /// <list type="bullet">
    ///   <item><c>{RootPath}/table.xml</c>, <c>{RootPath}/status.xml</c>: DATEX II 3.7 XML (text/xml),</item>
    ///   <item><c>{RootPath}/table.json</c>, <c>{RootPath}/status.json</c>: DATEX II 3.7 JSON,</item>
    ///   <item><c>{RootPath}/mobilithek/table.json</c>, <c>{RootPath}/mobilithek/status.json</c>: the JSON of the Mobilithek
    ///         profile 01-00-00, the status in its message container as a snapshot.</item>
    /// </list>
    /// Every response tells the publication time as Last-Modified, and a request with If-Modified-Since gets 304 Not
    /// Modified when nothing newer was published; before the first publication, the answer is 503 Service Unavailable.
    /// Compressing the responses, e.g. with gzip, and client certificates are up to the HTTP server.
    /// </summary>
    public class DatexIIHTTPAPI : HTTPAPI
    {

        #region Data

        /// <summary>
        /// The formats served, by the path of their resources.
        /// </summary>
        private static readonly (String Path, HTTPContentType ContentType, Func<APayloadPublication, Byte[]> Render)[] formats = [
            ("{0}.xml",             HTTPContentType.Text.       XML_UTF8,   publication => XMLOf (publication)),
            ("{0}.json",            HTTPContentType.Application.JSON_UTF8,  publication => JSONOf(publication, DatexIIJSONDialect.DATEXII_3_7)),
            ("mobilithek/{0}.json", HTTPContentType.Application.JSON_UTF8,  publication => JSONOf(publication, DatexIIJSONDialect.Mobilithek_01_00_00))
        ];

        /// <summary>
        /// The rendered publications: each one is rendered once per format.
        /// </summary>
        private readonly ConditionalWeakTable<APayloadPublication, ConcurrentDictionary<String, Byte[]>> rendered = [];

        #endregion

        #region Properties

        /// <summary>
        /// The current table publication, if there is one yet.
        /// </summary>
        public Func<EnergyInfrastructureTablePublication?>   TablePublication     { get; }

        /// <summary>
        /// The current full status publication, if there is one yet.
        /// </summary>
        public Func<EnergyInfrastructureStatusPublication?>  StatusPublication    { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Serve the current table and status publications, e.g. those of a POI3ToDatexIIPublisher.
        /// </summary>
        /// <param name="HTTPServer">The HTTP server.</param>
        /// <param name="TablePublication">The current table publication, if there is one yet.</param>
        /// <param name="StatusPublication">The current full status publication, if there is one yet.</param>
        /// <param name="RootPath">The path the resources are served under; the root by default.</param>
        public DatexIIHTTPAPI(HTTPServer                                    HTTPServer,
                              Func<EnergyInfrastructureTablePublication?>   TablePublication,
                              Func<EnergyInfrastructureStatusPublication?>  StatusPublication,
                              HTTPPath?                                     RootPath   = null)

            : base(HTTPServer,
                   RootPath: RootPath)

        {

            this.TablePublication   = TablePublication;
            this.StatusPublication  = StatusPublication;

            foreach (var format in formats)
            {

                AddHandler(HTTPPath.Root + String.Format(format.Path, "table"),
                           HTTPMethod:    HTTPMethod.GET,
                           HTTPDelegate:  request => Task.FromResult(Respond(request, this.TablePublication(),  format.Path, format.ContentType, format.Render)));

                AddHandler(HTTPPath.Root + String.Format(format.Path, "status"),
                           HTTPMethod:    HTTPMethod.GET,
                           HTTPDelegate:  request => Task.FromResult(Respond(request, this.StatusPublication()?.AsDelta(false), format.Path, format.ContentType, format.Render)));

            }

        }

        #endregion


        #region (private) Respond(Request, Publication, Format, ContentType, Render)

        /// <summary>
        /// The given publication in the given format: 503 when there is none yet, 304 when it is not newer than
        /// If-Modified-Since tells, else 200 with it.
        /// </summary>
        private HTTPResponse Respond(HTTPRequest                         Request,
                                     APayloadPublication?                Publication,
                                     String                              Format,
                                     HTTPContentType                     ContentType,
                                     Func<APayloadPublication, Byte[]>   Render)
        {

            if (Publication is null)
                return new HTTPResponse.Builder(Request) {
                           HTTPStatusCode  = HTTPStatusCode.ServiceUnavailable,
                           CacheControl    = "no-cache"
                       }.AsImmutable;

            // HTTP dates have whole seconds.
            var lastModified = DateTimeOffset.FromUnixTimeSeconds(Publication.PublicationTime.ToUnixTimeSeconds());

            if (Request.IfModifiedSince is String ifModifiedSince &&
                DateTimeOffset.TryParse(ifModifiedSince, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var since) &&
                lastModified <= since)
            {
                return new HTTPResponse.Builder(Request) {
                           HTTPStatusCode  = HTTPStatusCode.NotModified,
                           LastModified    = lastModified,
                           CacheControl    = "no-cache"
                       }.AsImmutable;
            }

            return new HTTPResponse.Builder(Request) {
                       HTTPStatusCode  = HTTPStatusCode.OK,
                       ContentType     = ContentType,
                       Content         = rendered.GetOrCreateValue(Publication).GetOrAdd(Format, _ => Render(Publication)),
                       LastModified    = lastModified,
                       CacheControl    = "no-cache"
                   }.AsImmutable;

        }

        #endregion

        #region (private static) XMLOf(Publication) / JSONOf(Publication, Dialect)

        private static Byte[] XMLOf(APayloadPublication Publication)

            => Encoding.UTF8.GetBytes(
                   """<?xml version="1.0" encoding="UTF-8"?>""" +
                   (Publication switch {
                       EnergyInfrastructureTablePublication   table   => table. ToXML(),
                       EnergyInfrastructureStatusPublication  status  => status.ToXML(),
                       _                                              => throw new ArgumentException($"Publications of the type '{Publication.GetType().Name}' are not served!")
                   }).ToString(SaveOptions.DisableFormatting));

        private static Byte[] JSONOf(APayloadPublication  Publication,
                                     DatexIIJSONDialect   Dialect)

            => Encoding.UTF8.GetBytes(
                   (Publication switch {
                       EnergyInfrastructureTablePublication   table   => table. ToJSON(Dialect),
                       EnergyInfrastructureStatusPublication  status  => status.ToJSON(Dialect),
                       _                                              => throw new ArgumentException($"Publications of the type '{Publication.GetType().Name}' are not served!")
                   }).ToString(Formatting.None));

        #endregion

    }

}
