# The WWCP Datex II data model

How the classes in `WWCP_DatexII/DataStructures` belong together. They follow the DATEX II 3.7 schemas; where they come from and how changes are taken over is told in the [README](README.md#where-the-data-model-comes-from).

The diagrams show inheritance and containment, not the simple properties. A name with a leading `A` and the stereotype `abstract` is an abstract class of the schemas; a multiplicity is that of the schema.

| Folder | DATEX II namespace | Prefix |
|---|---|---|
| `EnergyInfrastructure` | `http://datex2.eu/schema/3/afirEnergyInfrastructure` | `aegi` |
| `Facilities` | `http://datex2.eu/schema/3/afirFacilities` | `afac` |
| `Common` | `http://datex2.eu/schema/3/common` | `com` |
| `CommonExtension` | `http://datex2.eu/schema/3/commonExtension` | `comx` |
| `LocationReferencing` | `http://datex2.eu/schema/3/locationReferencing` | `loc` |
| `LocationExtension` | `http://datex2.eu/schema/3/locationExtension` | `locx` |
| `D2Payload` | `http://datex2.eu/schema/3/d2Payload` | `d2` |

Each folder has its classes in `Complex`, its enumerations in `PredefinedStrings` and its value types in `Simple`.


## Overview

AFIR asks for two publications: the **table** with the static data, and the **status** with the dynamic data, which refers to the table by versioned references.

```mermaid
classDiagram
    direction LR

    class APayloadPublication {
        <<abstract>>
    }

    APayloadPublication <|-- EnergyInfrastructureTablePublication
    APayloadPublication <|-- EnergyInfrastructureStatusPublication
    APayloadPublication <|-- Payload

    EnergyInfrastructureTablePublication  *-- "1..*" EnergyInfrastructureTable
    EnergyInfrastructureTable             *-- "1..*" EnergyInfrastructureSite
    EnergyInfrastructureSite              *-- "0..*" EnergyInfrastructureStation
    EnergyInfrastructureStation           *-- "0..*" RefillPoint
    RefillPoint                           <|-- ElectricChargingPoint
    ElectricChargingPoint                 *-- "0..*" Connector

    EnergyInfrastructureStatusPublication *-- "0..*" EnergyInfrastructureSiteStatus
    EnergyInfrastructureSiteStatus        *-- "0..*" EnergyInfrastructureStationStatus
    EnergyInfrastructureStationStatus     *-- "0..*" RefillPointStatus
    RefillPointStatus                     <|-- ElectricChargingPointStatus

    EnergyInfrastructureStatusPublication ..> EnergyInfrastructureTable   : tableReference
    EnergyInfrastructureSiteStatus        ..> EnergyInfrastructureSite    : reference
    EnergyInfrastructureStationStatus     ..> EnergyInfrastructureStation : reference
    RefillPointStatus                     ..> RefillPoint                 : reference
```

In POI3 terms a site is a charging pool, a station a charging station, an electric charging point an EVSE and a connector a charging connector (see the [POI3 adapter](WWCP_DatexII_Adapter/POI3/README.md)).


## The table publication: sites, stations and refill points

Everything in a table is a facility object: it has a name, external identifiers, operating hours, a location, an owner, an operator and a helpdesk.

```mermaid
classDiagram
    direction TB

    class APayloadPublication {
        <<abstract>>
    }
    class AFacilityObject {
        <<abstract>>
    }
    class Facility {
        <<abstract>>
    }
    class ASupplementalFacility {
        <<abstract>>
    }
    class RefillPoint {
        <<abstract>>
    }

    APayloadPublication <|-- EnergyInfrastructureTablePublication
    APayloadPublication *-- "1" InternationalIdentifier : publicationCreator
    EnergyInfrastructureTablePublication *-- "0..1" HeaderInformation
    EnergyInfrastructureTablePublication *-- "1..*" EnergyInfrastructureTable
    EnergyInfrastructureTable *-- "1..*" EnergyInfrastructureSite

    AFacilityObject <|-- Facility
    AFacilityObject <|-- ASupplementalFacility
    AFacilityObject <|-- DedicatedParkingSpaces

    Facility <|-- EnergyInfrastructureSite
    Facility <|-- EnergyInfrastructureStation
    Facility <|-- RefillPoint
    Facility *-- "0..*" ASupplementalFacility  : supplementalFacility
    Facility *-- "0..*" DedicatedParkingSpaces : dedicatedParkingSpaces

    ASupplementalFacility <|-- SupplementalEquipment
    ASupplementalFacility <|-- SupplementalServiceFacility
    ASupplementalFacility <|-- ElectricChargingEquipment
    ElectricChargingEquipment *-- "1" ElectricChargingPoint

    EnergyInfrastructureSite    *-- "0..*" EnergyInfrastructureStation
    EnergyInfrastructureSite    *-- "0..*" Service         : serviceType
    EnergyInfrastructureSite    *-- "0..*" ALocation       : entrance, exit
    EnergyInfrastructureStation *-- "0..*" RefillPoint
    EnergyInfrastructureStation *-- "0..*" Service         : serviceType
    EnergyInfrastructureStation *-- "0..1" Hydrogen
    EnergyInfrastructureStation *-- "0..*" EnergyProduct

    RefillPoint <|-- ElectricChargingPoint
    RefillPoint <|-- OtherRefillPoint
    RefillPoint *-- "0..*" EnergyProduct
    ElectricChargingPoint *-- "0..*" Connector
    Connector *-- "0..*" ExternalIdentifier

    AFacilityObject *-- "0..*" ExternalIdentifier
    AFacilityObject *-- "0..1" AOperatingHours        : operatingHours
    AFacilityObject *-- "0..1" ALocationReference     : locationReference
    AFacilityObject *-- "0..3" AOrganisation          : owner, operator, helpdesk
    AFacilityObject *-- "0..*" VehicleCharacteristics : applicableForVehicles
    AFacilityObject *-- "0..1" Dimension
    AFacilityObject *-- "0..1" Amenities
    AFacilityObject *-- "0..*" Image                  : photo
```

A site and a station also name their energy distributor, mobility service providers and roaming platforms, each an `AOrganisation` (see [Organisations](#organisations)).
`OtherRefillPoint` is a refill point that is no electric charging point (`aegi:AlternativeFuelsRefillPoint`, `aegi:DieselRefillPoint` or `aegi:PetrolRefillPoint`): what every refill point has is read, what its type adds is kept and written again as it was read.


## Energy products, rates and prices

What is sold at a refill point or station, and for how much.

```mermaid
classDiagram
    direction LR

    class RefillPoint {
        <<abstract>>
    }
    class AOrganisation {
        <<abstract>>
    }

    RefillPoint                 *-- "0..*" EnergyProduct
    EnergyInfrastructureStation *-- "0..*" EnergyProduct

    EnergyProduct  <|-- ElectricEnergy
    ElectricEnergy *-- "0..*" ElectricEnergySourceRatio

    EnergyProduct *-- "0..*" EnergyRate
    EnergyProduct *-- "0..*" EnergyRateReference : energyRateByReference
    EnergyProduct *-- "0..*" AOrganisation       : mobilityServiceProvider
    Reference     <|-- EnergyRateReference
    EnergyRateReference ..> EnergyRate

    EnergyRate  *-- "0..*" EnergyPrice
    EnergyRate  *-- "0..1" Payment
    EnergyRate  *-- "0..1" OverallPeriod
    EnergyPrice *-- "0..1" TimeBasedApplicability
    EnergyPrice *-- "0..1" EnergyBasedApplicability
    EnergyPrice *-- "0..1" OverallPeriod
```


## The status publication

The status mirrors the table: a status per site, station and refill point, each naming its object by a `FacilityObjectVersionedReference`.

```mermaid
classDiagram
    direction TB

    class APayloadPublication {
        <<abstract>>
    }

    APayloadPublication <|-- EnergyInfrastructureStatusPublication
    EnergyInfrastructureStatusPublication *-- "0..1" HeaderInformation
    EnergyInfrastructureStatusPublication *-- "0..*" EnergyInfrastructureTableVersionedReference : tableReference
    EnergyInfrastructureStatusPublication *-- "0..*" EnergyInfrastructureSiteStatus

    FacilityObjectStatus <|-- FacilityStatus
    FacilityObjectStatus <|-- SupplementalFacilityStatus
    FacilityStatus       <|-- EnergyInfrastructureSiteStatus
    FacilityStatus       <|-- EnergyInfrastructureStationStatus
    FacilityStatus       <|-- RefillPointStatus
    RefillPointStatus    <|-- ElectricChargingPointStatus

    FacilityObjectStatus *-- "1"    FacilityObjectVersionedReference : reference
    FacilityObjectStatus *-- "0..1" AOperatingHours                  : newOperatingHours
    FacilityObjectStatus *-- "0..1" Fault
    FacilityStatus       *-- "0..*" SupplementalFacilityStatus

    EnergyInfrastructureSiteStatus    *-- "0..*" EnergyInfrastructureStationStatus
    EnergyInfrastructureSiteStatus    *-- "0..*" Service
    EnergyInfrastructureStationStatus *-- "0..*" RefillPointStatus
    EnergyInfrastructureStationStatus *-- "0..*" Service
    EnergyInfrastructureStationStatus *-- "0..*" EnergyRateUpdate
    RefillPointStatus                 *-- "0..*" EnergyRateUpdate
    RefillPointStatus                 *-- "0..*" PlannedRefillPointStatus

    EnergyRateUpdate         *-- "1"    EnergyRateReference
    EnergyRateUpdate         *-- "0..*" EnergyPrice
    PlannedRefillPointStatus *-- "1"    OverallPeriod

    VersionedReference <|-- FacilityObjectVersionedReference
    VersionedReference <|-- EnergyInfrastructureTableVersionedReference
```

`FacilityObjectParts`, `FacilityParts`, `FacilityObjectStatusParts` and `FacilityStatusParts` are not DATEX II types: the parsers collect the inherited elements in them before a subclass is built.


## Organisations

Owner, operator, helpdesk, energy distributor, mobility service provider and roaming platform are all of the same kind.

```mermaid
classDiagram
    direction TB

    class AOrganisation {
        <<abstract>>
    }

    AOrganisation  <|-- AnOrganisation
    AOrganisation  <|-- OrganisationByReference
    AOrganisation  <|-- UndefinedOrganisation
    AOrganisation  <|-- UnknownOrganisation
    AnOrganisation <|-- ReferenceableOrganisation

    AOrganisation  *-- "0..1" OverallPeriod      : generalTimeValidity
    AnOrganisation *-- "0..*" ExternalIdentifier
    AnOrganisation *-- "0..*" OrganisationUnit
    AnOrganisation *-- "0..*" AOrganisation      : subOrganisation

    OrganisationUnit   *-- "0..*" ContactInformation
    OrganisationUnit   *-- "0..1" AOperatingHours
    OrganisationUnit   *-- "0..1" ALocationReference
    ContactInformation <|-- ContactPerson

    OrganisationByReference *-- "1"    ReferenceableOrganisationVersionedReference
    OrganisationByReference *-- "0..1" OrganisationTableVersionedReference
    ReferenceableOrganisationVersionedReference ..> ReferenceableOrganisation
    VersionedReference <|-- ReferenceableOrganisationVersionedReference
    VersionedReference <|-- OrganisationTableVersionedReference
```


## Operating hours and periods

```mermaid
classDiagram
    direction TB

    class AOperatingHours {
        <<abstract>>
    }
    class ANamedArea {
        <<abstract>>
    }

    AOperatingHours <|-- OpenAllHours
    AOperatingHours <|-- OperatingHoursSpecification
    AOperatingHours <|-- OperatingHoursByReference
    AOperatingHours <|-- UndefinedOperatingHours
    AOperatingHours <|-- UnknownOperatingHours
    AOperatingHours *-- "0..1" ClosureInformation

    OperatingHoursSpecification *-- "1"    OverallPeriod
    OperatingHoursByReference   *-- "1"    OperatingHoursSpecificationVersionedReference
    OperatingHoursByReference   *-- "0..1" OperatingHoursTableVersionedReference
    OperatingHoursSpecificationVersionedReference ..> OperatingHoursSpecification
    VersionedReference <|-- OperatingHoursSpecificationVersionedReference
    VersionedReference <|-- OperatingHoursTableVersionedReference

    OverallPeriod *-- "0..*" Period : validPeriod, exceptionPeriod
    Period        *-- "0..*" TimePeriodOfDay
    Period        *-- "0..*" DayWeekMonth
    Period        *-- "0..*" SpecialDay

    DayWeekMonth <|-- CalendarWeekWithinMonth
    DayWeekMonth <|-- InstanceOfDayWithinMonth
    SpecialDay   <|-- PublicHoliday
    SpecialDay   *-- "0..*" ANamedArea
```

`OverallPeriod` is also what a service, an energy rate, an energy price and a planned refill point status are valid in.


## Locations

```mermaid
classDiagram
    direction TB

    class ALocationReference {
        <<abstract>>
    }
    class ALocation {
        <<abstract>>
    }
    class ALocationGroup {
        <<abstract>>
    }
    class ANetworkLocation {
        <<abstract>>
    }
    class ANamedArea {
        <<abstract>>
    }
    class AOpenLRPointLocationReference {
        <<abstract>>
    }
    class AOpenLRAreaLocationReference {
        <<abstract>>
    }

    ALocationReference <|-- ALocation
    ALocationReference <|-- ALocationGroup
    ALocation          <|-- ANetworkLocation
    ALocation          <|-- AreaLocation
    ALocation          <|-- LocationByReference
    ANetworkLocation   <|-- PointLocation
    ALocationGroup     <|-- LocationGroupByList
    ALocationGroup     <|-- LocationGroupByReference
    LocationGroupByList *-- "2..*" ALocation

    ALocation *-- "0..*" ExternalReferencing
    ALocation *-- "0..1" PointCoordinates     : coordinatesForDisplay
    ALocation *-- "0..1" AfirFacilityLocation : _locationExtension
    AfirFacilityLocation *-- "0..1" Address
    AfirFacilityLocation *-- "0..*" NutsArea
    Address              *-- "0..*" AddressLine

    ANetworkLocation   *-- "0..1" SupplementaryPositionalDescription
    PointLocation      *-- "0..1" PointByCoordinates
    PointLocation      *-- "0..1" AOpenLRPointLocationReference
    PointByCoordinates *-- "1"    PointCoordinates
    PointCoordinates   *-- "0..3" HeightCoordinate
    PointCoordinates   *-- "0..1" PositionConfidenceEllipse
    PointCoordinates   *-- "0..1" PositionAccuracy

    AreaLocation    *-- "0..1" NamedArea
    AreaLocation    *-- "0..1" GMLMultiPolygon
    AreaLocation    *-- "0..1" AOpenLRAreaLocationReference
    ANamedArea      <|-- NamedArea
    NamedArea       <|-- ISONamedArea
    NamedArea       <|-- NutsNamedArea
    GMLMultiPolygon *-- "1..*" GMLPolygon
    GMLPolygon      *-- "1..*" GMLLinearRing : exterior, interior
    GMLLineString   <|-- GMLLinearRing

    LocationByReference      *-- "1" PredefinedLocationVersionedReference
    LocationGroupByReference *-- "1" PredefinedLocationGroupVersionedReference
```

A site, station or charging point is usually a `PointLocation` with its `PointByCoordinates`, and its address, time zone and NUTS areas in the `AfirFacilityLocation` of the location extension. `FacilityLocation` of the location extension is its counterpart for the older `facilities` namespace; the Mobilithek dialect writes an `AfirFacilityLocation` under that name.


## Vehicles

Which vehicles a facility object is for.

```mermaid
classDiagram
    direction LR

    VehicleCharacteristics *-- "0..*" GrossWeightCharacteristic
    VehicleCharacteristics *-- "0..*" HeaviestAxleWeightCharacteristic
    VehicleCharacteristics *-- "0..*" HeightCharacteristic
    VehicleCharacteristics *-- "0..*" LengthCharacteristic
    VehicleCharacteristics *-- "0..*" WidthCharacteristic
    VehicleCharacteristics *-- "0..*" NumberOfAxlesCharacteristic
    VehicleCharacteristics *-- "0..1" Emissions
```

The EU vehicle categories are a property of `VehicleCharacteristics` and are written into the regulated characteristics of its common extension. `EmissionsExtension` is the common extension of `Emissions`.


## Texts

Every text a person reads is a `MultilingualString`: one `MultilingualStringValue` per language.

```mermaid
classDiagram
    direction LR
    MultilingualString *-- "1..*" MultilingualStringValue
```
