using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace DDM_Messwagen.Actors
{
    public class LMIViewModel : ActorBase
    {
        public LMIViewModel(ConcurrentQueue<ReceiveLMI1Actor.LMIData> dataBuffer) : base(new List<IActorRef>())
        {
            Receive<ReceiveLMI1Actor.LMIData>(data =>
            {
                dataBuffer.Enqueue(data);
            });

            Receive<GeneratorCommand>(gcm =>
            {
                Publish(gcm);
            });
        }
    }
}
