

namespace DiningPhilosophers
{
    internal abstract class Philosopher: IPhilosopher
    {
        public Philosopher(RunnerInstance runnerInstance)
        {
            RunnerInstance = runnerInstance;
        }
        public int Id { get; set; }
        public PhilosopherState State { get; set; }
        protected RunnerInstance RunnerInstance;
        internal abstract void GetForks();
        void GetForksWrapper()
        {
            GetForks();
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
        public void Run(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                RunnerInstance.Think(Id, ct);
                GetForksWrapper();
                EatWrapper(ct);
                PutForksWrapper();
            }
        }
    }
}
