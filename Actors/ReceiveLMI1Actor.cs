using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace DDM_Messwagen.Actors
{
    public class ReceiveLMI1Actor : DataSource
    {

        public Task Generator { get; set; }

        public ReceiveLMI1Actor(List<IActorRef> subscribers) : base(subscribers)
        {
            // test
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
                        Random rnd1 = new Random(); Random rnd2 = new Random(); Random rnd3 = new Random(); Random rnd4 = new Random();
                        List<double> rndList = new List<double>() {rnd1.NextDouble(), rnd2.NextDouble(), rnd3.NextDouble(), rnd4.NextDouble() };
                        LMIData newData = new LMIData(rndList);
                        Publish(newData);
                        Thread.Sleep(2000);
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


        public class LMIData
        {
            private List<double> dummyData;

            public LMIData() { }

            public LMIData(List<double> dummyData)
            {
                this.dummyData = dummyData;
            }

            public List<double> DummyData
            {
                get => dummyData;
                set => dummyData = value;
            }


        }
    }
}
