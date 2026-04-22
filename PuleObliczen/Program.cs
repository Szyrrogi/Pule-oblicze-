class Program
{
    static async Task Main(string[] args)
    {
        var prototypeA = new ComputationTypeA();
        var poolA = new ComputationPool<ComputationTypeA>(prototypeA);

        int numberOfWorkers = 10;
        var tasks = new List<Task>();


        for (int i = 0; i < numberOfWorkers; i++)
        {
            var worker = new WorkerBuilder()
                            .UsePoolA(poolA)
                            .RequestObjectsFromA(3, $"Konfig_Wątek_{i}")
                            .Build();

            tasks.Add(Task.Run(() => worker.Execute()));
        }

        await Task.WhenAll(tasks);

    }
}