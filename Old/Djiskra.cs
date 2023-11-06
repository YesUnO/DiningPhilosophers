namespace DiningPhilosophers.Old
{
    internal class Djiskra
    {
        private PhilosopherState[] PhilosphersStates;
        private SemaphoreSlim[] BothForksAvailable;
        private readonly object _lock = new object();
        public int[] EatCount { get; set; } = null;
        internal Djiskra(int philosophersCount)
        {
            EatCount = new int[philosophersCount];
            Utills.SetCount(philosophersCount);
            Utills.SetIsSingleRun(true);
            PhilosphersStates = new PhilosopherState[philosophersCount];
            BothForksAvailable = Enumerable.Range(0, philosophersCount)
                .Select(_ => new SemaphoreSlim(0))
                .ToArray();
        }
        internal Djiskra()
        {
            EatCount = new int[Utills.Count];
            PhilosphersStates = new PhilosopherState[Utills.Count];
            BothForksAvailable = Enumerable.Range(0, Utills.Count)
                .Select(_ => new SemaphoreSlim(0))
                .ToArray();
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
                Utills.LogGetForks(philospher);
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

                Utills.LogPutForks(philospher);
            }
        }

        private void RunPhilospher(int philospher, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                Utills.Think(philospher, ct);
                TakeForks(philospher);
                Utills.Eat(philospher, ct);
                PutDownForks(philospher);
                lock (EatCount)
                {
                    EatCount[philospher]++;
                }
            }
        }

        public async Task RunTaskWithOptionalCancelationToken(CancellationToken ct = default)
        {
            Task[] tasks = new Task[Utills.Count];
            for (int i = 0; i < Utills.Count; i++)
            {
                var philosopherId = i;
                tasks[i] = Task.Run(() => RunPhilospher(philosopherId, ct));
            }
            await Task.WhenAll(tasks);
        }
    }
}
