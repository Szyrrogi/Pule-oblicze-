using System;
using System.Collections.Generic;

public class ComputationWorker
{
    private readonly ComputationPool<ComputationTypeA> _pA; 
    private readonly int _cA; 
    private readonly string _cfgA;
    private readonly ComputationPool<ComputationTypeB> _pB; 
    private readonly int _cB; 
    private readonly string _cfgB;
    private readonly ComputationPool<ComputationTypeC> _pC; 
    private readonly int _cC; 
    private readonly string _cfgC;

    internal ComputationWorker(
        ComputationPool<ComputationTypeA> pA, int cA, string cfgA,
        ComputationPool<ComputationTypeB> pB, int cB, string cfgB,
        ComputationPool<ComputationTypeC> pC, int cC, string cfgC)
    {
        _pA = pA; _cA = cA; _cfgA = cfgA;
        _pB = pB; _cB = cB; _cfgB = cfgB;
        _pC = pC; _cC = cC; _cfgC = cfgC;
    }

    public void Execute()
    {
        var activeA = new List<ComputationTypeA>();
        var activeB = new List<ComputationTypeB>();
        var activeC = new List<ComputationTypeC>();

        for (int i = 0; i < _cA; i++) 
        { 
            var c = _pA.Get(); 
            c.Configure(_cfgA); 
            activeA.Add(c); 
        }
        for (int i = 0; i < _cB; i++) 
        { 
            var c = _pB.Get(); 
            c.Configure(_cfgB); 
            activeB.Add(c); 
        }
        for (int i = 0; i < _cC; i++) 
        { 
            var c = _pC.Get(); 
            c.Configure(_cfgC); 
            activeC.Add(c); 
        }

        activeA.ForEach(c => c.Compute());
        activeB.ForEach(c => c.Compute());
        activeC.ForEach(c => c.Compute());

        activeA.ForEach(c => _pA.Return(c));
        activeB.ForEach(c => _pB.Return(c));
        activeC.ForEach(c => _pC.Return(c));
    }
}

public class WorkerBuilder
{
    private ComputationPool<ComputationTypeA> _pA; 
    private int _cA; 
    private string _cfgA;
    private ComputationPool<ComputationTypeB> _pB; 
    private int _cB; 
    private string _cfgB;
    private ComputationPool<ComputationTypeC> _pC; 
    private int _cC; 
    private string _cfgC;

    public WorkerBuilder UsePoolA(ComputationPool<ComputationTypeA> p) 
    { 
        _pA = p; 
        return this; 
    }
    public WorkerBuilder UsePoolB(ComputationPool<ComputationTypeB> p) 
    { 
        _pB = p; 
        return this; 
    }
    public WorkerBuilder UsePoolC(ComputationPool<ComputationTypeC> p) 
    { 
        _pC = p; 
        return this; 
    }

    public WorkerBuilder RequestA(int count, string cfg) 
    { 
        _cA = count; 
        _cfgA = cfg; 
        return this; 
    }
    public WorkerBuilder RequestB(int count, string cfg)
    { 
        _cB = count; 
        _cfgB = cfg; 
        return this; 
    }
    public WorkerBuilder RequestC(int count, string cfg) 
    { 
        _cC = count; 
        _cfgC = cfg; 
        return this; 
    }

    public ComputationWorker Build() => new ComputationWorker(_pA, _cA, _cfgA, _pB, _cB, _cfgB, _pC, _cC, _cfgC);
}