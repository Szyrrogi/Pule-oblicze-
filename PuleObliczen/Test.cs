using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

public class ComputationFactory
{
    public ComputationTypeA CreateNew()
    {
        return new ComputationTypeA();
    }
}

[MemoryDiagnoser] 
public class FactoryVsPrototypeBenchmark
{
    private ComputationTypeA _prototype = new ComputationTypeA();
    private ComputationFactory _factory = new ComputationFactory();

    [Benchmark(Baseline = true)]
    public ComputationTypeA TestFactory()
    {
        return _factory.CreateNew();
    }

    [Benchmark]
    public ComputationTypeA TestPrototype()
    {
        return (ComputationTypeA)_prototype.Clone();
    }
}