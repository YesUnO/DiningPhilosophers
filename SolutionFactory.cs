using DiningPhilosophers.ChandyMisra;
using DiningPhilosophers.Djiskra;
using DiningPhilosophers.SolutionInstance;

namespace DiningPhilosophers
{
    class SolutionFactory
    {
        internal IRunnerInstance CreateDjiskraSolution(bool isSingle, int count)
        {
            var runnerInstance = new Instance();
            runnerInstance.SetIsSingleRun(isSingle);
            var philosophers = new DjiskraPhilosopher[count];
            runnerInstance.Initialize(philosophers);
            for (int i = 0; i < count; i++)
            {
                var philosopher = new DjiskraPhilosopher(i, runnerInstance);
                philosophers[i] = philosopher;
            }
            return runnerInstance;
        }

        internal IRunnerInstance CreateChandyMisraSolution(bool isSingle, int count)
        {
            var runnerInstance = new ChandyMisraInstance();
            runnerInstance.SetIsSingleRun(isSingle);
            var philosophers = runnerInstance.Philosophers = new ChandyMisraPhilosopher[count];
            runnerInstance.Initialize(philosophers);
            for (int i = 0; i < count; i++)
            {
                var philosopher = new ChandyMisraPhilosopher(i, runnerInstance);
                philosophers[i] = philosopher;
            }
            runnerInstance.FinalizeInitialization();
            return runnerInstance;
        }

        async Task DisplayElapsed(CancellationToken ct)
        {
            int seconds = 0;
            while (!ct.IsCancellationRequested)
            {
                Console.WriteLine($"{seconds++} seconds");
                await Task.Delay(1000);
            }
        }

        internal async Task CreateRunAllTask(int runTimeInSeconds = 5, int count = 5)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken ct = cancellationTokenSource.Token;
            var timer = Task.Delay(runTimeInSeconds * 1000).ContinueWith(_ =>
            {
                cancellationTokenSource.Cancel();
            });

            var djiskra = CreateDjiskraSolution(false, count);
            var chandyMisra = CreateChandyMisraSolution(false, count);
            var tasks = new Task[] { DisplayElapsed(ct), djiskra.Run(ct), chandyMisra.Run(ct)};
            await Task.WhenAll(tasks);

            LogAllRunResult(chandyMisra, djiskra);
        }

        private void LogAllRunResult(IRunnerInstance chandyMisra, IRunnerInstance djiskra)
        {
            var djiskraCount = 0;
            var chandyMisraCount = 0;

            for (int i = 0; i < chandyMisra.EatCounter.Length; i++)
            {
                djiskraCount += djiskra.EatCounter[i];
                chandyMisraCount += chandyMisra.EatCounter[i];

                Console.WriteLine($"{i} __ Djiskra: {djiskra.EatCounter[i]}; ChandyMisra: {chandyMisra.EatCounter[i]} ");
            }
            Console.WriteLine($"total: Djiskra: {djiskraCount}; ChandyMisra: {chandyMisraCount} ");
        }
    }
}
