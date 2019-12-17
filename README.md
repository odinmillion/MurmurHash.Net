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

// * Summary *

BenchmarkDotNet=v0.12.0, OS=macOS Mojave 10.14.6 (18G103) [Darwin 18.7.0]
Intel Core i7-7660U CPU 2.50GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET Core SDK=3.1.100
  [Host]     : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT
  DefaultJob : .NET Core 3.1.0 (CoreCLR 4.700.19.56402, CoreFX 4.700.19.56404), X64 RyuJIT

## Byte[] to uint

|                 Method |      Mean |    Error |    StdDev |    Median | Ratio | RatioSD |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------- |----------:|---------:|----------:|----------:|------:|--------:|--------:|------:|------:|----------:|
|                  CRC32 |  40.73 us | 0.744 us |  0.696 us |  40.70 us |  3.32 |    0.06 |       - |     - |     - |         - |
|                 XxHash | 197.13 us | 3.968 us | 10.995 us | 194.38 us | 15.65 |    0.62 | 87.8906 |     - |     - |  184000 B |
| MurMurHashByDarrenkopp |  51.46 us | 1.202 us |  3.469 us |  50.85 us |  4.04 |    0.16 | 15.3198 |     - |     - |   32000 B |
|                Blake2B |  91.83 us | 1.834 us |  3.949 us |  90.26 us |  7.57 |    0.41 |  7.5684 |     - |     - |   16000 B |
|          MurMurHashNet |  12.28 us | 0.131 us |  0.122 us |  12.29 us |  1.00 |    0.00 |       - |     - |     - |         - |

## String to uint

|                 Method |      Mean |    Error |   StdDev | Ratio | RatioSD |    Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------- |----------:|---------:|---------:|------:|--------:|---------:|------:|------:|----------:|
|                  CRC32 |  62.34 us | 1.239 us | 1.159 us |  2.56 |    0.05 |  15.2588 |     - |     - |   32000 B |
|                 XxHash | 210.44 us | 4.067 us | 4.842 us |  8.62 |    0.29 | 103.2715 |     - |     - |  216000 B |
| MurMurHashByDarrenkopp |  76.71 us | 1.517 us | 2.575 us |  3.13 |    0.11 |  30.6396 |     - |     - |   64000 B |
|                Blake2B | 131.63 us | 3.311 us | 9.499 us |  5.33 |    0.55 |  22.9492 |     - |     - |   48000 B |
|          MurMurHashNet |  24.43 us | 0.476 us | 0.549 us |  1.00 |    0.00 |        - |     - |     - |         - |