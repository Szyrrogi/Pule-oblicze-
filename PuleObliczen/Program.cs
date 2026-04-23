using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenchmarkDotNet.Running; // Odkomentuj gdy będziesz robił benchmarki

class Program
{
    static async Task Main(string[] args)
    {
        //var summary = BenchmarkRunner.Run<FactoryVsPrototypeBenchmark>();

        //dotnet run -c Release

        var prototypeA = new ComputationTypeA();
        var prototypeB = new ComputationTypeB();
        var prototypeC = new ComputationTypeC();

        var poolA = new ComputationPool<ComputationTypeA>(prototypeA);
        var poolB = new ComputationPool<ComputationTypeB>(prototypeB);
        var poolC = new ComputationPool<ComputationTypeC>(prototypeC);

        int numberOfWorkers = 1000;
        var tasks = new List<Task>();

        for (int i = 0; i < numberOfWorkers; i++)
        {
            var worker = new WorkerBuilder()
                            .UsePoolA(poolA).RequestObjectsFromA(3, $"A_Konfig_Wątek_{i}")
                            .UsePoolB(poolB).RequestObjectsFromB(2, $"B_Konfig_Wątek_{i}") 
                            .UsePoolC(poolC).RequestObjectsFromC(1, $"C_Konfig_Wątek_{i}") 
                            .Build();

            tasks.Add(Task.Run(() => worker.Execute()));
        }

        await Task.WhenAll(tasks);
        
        Console.WriteLine("\n--- PODSUMOWANIE ---");
        Console.WriteLine($"Liczba wątków obliczeniowych: {numberOfWorkers}\n");
        
        Console.WriteLine("PULA A:");
        Console.WriteLine($"Maks. możliwe zapotrzebowanie: {numberOfWorkers * 3}");
        Console.WriteLine($"Faktycznie utworzone obiekty (wielkość puli): {poolA.TotalCreated}");

        Console.WriteLine("PULA B:");
        Console.WriteLine($"Maks. możliwe zapotrzebowanie: {numberOfWorkers * 2}");
        Console.WriteLine($"Faktycznie utworzone obiekty (wielkość puli): {poolB.TotalCreated}");

        Console.WriteLine("PULA C:");
        Console.WriteLine($"Maks. możliwe zapotrzebowanie: {numberOfWorkers * 1}");
        Console.WriteLine($"Faktycznie utworzone obiekty (wielkość puli): {poolC.TotalCreated}");    }
}