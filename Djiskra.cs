
namespace DiningPhilosophers
{
    internal class Djiskra
    {
        private PhilosopherState[] PhilosphersStates;
        private SemaphoreSlim[] BothForksAvailable;
        private readonly object _lock = new object();
        internal Djiskra(int philosophersCount)
        {
            Utills.SetCount(philosophersCount);
            PhilosphersStates = new PhilosopherState[philosophersCount];
            BothForksAvailable = Enumerable.Range(0, philosophersCount)
                .Select(_ => new SemaphoreSlim(0))
                .ToArray();
        }
        private enum PhilosopherState
        {
            Thinking,
            Hungry,
            Eating
        }

        private void TestForks(int philosopher)
        {
            if (PhilosphersStates[philosopher] == PhilosopherState.Hungry
                && PhilosphersStates[Utills.GetLeft(philosopher)] != PhilosopherState.Eating
                && PhilosphersStates[Utills.GetRight(philosopher)] != PhilosopherState.Eating)
            {
                PhilosphersStates[philosopher] = PhilosopherState.Eating;
                BothForksAvailable[philosopher].Release();
            }
        }

        private void TakeForks(int philospher)
        {
            lock (_lock)
            {
                PhilosphersStates[philospher] = PhilosopherState.Hungry;
                Utills.GetForks(philospher);
                TestForks(philospher);
            }
            BothForksAvailable[philospher].Wait();

        }

        private void PutDownForks(int philospher)
        {
            lock (_lock)
            {
                PhilosphersStates[philospher] = PhilosopherState.Thinking;

                TestForks(Utills.GetLeft(philospher));
                TestForks(Utills.GetRight(philospher));
            }
        }

        private void Philospher(int philospher)
        {
            while (true)
            {
                Utills.Think(philospher);
                TakeForks(philospher);
                Utills.Eat(philospher);
                PutDownForks(philospher);
            }
        }

        public void Run()
        {
            Task[] tasks = new Task[Utills.Count];
            for (int i = 0; i < Utills.Count; i++)
            {
                var philosopherId = i;
                tasks[i] = Task.Run(() => Philospher(philosopherId));
            }
            Task.WaitAll(tasks);
        }
    }
}
