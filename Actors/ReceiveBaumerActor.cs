using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Akka.Actor;
using Akka.Actor.Dsl;
using Akka.Event;

namespace DDM_Messwagen.Actors
{
    public class ReceiveBaumerActor : DataSource
    {

        public Task Generator { get; set; }

        public ReceiveBaumerActor(List<IActorRef> subscribers) : base(subscribers)
        {
                        
        }

        public override void StartGenerator(object data = null)
        {
            Publish(new DDMViewModel.ViewData(null, this));
            Generator = Task.Run(() =>
            {
                while (CurrentState != State.Stopped)
                {
                    if (CurrentState == State.Running)
                    {
                        Random rnd = new Random();
                        BaumerData newData = new BaumerData(rnd.NextDouble() + 5);
                        Publish(newData);
                        Thread.Sleep(100);
                    }
                }
            });
        }
        public override void StopGenerator(object data = null)
        {
            Publish(new DDMViewModel.ViewData(null, this));
        }
        public override void PauseGenerator(object data = null)
        {
            Publish(new DDMViewModel.ViewData(null, this));
        }
        public override void ContinueGenerator(object data = null)
        {
            Publish(new DDMViewModel.ViewData(null, this));
        }


        public class BaumerData
        {
            private double dummyData;

            public BaumerData() { }

            public BaumerData(double dummyData)
            {
                this.dummyData = dummyData;
            }

            public double DummyData
            {
                get => dummyData;
                set => dummyData = value;
            }


        }
    }
}
