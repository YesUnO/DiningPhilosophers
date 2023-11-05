
namespace DiningPhilosophers
{
    public interface IPhilosopher
    {
        void Run(CancellationToken ct);
    }
}
