
namespace DiningPhilosophers
{
    internal class Djiskra
    {
        private int PhilosophersCount;
        private PhilosopherState[] PhilosphersStates;
        private Random Random = new();
        private SemaphoreSlim[] BothForksAvailable;
        private readonly object _lock = new object();
        public Djiskra(int philosophersCount)
        {
            PhilosophersCount = philosophersCount;
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


        private int GetLeft(int philosopher)
        {
            return (philosopher - 1 + PhilosophersCount) % PhilosophersCount;
        }

        private int GetRight(int philosopher)
        {
            return (philosopher + 1) % PhilosophersCount;
        }

        private void TestForks(int philosopher)
        {
            if (PhilosphersStates[philosopher] == PhilosopherState.Hungry
                && PhilosphersStates[GetLeft(philosopher)] != PhilosopherState.Eating
                && PhilosphersStates[GetRight(philosopher)] != PhilosopherState.Eating)
            {
                PhilosphersStates[philosopher] = PhilosopherState.Eating;
                BothForksAvailable[philosopher].Release();
            }
        }

        private void Think(int philospher)
        {
            var dur = Random.Next(5000, 8000);
            lock (Console.Out)
            {
                Console.WriteLine($"Philosopher {philospher} is thinking for next {dur / 1000} s");
            }
            Thread.Sleep(dur);
        }

        private void TakeForks(int philospher)
        {
            lock (_lock)
            {
                PhilosphersStates[philospher] = PhilosopherState.Hungry;
                lock (Console.Out)
                {
                    Console.WriteLine($"Philosopher {philospher} is getting forks");
                }
                TestForks(philospher);
            }
            BothForksAvailable[philospher].Wait();

        }

        private void PutDownForks(int philospher)
        {
            lock (_lock)
            {
                PhilosphersStates[philospher] = PhilosopherState.Thinking;

                TestForks(GetLeft(philospher));
                TestForks(GetRight(philospher));
            }
        }

        private void Eat(int philospher)
        {
            var dur = Random.Next(5000, 8000);
            lock (Console.Out)
            {
                Console.WriteLine($"_ Philosopher {philospher} is eating for next {dur / 1000} s");
            }
            Thread.Sleep(dur);

        }

        private void Philospher(int philospher)
        {
            while (true)
            {
                Think(philospher);
                TakeForks(philospher);
                Eat(philospher);
                PutDownForks(philospher);
            }
        }

        public void Run()
        {
            Task[] tasks = new Task[PhilosophersCount];
            for (int i = 0; i < PhilosophersCount; i++)
            {
                var philosopherId = i;
                tasks[i] = Task.Run(() => Philospher(philosopherId));
            }
            Task.WaitAll(tasks);
        }
    }
}
