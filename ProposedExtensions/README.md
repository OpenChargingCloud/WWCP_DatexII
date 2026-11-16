# Proposed extensions of DATEX II

What the ***DATEX II 3.7 AFIR Energy Infrastructure model*** cannot tell yet, and how it could: first inside the extension points DATEX II 3.7 has, valid against its schemas today, then as a part of a future version of the standard.

| Extension | Of | O/M | Description | Status |
|---|---|---|---|---|
| [`meterPublicKey`](MeterPublicKey.md) | `aegi:ElectricChargingPoint` | *Optional* | An EU MI-011 energy meter signs the measurements of a charging session. For this the public key(s) used need to be known before the charging session starts. | proposed (2026-11-16) |
