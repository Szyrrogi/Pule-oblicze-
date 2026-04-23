```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.8246/25H2/2025Update/HudsonValley2)
Intel Core i5-10400F CPU 2.90GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 10.0.100-rc.2.25502.107
  [Host]     : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v3


```
| Method        | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|-------------- |----------:|----------:|----------:|------:|--------:|-------:|----------:|------------:|
| TestFactory   |  4.196 ns | 0.1276 ns | 0.1614 ns |  1.00 |    0.05 | 0.0051 |      32 B |        1.00 |
| TestPrototype | 33.781 ns | 0.2475 ns | 0.2194 ns |  8.06 |    0.30 | 0.0051 |      32 B |        1.00 |
