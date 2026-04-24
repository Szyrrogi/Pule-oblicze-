using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var poolA = new ComputationPool<ComputationTypeA>(new ComputationTypeA());
        var poolB = new ComputationPool<ComputationTypeB>(new ComputationTypeB());
        var poolC = new ComputationPool<ComputationTypeC>(new ComputationTypeC());

        int numWorkers = 1000;
        var tasks = new List<Task>();

        for (int i = 0; i < numWorkers; i++)
        {
            var worker = new WorkerBuilder()
                .UsePoolA(poolA).RequestA(3, $"KonfigA_{i}")
                .UsePoolB(poolB).RequestB(2, $"KonfigB_{i}")
                .UsePoolC(poolC).RequestC(1, $"KonfigC_{i}")
                .Build();

            tasks.Add(Task.Run(() => worker.Execute()));
        }

        await Task.WhenAll(tasks);

        Console.WriteLine("\nWYNIKI");
        PrintPoolStats("PULA A", poolA, numWorkers * 3);
        PrintPoolStats("PULA B", poolB, numWorkers * 2);
        PrintPoolStats("PULA C", poolC, numWorkers * 1);
    }

    static void PrintPoolStats<T>(string name, ComputationPool<T> pool, int maxReq) where T : class, IComputation
    {
        Console.WriteLine($"{name}:");
        Console.WriteLine($"Zapotrzebowanie: {maxReq}");
        Console.WriteLine($"Utworzono: {pool.TotalCreated}");
        Console.WriteLine($"Oszczędność: {maxReq - pool.TotalCreated}\n");
    }
}