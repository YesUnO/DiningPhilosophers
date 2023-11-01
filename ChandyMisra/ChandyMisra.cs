namespace DiningPhilosophers.ChandyMisra
{
    internal class ChandyMisra
    {
        Fork[] Forks;
        internal Philosopher[] Philosophers { get; }
        internal int[] EatCount { get; } = null;
        internal ChandyMisra(int count)
        {
            Utills.SetCount(count);
            Utills.SetImplRun(true);
            Forks = new Fork[count];
            Philosophers = new Philosopher[count];
            for (int i = 0; i < count; i++)
            {
                var fork = new Fork(i);
                var philosopher = new Philosopher(i, this);

                fork.Owner = philosopher;

                philosopher.LeftFork = fork;

                Philosophers[i] = philosopher;
                Forks[i] = fork;
            }

            foreach (var philosopher in Philosophers)
            {
                philosopher.LeftPhilosopher = Philosophers[Utills.GetLeft(philosopher.Id)];
                philosopher.RightPhilosopher = Philosophers[Utills.GetRight(philosopher.Id)];

                philosopher.RightFork = Forks[Utills.GetRight(philosopher.Id)];
            }
        }

        internal ChandyMisra()
        {
            Forks = new Fork[Utills.Count];
            Philosophers = new Philosopher[Utills.Count];
            EatCount = new int[Utills.Count];
            for (int i = 0; i < Utills.Count; i++)
            {
                var fork = new Fork(i);
                var philosopher = new Philosopher(i, this);

                fork.Owner = philosopher;

                philosopher.LeftFork = fork;

                Philosophers[i] = philosopher;
                Forks[i] = fork;
            }

            foreach (var philosopher in Philosophers)
            {
                philosopher.LeftPhilosopher = Philosophers[Utills.GetLeft(philosopher.Id)];
                philosopher.RightPhilosopher = Philosophers[Utills.GetRight(philosopher.Id)];

                philosopher.RightFork = Forks[Utills.GetRight(philosopher.Id)];
            }
        }

        internal async Task RunTaskWithOptionalCancelationToken(CancellationToken cancellationToken = default)
        {
            Task[] tasks = new Task[Utills.Count];
            for (int i = 0; i < Utills.Count; i++)
            {
                var philosopherId = i;
                tasks[i] = Task.Run(() => Philosophers[philosopherId].Run(cancellationToken));
            }
            await Task.WhenAll(tasks);
        }
    }
}
