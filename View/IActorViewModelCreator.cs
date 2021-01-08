using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace DDM_Messwagen
{
    public interface IActorViewModelCreator
    {
        IActorRef GetViewModel(ActorSystem actorSystemRef);

        void UpdateContentFromViewModel();
    }
}
