namespace DiningPhilosophers.Philosophers
{
    public interface IPhilosopher
    {
        public int Id { get; set; }
        public PhilosopherState State { get; set; }
        void Run(CancellationToken ct);
    }
}
