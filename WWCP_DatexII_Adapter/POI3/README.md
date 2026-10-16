# POI3 ↔ DATEX II 3.7 AFIR Energy Infrastructure

This adapter maps a POI3 roaming network ([WWCP_POI](https://github.com/OpenChargingCloud/WWCP_POI), `WWCP_POI3`) to
DATEX II 3.7 *AFIR Energy Infrastructure* table and status publications, and maps them back. These publications are what
a charge point operator (CPO) has to publish to its national access point under AFIR. In Germany that is the Mobilithek,
with the profile "AFIR Energy Infrastructure" 01-00-00.

| Class | What it does |
|---|---|
| `POI3Mapper` | The mapping itself: pure conversions in both directions, with warnings for what does not map. |
| `POI3ToDatexIIPublisher` | **CPO side.** Turns a `RoamingNetworkHistory` into a current `EnergyInfrastructureTablePublication` and `EnergyInfrastructureStatusPublication`, and raises events for new ones, e.g. to push them to a national access point. |
| `DatexIIToPOI3Importer` | **Consumer side.** Commits table and status publications, e.g. read from a national access point, to a `RoamingNetworkHistory`. |

The DATEX II side is the model of [`WWCP_DatexII`](../../WWCP_DatexII): DATEX II 3.7 in the namespaces
`afirEnergyInfrastructure` (`aegi`) and `afirFacilities` (`afac`). It is written and read as XML, as DATEX II 3.7 JSON,
and as the JSON of the Mobilithek profile (see the [main README](../../README.md)).

## Usage

```csharp
// CPO side: the head as the current publications, and every commit and EVSE status change from now on.
await using var publisher = new POI3ToDatexIIPublisher(history,
                                                       new InternationalIdentifier(Country.Germany, "DE-NAP-GEF"),
                                                       Languages.de,
                                                       OperatorIds:     [ ChargingStationOperator_Id.Parse("DE*GEF") ],
                                                       StationIdBNetzA: station => stationIds.GetValueOrDefault(station.Id));
publisher.OnTablePublication  += async (sender, table)           => await PushToNAP(table. ToJSON(DatexIIJSONDialect.Mobilithek_01_00_00));
publisher.OnStatusPublication += async (sender, status, isDelta) => await PushToNAP(status.ToJSON(DatexIIJSONDialect.Mobilithek_01_00_00));  // snapshotPush or deltaPush
publisher.OnError             += (sender, description, warnings) => …;
await publisher.StartAsync();
var servedOnRequest = publisher.CurrentTablePublication;     // and CurrentStatusPublication; ToXML() for DATEX II XML

// Consumer side: commit what is read, signed with the keys of the operators.
await using var importer = new DatexIIToPOI3Importer(history, changeSet => changeSet.Sign(operatorKey, operatorKeyId, COSEAlgorithm.ES256));
importer.OnError += (sender, description, warnings) => …;
await importer.ImportAsync(XDocument.Load(tableURL). Root!);  // a d2:payload: table or status publication
await importer.ImportAsync(XDocument.Load(statusURL).Root!);

// JSON, e.g. from the Mobilithek: read into the model, then imported.
EnergyInfrastructureTablePublication.TryParseJSON(json, out var table, out var errorResponse, DatexIIJSONDialect.Mobilithek_01_00_00);
await importer.ImportAsync(table!);
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
  only that EVSE is published. The delta is marked as one (`IsDelta`), so its Mobilithek JSON is a `deltaPush`, and full
  status publications are a `snapshotPush`.
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
- **Other refill points:** diesel, petrol and alternative fuels refill points have no POI3 counterpart. They are left out
  with a warning ("imported partly"), and their statuses are skipped.
- **Operators:** they must already be in the roaming network. Adding them is up to the roaming network operator.

Both classes work one job after the other, in the order the jobs came. Do not use both on the same history and the
same publications: each would feed the other's changes back.

## Mapping

### Identifications and versions

DATEX II 3.7 identifies facilities and organisations by `externalIdentifier`s (`identifier` with a `typeOfIdentifier`).
It knows only `evseId` as a type, and only as an extended value (`_extended` with `_extendedValue="evseId"`). Every
other type is an `otherTypeOfIdentifier`. The Mobilithek JSON writes those as extended values too.

| POI3 | DATEX II |
|---|---|
| operator `DE*GEF` | the site `operator`: an `afac:AnOrganisation` with the external identifier `DE*GEF` of the type `operatorId`, as the Mobilithek profile wants it. Import: from the `operatorId` of the site's operator, else of a station's operator, else from the EVSE identification of the first charging point. |
| charging pool `DE*GEF*P1` | site `id` `DE*GEF*P1`. Import: a foreign identification, e.g. a GUID, becomes `DE*GEF*P` + its first 30 letters and digits. |
| charging station `DE*GEF*S1` | station `id` `DE*GEF*S1`. Import: as for the pool, `…*S…`. |
| — | the station's external identifier of the type `stationIdBNetzA`, from the `StationIdBNetzA` delegate. Without one there is none, and a warning says so. |
| EVSE `DE*GEF*E1*1` | refill point `id`, and its external identifier `DE*GEF*E1*1` of the type `evseId` (only on the charging point, as the profile wants it). Import: from the `evseId`, else `id`, else `…*E…`. |
| charging connector `1` | — (connectors have no identification; import numbers them 1, 2, …) |
| charging tariff `DE*GEF*T1` | energy rate `id` `DE*GEF*T1`. Import: a foreign identification becomes the suffix `DE*GEF*T<id>`. |
| `LastChangeDate` | `version`: milliseconds since 1970, so a later change has a greater version, and `lastUpdated` |
| commit revision | table `version`; the status refers to it as `tableReference` |

### Charging pool ↔ site (`EnergyInfrastructureSite`)

| POI3 charging pool | DATEX II site |
|---|---|
| `Name`, `Description` | `name`, `description` (`MultilingualString`; texts without a language get the given one on import, and the language of the publication in JSON) |
| `ArrivalInstructions` | first `additionalInformation` |
| `GeoLocation` | `locationReference` `loc:PointLocation`: `pointByCoordinates` and `coordinatesForDisplay`. Import: also `loc:AreaLocation` (its `coordinatesForDisplay`). A pool without a geo location is not published. |
| `Address`: street, house number, region, postal code, city, country | `loc:_locationExtension/loc:afirFacilityLocation/afac:address`: address lines `street` (order 0), `houseNumber` (1) and `region` (2), `postcode`, `city`, `countryCode` |
| `NUTSCodes` | `afirFacilityLocation/nutsArea`s of the site: level 1 → `nuts1Code`, 2 → `nuts2Code`, 3 → `nuts3Code`; a country (level 0) is left out. Import: the NUTS 1 to 3 areas of the site and of its stations. |
| `TimeZone` (or that of the address) | `afirFacilityLocation/timeZone`, as an offset: the standard offset of the zone, e.g. Europe/Berlin → `+01:00` |
| `ParkingType` | `typeOfSite`: ON_STREET ↔ `onstreet`; PARKING_GARAGE, UNDERGROUND_GARAGE → `inBuilding` → PARKING_GARAGE; PARKING_LOT, ALONG_MOTORWAY → `openSpace` → PARKING_LOT; ON_DRIVEWAY → `other` |
| `OpeningTimes` | `operatingHours`: open 24/7 ↔ `afac:OpenAllHours`. Regular openings ↔ `afac:OperatingHoursSpecification` with one valid period per time of day (`recurringTimePeriodOfDay`) on its weekdays (`recurringDayWeekMonthPeriod`); the overall period starts with the creation of the pool. |
| `Facilities` | `supplementalFacility`: HOTEL ↔ `hotel`, RESTAURANT ↔ `restaurant` (import also `restaurantSelfService`, `motorwayRestaurant…`), CAFE ↔ `cafe` (import also `snackBar`), MALL ↔ `shop`, SUPERMARKET ↔ `foodShopping`, BIKE_SHARING ↔ `bikeSharing`, FUEL_STATION ↔ `petrolStation` (all `afac:SupplementalServiceFacility`), WIFI ↔ `afac:SupplementalEquipment` `internetWireless` |
| `Images` (URLs) | `photoUrl`. Import: images in the category LOCATION, type by file extension. |
| `HotlinePhoneNumber`, else the operator's | `helpdesk`: an `afac:AnOrganisation` "Hotline" whose organisation unit has the telephone number |
| first brand | `brand` (its name) |
| operator: name, description, homepage, logo, telephone, e-mail | `operator`: `name`, `description`, `linkToGeneralInformation`, `linkToLogo`, organisation unit contact information, and the external identifier `operatorId` |
| `LocationLanguages` | the `userInterfaceLanguage`s of the stations (union on import) |
| `AuthenticationModes`, `PaymentOptions` | the `authenticationAndIdentificationMethods` of the stations (see below; union on import) |
| `EnergyMix` | the `ElectricEnergy` energy product of every charging point (import: the first one) |
| `Features` Illuminated, Roofed, IlluminatedParkingNearby, RoofedParkingNearby | `amenities` `illuminated`, `roofed`, `illuminatedRechargingParkingNearby`, `roofedRechargingParkingNearby` of the site (only `true` is written). The import sets these four features and keeps the others of an existing charging pool. |
| `Services` ACCESSIBLE_CHARGING | `accessibility` `barrierFreeAccessible` of the site. Import: also `disabilityAccessible` and `wheelChairAccessible`. |
| `Services` ACCESSIBLE_TOILETS | a `supplementalFacility` `afac:SupplementalEquipment` `toilet` whose `accessibility` is `barrierFreeAccessible` (import: also `disabilityAccessible`, `wheelChairAccessible`). The import sets these two services and keeps the others of an existing charging pool. |
| `LastChangeDate` | `lastUpdated` |
| — | `serviceType` `unattended` (POI3 has nothing to tell) |

### Charging station ↔ station (`EnergyInfrastructureStation`)

| POI3 charging station | DATEX II station |
|---|---|
| `Name`, `Description`, `GeoLocation`, `Images` | `name`, `description`, `locationReference` (point), `photoUrl` |
| — | external identifier `stationIdBNetzA` (see above) |
| `MaxPower`, else the sum of its EVSEs | `totalMaximumPower` (optional in 3.7, required by the profile). Import: only when it differs from the sum of its charging points. |
| pool `AuthenticationModes` + `PaymentOptions` | `authenticationAndIdentificationMethods`: RFID → `rfid` (import also `activeRFIDChip`, `mifareClassic`, `mifareDesfire`, `calypso`), NFC → `nfc`, CreditCard → `creditCard`, DebitCard → `debitCard`, PrepaidCard → `prepaidCard`, PINPAD → `pinpad`, REMOTE → `apps`, ISO 15118 PLC → `plc`, ISO 15118 over the air → `overTheAir`, SMS → `phoneSMS`, PhoneCall → `phoneDialog`, DirectPayment → `website`, free / no authentication → `unlimitedAccess`. Payment options Cash → `cashPayment`, CreditCard, DebitCard. |
| number of EVSEs | `numberOfRefillPoints` |
| pool `LocationLanguages`, else the language of the publication | `userInterfaceLanguage` |
| — | `serviceType` `unattended` |
| feature `Reservable` | `reservation` `optional` of its charging points (import: `optional`, `mandatory`, `partly`) |
| `Features` Illuminated, Roofed, IlluminatedParkingNearby, RoofedParkingNearby | `amenities` of the station, as of the site. The import sets them and `Reservable`, and keeps the other features of an existing charging station. |
| parking spaces of the network whose `ChargingStationIds` name it | `dedicatedParkingSpaces`: one per combination of `ParkingRestrictions`, `EUVehicleCategories`, `Length`, `Width`, `MaxVehicleHeight`, `Illuminated` and `Roofed`, with their `numberOfSpaces`, their `dimension` (`length`, `width`, `height` as the clear height) and `amenities`, and the `locationReference` of a single space with a `GeoLocation`; DISABLED → `userSpecific` `personsWithDisabilities` and `accessibility` `barrierFreeAccessible`, EMPLOYEES → `employees`, CUSTOMERS → `customers`, TENANTS → `residents`, EV_ONLY → `applicableForVehicles` `fuelType` `battery`, the EU vehicle categories → its `euVehicleCategory`s. Not imported: POI3 parking spaces belong to parking operators. |
| `EUVehicleCategories` | one `applicableForVehicles` with the `euVehicleCategory`s, in lower case (`M1` ↔ `m1`); categories DATEX II 3.7 does not know, e.g. `L`, as extended values. Import: the categories of all its `applicableForVehicles`, in upper case. |
| EVSEs | `refillPoint`s, `xsi:type` `aegi:ElectricChargingPoint` |

### EVSE ↔ electric charging point (`ElectricChargingPoint`)

| POI3 EVSE | DATEX II electric charging point |
|---|---|
| `Id` | `id`, and the external identifier of the type `evseId` |
| `Name`, `Description`, `GeoLocation`, `Images` | `name`, `description`, `locationReference`, `photoUrl` |
| — | `deliveryUnit` `kWh`, `usageType` `electricVehicle` |
| `CurrentTypes` | `currentType` (mandatory in 3.7): `dc` with DC, else `ac`. Import: `dc` without a charging mode → DC. |
| `CurrentTypes`, `ChargingModes`, connector types | `chargingMode` (on the charging point in 3.7, no longer on its connectors): a combo plug → `ccs`; DC → `mode4DC`; AC three phases → `mode3AC3p` (`mode2AC3p`/`mode1AC3p` with mode 2/1); AC one phase → `mode2AC1p`/`mode1AC1p` with mode 2/1, else none. Import: `mode1…`/`mode2…`/`mode3AC3p` → AC one/three phases with mode 1/2/3, `mode4DC` and `ccs` → DC with mode 4. |
| `MaxVoltage` | `availableVoltage` (import: the highest, else the highest connector `voltage`) |
| `MaxPower` | `availableChargingPower` (import: the highest, else the highest `maxPowerAtSocket`) |
| `MaxCurrent` | `maximumCurrent` of its connectors (import: the highest) |
| ISO 15118 authentication mode of the pool | `vehicleToGridCommunicationType` `iso15118` |
| `ChargingConnectors` | `connector`s, `numberOfConnectors` |
| pool `EnergyMix`, and `ChargingTariffIds` + tariffs of its EVSE, station and pool groups | one `energyProduct` of the type `aegi:ElectricEnergy` (`electricEnergy` in the Mobilithek JSON): `energyProductName`, `electricEnergySourceRatio`s, `isGreenEnergy` (all sources renewable), and its `energyRate`s, each with the full rate |

### Charging connector ↔ connector (`Connector`)

| POI3 | DATEX II |
|---|---|
| `Type` | `connectorType`: CHAdeMO ↔ `chademo`, DOMESTIC_A…O ↔ `domesticAType`…`domesticOType` (`domesticA`…`domesticO` in the Mobilithek JSON), IEC_60309_2_single_16 ↔ `iec60309x2single16`, IEC_60309_2_three_16/32/64 ↔ `iec60309x2three16/32/64`, IEC_62196_T1/T1_COMBO/T2/T2_COMBO/T3A/T3C ↔ `iec62196T1`/`iec62196T1COMBO`/`iec62196T2`/`iec62196T2COMBO`/`iec62196T3A`/`iec62196T3C`, PANTOGRAPH_BOTTOM_UP/TOP_DOWN ↔ `pantographBottomUp`/`pantographTopDown`, TESLA_R/S ↔ `teslaR`/`teslaS`. Others: `other` with the POI3 type as `otherConnector`. |
| EVSE `MaxPower`, `MaxVoltage`, `MaxCurrent` | `maxPowerAtSocket` (0 when unknown), `voltage`, `maximumCurrent` |
| `Format` | `connectorFormat`: SOCKET ↔ `socket`; CABLE → `cableMode3` with charging mode 3, `cableMode2` with mode 2, else `otherCable`. Import: every cable → CABLE. |

### Charging tariff ↔ energy rate (`EnergyRate`)

| POI3 charging tariff | DATEX II energy rate |
|---|---|
| `Id`, `LastChangeDate` | `id`, `lastUpdated` |
| — | `ratePolicy` `adHoc` (import of a `contract` rate: with a warning) |
| `Currency` | `applicableCurrency` (import: the first) |
| `Name`, `Description` | `rateName`, `additionalInformation` |
| `PaymentMeans` | `payment` `paymentMeans`: CREDIT_CARD ↔ `paymentCreditCard`, DEBIT_CARD ↔ `paymentDebitCard`, VALUE_CARD ↔ `paymentValueCard`, CASH ↔ `cashCoinsAndBills`, CASH_COINS ↔ `cashCoinsOnly`, CASH_BILLS ↔ `cashBillsOnly`, MOBILE_ACCOUNT ↔ `mobileAccount`, TOLL_TAG ↔ `tollTag`, PREPAY ↔ `prepay`, NFC ↔ `nfc`, EMV ↔ `emv`, QR_CODE ↔ `qrCode`, WEBSITE ↔ `website`. Others: `otherPaymentMeans`. Import: also extended values as they are named, and `otherPaymentMeans` by their text; not `unknown`. |
| `PaymentBrands` | `payment` `brandsAccepted`: AMERICAN_EXPRESS ↔ `americanExpress`, APPLE_PAY ↔ `applePay`, CIRRUS ↔ `cirrus`, DINERS_CLUB ↔ `dinersClub`, DISCOVER ↔ `discoverCard`, GIROCARD ↔ `giroCard`, MAESTRO ↔ `maestro`, MASTERCARD ↔ `masterCard`, VISA ↔ `visa`, VPAY ↔ `vpay`. Others, e.g. `Google Pay`: `otherBrandsAccepted`. Import: also extended values, and `otherBrandsAccepted`; not `other`. An existing tariff keeps its payment when the rate tells none. |
| price component ENERGY | `energyPrice` `pricePerKWh` |
| price component TIME (per hour) | `energyPrice` `pricePerMinute` (price / 60; import × 60) |
| price component FLAT | `energyPrice` `basePrice` (import also `flatRate`, and `free` as 0) |
| `VAT` | `taxRate`, with `taxIncluded` false |
| tariff element | `priceGroupIndex` (mandatory in 3.7) of its prices: the number of the element, 1, 2, … The prices of a group apply together. Import: the prices of a group make one element; when all prices are of one group, as in the Mobilithek profile (which has no price groups, so its JSON is read as group 1), those with the same overall period do. |
| `ValidFrom`, `ValidUntil` | `overallPeriod` of the rate: `overallStartTime` (`ValidFrom`, else the creation of the tariff when it tells only `ValidUntil`) and `overallEndTime`. Import: an end not after the start is left out; an existing tariff keeps its validity when the rate tells none. |
| element with a `Reservation` restriction | — (DATEX II prices charging, not reservations): left out with a warning |
| element restrictions `StartDate`, `EndDate`, `StartTime`/`EndTime`, `DaysOfWeek` | `overallPeriod` of each price of the element: `overallStartTime` (start date, else the creation of the tariff), `overallEndTime`, one valid period with `recurringTimePeriodOfDay` and `recurringDayWeekMonthPeriod` |
| element restrictions `MinEnergy`, `MaxEnergy` (Wh) | `energyBasedApplicability` `fromKWh`, `toKWh` of each price of the element, in whole kWh (rounded, with a warning) |
| element restrictions `MinDuration`, `MaxDuration` (s) | `timeBasedApplicability` `fromMinute`, `toMinute` of each price of the element, in whole minutes (rounded, with a warning). Import: without price groups, as in the Mobilithek profile, prices of the same overall period and applicability make one element. |
| element restrictions `MinPower`, `MaxPower`, `MinCurrent`, `MaxCurrent` | — (left out with a warning) |

### Status

| POI3 | DATEX II |
|---|---|
| EVSE status | `refillPointStatus` (`aegi:ElectricChargingPointStatus`) `status`: available, waitingForPlugin → `available`; reserved → `reserved`; charging → `charging`; pluggedIn, occupied → `occupied`; blocked → `blocked`; offline, unavailable → `unavailable`; inoperative, outOfService, doorNotClosed → `inoperative`; error, faulted → `faulted`; outOfOrder → `outOfOrder`; inDeployment → `planned`; removed → `removed`; else `unknown` |
| EVSE admin status, where not operational | planned, inDeployment → `planned`; deleted → `removed`; outOfService → `inoperative`; blocked → `blocked`; internalUse → `internalUse` |
| import | `available`, `blocked`, `charging`, `faulted`, `inoperative`, `occupied`, `outOfOrder`, `planned` (→ inDeployment), `removed`, `reserved`, `unavailable`, `outOfStock` (→ unavailable), else unknown |
| status / admin status timestamp | `lastUpdated` (the later of both) |
| admin status | `operationStatus`: operational → `inOperation`, planned/inDeployment/outOfService/blocked/deleted → `notInOperation` (also of the site, from the pool's admin status) |
| — | station `isAvailable`: any of its charging points `available` (full publications only) |
| entity versions | `reference` `id` and `version`, the same as in the table |
| full status / one changed EVSE | `IsDelta` false / true: `snapshotPush` / `deltaPush` in the Mobilithek message container |

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

### What the Mobilithek profile asks for and POI3 cannot tell

The publications are valid against the DATEX II 3.7 schemas and against the JSON schemas of the profile. Its
documentation (revision 2) asks for more than its schemas require. POI3 has no data for:

- **NUTS:** the NUTS 1 region of a site is published when the charging pool tells it (`nutsCodes`).
- **Amenities and parking:** whether a site or station is illuminated or roofed is published when its features tell it;
  dedicated parking spaces, e.g. for persons with disabilities, when parking spaces of the network name the charging
  station. Dedicated parking spaces of a site are not published: POI3 parking spaces name charging stations, and the
  `parkingRestrictions` of a charging pool tell no number of spaces, which DATEX II requires.
- **Vehicles:** the EU vehicle categories of a station are published when the charging station tells them
  (`euVehicleCategories`); those of a site are not.
- **Payment:** the payment means and brands of a rate are published when the charging tariff tells them
  (`paymentMeans`, `paymentBrands`). The payment options of a charging pool still go into the authentication methods.
- **Green energy:** `isGreenEnergy` is published only with an energy mix.
- **Helpdesk and operating hours:** published only when POI3 has a hotline and opening times.
- **Station identification:** POI3 has no BNetzA station identification; the `StationIdBNetzA` delegate has to provide
  it.

### What POI3 has and DATEX II does not, or not here

- **Accessibility:** POI3's `AccessibilityType` (free, paying or restricted public access, test station) has no
  counterpart. DATEX II `accessibility` means barrier-free access. A warning tells. Of the accessibility as the OCPI
  Accessibility Extension, only the services ACCESSIBLE_CHARGING and ACCESSIBLE_TOILETS are published (see above); the
  services ASSISTANCE, CAMERA_SURVEILLANCE, EMERGENCY_CALL, EMERGENCY_ASSISTANCE and PERIMETER_FENCE (a warning tells),
  the `assistanceServiceDetails` and `standards` of a pool, and the `reachDistance`, `operationTimeout`,
  `extendedOperationTimeout` and `standards` of an EVSE have no counterpart.
- **Price components:** PARKING_TIME prices, step sizes and the prices of a reservation have no counterpart. Power and
  current restrictions have none either; energy and duration restrictions only in whole kWh and minutes.
- **Opening times:** exceptional openings and closings are left out. So is the free text.
- **Time zone:** DATEX II has a fixed UTC offset, not a named zone with daylight saving time. The standard offset is
  published; the import does not set the time zone.
- **Connectors:** connector identifications are not published (3.7 has `externalIdentifier` on a connector). CHAOJI,
  GB/T, NEMA and others become `other`. Per connector, DATEX II gives power, voltage and current, but POI3 has them only
  per EVSE, so all connectors of an EVSE get the same values. Whether a connector can be locked, its cable length,
  weight and cable management system, its standards and images have no counterpart; as DATEX II tells the connectors
  of a charging point as a whole, the import replaces them and these are lost.
- **Charging modes:** AC one phase in mode 3 has no DATEX II charging mode. An EVSE with connectors of different
  charging modes gets one, the first that applies.
- **No counterpart at all:** EVSE, charging station and charging pool groups (their tariffs are resolved onto every
  charging point), tariff groups, signing and identity keys, data licenses, energy meters, grid connection points,
  manufacturers and models, calibration and certification links, entrance and exit addresses, related locations,
  charging when closed, UI features and the further pool and station features.
- **Brands:** only the first brand's name is published.
- **Images:** POI3 images are URLs, so they are published as `photoUrl`, never as embedded `photo` data.

### What DATEX II has and POI3 does not, or not here

- **Facility objects:** the owner, dimension, amenities (only illuminated and roofed of sites and stations are), vehicle
  restrictions (`applicableForVehicles`; of a station only its EU vehicle categories), dedicated parking spaces, entrances and exits, exclusive and preferred users, service types, alias, other external identifiers
  and further additional information are not imported. So are the energy distributor, mobility service providers and
  roaming platforms (of sites and stations), and the renewable sources of a station.
- **Payment:** the payment modes of a rate (only its means and brands are), minimum and maximum delivery fees, discount and
  combination with parking fee are not imported. Neither are the price cap and the `taxIncluded` of a price that
  includes tax.
- **Charging points:** the kind of cable of a connector (only that it is one), country of a domestic socket, smart recharging services,
  model type, and minimum and maximum delivery amounts are not imported.
- **Energy:** carbon dioxide and nuclear waste impact, the product information and renewable energy evidence, and the
  `isGreenEnergy` flag itself are not imported. Water becomes OtherGreen, as POI3 has no water category. Energy
  products of a station (rather than of its charging points) are imported only as rates.
- **Hydrogen:** the hydrogen of a station (refuelling modes, capacity) is read and written by the library, but has no
  POI3 counterpart.
- **Status:** only the refill point `status` is imported. Waiting time, planned statuses (e.g. reservations), remaining
  charging time, current voltage and power, next available slots, energy rate updates (ad hoc price changes),
  opening and operation status, faults, limited hydrogen amount, and site and station statuses are not.
- **Locations:** only point and area locations are used, and only their display coordinates and address. GML
  geometries, NUTS areas, heights and accuracies are read by the library, but not imported; OpenLR is not read.
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

## The DATEX II implementation and the standard

### The standard (as of 2026-10)

- **DATEX II 3.7 is the current release.** Its AFIR namespaces are what AFIR data is published in:
  - `http://datex2.eu/schema/3/afirEnergyInfrastructure` (`aegi`)
  - `http://datex2.eu/schema/3/afirFacilities` (`afac`)

  Downloads: [docs.datex2.eu](https://docs.datex2.eu/downloads/modelv37/).
- **The German Mobilithek profile** ([MobilithekDE/AFIR-DATEX-II-Recharging-Profil](https://github.com/MobilithekDE/AFIR-DATEX-II-Recharging-Profil))
  is "AFIR Energy Infrastructure" 01-00-00 (August 2025; documentation revision 2 of 2026-04-10, without schema changes).
  It is built on the AFIR namespaces and is defined as JSON schemas only. Its profile documentation (PDF) and data
  dictionaries are in that repository.
- **AFIR** requires DATEX II for the static and dynamic data of public recharging points from 2026-04-14.

### What is compliant now

- **Model:** the table and status publications are DATEX II 3.7 in the AFIR namespaces, with the profile name "AFIR
  Energy Infrastructure" and version 01-00-00 by default. Everything written is validated against the 3.7 XSDs, which
  the library embeds (`DatexIISchemas`).
- **3.7 changes taken over:**
  - external identifiers instead of `evseId`, `stationIdBNetzA`, `operatorId`, `providerId` and `operatorIdBnetzA`;
    `AnOrganisation`/`ReferenceableOrganisation` instead of `OrganisationSpecification`;
  - the mandatory `currentType` of a charging point, and its `chargingMode`;
  - `energyProduct`s (of the type `ElectricEnergy`) on refill points and stations instead of `electricEnergy`;
  - the mandatory `priceGroupIndex`, `priceCap`, and the time and energy based applicability of a price;
  - `payment` instead of `paymentMethod`, with payment brands;
  - the facility location as `afirFacilityLocation` in the location extension, with NUTS areas;
  - the EU vehicle category in the regulated characteristics of the common extension;
  - the site's energy distributor, mobility service providers and roaming platforms, the station's renewable sources
    and hydrogen, and the status' limited hydrogen amount;
  - the 3.7 enumerations, e.g. `domesticAType`, `pricePerKg`, `plugAndCharge`, `physicalAttendance`, `pregnantPerson`,
    the fuel delivery units, and values the schemas do not know written as `_extended`.
- **JSON:** DATEX II 3.7 JSON, valid against its official JSON schemas, and the JSON of the Mobilithek profile, valid
  against its JSON schemas. The profile's examples go through the model and come back as they were.

### Where the Mobilithek profile differs from DATEX II 3.7

The profile's JSON is a dialect of DATEX II 3.7 JSON from before the release. The Mobilithek dialect of the library
writes and reads these differences:

| DATEX II 3.7 | Mobilithek 01-00-00 |
|---|---|
| `energyProduct: [{ "aegiElectricEnergy": … }]` | `electricEnergy: [ … ]` |
| `priceGroupIndex` (mandatory) | none: all prices of a rate are read as group 1 |
| `domesticAType` … `domesticOType` | `domesticA` … `domesticO` |
| `locLocationExtensionG.AfirFacilityLocation` | `locLocationExtensionG.FacilityLocation` |
| `VehicleCharacteristicsExtended.regulatedCharacteristics[].euVehicleCategory` | `euVehicleCategory` directly in the extension, with categories 3.7 does not know as values |
| site `roamingPlatform` | site `roamingPlatfom` |
| `otherTypeOfIdentifier` `operatorId` etc. | `typeOfIdentifier` extended value `operatorId` etc. |
| times of day `09:00:00` | `09:00:00Z`, meaning the local time |
| status publication as `payload` | `messageContainer` with `payload[]` and `exchangeInformation` (`snapshotPush`/`deltaPush`) |
| `allowedDeliveryChannel` in the header | none: the adapter does not write it |

### Still open in the library

- **Not read:**
  - OpenLR (17 types without a class);
  - calendar variants and NUTS named areas (named areas and ISO named areas are read);
  - `LocationByReference` and location groups.
- **Not modelled:** `associatedFacility`, `rates`/`newRates` (the generic rate tables of 3.7) and the 3.7 publications
  other than table and status. The profile uses none of them; the parsers skip them.
- **Refill points:** electric charging points are modelled in full, as they are the only ones the profile knows. The
  other refill points of the AFIR namespace (`aegi:AlternativeFuelsRefillPoint`, `aegi:DieselRefillPoint`,
  `aegi:PetrolRefillPoint`) are read as an `OtherRefillPoint`. It has everything every refill point has; the elements
  its type adds (all simple values, e.g. `cetane` or `pressure`) are kept as they were read and written again as they
  are. The status of such a refill point is a plain `RefillPointStatus`, which is also read without an `xsi:type`.
- **URLs:** links are informative only. One that is no URL, e.g. a placeholder of the profile's example
  (`https://---.com`), is left out when read, rather than making its energy product, organisation or operating hours
  unreadable.
- **`[XmlElement(Namespace = …)]` annotations:** they name the namespace of the element's type rather than of the
  element. Nothing uses `XmlSerializer`, so this is only misleading documentation.
- **Extensions:** an extension read is the extension element itself, and it is written again as it was. An extension
  given as an element of another name is written inside an extension element. Their content is kept as XML.
