
namespace DiningPhilosophers
{
    internal class ChandyMisra
    {
        private readonly object _lock = new object();
        private enum ForkState
        {
            Clean,
            Dirty
        }
        internal ChandyMisra(int count)
        {
            Forks = new Fork[count];
            Philosophers = new Philosopher[count];
            Count = count;
            for (int i = 0; i < count; i++)
            {
                var fork = new Fork(i);
                var philosopher = new Philosopher(i);

                fork.State = ForkState.Dirty;
                fork.Owner = philosopher;

                philosopher.LeftFork = fork;

                Philosophers[i] = philosopher;
                Forks[i] = fork;
            }
        }
        int Count;
        Fork[] Forks;
        Philosopher[] Philosophers;

        private class Fork
        {
            internal Fork(int id)
            {
                Id = id;
            }
            int Id { get; set; }
            internal ForkState? State { get; set; }
            internal Philosopher? Owner { get; set; }
        }

        private class Philosopher
        {
            public Philosopher(int id)
            {
                Id = id;
            }
            int Id { set; get; }
            internal Fork? LeftFork { get; set; }
            internal Fork? RightFork { get; set; }

            
        }
    }
}
