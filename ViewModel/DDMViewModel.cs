using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace DDM_Messwagen.Actors
{
    public class DDMViewModel : ActorBase
    {

        public DDMViewModel(ConcurrentQueue<DDMViewModel.ViewData> dataBuffer) : base(new List<IActorRef>())
        {
            Receive<DDMViewModel.ViewData>(data =>
            {
                dataBuffer.Enqueue(data);
            });

            Receive<GeneratorCommand>(gcm =>
            {
                Publish(gcm);
            });
        }

        public class ViewData
        {
            public enum State
            {
                Running,
                Paused,
                Stopped,
                Unknown
            }

            private List<bool> dummyData;
            private State lmiState;
            private State wenglorState;

            public ViewData() { }

            public ViewData(List<bool> dummyData)
            {
                this.dummyData = dummyData;
                this.lmiState = State.Unknown;
                this.wenglorState = State.Unknown;
            }

            public ViewData(List<bool> dummyData, DataSource connectActor)
            {
                try
                {
                    
                    if (connectActor is ReceiveLMI1Actor)
                    {
                        lmiState = (State)connectActor.CurrentState;
                        wenglorState = State.Unknown;
                    } else if(connectActor is ReceiveBaumerActor)
                    {
                        wenglorState = (State)connectActor.CurrentState;
                        lmiState = State.Unknown;
                    }
                }
                catch
                {

                }
            }

            public List<bool> DummyData
            {
                get => dummyData;
                set => dummyData = value;
            }

            public State LmiState
            {
                get => lmiState;
                set => lmiState = value;
            }

            public State WenglorState
            {
                get => wenglorState;
                set => wenglorState = value;
            }

            //public State GetStateFromCommand(GeneratorCommand gcm)
            //{
            //    if(gcm == GeneratorCommand.StartGenerator || gcm == GeneratorCommand.ContinueGenerator)
            //    {
            //        return State.Running;
            //    }
            //    else if(gcm == GeneratorCommand.PauseGenerator)
            //    {
            //        return State.Paused;
            //    }
            //    else if(gcm == GeneratorCommand.StopGenerator)
            //    {
            //        return State.Stopped;
            //    }
            //    else
            //    {
            //        return State.Unknown;
            //    }
            //}


        }
    }
}
