using System;

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

    public void Configure(string config) => _configuration = config;
    public void Compute() {
        for(int i = 0; i < 50; i++) _internalState = new Random().Next(1, 100);
    }
    public void Reset() { _configuration = null; _internalState = 0; }
    public object Clone() => this.MemberwiseClone(); 
}

public class ComputationTypeB : IComputation
{
    private string _configuration;  
    private int _internalState;

    public void Configure(string config) => _configuration = config;
    public void Compute() {
        for(int i = 0; i < 500; i++) _internalState = new Random().Next(100, 200);
    }
    public void Reset() { _configuration = null; _internalState = 0; }
    public object Clone() => this.MemberwiseClone(); 
}

public class ComputationTypeC : IComputation
{
    private string _configuration;  
    private int _internalState;

    public void Configure(string config) => _configuration = config;
    public void Compute() {
        for(int i = 0; i < 5000; i++) _internalState = new Random().Next(200, 300);
    }
    public void Reset() { _configuration = null; _internalState = 0; }
    public object Clone() => this.MemberwiseClone(); 
}