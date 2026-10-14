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
