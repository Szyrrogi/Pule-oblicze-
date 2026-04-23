using System;
using System.Collections.Generic;

public class ComputationWorker
{
    private readonly ComputationPool<ComputationTypeA> _poolA;
    private readonly int _countA;
    private readonly string _configA;

    private readonly ComputationPool<ComputationTypeB> _poolB;
    private readonly int _countB;
    private readonly string _configB;

    private readonly ComputationPool<ComputationTypeC> _poolC;
    private readonly int _countC;
    private readonly string _configC;

    internal ComputationWorker(
        ComputationPool<ComputationTypeA> poolA, int countA, string configA,
        ComputationPool<ComputationTypeB> poolB, int countB, string configB,
        ComputationPool<ComputationTypeC> poolC, int countC, string configC)
    {
        _poolA = poolA; _countA = countA; _configA = configA;
        _poolB = poolB; _countB = countB; _configB = configB;
        _poolC = poolC; _countC = countC; _configC = configC;
    }

    public void Execute()
    {
        var activeA = new List<ComputationTypeA>();
        var activeB = new List<ComputationTypeB>();
        var activeC = new List<ComputationTypeC>();

        for (int i = 0; i < _countA; i++) { var comp = _poolA.Get(); comp.Configure(_configA); activeA.Add(comp); }
        for (int i = 0; i < _countB; i++) { var comp = _poolB.Get(); comp.Configure(_configB); activeB.Add(comp); }
        for (int i = 0; i < _countC; i++) { var comp = _poolC.Get(); comp.Configure(_configC); activeC.Add(comp); }

        foreach (var comp in activeA) comp.Compute();
        foreach (var comp in activeB) comp.Compute();
        foreach (var comp in activeC) comp.Compute();

        foreach (var comp in activeA) _poolA.Return(comp);
        foreach (var comp in activeB) _poolB.Return(comp);
        foreach (var comp in activeC) _poolC.Return(comp);
    }
}

public class WorkerBuilder
{
    private ComputationPool<ComputationTypeA> _poolA; private int _countA = 0; private string _configA = "Default";
    private ComputationPool<ComputationTypeB> _poolB; private int _countB = 0; private string _configB = "Default";
    private ComputationPool<ComputationTypeC> _poolC; private int _countC = 0; private string _configC = "Default";

    public WorkerBuilder UsePoolA(ComputationPool<ComputationTypeA> pool) { _poolA = pool; return this; }
    public WorkerBuilder UsePoolB(ComputationPool<ComputationTypeB> pool) { _poolB = pool; return this; }
    public WorkerBuilder UsePoolC(ComputationPool<ComputationTypeC> pool) { _poolC = pool; return this; }

    public WorkerBuilder RequestObjectsFromA(int count, string config) { _countA = count; _configA = config; return this; }
    public WorkerBuilder RequestObjectsFromB(int count, string config) { _countB = count; _configB = config; return this; }
    public WorkerBuilder RequestObjectsFromC(int count, string config) { _countC = count; _configC = config; return this; }

    public ComputationWorker Build()
    {
        if (_poolA == null && _countA > 0) throw new InvalidOperationException("Pula A nie ustawiona!");
        if (_poolB == null && _countB > 0) throw new InvalidOperationException("Pula B nie ustawiona!");
        if (_poolC == null && _countC > 0) throw new InvalidOperationException("Pula C nie ustawiona!");

        return new ComputationWorker(_poolA, _countA, _configA, _poolB, _countB, _configB, _poolC, _countC, _configC);
    }
}