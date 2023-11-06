using DiningPhilosophers.Philosophers;

namespace DiningPhilosophers.SolutionInstance
{
    public interface IRunnerInstance
    {
        public IPhilosopher[] Philosophers { get; set; }

        Task Run(CancellationToken ct = default);
        void SetIsSingleRun(bool isSingle);
    }
}
