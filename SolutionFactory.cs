
using DiningPhilosophers.ChandyMisra;
using DiningPhilosophers.Djiskrax;
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
            var runnerInstance = new RunnerInstance();
            runnerInstance.SetIsSingleRun(isSingle);
            var philosophers = runnerInstance.Philosophers = new ChandyMisraPhilosopher[count];
            runnerInstance.Initialize(philosophers);
            for (int i = 0; i < count; i++)
            {
                var philosopher = new ChandyMisraPhilosopher(i, runnerInstance);
                philosophers[i] = philosopher;
            }
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

        internal async Task CreateRunAllTask(int runTimeInSeconds, int count)
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
        }
    }
}
