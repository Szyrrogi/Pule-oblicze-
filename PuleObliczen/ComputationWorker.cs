public class ComputationWorker
{
    private readonly ComputationPool<ComputationTypeA> _poolA;
    private readonly int _countA;
    private readonly string _configA;

    internal ComputationWorker(ComputationPool<ComputationTypeA> poolA, int countA, string configA)
    {
        _poolA = poolA;
        _countA = countA;
        _configA = configA;
    }

    public void Execute()
    {
        var activeComputations = new List<ComputationTypeA>();

        for (int i = 0; i < _countA; i++)
        {
            var comp = _poolA.Get();
            comp.Configure(_configA);
            activeComputations.Add(comp);
        }

        foreach (var comp in activeComputations)
        {
            comp.Compute();
        }

        foreach (var comp in activeComputations)
        {
            _poolA.Return(comp);
        }
    }
}

public class WorkerBuilder
{
    private ComputationPool<ComputationTypeA> _poolA;
    private int _countA = 0;
    private string _configA = "Default";

    public WorkerBuilder UsePoolA(ComputationPool<ComputationTypeA> pool)
    {
        _poolA = pool;
        return this;
    }

    public WorkerBuilder RequestObjectsFromA(int count, string config)
    {
        _countA = count;
        _configA = config;
        return this;
    }

    public ComputationWorker Build()
    {
        if (_poolA == null && _countA > 0)
            throw new InvalidOperationException("Pula A nie została ustawiona!");

        return new ComputationWorker(_poolA, _countA, _configA);
    }
}