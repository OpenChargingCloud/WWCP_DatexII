# WWCP Datex II

This software libraries allow you to build EU [DatexII](https://github.com/DATEX-II-EU) services and use case specific gateways between Datex II and the World Wide Charging Protocol (WWCP).


## Known DatexII Specification Issues


### applicableCalenderWeekWithinMonth vs. applicableCalendarWeekWithinMonth

DATEXII_3_Common.xsd (line 99 for status version, line 200 for table version) has the following spelling mistake within a XML element name `applicableCalenderWeekWithinMonth` of the `CalendarWeekWithinMonth` complex type:
```
<xs:complexType name="CalendarWeekWithinMonth">
  <xs:annotation>
    <xs:documentation>Specification of periods defined by relevant calendar weeks in a month, see ISO8601. Note: Calendar weeks start with Monday. First week is the week containing the first of the month.</xs:documentation>
  </xs:annotation>
  <xs:complexContent>
    <xs:extension base="com:DayWeekMonth">
      <xs:sequence>
        <xs:element name="applicableCalenderWeekWithinMonth" type="com:_CalendarWeekWithinMonthEnum" minOccurs="1" maxOccurs="6">
          <xs:annotation>
            <xs:documentation>Calender week in month. See ISO8601.  "All weeks of the month" is expressed by not using the CalendarWeekOfMonth class. Note: Calendar weeks start with Monday. First week is the week containing the first of the month.</xs:documentation>
          </xs:annotation>
        </xs:element>
        <xs:element name="_calendarWeekWithinMonthExtension" type="com:_ExtensionType" minOccurs="0" />
      </xs:sequence>
    </xs:extension>
  </xs:complexContent>
</xs:complexType>
```

### perman(an)entlyClosed vs. permanentlyClosed

DATEXII_3_Facilities.xsd (line 101 for status version, line 255 for table version) has the following spelling mistake within a XML element name of the `ClosureInformation` complex type:
```
<xs:element name="permananentlyClosed" type="com:Boolean" minOccurs="0" maxOccurs="1">
  <xs:annotation>
    <xs:documentation>Permanently closed, i.e. it is not intended to open again.</xs:documentation>
  </xs:annotation>
</xs:element>
```

### pyhsicalAttendance vs. physicalAttendance

DATEXII_3_EnergyInfrastructure.xsd (line 463 for status version, line 1411 for table version) has the following spelling mistake within an ENUM name of the `ServiceTypeEnum` enumeration:
```
<xs:enumeration value="pyhsicalAttendance">
  <xs:annotation>
    <xs:documentation>Presence of physical persons attending the recharging or refuelling station.</xs:documentation>
  </xs:annotation>
</xs:enumeration>
```

### onstreet vs. onStreet

DATEXII_3_EnergyInfrastructure.xsd (line 164 for table version) has the following spelling mistake within an ENUM name of the `EnergyInfrastructureSiteTypeEnum` enumeration:
```
<xs:enumeration value="onstreet">
  <xs:annotation>
    <xs:documentation>The energy infrastructure site is located alongside a street, for example some singular charging stations.</xs:documentation>
  </xs:annotation>
</xs:enumeration>
```



#### Your contributions

This software is developed by [GraphDefined GmbH](http://www.graphdefined.com).
We appreciate your participation in this ongoing project, and your help to improve it.
If you find bugs, want to request a feature or send us a pull request, feel free to
use the normal GitHub features to do so. For this please read the
[Contributor License Agreement](Contributor%20License%20Agreement.txt)
carefully and send us a signed copy.
