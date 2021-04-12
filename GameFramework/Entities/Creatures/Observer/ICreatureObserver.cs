using GameFramework.Entities.Creatures.Interface;
using GameFramework.Observer;

namespace GameFramework.Entities.Creatures.Observer
{
    public interface ICreatureObserver
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(ICreature creature);
    }
}