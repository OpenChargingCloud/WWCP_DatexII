# POI3 ↔ DATEX II v3 Energy Infrastructure

This adapter maps a POI3 roaming network ([WWCP_POI](https://github.com/OpenChargingCloud/WWCP_POI), `WWCP_POI3`) to
DATEX II v3 *Energy Infrastructure* table and status publications, and maps them back. These publications are what a
charge point operator (CPO) has to publish to its national access point under AFIR.

| Class | What it does |
|---|---|
| `POI3Mapper` | The mapping itself: pure conversions in both directions, with warnings for what does not map. |
| `POI3ToDatexIIPublisher` | **CPO side.** Turns a `RoamingNetworkHistory` into a current `EnergyInfrastructureTablePublication` and `EnergyInfrastructureStatusPublication`, and raises events for new ones, e.g. to push them to a national access point. |
| `DatexIIToPOI3Importer` | **Consumer side.** Commits table and status publications, e.g. read from a national access point, to a `RoamingNetworkHistory`. |

The DATEX II side is the model of [`WWCP_DatexII`](../../WWCP_DatexII): the namespaces `energyInfrastructure` and
`facilities` of DATEX II 3.5, with the German "Level C" extensions. See
[the DATEX II implementation and the current standard](#the-datex-ii-implementation-and-the-current-standard) for how that
compares with DATEX II 3.7 and the Mobilithek AFIR profile.

## Usage

```csharp
// CPO side: the head as the current publications, and every commit and EVSE status change from now on.
await using var publisher = new POI3ToDatexIIPublisher(history,
                                                       new InternationalIdentifier(Country.Germany, "DE-NAP-GEF"),
                                                       Languages.de,
                                                       OperatorIds:     [ ChargingStationOperator_Id.Parse("DE*GEF") ],
                                                       StationIdBNetzA: station => stationIds.GetValueOrDefault(station.Id));
publisher.OnTablePublication  += async (sender, table)           => await PushToNAP(table. ToXDocument());
publisher.OnStatusPublication += async (sender, status, isDelta) => await PushToNAP(status.ToXDocument());
publisher.OnError             += (sender, description, warnings) => …;
await publisher.StartAsync();
var servedOnRequest = publisher.CurrentTablePublication;     // and CurrentStatusPublication

// Consumer side: commit what is read, signed with the keys of the operators.
await using var importer = new DatexIIToPOI3Importer(history, changeSet => changeSet.Sign(operatorKey, operatorKeyId, COSEAlgorithm.ES256));
importer.OnError += (sender, description, warnings) => …;
await importer.ImportAsync(XDocument.Load(tableURL). Root!);  // a d2:payload: table or status publication
await importer.ImportAsync(XDocument.Load(statusURL).Root!);
```

### Publisher

- **At the start:** the head becomes the current table publication: one table, by default with the roaming network
  identification as its id and the revision of the head commit as its version, holding one site per charging pool of the
  selected operators (all, unless given). The status of every EVSE becomes the current status publication, and both are
  raised as events.
- **On every commit:** the head is mapped again. When the sites differ, the table gets the revision of the commit as its
  new version. It is published, and so is the full status, whose references carry the new versions. A commit that does
  not change a published site publishes nothing.
- **On every status or admin status change of an EVSE:** the current status publication is renewed, and a delta with
  only that EVSE is published (`IsDelta`).
- **Serving and pushing:** the publisher keeps the current publications and raises events. Serving them, e.g. over
  HTTP for a pull by the national access point, and pushing them is up to the subscribers.

### Importer

- **Tables:** the sites of a table publication become one change set per operator against the head. Each charging pool
  is mapped as DATEX II describes it and laid onto the existing one, keeping what only POI3 knows (see
  [What the import keeps](#what-the-import-keeps)). The energy rates of its charging points are imported as charging
  tariffs.
- **Removed sites:** charging pools of that operator that the table no longer has are removed, unless
  `RemoveMissingSites` is false (for partial tables).
- **Diff, sign, commit:** the difference is computed as a three-way merge whose left side is its base
  (`RoamingNetwork.TryMerge(head, head, desired)`), so only what changed is committed; when nothing changed, nothing is
  committed. The change set is signed by the given delegate, since every change set must be signed by the operator that
  owns the data.
- **Status:** the status of a refill point goes into the runtime status of its EVSE, with `DataSource` "DATEX II". The
  refill points of the last imported table tell which EVSE a reference means; a reference that is itself an EVSE
  identification works without a table.
- **Operators:** they must already be in the roaming network. Adding them is up to the roaming network operator.

Both classes work one job after the other, in the order the jobs came. Do not use both on the same history and the
same publications: each would feed the other's changes back.

## Mapping

### Identifications and versions

| POI3 | DATEX II |
|---|---|
| operator `DE*GEF` | `fac:OrganisationSpecification` as the site `operator`: `id` and `operatorId` `DE*GEF`. Import: from `operatorId`, else from the EVSE identification of the first charging point. |
| charging pool `DE*GEF*P1` | site `id` `DE*GEF*P1`. Import: a foreign identification, e.g. a GUID, becomes `DE*GEF*P` + its first 30 letters and digits. |
| charging station `DE*GEF*S1` | station `id` `DE*GEF*S1`. Import: as for the pool, `…*S…`. |
| EVSE `DE*GEF*E1*1` | refill point `id` and `evseId` `DE*GEF*E1*1`. Import: from `evseId`, else `id`, else `…*E…`. |
| charging connector `1` | — (connectors have no identification; import numbers them 1, 2, …) |
| charging tariff `DE*GEF*T1` | energy rate `id` `DE*GEF*T1`. Import: a foreign identification becomes the suffix `DE*GEF*T<id>`. |
| `LastChangeDate` | `version`: milliseconds since 1970, so a later change has a greater version, and `lastUpdated` |
| commit revision | table `version`; the status refers to it as `tableReference` |

### Charging pool ↔ site (`EnergyInfrastructureSite`)

| POI3 charging pool | DATEX II site |
|---|---|
| `Name`, `Description` | `name`, `description` (`MultilingualString`; texts without a language get the given one on import) |
| `ArrivalInstructions` | first `additionalInformation` |
| `GeoLocation` | `locationReference` `loc:PointLocation`: `pointByCoordinates` and `coordinatesForDisplay`. Import: also `loc:AreaLocation` (its `coordinatesForDisplay`). A pool without a geo location is not published. |
| `Address`: street, house number, region, postal code, city, country | `facilityLocation/address`: address lines `street` (order 0), `houseNumber` (1) and `region` (2), `postcode`, `city`, `countryCode` |
| `TimeZone` (or that of the address) | `facilityLocation/timeZone`, as an offset: the standard offset of the zone, e.g. Europe/Berlin → `+01:00` |
| `ParkingType` | `typeOfSite`: ON_STREET ↔ `onstreet`; PARKING_GARAGE, UNDERGROUND_GARAGE → `inBuilding` → PARKING_GARAGE; PARKING_LOT, ALONG_MOTORWAY → `openSpace` → PARKING_LOT; ON_DRIVEWAY → `other` |
| `OpeningTimes` | `operatingHours`: open 24/7 ↔ `fac:OpenAllHours`. Regular openings ↔ `fac:OperatingHoursSpecification` with one valid period per time of day (`recurringTimePeriodOfDay`) on its weekdays (`recurringDayWeekMonthPeriod`); the overall period starts with the creation of the pool. |
| `Facilities` | `supplementalFacility`: HOTEL ↔ `hotel`, RESTAURANT ↔ `restaurant` (import also `restaurantSelfService`, `motorwayRestaurant…`), CAFE ↔ `cafe` (import also `snackBar`), MALL ↔ `shop`, SUPERMARKET ↔ `foodShopping`, BIKE_SHARING ↔ `bikeSharing`, FUEL_STATION ↔ `petrolStation` (all `fac:SupplementalServiceFacility`), WIFI ↔ `fac:SupplementalEquipment` `internetWireless` |
| `Images` (URLs) | `photoUrl`. Import: images in the category LOCATION, type by file extension. |
| `HotlinePhoneNumber`, else the operator's | `helpdesk`: an `OrganisationSpecification` `DE*GEF*HELPDESK` whose organisation unit has the telephone number |
| first brand | `brand` (its name) |
| operator: name, description, homepage, logo, telephone, e-mail | `operator`: `name`, `description`, `linkToGeneralInformation`, `linkToLogo`, organisation unit contact information |
| `LocationLanguages` | the `userInterfaceLanguage`s of the stations (union on import) |
| `AuthenticationModes`, `PaymentOptions` | the `authenticationAndIdentificationMethods` of the stations (see below; union on import) |
| `EnergyMix` | `electricEnergy` of every charging point (import: the first one) |
| `LastChangeDate` | `lastUpdated` |
| — | `serviceType` `unattended` (POI3 has nothing to tell) |

### Charging station ↔ station (`EnergyInfrastructureStation`)

| POI3 charging station | DATEX II station |
|---|---|
| `Name`, `Description`, `GeoLocation`, `Images` | `name`, `description`, `locationReference` (point), `photoUrl` |
| — | `stationIdBNetzA`: from the `StationIdBNetzA` delegate. Without one it is empty, and a warning says so. |
| `MaxPower`, else the sum of its EVSEs | `totalMaximumPower`. Import: only when it differs from the sum of its charging points. |
| pool `AuthenticationModes` + `PaymentOptions` | `authenticationAndIdentificationMethods`: RFID → `rfid` (import also `activeRFIDChip`, `mifareClassic`, `mifareDesfire`, `calypso`), NFC → `nfc`, CreditCard → `creditCard`, DebitCard → `debitCard`, PrepaidCard → `prepaidCard`, PINPAD → `pinpad`, REMOTE → `apps`, ISO 15118 PLC → `plc`, ISO 15118 over the air → `overTheAir`, SMS → `phoneSMS`, PhoneCall → `phoneDialog`, DirectPayment → `website`, free / no authentication → `unlimitedAccess`. Payment options Cash → `cashPayment`, CreditCard, DebitCard. |
| number of EVSEs | `numberOfRefillPoints` |
| pool `LocationLanguages`, else the language of the publication | `userInterfaceLanguage` |
| feature `Reservable` | `reservation` `optional` of its charging points (import: `optional`, `mandatory`, `partly`) |
| EVSEs | `refillPoint`s, `xsi:type` `egi:ElectricChargingPoint` |

### EVSE ↔ electric charging point (`ElectricChargingPoint`)

| POI3 EVSE | DATEX II electric charging point |
|---|---|
| `Id` | `id` and `evseId` |
| `Name`, `Description`, `GeoLocation`, `Images` | `name`, `description`, `locationReference`, `photoUrl` |
| — | `deliveryUnit` `kWh`, `usageType` `electricVehicle` |
| `MaxVoltage` | `availableVoltage` (import: the highest, else the highest connector `voltage`) |
| `MaxPower` | `availableChargingPower` (import: the highest, else the highest `maxPowerAtSocket`) |
| `MaxCurrent` | `maximumCurrent` of its connectors (import: the highest) |
| `CurrentTypes`, `ChargingModes` | `chargingMode` of its connectors (see below) |
| ISO 15118 authentication mode of the pool | `vehicleToGridCommunicationType` `iso15118` |
| `ChargingConnectors` | `connector`s, `numberOfConnectors` |
| `ChargingTariffIds` + tariffs of its EVSE, station and pool groups | `electricEnergy/energyRate`s, each with the full rate |
| pool `EnergyMix` | `electricEnergy`: `energyProductName`, `electricEnergySourceRatio`s, `isGreenEnergy` (all sources renewable) |

### Charging connector ↔ connector (`Connector`)

| POI3 | DATEX II |
|---|---|
| `Type` | `connectorType`: CHAdeMO ↔ `chademo`, DOMESTIC_A…O ↔ `domesticA`…`domesticO`, IEC_60309_2_single_16 ↔ `iec60309x2single16`, IEC_60309_2_three_16/32/64 ↔ `iec60309x2three16/32/64`, IEC_62196_T1/T1_COMBO/T2/T2_COMBO/T3A/T3C ↔ `iec62196T1`/`iec62196T1COMBO`/`iec62196T2`/`iec62196T2COMBO`/`iec62196T3A`/`iec62196T3C`, PANTOGRAPH_BOTTOM_UP/TOP_DOWN ↔ `pantographBottomUp`/`pantographTopDown`, TESLA_R/S ↔ `teslaR`/`teslaS`. Others: `other` with the POI3 type as `otherConnector`. |
| EVSE `MaxPower`, `MaxVoltage`, `MaxCurrent` | `maxPowerAtSocket` (0 when unknown), `voltage`, `maximumCurrent` |
| EVSE `CurrentTypes` / `ChargingModes` | `chargingMode`: combo plug → `ccs`; DC → `mode4DC`; AC three phases → `mode3AC3p` (`mode2AC3p`/`mode1AC3p` with mode 2/1); AC one phase → `mode2AC1p`/`mode1AC1p` with mode 2/1, else none. Import: `mode1…`/`mode2…`/`mode3AC3p` → AC one/three phases with mode 1/2/3, `mode4DC` and `ccs` → DC with mode 4. |

### Charging tariff ↔ energy rate (`EnergyRate`)

| POI3 charging tariff | DATEX II energy rate |
|---|---|
| `Id`, `LastChangeDate` | `id`, `lastUpdated` |
| — | `ratePolicy` `adHoc` (import of a `contract` rate: with a warning) |
| `Currency` | `applicableCurrency` (import: the first) |
| `Name`, `Description` | `rateName`, `additionalInformation` |
| price component ENERGY | `energyPrice` `pricePerKWh` |
| price component TIME (per hour) | `energyPrice` `pricePerMinute` (price / 60; import × 60) |
| price component FLAT | `energyPrice` `basePrice` (import also `flatRate`, and `free` as 0) |
| `VAT` | `taxRate`, with `taxIncluded` false |
| element restrictions `StartDate`, `EndDate`, `StartTime`/`EndTime`, `DaysOfWeek` | `overallPeriod` of each price of the element: `overallStartTime` (start date, else the creation of the tariff), `overallEndTime`, one valid period with `recurringTimePeriodOfDay` and `recurringDayWeekMonthPeriod`. Import: prices with the same overall period make one element. |

### Status

| POI3 | DATEX II |
|---|---|
| EVSE status | `refillPointStatus` (`egi:ElectricChargingPointStatus`) `status`: available, waitingForPlugin → `available`; reserved → `reserved`; charging → `charging`; pluggedIn, occupied → `occupied`; blocked → `blocked`; offline, unavailable → `unavailable`; inoperative, outOfService, doorNotClosed → `inoperative`; error, faulted → `faulted`; outOfOrder → `outOfOrder`; inDeployment → `planned`; removed → `removed`; else `unknown` |
| EVSE admin status, where not operational | planned, inDeployment → `planned`; deleted → `removed`; outOfService → `inoperative`; blocked → `blocked`; internalUse → `internalUse` |
| import | `available`, `blocked`, `charging`, `faulted`, `inoperative`, `occupied`, `outOfOrder`, `planned` (→ inDeployment), `removed`, `reserved`, `unavailable`, `outOfStock` (→ unavailable), else unknown |
| status / admin status timestamp | `lastUpdated` (the later of both) |
| admin status | `operationStatus`: operational → `inOperation`, planned/inDeployment/outOfService/blocked/deleted → `notInOperation` (also of the site, from the pool's admin status) |
| — | station `isAvailable`: any of its charging points `available` (full publications only) |
| entity versions | `reference` `id` and `version`, the same as in the table |

### What the import keeps

DATEX II tells only part of a charging pool, so the import lays it onto the existing one and keeps the rest:

- **Charging pool:** sets `name`, `description`, `address`, `geoLocation`, `parkingType`, `hotline`, `openingTimes`,
  `energyMix`, `images`, `facilities`, `paymentOptions`, `authenticationModes`, `locationLanguages` and
  `arrivalInstructions`.
- **Charging station:** sets `name`, `description`, `geoLocation`, `images`, `features` and `maxPower`.
- **EVSE:** sets `name`, `description`, `currentTypes`, `chargingModes`, `maxVoltage`, `maxCurrent`, `maxPower`,
  `chargingConnectors`, `chargingTariffIds`, `images` and `geoLocation`.
- **Charging tariff:** everything DATEX II tells; other properties of an existing tariff are kept.
- **Identity:** stations and EVSEs are matched by their identification. New ones are added, ones that are gone are
  removed.
- **Kept untouched:** accessibility, data licenses, grid connection, energy meters, brands, UI features, features of the
  pool, entrances and exits, related locations, charging when closed, manufacturer and model, calibration, parking
  spaces, groups, keys and custom data.

## Gaps

### What POI3 has and DATEX II does not, or not here

- **Accessibility:** POI3's `AccessibilityType` (free, paying or restricted public access, test station) has no
  counterpart. DATEX II `accessibility` means barrier-free access. A warning tells.
- **Price components:** PARKING_TIME prices, step sizes, and energy, power, current and duration restrictions have no
  counterpart. Restricted prices keep only their dates, times of day and weekdays.
- **Opening times:** exceptional openings and closings are left out. So is the free text.
- **Time zone:** DATEX II has a fixed UTC offset, not a named zone with daylight saving time. The standard offset is
  published; the import does not set the time zone.
- **Connectors:** connector identifications have no counterpart. CHAOJI, GB/T, NEMA and others become `other`. Per
  connector, DATEX II gives power, voltage and current, but POI3 has them only per EVSE, so all connectors of an EVSE get
  the same values.
- **Charging modes:** AC one phase in mode 3 has no DATEX II charging mode.
- **No counterpart at all:** EVSE, charging station and charging pool groups (their tariffs are resolved onto every
  charging point), tariff groups, signing and identity keys, data licenses, energy meters, grid connection points,
  manufacturers and models, calibration and certification links, entrance and exit addresses, related locations,
  charging when closed, UI features and the further pool and station features.
- **Brands:** only the first brand's name is published.
- **Images:** POI3 images are URLs, so they are published as `photoUrl`, never as embedded `photo` data.
- **Station identification:** POI3 has no BNetzA station identification; the `StationIdBNetzA` delegate has to provide
  it.

### What DATEX II has and POI3 does not, or not here

- **Facility objects:** the owner, dimension, amenities, vehicle restrictions (`applicableForVehicles`), dedicated
  parking spaces, entrances and exits, exclusive and preferred users, service types, alias, external identifiers and
  further additional information are not imported. So are the energy distributor, mobility service providers and
  roaming platforms.
- **Payment:** the payment methods of a rate (means, mode, brands), minimum and maximum delivery fees, discount and
  combination with parking fee are not imported. Neither is the `taxIncluded` of a price that includes tax.
- **Charging points:** connector format (socket or cable), country of a domestic socket, smart recharging services,
  model type, and minimum and maximum delivery amounts are not imported.
- **Energy:** carbon dioxide and nuclear waste impact, the supplier name, and the `isGreenEnergy` flag itself are not
  imported. Water becomes OtherGreen, as POI3 has no water category.
- **Status:** only the refill point `status` is imported. Waiting time, planned statuses (e.g. reservations), remaining
  charging time, current voltage and power, next available slots, energy rate updates (ad hoc price changes),
  opening and operation status, faults, and site and station statuses are not.
- **Locations:** only point and area locations are read, and only their display coordinates and address. GML
  geometries, OpenLR references, NUTS areas, heights and accuracies are not.
- **Identifications:** foreign identifications longer than 30 letters and digits are cut. Two such sites or stations of
  one operator that differ only after 30 characters would collide.

### Structural

- **Two publications:** DATEX II separates static data (table) and status (status publication). The status refers to
  the table by version, so a status publication is only meaningful together with its table. The publisher therefore
  publishes the full status again after every new table version.
- **Deltas:** the publisher publishes status deltas, but always full tables. Delta tables (only the changed sites, as the
  Mobilithek profile allows) are not made.
- **Transport:** neither class does HTTP, the Mobilithek push or pull, or the subscription handling of DATEX II
  (`WWCP_DatexII_WebAPI` is still empty).

## The DATEX II implementation and the current standard

### The current standard (as of 2026-10)

- **DATEX II 3.7 is the current release.** It adds two AFIR namespaces:
  - `http://datex2.eu/schema/3/afirEnergyInfrastructure` (`aegi`)
  - `http://datex2.eu/schema/3/afirFacilities` (`afac`)

  The `energyInfrastructure` and `facilities` namespaces this library implements are frozen. They are the same in 3.5,
  3.6 and 3.7, apart from the version attribute. Downloads: [docs.datex2.eu](https://docs.datex2.eu/downloads/modelv37/).
- **The German Mobilithek profile** ([MobilithekDE/AFIR-DATEX-II-Recharging-Profil](https://github.com/MobilithekDE/AFIR-DATEX-II-Recharging-Profil))
  is "AFIR Energy Infrastructure" 01-00-00 (August 2025; documentation revision 2 of 2026-04-10, without schema changes).
  - It is built on the AFIR namespaces and is defined as JSON schemas.
  - This library is built from the XSDs and dictionaries of the predecessor, "Level C profile Energy Infrastructure"
    00-01-00 / 00-02-00 (see `WWCP_DatexII/Documentation`).
- **AFIR** requires DATEX II for the static and dynamic data of public recharging points from 2026-04-14.

### Where the schemas of this library differ from the current standard

Moving to the 3.7 AFIR namespaces (and the 01-00-00 profile) means:

- **Namespaces:** `egi`/`fac` become `aegi`/`afac`. The root is an `aegi:EnergyInfrastructureTablePublication` with
  profile name "AFIR Energy Infrastructure" and profile version 01-00-00.
- **Identifiers:** `evseId`, `stationIdBNetzA`, `operatorId`, `providerId` and `operatorIdBnetzA` are gone. They become
  `externalIdentifier` entries (`identifier` + `typeOfIdentifier`), and `externalIdentifier` changes from a string to
  0..n structures. `OrganisationSpecification` becomes `AnOrganisation`/`ReferenceableOrganisation`.
- **ElectricChargingPoint:**
  - gets a mandatory `currentType` (`ac`/`dc`);
  - `chargingMode` moves here from the connector;
  - `electricEnergy` becomes an `energyProduct` of the refill point (the DE JSON still says `electricEnergy`).
- **Station:**
  - `totalMaximumPower`, `serviceType`, `authenticationAndIdentificationMethods`, `userInterfaceLanguage` and
    `numberOfRefillPoints` become optional;
  - there are new `energyProduct`, `hydrogen` and `renewableSources`.
  - In the 3.7 XSD, energy distributor, mobility service providers and roaming platforms move to the site.
- **Prices:**
  - `EnergyPrice` gets a mandatory `priceGroupIndex`, `priceCap`, and time and energy based applicability (from/to minute,
    from/to kWh);
  - `EnergyRate.paymentMethod` becomes `payment`;
  - the carbon dioxide and nuclear waste impact become typed (g/kWh).
- **Enumerations:**
  - `pricePerKg`, `plugAndCharge`, more delivery units, and fuel and hydrogen refill points are new;
  - `domesticA`…`domesticO` are `domesticAType`…`domesticOType` in the 3.7 XSD, but not in the DE JSON;
  - `pregnantWomen` becomes `pregnantPerson`;
  - the service types are `physicalAttendance`/`unattended` (DE) or full/self service (3.7).
- **Status:** `limitedHydrogenAmount`, `newRates`, and `serviceType` becomes 1..1.
- **Spellings:** `pyhsicalAttendance` is fixed only in the AFIR namespace. `permananentlyClosed`, `onstreet` and
  `applicableCalenderWeekWithinMonth` are still spelled so in 3.7, so this library keeps writing them as the schemas
  spell them.

None of this is done here. The adapter would only need a new mapping target. The library would need either a
v3.7 AFIR data model or JSON next to the XML, since the profile is defined in JSON.

### Where the implementation differed from its own schemas, and what is fixed now

These were found while building the adapter. They are fixed, and the tests read the published examples in full and
validate everything written against the XSDs:

- **Reading tables:** tables could only be written. `EnergyInfrastructureTable.TryParseXML` returned success without a
  table. Now sites, stations, electric charging points, connectors, electric energy, energy rates (with prices), service
  types, organisations, contacts, point and area locations, addresses, images, dimensions, amenities, supplemental
  facilities and dedicated parking spaces are read.
- **Writing status:** status could not be written (`NotImplementedException`). Site, station, refill point and electric
  charging point statuses, planned statuses, energy rate updates, faults and supplemental facility statuses now are,
  and the status parsers read every particle, not only the references.
- **Energy rates:** `EnergyRate` had no `energyPrice`s, so ad hoc prices, which AFIR requires, could not be published.
  The reference to a rate was written as `tableReference`.
- **Swallowed parse errors:** an invalid optional value was silently taken as missing. The parsers asked
  `if (TryParseOptional…) { if (ErrorResponse is not null) … }`, while the helpers return false on an error.
- **Wrong element names:** the parsers looked for elements without their namespace, or in the wrong one:
  - `MultilingualString` (`values` and `value`), `OperatingHoursSpecification` (`id`, `version`, `overallPeriod`),
    `OrganisationByReference`;
  - `closureInformation` in the operating hours types;
  - `ClosureInformation`, `EnergyPrice`, `EnergyRateUpdate` and `NamedArea`, and `regularOperatingHoursInForce` and
    `statusDescription` of a charging point status.
- **Wrong values read:**
  - `nextAvailableChargingSlots` was read from `lastUpdated`;
  - `SpecialDay.specialDayType` was read from `intersectWithApplicableDays`;
  - the extension of the operating hours types was read from `_payloadPublicationExtension`;
  - a target class with another prefix, or the default namespace, was refused.
- **Wrong values written:**
  - `OverallPeriod` did not write its valid and exception periods;
  - `DayWeekMonth` wrapped its days in an element the schema does not know, which `Serialize_Period_Test1` had pinned;
  - `SupplementalServiceFacility` and `SupplementalEquipment` did not write their mandatory type.
- **Culture:** `AmountOfMoney` parsed amounts with the culture of the machine, so 0.39 became 39 on a German system.
- **Enumeration values:** `"xxx"` is now `mifareDesfire`, and `"xxn1ClassIIx"` is now `n1ClassII`.
- **Profile:** the publications wrote a fixed profile name and version (00-01-00), whatever they were given. They now
  write their `ProfileName`/`ProfileVersion` (by default "Level C profile Energy Infrastructure" 00-02-00, the version of
  the dictionaries).
- **Timestamps:** `lastUpdated` of stations, refill points, organisations and the like was a `DateTime`. It is now a
  `DateTimeOffset`, as elsewhere.
- **`ANamedArea.TryParseXML`** returned success without an area. It now reads `loc:NamedArea` and refuses other types.

### Still open

- **Not read:**
  - OpenLR (17 types without a class);
  - GML geometries, NUTS areas, heights and position accuracies;
  - calendar variants, ISO and NUTS named areas;
  - vehicle characteristics (`applicableForVehicles`), brands accepted, refill points by reference, entrances and exits;
  - refill points other than electric charging points, and `LocationByReference` and location groups.
- **`[XmlElement(Namespace = …)]` annotations:** they name the namespace of the element's type rather than of the
  element. Nothing uses `XmlSerializer`, so this is only misleading documentation.
- **`ImageFormat`:** it knows formats the schema does not (webp, svg, heif, …). Writing them makes an invalid document.
- **`Serialize_Period_Test1`:** it still pins a `NamedArea`/`IsoNamedArea` output that nests an extra
  `NamedArea`/`MultilingualString` element.
- **Extensions:** extension elements are written inside a wrapper of their name. A parsed extension (the element itself)
  is therefore nested once more when it is written again.
