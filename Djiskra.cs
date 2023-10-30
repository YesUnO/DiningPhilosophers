
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
            PhilosphersStates[philospher] = PhilosopherState.Hungry;
        }

        private void TakeForks(int philospher)
        {
            lock (_lock)
            {
                Console.WriteLine($"Philosopher {philospher} is getting forks");
            }
            bool hasLeft = false;
            bool hasRight = false;
            while (!hasLeft || !hasRight)
            {
                hasLeft = GetLeft(philospher) != 2;
                hasRight = GetRight(philospher) != 2;
            }

            PhilosphersStates[PhilosophersCount] = PhilosopherState.Eating;
        }

        private void PutDownForks(int philospher)
        {

        }

        private void Eat(int philospher)
        {
            var dur = Random.Next(5000, 8000);
            lock (Console.Out)
            {
                Console.WriteLine($"Philosopher {philospher} is eating for next {dur / 1000} s");
            }
            Thread.Sleep(dur);
            PhilosphersStates[PhilosophersCount] = PhilosopherState.Thinking;
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
            for (int i = 0; i < PhilosophersCount; i++)
            {
                Philospher(i);
            }
        }
    }
}
