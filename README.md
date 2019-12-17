# MurmurHash.Net

[![NuGet](https://img.shields.io/nuget/v/MurmurHash.Net.svg)](https://www.nuget.org/packages/MurmurHash.Net/)
[![Build Status](https://travis-ci.org/odinmillion/MurmurHash.Net.svg?branch=master)](https://travis-ci.org/odinmillion/MurmurHash.Net)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.txt)

Extremely fast MurmurHash3 implementation with zero heap allocations

This is an fork of the [System.Data.HashFunction.MurmurHash](https://www.nuget.org/packages/System.Data.HashFunction.MurmurHash/).

# Differences
* Faster (zero heap allocations via Spans, reduced class inheritance, bytes to uint conversion optimisations)
* Only 32-bit output hash value. Maybe 128-bit will be supported in the future. But you can implement it by yourself and send me PR :)

## Installation

This project is available as a [NuGet package](https://www.nuget.org/packages/MurmurHash.Net/)

## Example

### Some code 1. You can use traditional byte[]
```csharp
uint hash = MurmurHash3.Hash32(bytes: new byte[]{1,2,3}, seed: 123456U);
Console.WriteLine(hash);
```

### Output 1
```
3800434721
```

### Some code 2. State-of-the-art Span&lt;byte&gt; also supported
```csharp
Span<byte> span = ...
uint hash = MurmurHash3.Hash32(bytes: span, seed: 123456U);
```

# Benchmarks

