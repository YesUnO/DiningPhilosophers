

namespace DiningPhilosophers
{
    internal abstract class Philosopher: IPhilosopher
    {
        int Id { get; set; }
        PhilosopherState PhilosopherState;
        RunnerInstance RunnerInstance;
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
