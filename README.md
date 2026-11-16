# WWCP Datex II

This software libraries allow you to build EU [DatexII](https://github.com/DATEX-II-EU) services and use case specific gateways between Datex II and the World Wide Charging Protocol (WWCP).

The data model implements the DATEX II 3.7 *AFIR Energy Infrastructure* table and status publications, in the namespaces `afirEnergyInfrastructure` (`aegi`) and `afirFacilities` (`afac`), as the EU Alternative Fuels Infrastructure Regulation (AFIR) wants them published, and as the German national access point (Mobilithek) takes them with its profile "AFIR Energy Infrastructure" 01-00-00.
It reads and writes them as XML, and as JSON in two dialects:

- `DatexIIJSONDialect.DATEXII_3_7`: the JSON of DATEX II 3.7, as its official JSON schemas define it.
- `DatexIIJSONDialect.Mobilithek_01_00_00`: the JSON of the Mobilithek profile, which predates DATEX II 3.7 in a few places (`electricEnergy` instead of `energyProduct`, no `priceGroupIndex`, `domesticA` instead of `domesticAType`, `FacilityLocation` instead of `AfirFacilityLocation`, the EU vehicle category directly in the extension of the vehicle characteristics, `roamingPlatfom` on sites, the identifier types `stationIdBNetzA`, `operatorIdBNetzA`, `operatorId` and `providerId` as extended values, times of day with a `Z`, and status publications in a message container with a `snapshotPush` or, for `IsDelta` publications, a `deltaPush`).

```csharp
var xml   = publication.ToXML();                                            // DATEX II 3.7 XML
var json  = publication.ToJSON();                                           // DATEX II 3.7 JSON
var nap   = publication.ToJSON(DatexIIJSONDialect.Mobilithek_01_00_00);     // Mobilithek JSON

EnergyInfrastructureTablePublication.TryParseJSON(nap, out var again, out var errorResponse, DatexIIJSONDialect.Mobilithek_01_00_00);
```

The DATEX II 3.7 XML schemas are embedded in the library (`WWCP_DatexII/Schemas/3.7`): `DatexIISchemas.Validate` validates against them, and the JSON conversion (`DatexIIJSON`) follows them, so JSON read becomes XML in schema order.
Enumeration values the schemas do not know, e.g. the identifier type `evseId`, are written as DATEX II 3.7 wants it, `_extended` with the value as `_extendedValue`, and read back as the value itself.
The tests validate against the XML schemas, the official DATEX II 3.7 JSON schemas and the JSON schemas of the Mobilithek profile, with its examples.

[WWCP_DatexII_Adapter/POI3](WWCP_DatexII_Adapter/POI3/README.md) maps the publications to and from a POI3 roaming network, and documents what the mapping leaves out and how this implementation compares with the standard and the profile.

[DataModel.md](DataModel.md) shows as class diagrams how the classes of the data model belong together.

[ProposedExtensions.md](ProposedExtensions.md) proposes what DATEX II 3.7 cannot tell yet, e.g. the public keys of the energy meter of a charging point.


## Where the data model comes from

The classes are written by hand after two sources. Neither is generated from, so a change of a source has to be taken over by hand.

| Source | What it gives | Where it is in this repository |
|---|---|---|
| **DATEX II 3.7**, the release of the DATEX II organisation: [docs.datex2.eu/downloads/modelv37](https://docs.datex2.eu/downloads/modelv37/). Its UML model is kept in [DATEX-II-EU/datexiimodel](https://github.com/DATEX-II-EU/datexiimodel), issues of the standard in [DATEX-II-EU/DatexII](https://github.com/DATEX-II-EU/DatexII). | The domain model itself: the XML schemas and the official JSON schemas of the namespaces `afirEnergyInfrastructure`, `afirFacilities`, `common`, `commonExtension`, `locationReferencing`, `locationExtension` and `d2Payload`. | XML schemas: `WWCP_DatexII/Schemas/3.7` (embedded in the library). JSON schemas: `WWCP_DatexII_Tests/Documentation/3.7`. |
| **The Mobilithek profile** "AFIR Energy Infrastructure" 01-00-00 of the German national access point: [MobilithekDE/AFIR-DATEX-II-Recharging-Profil](https://github.com/MobilithekDE/AFIR-DATEX-II-Recharging-Profil). | What of DATEX II the German NAP takes, and how: its JSON schemas, its examples, the profile documentation and the data dictionaries (PDF). | JSON schemas and examples: `WWCP_DatexII_Tests/Documentation/Mobilithek-01-00-00`, from `Releases/Version 01-00-00/Schema und Beispiele` (revision 2 of 2026-04-10). |

The Mobilithek offers the profile for download as well:

- [mobilithek.info/cms/downloads/datenprofile-afir](https://mobilithek.info/cms/downloads/datenprofile-afir): the profile as one ZIP archive, with the same documentation, schemas and examples as `Releases/Version 01-00-00` of the repository (revision 2).
- [mobilithek.info/help/download](https://mobilithek.info/help/download): the download page of the Mobilithek, with its guidelines and technical documents, and the certificate chain the `MobilithekPushClient` verifies the Mobilithek by.

The regulation behind both is the EU Alternative Fuels Infrastructure Regulation, (EU) 2023/1804 (AFIR), which requires DATEX II for the static and dynamic data of public recharging points from 2026-04-14.

### Taking over changes of the data model

The repository to watch is **[MobilithekDE/AFIR-DATEX-II-Recharging-Profil](https://github.com/MobilithekDE/AFIR-DATEX-II-Recharging-Profil)**: it is what the Mobilithek validates a publication against, and it changes without a new DATEX II release. The same profile is published at [mobilithek.info/cms/downloads/datenprofile-afir](https://mobilithek.info/cms/downloads/datenprofile-afir). A new DATEX II release is announced at [docs.datex2.eu](https://docs.datex2.eu/).

Last compared: the profile at its commit `7ab184bc` of 2026-04-10 (release 01-00-00, revision 2), DATEX II at release 3.7.

To take over a change:

1. Copy the new schemas and examples over those in the repository: a new profile release into `WWCP_DatexII_Tests/Documentation/Mobilithek-<version>`, a new DATEX II release into `WWCP_DatexII/Schemas/<version>` and `WWCP_DatexII_Tests/Documentation/<version>`.
2. Run the tests. They validate everything the library writes against the XML schemas and both sets of JSON schemas, and send the examples of the profile through the model and back, so what a schema newly requires or no longer knows fails there.
3. Change the classes in `WWCP_DatexII/DataStructures`, for a difference between the profile and DATEX II the dialect in `DatexIIJSON`, and, where POI3 can tell it, the mapping in `WWCP_DatexII_Adapter/POI3`.
4. Write down here what was compared last, in the [adapter README](WWCP_DatexII_Adapter/POI3/README.md#the-datex-ii-implementation-and-the-standard) how the implementation compares now, and in [DataModel.md](DataModel.md) what changed between the classes.


## Known DatexII Specification Issues

The DATEX II 3.7 schemas misspell some names. The library writes them as the schemas spell them. `pyhsicalAttendance` of the older `energyInfrastructure` namespace is `physicalAttendance` in `afirEnergyInfrastructure`, and so in this library.


### applicableCalenderWeekWithinMonth vs. applicableCalendarWeekWithinMonth

DATEXII_3_Common.xsd (line 419) has the following spelling mistake within a XML element name `applicableCalenderWeekWithinMonth` of the `CalendarWeekWithinMonth` complex type:
```
<xs:element name="applicableCalenderWeekWithinMonth" type="com:_CalendarWeekWithinMonthEnum" minOccurs="1" maxOccurs="6" />
```

### perman(an)entlyClosed vs. permanentlyClosed

DATEXII_3_AfirFacilities.xsd (line 342) has the following spelling mistake within a XML element name of the `ClosureInformation` complex type:
```
<xs:element name="permananentlyClosed" type="com:Boolean" minOccurs="0" maxOccurs="1" />
```

### onstreet vs. onStreet

DATEXII_3_AfirEnergyInfrastructure.xsd (line 506) has the following spelling mistake within an ENUM name of the `EnergyInfrastructureSiteTypeEnum` enumeration:
```
<xs:enumeration value="onstreet" />
```

### cryoCompressedHydrogren vs. cryoCompressedHydrogen

DATEXII_3_AfirEnergyInfrastructure.xsd (line 672) has the following spelling mistake within an ENUM name of the `HydrogenRefuellingModeEnum` enumeration:
```
<xs:enumeration value="cryoCompressedHydrogren" />
```

### cumulativeCapacityperDay vs. cumulativeCapacityPerDay

DATEXII_3_AfirEnergyInfrastructure.xsd (line 651) writes a XML element name of the `Hydrogen` complex type with a lower case "per":
```
<xs:element name="cumulativeCapacityperDay" type="aegi:KilogramPerDay" minOccurs="1" maxOccurs="1" />
```

### roamingPlatfom vs. roamingPlatform (Mobilithek profile)

The JSON schema of the Mobilithek profile 01-00-00 (DATEXII_3_AfirEnergyInfrastructure.json) names the roaming platforms of a site `roamingPlatfom`; DATEX II 3.7 names them `roamingPlatform`. The Mobilithek dialect writes and reads the profile's spelling.



#### Your contributions

This software is developed by [GraphDefined GmbH](http://www.graphdefined.com).
We appreciate your participation in this ongoing project, and your help to improve it.
If you find bugs, want to request a feature or send us a pull request, feel free to
use the normal GitHub features to do so. For this please read the
[Contributor License Agreement](Contributor%20License%20Agreement.txt)
carefully and send us a signed copy.
