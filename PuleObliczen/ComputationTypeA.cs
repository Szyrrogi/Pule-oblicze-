using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// Interfejs prototypu
public interface IComputation : ICloneable
{
    void Configure(string config);
    void Compute();
    void Reset(); 
}

public class ComputationTypeA : IComputation
{
    private string _configuration;
    private int _internalState;

    public void Configure(string config)
    {
        _configuration = config;
        _internalState = 0; 
    }

    public void Compute()
    {
        Thread.Sleep(50); 
        _internalState = new Random().Next(1, 100);
        Console.WriteLine($"[Wątek {Thread.CurrentThread.ManagedThreadId}] TypA obliczył: {_internalState} z konfig: {_configuration}");
    }

    public void Reset()
    {
        _configuration = null;
        _internalState = 0;
    }

    public object Clone()
    {
        return this.MemberwiseClone(); 
    }
}
