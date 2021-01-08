using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using DDM_Messwagen.Actors;

namespace DDM_Messwagen.Actors
{
    public class BaumerViewModel : ActorBase
    {

        public BaumerViewModel(ConcurrentQueue<ReceiveBaumerActor.BaumerData> dataBuffer) : base (new List<IActorRef>())
        {
            Receive<ReceiveBaumerActor.BaumerData>(data =>
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
