using GameFramework.Entities.Creatures.Interface;

namespace GameFramework.Observer
{
    public interface IObserver
    {
        // Receive update from subject
        void Update(ICreature creature);
    }
}
