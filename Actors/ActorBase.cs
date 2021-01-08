using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;

namespace DDM_Messwagen.Actors
{
    public class ActorBase : ReceiveActor
    {

        private List<IActorRef> receivers = new List<IActorRef>();
        protected readonly ILoggingAdapter log = Logging.GetLogger(Context);

        public ActorBase(List<IActorRef> subscribers)
        {
            try
            {
                for (int i = 0; i < subscribers.Count; i++)
                {
                    receivers.Add(subscribers[i]);
                }
                   
            }
            catch(NullReferenceException e)
            {
                log.Debug("ActorBase: no Subscribers --- {0}", e.Message);
            }

            Receive<SubscriptionCommand>(cmd =>
            {
                if (cmd == SubscriptionCommand.AddSubscriber)
                {
                    receivers.Add((IActorRef)cmd.Data);
                }
                if (cmd == SubscriptionCommand.RemoveSubscriber)
                {
                    receivers.Remove((IActorRef)cmd.Data);
                }
            });
        }

        public void Publish(object message)
        {
            foreach (var rec in receivers)
            {
                rec.Tell(message);
            }
        }
    }
}
