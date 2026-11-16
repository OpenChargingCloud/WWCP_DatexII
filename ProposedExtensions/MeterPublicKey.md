Proposed extensions for DATEX II in E-Mobility

# MeterPublicKey: The Public Keys of a Charging Point's Energy Meter

| Extension | Of | Status |
|---|---|---|
| [`meterPublicKey`](#meterpublickey-the-public-keys-of-a-charging-points-energy-meter) | `aegi:ElectricChargingPoint` | proposed (2026-11-16) |


An **EU MI-011** energy meter signs the measurements of a charging session, and the driver, or a [transparency software](https://github.com/OpenChargingCloud/ChargyWebApp) for them, verifies these signatures. For that the public key of the meter has to be known **before**, and from a source other than the one handing out the signed measurements. The data a national access point publishes about a charging point is such a source: public, and independent of a single charging session.

For this a charging point has a list of **`meterPublicKey`**: the public keys the energy meter of this charging point signs its measurements with. It is a list, not a single value, because...

- **Crypto agility:** a meter can sign with more than one algorithm or curve, e.g. while a manufacturer moves from one to the next.
- **Key rollover:** after a meter was exchanged or got a new key, the measurements of the sessions before are still verified with the key before.

A signature is valid when one of the keys verifies it. The order of the list means nothing, and no key is in it twice.

DATEX II 3.7 has currently no element for an energy meter, nor for a public key or a certificate of a charging point.


## Data Format

Each `meterPublicKey` is a string value of...

1. The value is a **`SubjectPublicKeyInfo`** as [RFC 5280, section 4.1](https://www.rfc-editor.org/rfc/rfc5280#section-4.1) defines it: the algorithm with its parameters, and the key itself.
2. It is **DER** encoded.
3. The DER is written as **Base64** as [RFC 4648, section 4](https://www.rfc-editor.org/rfc/rfc4648#section-4) defines it: the standard alphabet (`+` and `/`), with padding (`=`), in one line without white space. Not Base64url, and not PEM: no `-----BEGIN PUBLIC KEY-----`, no line breaks.

For a key on an elliptic curve ([RFC 5480](https://www.rfc-editor.org/rfc/rfc5480)) there is exactly one way to write it:

4. The curve is a **named curve**, given by its object identifier. Explicit curve parameters (`ecParameters`) and `implicitCurve` are not allowed.
5. The point is **uncompressed**: `0x04 || X || Y`. Compressed and hybrid points are not allowed.

So a key has one written form, and two written forms that are the same are the same key. The SHA-256 of the DER is the fingerprint of the key.

The `SubjectPublicKeyInfo` names the algorithm and the curve itself. That is why there is no element for them: meters sign with curves of many families (`secp192r1`, `secp224k1`, `secp256r1`, `brainpoolP256r1`, `brainpoolP384r1`, …), and a list of them in the standard would be outdated with the next meter. Keys of other algorithms, e.g. Ed25519 ([RFC 8410](https://www.rfc-editor.org/rfc/rfc8410)), are written as their `SubjectPublicKeyInfo` defines it.

What a meter or a transparency software shows instead, e.g. the coordinates of the point as hexadecimal digits, is derived from the `SubjectPublicKeyInfo`. The other way round it is not possible: the coordinates do not tell their curve.


## Examples

A key on `secp256r1` (91 bytes of DER):

```
MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEE1vvmOnBQf/7txjgVdbo/wn/98ZYUG+nbWWetWBhmt1nPjjkYhDEsu+cm5FL+4+fU/IMJ2fTQZzySZ0gqOYFpQ==
```

```
SEQUENCE
  SEQUENCE
    OBJECT IDENTIFIER  1.2.840.10045.2.1    (id-ecPublicKey)
    OBJECT IDENTIFIER  1.2.840.10045.3.1.7  (secp256r1)
  BIT STRING           04 135BEF98… 673E38E4…   (uncompressed point)
```

A key on `brainpoolP384r1` (124 bytes of DER):

```
MHowFAYHKoZIzj0CAQYJKyQDAwIIAQELA2IABBY8ewdVsZD2+3bOZRk7WJtZeY6ZTTbClwInbar6dl6pfLnkuSS13PHCzyqgFapZlDyQs87Zj0KGw5O5xWFrT9vHMYbHott+021XJFqSucUP3IPHgqTuoAeDmqBaP2la7g==
```


## Parsing Rules

- A reader refuses to parse the value of a `meterPublicKey` that is not written as told above: not canonical Base64, not DER, explicit curve parameters, or a compressed point. It does not correct it.
- A reader that does not know the algorithm or the curve of a key keeps the key and does not verify with it. The other keys of the list are not affected.
- A charging point without a `meterPublicKey` tells nothing about its meter, not that it has none.


## Today: inside `_electricChargingPointExtension`

`aegi:ElectricChargingPoint` ends with the extension point `aegi:_electricChargingPointExtension`. Its type `com:_ExtensionType` allows any elements of any namespace, so a publication with the keys in it is valid against the DATEX II 3.7 schemas as they are.

The keys are elements `meterPublicKey` of the extension namespace

```
https://open.charging.cloud/contexts/datex2/extensions/v1
```

with the prefix `wwcpx`, one element per key, of the type `xs:base64Binary`:

```xml
<aegi:refillPoint xsi:type="aegi:ElectricChargingPoint" id="DE*GEF*E12345678*1" version="1">
    …
    <aegi:connector>
        …
    </aegi:connector>
    <aegi:_electricChargingPointExtension>
        <wwcpx:meterPublicKey xmlns:wwcpx="https://open.charging.cloud/contexts/datex2/extensions/v1">MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEE1vvmOnBQf/7txjgVdbo/wn/98ZYUG+nbWWetWBhmt1nPjjkYhDEsu+cm5FL+4+fU/IMJ2fTQZzySZ0gqOYFpQ==</wwcpx:meterPublicKey>
        <wwcpx:meterPublicKey xmlns:wwcpx="https://open.charging.cloud/contexts/datex2/extensions/v1">MHowFAYHKoZIzj0CAQYJKyQDAwIIAQELA2IABBY8ewdVsZD2+3bOZRk7WJtZeY6ZTTbClwInbar6dl6pfLnkuSS13PHCzyqgFapZlDyQs87Zj0KGw5O5xWFrT9vHMYbHott+021XJFqSucUP3IPHgqTuoAeDmqBaP2la7g==</wwcpx:meterPublicKey>
    </aegi:_electricChargingPointExtension>
</aegi:refillPoint>
```

The schema of the extension namespace:

```xml
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema"
           xmlns:wwcpx="https://open.charging.cloud/contexts/datex2/extensions/v1"
           targetNamespace="https://open.charging.cloud/contexts/datex2/extensions/v1"
           elementFormDefault="qualified">
    <xs:element name="meterPublicKey" type="xs:base64Binary" />
</xs:schema>
```

In JSON the extension point is the object `aegiElectricChargingPointExtensionG`, in DATEX II 3.7 JSON and in the JSON of the Mobilithek profile alike. Both JSON schemas allow any properties in it. The keys are one array of strings:

```json
{
    "aegiElectricChargingPoint": {
        "idG":      "DE*GEF*E12345678*1",
        "versionG": "1",
        "connector": [ … ],
        "aegiElectricChargingPointExtensionG": {
            "meterPublicKey": [
                "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEE1vvmOnBQf/7txjgVdbo/wn/98ZYUG+nbWWetWBhmt1nPjjkYhDEsu+cm5FL+4+fU/IMJ2fTQZzySZ0gqOYFpQ==",
                "MHowFAYHKoZIzj0CAQYJKyQDAwIIAQELA2IABBY8ewdVsZD2+3bOZRk7WJtZeY6ZTTbClwInbar6dl6pfLnkuSS13PHCzyqgFapZlDyQs87Zj0KGw5O5xWFrT9vHMYbHott+021XJFqSucUP3IPHgqTuoAeDmqBaP2la7g=="
            ]
        }
    }
}
```

A receiver that does not know the extension ignores it, as it does with every extension. A publication does not become invalid by it. Whether a national access point passes the content of an extension on to those who take the data from it is its decision, and has to be asked of each.

## In a future version of the standard: an element of `ElectricChargingPoint`

In the standard itself the keys are an attribute of the class `ElectricChargingPoint` of the UML model, with the multiplicity `0..*` and the data type `Base64Binary` of the `Common` package:

| Attribute | Type | Multiplicity | Definition |
|---|---|---|---|
| `meterPublicKey` | `com:Base64Binary` | `0..*` | A public key the energy meter of this charging point signs its measurements with, as the DER encoded SubjectPublicKeyInfo of RFC 5280. A key on an elliptic curve names its curve by its object identifier and has its point uncompressed. |

In the XML schema this is one more optional element of the type `aegi:ElectricChargingPoint`, after `connector` and before the extension point, so every publication valid before stays valid:

```xml
<xs:complexType name="ElectricChargingPoint">
  <xs:complexContent>
    <xs:extension base="aegi:RefillPoint">
      <xs:sequence>
        <xs:element name="currentType" type="aegi:_CurrentTypeEnum" minOccurs="1" maxOccurs="1" />
        …
        <xs:element name="connector" type="aegi:Connector" minOccurs="0" maxOccurs="unbounded" />
        <xs:element name="meterPublicKey" type="com:Base64Binary" minOccurs="0" maxOccurs="unbounded" />
        <xs:element name="_electricChargingPointExtension" type="com:_ExtensionType" minOccurs="0" />
      </xs:sequence>
    </xs:extension>
  </xs:complexContent>
</xs:complexType>
```

```xml
<aegi:refillPoint xsi:type="aegi:ElectricChargingPoint" id="DE*GEF*E12345678*1" version="1">
    …
    <aegi:connector>
        …
    </aegi:connector>
    <aegi:meterPublicKey>MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEE1vvmOnBQf/7txjgVdbo/wn/98ZYUG+nbWWetWBhmt1nPjjkYhDEsu+cm5FL+4+fU/IMJ2fTQZzySZ0gqOYFpQ==</aegi:meterPublicKey>
    <aegi:meterPublicKey>MHowFAYHKoZIzj0CAQYJKyQDAwIIAQELA2IABBY8ewdVsZD2+3bOZRk7WJtZeY6ZTTbClwInbar6dl6pfLnkuSS13PHCzyqgFapZlDyQs87Zj0KGw5O5xWFrT9vHMYbHott+021XJFqSucUP3IPHgqTuoAeDmqBaP2la7g==</aegi:meterPublicKey>
</aegi:refillPoint>
```

In JSON it is a property of the charging point itself:

```json
{
    "aegiElectricChargingPoint": {
        "idG":      "DE*GEF*E12345678*1",
        "versionG": "1",
        "connector": [ … ],
        "meterPublicKey": [
            "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEE1vvmOnBQf/7txjgVdbo/wn/98ZYUG+nbWWetWBhmt1nPjjkYhDEsu+cm5FL+4+fU/IMJ2fTQZzySZ0gqOYFpQ==",
            "MHowFAYHKoZIzj0CAQYJKyQDAwIIAQELA2IABBY8ewdVsZD2+3bOZRk7WJtZeY6ZTTbClwInbar6dl6pfLnkuSS13PHCzyqgFapZlDyQs87Zj0KGw5O5xWFrT9vHMYbHott+021XJFqSucUP3IPHgqTuoAeDmqBaP2la7g=="
        ]
    }
}
```

The name and the written form of a key are the same in both places, so the step from the extension to the standard moves the keys and changes nothing of them:

| | Today, DATEX II 3.7 | A future version |
|---|---|---|
| XML | `aegi:_electricChargingPointExtension/wwcpx:meterPublicKey` | `aegi:meterPublicKey` |
| JSON | `aegiElectricChargingPointExtensionG.meterPublicKey[]` | `meterPublicKey[]` |
| Value | Base64 of the DER encoded `SubjectPublicKeyInfo` | the same |

A publisher of a version that has the element writes the keys there, and no longer in the extension. A reader of such a version that finds keys in both places takes those of the element.


## How this compares with WWCP POI

In [WWCP_POI](https://github.com/OpenChargingCloud/WWCP_POI) an EVSE has an energy meter, and the meter its `publicKeys`. Each of them is a `MeasurementPublicKey` with the same written form of the key, Base64 of the DER encoded `SubjectPublicKeyInfo`, plus its identification (the SHA-256 of the DER) and an optional certificate chain. A `meterPublicKey` is the `key` of such a `MeasurementPublicKey`; the identification is computed from it, and the certificate chain has no counterpart here.
