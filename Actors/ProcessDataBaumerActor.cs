using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace DDM_Messwagen.Actors
{
    public class ProcessDataBaumerActor : ActorBase
    {
        public ProcessDataBaumerActor(List<IActorRef> subscribers) : base(subscribers)
        {
            //List<ConnectWenglorActor.WenglorData> dummyList = new List<ConnectWenglorActor.WenglorData>();

            Receive<ReceiveBaumerActor.BaumerData>(data =>
            {
                Publish(data);                
            });
        }
    }
}
