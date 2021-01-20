using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace DDM_Messwagen.Actors
{
    public class LMIAdvancedViewModel : ActorBase
    {
        public int counter;
        public LMIAdvancedViewModel(ConcurrentQueue<ReceiveLMI1Actor.LMIData> dataBuffer) : base(new List<IActorRef>())
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
