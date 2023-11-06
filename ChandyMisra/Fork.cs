using DiningPhilosophers.Philosophers;

namespace DiningPhilosophers.ChandyMisra
{
    internal class Fork
    {
        internal Fork(int id)
        {
            Id = id;
        }
        int Id { get; set; }
        internal bool IsClean { get; set; } = false;
        internal PhilosopherCh Owner { get; set; }

    }
}
