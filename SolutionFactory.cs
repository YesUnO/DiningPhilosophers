
namespace DiningPhilosophers
{
    internal abstract class SolutionFactory
    {
        internal abstract IPhilosopher CreatePhilosopher();
        internal abstract IRunnerInstance CreateRunnerInstance();
        IRunnerInstance CreateSolution(bool isSingle, int count)
        {
            var runnerInstance = CreateRunnerInstance();
            runnerInstance.SetIsSingleRun(isSingle);
            var philosophers = runnerInstance.Philosophers = new Philosopher[count];
            for (int i = 0; i < count; i++)
            {
                var philosopher = CreatePhilosopher();
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

            var djiskra = CreateSolution(false, count);
            var chandyMisra = CreateSolution(false, count);
            var tasks = new Task[] { DisplayElapsed(ct), djiskra.Run(ct), chandyMisra.Run(ct)};
            await Task.WhenAll(tasks);
        }
    }
}
