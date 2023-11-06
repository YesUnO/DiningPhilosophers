
namespace DiningPhilosophers.Old
{
    internal class Fork
    {
        internal Fork(int id)
        {
            Id = id;
        }
        int Id { get; set; }
        internal bool IsClean { get; set; } = false;
        internal Philosopher Owner { get; set; }

    }
}
