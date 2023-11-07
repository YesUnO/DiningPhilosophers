using DiningPhilosophers.SolutionInstance;

namespace DiningPhilosophers.Philosophers
{
    public abstract class Philosopher : IPhilosopher
    {
        public Philosopher(int id, RunnerInstance runnerInstance)
        {
            Id = id;
            RunnerInstance = runnerInstance;
        }
        public int Id { get; set; }
        public PhilosopherState State { get; set; }
        protected RunnerInstance RunnerInstance;
        internal abstract void GetForks(CancellationToken ct = default);
        void GetForksWrapper(CancellationToken ct = default)
        {
            GetForks(ct);
            RunnerInstance.LogGetForks(Id);
        }
        internal abstract void PutForks();
        void PutForksWrapper()
        {
            PutForks();
            RunnerInstance.LogPutForks(Id);
        }
        internal abstract void Eat();
        void EatWrapper(CancellationToken ct)
        {
            RunnerInstance.Eat(Id, ct);
            Eat();
        }
        public void Run(CancellationToken ct = default)
        {
            while (!ct.IsCancellationRequested)
            {
                RunnerInstance.Think(Id, ct);
                GetForksWrapper(ct);
                EatWrapper(ct);
                PutForksWrapper();
            }
        }
    }
}
