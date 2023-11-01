

namespace DiningPhilosophers
{
    internal static class Utills
    {
        private static Random Random = new();
        internal static int Count { get; private set; } = 5;
        internal static bool SingleImplRun { get; private set; } = false;
        static int _low = 5000;
        static int _high = 8000;

        internal static void Think(int philospher)
        {
            var dur = Random.Next(_low, _high);
            if (SingleImplRun)
            {
                lock (Console.Out)
                {
                    Console.WriteLine($"Philosopher {philospher} is thinking for next {dur / 1000} s");
                }
            }
            
            Thread.Sleep(dur);
        }

        internal static void Eat(int philospher)
        {
            var dur = Random.Next(_low, _high);
            if (SingleImplRun)
            {
                lock (Console.Out)
                {
                    Console.WriteLine($"__ Philosopher {philospher} is eating for next {dur / 1000} s");
                }
            }
            Thread.Sleep(dur);
        }

        internal static void LogGetForks(int philospher)
        {
            if (SingleImplRun)
            {
                lock (Console.Out)
                {
                    Console.WriteLine($" + Philosopher {philospher} is getting forks");
                }
            }
        }

        internal static void LogPutForks(int philospher)
        {
            if (SingleImplRun)
            {
                lock (Console.Out)
                {
                    Console.WriteLine($" - Philosopher {philospher} gave up forks");
                }
            }
        }

        internal static void SetCount(int count)
        {
            Count = count;
        }

        internal static void SetImplRun(bool isSingle)
        {
            SingleImplRun = isSingle;
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
