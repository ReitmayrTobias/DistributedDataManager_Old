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
using BGAPI2;

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
                BGAPI2.SystemList systemList = null;
                BGAPI2.System mSystem = null;
                string sSystemID = "";

                BGAPI2.InterfaceList interfaceList = null;
                BGAPI2.Interface mInterface = null;
                string sInterfaceID = "";

                BGAPI2.DataStreamList datastreamList = null;
                BGAPI2.DataStream mDataStream = null;
                string sDataStreamID = "";

                BufferList bufferList = null;
                BGAPI2.Buffer mBuffer = null;

                try
                {
                    systemList = SystemList.Instance;
                    systemList.Refresh();
                    System.Console.Write("5.1.2 Detected systems: {0}\n", systemList.Count);
                }
                catch (BGAPI2.Exceptions.IException ex)
                {
                    System.Console.Write("ErrorType: {0}.\n", ex.GetType());
                    System.Console.Write("ErrorException {0}.\n", ex.GetErrorDescription());
                    System.Console.Write("in function: {0}.\n", ex.GetFunctionName());
                }



                while (CurrentState != State.Stopped)
                {
                    if (CurrentState == State.Running)
                    {
                       
                        //Publish(newData);
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
