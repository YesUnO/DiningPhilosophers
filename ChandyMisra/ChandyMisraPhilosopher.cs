using DiningPhilosophers.Philosophers;
using DiningPhilosophers.SolutionInstance;

namespace DiningPhilosophers.ChandyMisra
{
    internal class ChandyMisraPhilosopher : Philosopher
    {
        internal Fork LeftFork { get; set; }
        internal Fork RightFork { get; set; }
        internal ChandyMisraPhilosopher LeftPhilosopher { get; set; }
        internal ChandyMisraPhilosopher RightPhilosopher { get; set; }
        public ChandyMisraPhilosopher(int id, ChandyMisraInstance intance) : base(id, intance)
        {
            Id = id;
            RunnerInstance = intance;
            var fork = new Fork(Id, this);
            ((ChandyMisraInstance)RunnerInstance).Forks[Id] = fork;
            LeftFork = fork;
        }
        internal override void Eat()
        {
            RightFork.IsClean = LeftFork.IsClean = false;
        }

        internal override void GetForks()
        {
            State = PhilosopherState.Hungry;

            RunnerInstance.LogGetForks(Id);

            WaitForFork(LeftFork);
            WaitForFork(RightFork);

            State = PhilosopherState.Eating;
        }

        internal override void PutForks()
        {
            GiveUpForksIfNeeded();

            RunnerInstance.LogPutForks(Id);

            State = PhilosopherState.Thinking;
        }

        private void WaitForFork(Fork fork)
        {
            if (fork.Owner != this)
            {
                lock (fork)
                {
                    while (fork.Owner != this)
                    {
                        GiveUpForksIfNeeded();
                        Monitor.Wait(fork);
                    }
                }
            }
        }

        private void GiveUpForksIfNeeded()
        {
            TryGiveUpFork(LeftFork, LeftPhilosopher);
            TryGiveUpFork(RightFork, RightPhilosopher);
        }

        private void TryGiveUpFork(Fork fork, IPhilosopher reciever)
        {
            lock (fork)
            {
                if (fork.Owner == this && reciever.State == PhilosopherState.Hungry && !fork.IsClean)
                {
                    SendFork(fork, reciever);
                }
            }
        }

        private void SendFork(Fork fork, IPhilosopher reciever)
        {
            fork.Owner = reciever;
            fork.IsClean = true;
            Monitor.PulseAll(fork);
        }
    }
}
