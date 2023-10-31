
namespace DiningPhilosophers
{
    internal class ChandyMisra
    {
        internal enum ForkState
        {
            Clean,
            Dirty
        }
        internal ChandyMisra(int count)
        {
            Utills.SetCount(count);
            Forks = new Fork[count];
            Philosophers = new Philosopher[count];
            for (int i = 0; i < count; i++)
            {
                var fork = new Fork(i);
                var philosopher = new Philosopher(i);

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
        Fork[] Forks;
        private Philosopher[] Philosophers { get; }

        internal class Fork
        {
            internal Fork(int id)
            {
                Id = id;
            }
            int Id { get; set; }
            internal bool IsClean { get; set; } = false;
            internal Philosopher Owner { get; set; }

            internal void Request(Philosopher requestor)
            {
               
            }
        }

        internal class Philosopher
        {
            public Philosopher(int id)
            {
                Id = id;
            }
            internal int Id { set; get; }
            internal Fork LeftFork { get; set; } 
            internal Fork RightFork { get; set; }
            internal Philosopher LeftPhilosopher { get; set; }
            internal Philosopher RightPhilosopher { get; set; }

            internal void GetForks()
            {
                LeftPhilosopher.GiveRightFork();
                RightPhilosopher.GiveLeftFork();
            }

            internal void GiveLeftFork() 
            {

            }

            internal void GiveRightFork()
            {

            }

            internal void PutForksDown()
            {

            }
        }
    }
}
