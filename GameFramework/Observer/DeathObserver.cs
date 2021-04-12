using System.Diagnostics;
using GameFramework.Entities.Creatures.Interface;
using GameFramework.Tracer;

namespace GameFramework.Observer
{
    public class DeathObserver : IObserver
    {

        public void Update(ICreature creature)
        {
            TraceWorker.Write(TraceEventType.Information, 2, $"{creature.GetType().Name} {creature.Name} was killed");

        }
    }
}
