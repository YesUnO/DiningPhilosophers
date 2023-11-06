
using DiningPhilosophers.Djiskrax;

namespace DiningPhilosophers
{
    internal class DjiskraPhilosopher : Philosopher
    {
        private Instance _instance;
        public DjiskraPhilosopher(Instance instance) : base(instance) 
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
