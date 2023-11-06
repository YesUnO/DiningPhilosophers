using DiningPhilosophers.Philosophers;

namespace DiningPhilosophers.ChandyMisra
{
    internal class Fork
    {
        internal Fork(int id, IPhilosopher owner)
        {
            Id = id;
            Owner = owner;
        }
        int Id { get; set; }
        internal bool IsClean { get; set; } = false;
        internal IPhilosopher Owner { get; set; }

    }
}
