using DiningPhilosophers.Philosophers;

namespace DiningPhilosophers.SolutionInstance
{
    public class RunnerInstance : IRunnerInstance
    {
        public IPhilosopher[] Philosophers { get; set; } = new IPhilosopher[0];
        int[] EatCounter { get; set; } = new int[0];
        private Random Random = new();
        public int Count { get { return Philosophers.Length; } }
        internal bool IsSingleRun { get; private set; } = false;
        private readonly object _lock = new();
        //private readonly object _counterLock = new(); maybe?
        private int _low = 10;
        private int _high = 15;

        public virtual void Initialize(IPhilosopher[] philosophers)
        {
            Philosophers = philosophers;
            EatCounter = new int[Count];
        }

        internal void Think(int philospher, CancellationToken ct = default)
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
            lock (EatCounter)
            {
                EatCounter[philospher]++;
            }
        }

        internal void Eat(int philospher, CancellationToken ct = default)
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

        internal void LogGetForks(int philospher)
        {
            if (IsSingleRun)
            {
                lock (_lock)
                {
                    Console.WriteLine($" + Philosopher {philospher} is getting forks");
                }
            }
        }

        internal void LogPutForks(int philospher)
        {
            if (IsSingleRun)
            {
                lock (_lock)
                {
                    Console.WriteLine($" - Philosopher {philospher} gave up forks");
                }
            }
        }

        public void SetIsSingleRun(bool isSingle)
        {
            IsSingleRun = isSingle;
            if (isSingle)
            {
                _high = 8000;
                _low = 5000;
            }
        }

        internal int GetLeft(int id)
        {
            return (id - 1 + Count) % Count;
        }

        internal int GetRight(int id)
        {
            return (id + 1) % Count;
        }

        public async Task Run(CancellationToken ct = default)
        {
            Task[] tasks = new Task[Count];
            for (int i = 0; i < Count; i++)
            {
                var philosopherId = i;
                tasks[i] = Task.Run(() => Philosophers[philosopherId].Run(ct));
            }
            await Task.WhenAll(tasks);
        }
    }
}
