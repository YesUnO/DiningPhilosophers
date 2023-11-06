namespace DiningPhilosophers.Old
{
    internal static class Utills
    {
        private static Random Random = new();
        internal static int Count { get; private set; } = 5;
        internal static bool IsSingleRun { get; private set; } = false;
        private static readonly object _lock = new object();
        static int _low = 10;
        static int _high = 15;

        internal static void Think(int philospher, CancellationToken ct = default)
        {
            int dur = 0;
            lock (_lock)
            {
                dur = Random.Next(_low, _high);
                if (IsSingleRun)
                {
                    Console.WriteLine($"Philosopher {philospher} is thinking for next {dur / 1000} s");
                }
            }
            ct.WaitHandle.WaitOne(TimeSpan.FromMilliseconds(dur));
        }

        internal static void Eat(int philospher, CancellationToken ct = default)
        {
            int dur = 0;
            lock (_lock)
            {
                dur = Random.Next(_low, _high);
                if (IsSingleRun)
                {
                    Console.WriteLine($"__ Philosopher {philospher} is eating for next {dur / 1000} s");
                }
            }
            ct.WaitHandle.WaitOne(TimeSpan.FromMilliseconds(dur));
        }

        internal static void LogGetForks(int philospher)
        {
            if (IsSingleRun)
            {
                lock (_lock)
                {
                    Console.WriteLine($" + Philosopher {philospher} is getting forks");
                }
            }
        }

        internal static void LogPutForks(int philospher)
        {
            if (IsSingleRun)
            {
                lock (_lock)
                {
                    Console.WriteLine($" - Philosopher {philospher} gave up forks");
                }
            }
        }

        internal static void SetCount(int count)
        {
            Count = count;
        }

        internal static void SetIsSingleRun(bool isSingle)
        {
            IsSingleRun = isSingle;
            _high = 8000;
            _low = 5000;
        }

        internal static int GetLeft(int id)
        {
            return (id - 1 + Count) % Count;
        }

        internal static int GetRight(int id)
        {
            return (id + 1) % Count;
        }

    }
}
