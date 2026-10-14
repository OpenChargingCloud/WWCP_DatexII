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

using System.Xml.Linq;
using System.Threading.Channels;

using org.GraphDefined.Vanaheimr.Illias;

using cloud.charging.open.protocols.DatexII.v3.Common;
using cloud.charging.open.protocols.DatexII.v3.EnergyInfrastructure;

using POI = cloud.charging.open.protocols.WWCP.POI;

#endregion

namespace cloud.charging.open.protocols.DatexII.v3.POI3
{

    /// <summary>
    /// Something could not be mapped, published or imported.
    /// </summary>
    /// <param name="Sender">The publisher or importer.</param>
    /// <param name="Description">What went wrong.</param>
    /// <param name="Warnings">What could not be mapped, if anything.</param>
    public delegate void OnPOI3DatexIIErrorDelegate(Object               Sender,
                                                    String               Description,
                                                    IEnumerable<String>  Warnings);

    /// <summary>
    /// A new table publication, e.g. to push it to a national access point.
    /// </summary>
    /// <param name="Sender">The publisher.</param>
    /// <param name="Publication">The table publication.</param>
    public delegate Task OnTablePublicationDelegate(POI3ToDatexIIPublisher                Sender,
                                                    EnergyInfrastructureTablePublication  Publication);

    /// <summary>
    /// A new status publication, e.g. to push it to a national access point.
    /// </summary>
    /// <param name="Sender">The publisher.</param>
    /// <param name="Publication">The status publication.</param>
    /// <param name="IsDelta">Whether it has only what changed, rather than the status of every refill point.</param>
    public delegate Task OnStatusPublicationDelegate(POI3ToDatexIIPublisher                 Sender,
                                                     EnergyInfrastructureStatusPublication  Publication,
                                                     Boolean                                IsDelta);


    /// <summary>
    /// The CPO side: a POI3 history becomes DATEX II Energy Infrastructure table and status publications, e.g. for
    /// the national access point a CPO has to publish to under AFIR.
    /// </summary>
    /// <remarks>
    /// At the start, the head becomes the current table publication, and the status of every EVSE the current status
    /// publication. Every new commit maps the head again: a table with other content gets the revision of the commit as
    /// its version, is published, and so is the full status, whose references carry the new versions. Every change of
    /// the status or admin status of an EVSE renews the current status publication, and publishes a delta with that EVSE.
    /// Serving the current publications, e.g. over HTTP, and pushing them is up to the subscribers of the events.
    /// The work is done one after the other, in the order it came.
    /// </remarks>
    public sealed class POI3ToDatexIIPublisher : IAsyncDisposable
    {

        #region Data

        private readonly Channel<Func<Task>>  work         = Channel.CreateUnbounded<Func<Task>>(new UnboundedChannelOptions { SingleReader = true });
        private          XElement?            publishedSites;
        private          Task?                worker;

        #endregion

        #region Properties

        /// <summary>
        /// The history published.
        /// </summary>
        public POI.RoamingNetworkHistory                       History                     { get; }

        /// <summary>
        /// The creator of the publications, e.g. the national access point identification of the CPO.
        /// </summary>
        public InternationalIdentifier                         PublicationCreator          { get; }

        /// <summary>
        /// The language of the publications.
        /// </summary>
        public Languages                                       Language                    { get; }

        /// <summary>
        /// The identification of the table.
        /// </summary>
        public String                                          TableId                     { get; }

        /// <summary>
        /// The operators published; all, unless given.
        /// </summary>
        public IReadOnlySet<POI.ChargingStationOperator_Id>?   OperatorIds                 { get; }

        /// <summary>
        /// Tells the station identification of the Bundesnetzagentur of a charging station.
        /// </summary>
        public StationIdBNetzADelegate?                        StationIdBNetzA             { get; }

        /// <summary>
        /// The current table publication, once started.
        /// </summary>
        public EnergyInfrastructureTablePublication?           CurrentTablePublication     { get; private set; }

        /// <summary>
        /// The current status publication with the status of every EVSE, once started.
        /// </summary>
        public EnergyInfrastructureStatusPublication?          CurrentStatusPublication    { get; private set; }

        /// <summary>
        /// The version of the current table: the revision of the commit it was made of.
        /// </summary>
        public String?                                         TableVersion                { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// A new table publication.
        /// </summary>
        public event OnTablePublicationDelegate?   OnTablePublication;

        /// <summary>
        /// A new status publication: a full one or a delta.
        /// </summary>
        public event OnStatusPublicationDelegate?  OnStatusPublication;

        /// <summary>
        /// Something could not be mapped or published.
        /// </summary>
        public event OnPOI3DatexIIErrorDelegate?   OnError;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Publish the given history as DATEX II Energy Infrastructure publications.
        /// </summary>
        /// <param name="History">A history of a roaming network.</param>
        /// <param name="PublicationCreator">The creator of the publications, e.g. the national access point identification of the CPO.</param>
        /// <param name="Language">The language of the publications; English by default.</param>
        /// <param name="TableId">The identification of the table; the roaming network identification by default.</param>
        /// <param name="OperatorIds">The operators to publish; all, unless given.</param>
        /// <param name="StationIdBNetzA">Tells the station identification of the Bundesnetzagentur of a charging station.</param>
        public POI3ToDatexIIPublisher(POI.RoamingNetworkHistory                      History,
                                      InternationalIdentifier                        PublicationCreator,
                                      Languages?                                     Language          = null,
                                      String?                                        TableId           = null,
                                      IEnumerable<POI.ChargingStationOperator_Id>?   OperatorIds       = null,
                                      StationIdBNetzADelegate?                       StationIdBNetzA   = null)
        {

            this.History             = History;
            this.PublicationCreator  = PublicationCreator;
            this.Language            = Language ?? Languages.en;
            this.TableId             = TableId  ?? History.RoamingNetworkId.ToString();
            this.OperatorIds         = OperatorIds?.ToHashSet();
            this.StationIdBNetzA     = StationIdBNetzA;

        }

        #endregion


        #region StartAsync()

        /// <summary>
        /// Publish the head, and every change from now on; returns once the head is published.
        /// </summary>
        public async Task StartAsync()
        {

            if (worker is not null)
                throw new InvalidOperationException("The publisher has been started already!");

            History.OnCommitAnnouncement          += Announced;
            History.RuntimeStatus.OnStatusUpdate  += StatusUpdated;

            worker = Task.Run(WorkAsync);

            var published = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

            work.Writer.TryWrite(async () => {
                try
                {
                    await PublishTableAsync();
                }
                finally
                {
                    published.TrySetResult();
                }
            });

            await published.Task;

        }

        #endregion

        #region DisposeAsync()

        /// <summary>
        /// Stop publishing, after what is waiting.
        /// </summary>
        public async ValueTask DisposeAsync()
        {

            History.OnCommitAnnouncement          -= Announced;
            History.RuntimeStatus.OnStatusUpdate  -= StatusUpdated;

            work.Writer.TryComplete();

            if (worker is not null)
                await worker;

        }

        #endregion


        #region (private) Announced(Announcement) / StatusUpdated(StatusUpdate)

        private void Announced(POI.RoamingNetworkCommitAnnouncement Announcement)

            => work.Writer.TryWrite(PublishTableAsync);

        // Raised while the status schedule of the EVSE is locked: only queued here.
        private void StatusUpdated(POI.POIStatusUpdate StatusUpdate)
        {
            if (StatusUpdate.EntityType == nameof(POI.EVSE) &&
                StatusUpdate.Kind is POI.POIRuntimeStatusKind.Status or POI.POIRuntimeStatusKind.AdminStatus &&
                POI.EVSE_Id.TryParse(StatusUpdate.EntityId) is POI.EVSE_Id evseId &&
                (OperatorIds is null || OperatorIds.Contains(evseId.OperatorId)))
            {
                work.Writer.TryWrite(() => PublishStatusAsync(evseId));
            }
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
                catch (Exception e)
                {
                    RaiseError($"Publishing failed: {e.Message}", []);
                }
            }
        }

        #endregion

        #region (private) PublishTableAsync()

        /// <summary>
        /// Map the head; when the sites changed, publish them as a new version of the table, and the full status.
        /// </summary>
        private async Task PublishTableAsync()
        {

            var head      = History.Head;
            var warnings  = new List<String>();

            var table     = POI3Mapper.ToTablePublication(head.RoamingNetwork,
                                                          PublicationCreator,
                                                          Language,
                                                          TableId,
                                                          head.Commit.Revision.ToString(),
                                                          warnings,
                                                          OperatorIds,
                                                          StationIdBNetzA: StationIdBNetzA);

            if (warnings.Count > 0)
                RaiseError($"Revision {head.Commit.Revision} is published partly.", warnings);

            // The sites, without the version of the table: a commit of something not published changes nothing here.
            var sites     = new XElement("sites", table.EnergyInfrastructureTables.SelectMany(table => table.EnergyInfrastructureSites).Select(site => site.ToXML()));

            if (publishedSites is not null && XNode.DeepEquals(publishedSites, sites))
                return;

            publishedSites           = sites;
            TableVersion             = head.Commit.Revision.ToString();
            CurrentTablePublication  = table;

            await Raise(OnTablePublication, handler => handler(this, table));

            CurrentStatusPublication = POI3Mapper.ToStatusPublication(head.RoamingNetwork,
                                                                      PublicationCreator,
                                                                      Language,
                                                                      TableId,
                                                                      TableVersion,
                                                                      OperatorIds);

            await Raise(OnStatusPublication, handler => handler(this, CurrentStatusPublication, false));

        }

        #endregion

        #region (private) PublishStatusAsync(EVSEId)

        /// <summary>
        /// Renew the full status, and publish the status of the given EVSE as a delta.
        /// </summary>
        private async Task PublishStatusAsync(POI.EVSE_Id EVSEId)
        {

            if (TableVersion is null)
                return;

            var head  = History.Head.RoamingNetwork;

            CurrentStatusPublication = POI3Mapper.ToStatusPublication(head,
                                                                      PublicationCreator,
                                                                      Language,
                                                                      TableId,
                                                                      TableVersion,
                                                                      OperatorIds);

            var delta = POI3Mapper.ToStatusPublication(head,
                                                       PublicationCreator,
                                                       Language,
                                                       TableId,
                                                       TableVersion,
                                                       OperatorIds,
                                                       [ EVSEId ]);

            if (delta.EnergyInfrastructureSiteStatus.Any())
                await Raise(OnStatusPublication, handler => handler(this, delta, true));

        }

        #endregion

        #region (private) Raise(Event, Invoke)

        private async Task Raise<TDelegate>(TDelegate?             Event,
                                            Func<TDelegate, Task>  Invoke)

            where TDelegate : Delegate

        {

            if (Event is null)
                return;

            foreach (var handler in Event.GetInvocationList().Cast<TDelegate>())
            {
                try
                {
                    await Invoke(handler);
                }
                catch (Exception e)
                {
                    RaiseError($"A subscriber of the publications failed: {e.Message}", []);
                }
            }

        }

        #endregion

        #region (private) RaiseError(Description, Warnings)

        private void RaiseError(String               Description,
                                IEnumerable<String>  Warnings)
        {
            try
            {
                OnError?.Invoke(this, Description, Warnings);
            }
            catch
            { }
        }

        #endregion

    }

}
