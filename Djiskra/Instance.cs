using DiningPhilosophers.Philosophers;
using DiningPhilosophers.SolutionInstance;

namespace DiningPhilosophers.Djiskra
{
    internal class Instance : RunnerInstance
    {
        internal SemaphoreSlim[] BothForksAvailable;
        private readonly object _lock = new object();

        public override void Initialize()
        {
            BothForksAvailable = Enumerable.Range(0, Count)
                .Select(_ => new SemaphoreSlim(0))
                .ToArray();
        }

        private void TestForks(int philosopher)
        {
            if (Philosophers[philosopher].State == PhilosopherState.Hungry
                && Philosophers[GetLeft(philosopher)].State != PhilosopherState.Eating
                && Philosophers[GetRight(philosopher)].State != PhilosopherState.Eating)
            {
                Philosophers[philosopher].State = PhilosopherState.Eating;
                BothForksAvailable[philosopher].Release();
            }
        }

        internal void TakeForks(int philospher)
        {
            lock (_lock)
            {
                Philosophers[philospher].State = PhilosopherState.Hungry;
                LogGetForks(philospher);
                TestForks(philospher);
            }
            BothForksAvailable[philospher].Wait();
        }

        internal void PutDownForks(int philospher)
        {
            lock (_lock)
            {
                Philosophers[philospher].State = PhilosopherState.Thinking;

                TestForks(GetLeft(philospher));
                TestForks(GetRight(philospher));

                LogPutForks(philospher);
            }
        }
    }
}
