

namespace DiningPhilosophers
{
    internal static class Utills
    {
        private static Random Random = new();
        internal static int Count { get; private set; }


        internal static void Think(int philospher)
        {
            var dur = Random.Next(5000, 8000);
            lock (Console.Out)
            {
                Console.WriteLine($"Philosopher {philospher} is thinking for next {dur / 1000} s");
            }
            Thread.Sleep(dur);
        }

        internal static void Eat(int philospher)
        {
            var dur = Random.Next(5000, 8000);
            lock (Console.Out)
            {
                Console.WriteLine($"_ Philosopher {philospher} is eating for next {dur / 1000} s");
            }
            Thread.Sleep(dur);
        }

        internal static void GetForks(int philospher)
        {
            lock (Console.Out)
            {
                Console.WriteLine($"Philosopher {philospher} is getting forks");
            }
        }

        internal static void SetCount(int count)
        {
            Count = count;
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
