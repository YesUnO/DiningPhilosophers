using DiningPhilosophers.Philosophers;

namespace DiningPhilosophers.Djiskrax
{
    internal class DjiskraPhilosopher : Philosopher
    {
        private Instance _instance;
        public DjiskraPhilosopher(int id, Instance instance) : base(id, instance)
        {
            _instance = instance;
        }

        internal override void Eat()
        {
        }

        internal override void GetForks()
        {
            _instance.TakeForks(Id);
        }

        internal override void PutForks()
        {
            _instance.PutDownForks(Id);
        }

    }
}
