namespace DiningPhilosophers.Old
{
    internal class Philosopher
    {
        public Philosopher(int id, ChandyMisra runInstance)
        {
            Id = id;
            State = PhilosopherState.Thinking;
            RunInstance = runInstance;
        }
        internal int Id { set; get; }
        internal PhilosopherState State { get; set; }
        internal Fork LeftFork { get; set; }
        internal Fork RightFork { get; set; }
        internal Philosopher LeftPhilosopher { get; set; }
        internal Philosopher RightPhilosopher { get; set; }
        private ChandyMisra RunInstance;

        private void GetForks(CancellationToken ct)
        {
            State = PhilosopherState.Hungry;

            Utills.LogGetForks(Id);

            WaitForFork(LeftFork, ct);
            WaitForFork(RightFork, ct);

            State = PhilosopherState.Eating;
        }

        private void WaitForFork(Fork fork, CancellationToken ct)
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

        private void TryGiveUpFork(Fork fork, Philosopher reciever)
        {
            lock (fork)
            {
                if (fork.Owner == this && reciever.State == PhilosopherState.Hungry && !fork.IsClean)
                {
                    SendFork(fork, reciever);
                }
            }
        }

        private void PutForks()
        {
            GiveUpForksIfNeeded();

            Utills.LogPutForks(Id);

            State = PhilosopherState.Thinking;
        }

        private void SendFork(Fork fork, Philosopher reciever)
        {
            fork.Owner = reciever;
            fork.IsClean = true;
            Monitor.PulseAll(fork);
        }

        internal void Run(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                Utills.Think(Id, ct);
                GetForks(ct);
                Utills.Eat(Id, ct);
                RightFork.IsClean = LeftFork.IsClean = false;
                PutForks();
                lock (RunInstance.EatCount)
                {
                    RunInstance.EatCount[Id]++;
                }
            }
        }
    }
}
