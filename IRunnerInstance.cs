namespace DiningPhilosophers
{
    public interface IRunnerInstance
    {
        public IPhilosopher[] Philosophers { get; set; }

        Task Run(CancellationToken ct);
        void SetIsSingleRun(bool isSingle);
    }
}
