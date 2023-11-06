using DiningPhilosophers.SolutionInstance;

namespace DiningPhilosophers.ChandyMisra
{
    internal class ChandyMisraInstance : RunnerInstance
    {
        internal Fork[] Forks = new Fork[0];
        public override void Initialize()
        {
            Forks = new Fork[Count];
        }
        public void FinalizeInitialization()
        {
            var philosophers = (ChandyMisraPhilosopher[])Philosophers;
            foreach (var philosopher in philosophers)
            {
                philosopher.LeftPhilosopher = philosophers[GetLeft(philosopher.Id)];
                philosopher.RightPhilosopher = philosophers[GetRight(philosopher.Id)];

                philosopher.RightFork = Forks[GetRight(philosopher.Id)];
            }
        }
    }
}
