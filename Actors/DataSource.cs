using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace DDM_Messwagen.Actors
{
    public abstract class DataSource : ActorBase
    {
        public enum State
        {
            Running,
            Paused,
            Stopped
        }

        public State CurrentState { get; private set; }

        public DataSource(List<IActorRef> subscribers) : base (subscribers)
        {
            CurrentState = State.Stopped;

            Receive<GeneratorCommand>(gcm =>
           {
               if (gcm == GeneratorCommand.StartGenerator)
               {
                   CurrentState = State.Running;
                   try
                   {
                       StartGenerator(gcm.Data);
                   }
                   catch(Exception ex)
                   {

                   }
               }
               if (gcm == GeneratorCommand.StopGenerator)
               {
                   CurrentState = State.Stopped;
                   try
                   {
                       StopGenerator(gcm.Data);
                   }
                   catch
                   {

                   }
               }
               if (gcm == GeneratorCommand.PauseGenerator)
               {
                   CurrentState = State.Paused;
                   try
                   {
                       PauseGenerator(gcm.Data);
                   }
                   catch
                   {

                   }
               }
               if (gcm == GeneratorCommand.ContinueGenerator)
               {
                   CurrentState = State.Running;
                   try
                   {
                       ContinueGenerator(gcm.Data);
                   }
                   catch
                   {

                   }
               }
           });
        }

        public abstract void StartGenerator(object data = null);
        public abstract void StopGenerator(object data = null);
        public abstract void PauseGenerator(object data = null);
        public abstract void ContinueGenerator(object data = null);






    }
}
