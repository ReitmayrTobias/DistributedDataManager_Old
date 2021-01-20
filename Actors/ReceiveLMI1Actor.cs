using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Lmi3d.GoSdk;
using Lmi3d.Zen;
using Lmi3d.Zen.Io;
using Lmi3d.GoSdk.Messages;

namespace DDM_Messwagen.Actors
{
    public class ReceiveLMI1Actor : DataSource
    {

        private GoSystem system;
        private GoSensor sensor;
        private GoDataSet dataSet;

        public Task Generator { get; set; }

        public ReceiveLMI1Actor(List<IActorRef> subscribers) : base(subscribers)
        {
            //KApiLib.Construct();              // Vom Konstruktor weg, weil 
            //GoSdkLib.Construct();
            //GoSystem system = new GoSystem();
            //GoDataSet dataSet = new GoDataSet();
            //KIpAddress ipAddress = KIpAddress.Parse("192.168.1.10");
        }

        public override void StartGenerator(object data = null)
        {
            Publish(new DDMViewModel.ViewData(null, this));
            Generator = Task.Run(() =>
            {
                try
                {
                    // Connect to sensor and set data handler
                    //sensor = system.FindSensorByIpAddress(ipAddress);
                    //sensor = system.FindSensorById(89550);
                    //sensor.Connect();
                    //system.EnableData(true);
                    //system.SetDataHandler(onData);
                }
                catch(Exception ex)
                {

                }
                while (CurrentState != State.Stopped)
                {
                    if (CurrentState == State.Running)
                    {
                        //system.EnableData(false);


                        // FOr Testing
                        Random random = new Random();
                        LMIData testData = new LMIData();
                        testData.dummyString = random.Next().ToString();
                        testData.MyMeasurement = LMIData.Measurement.Test;
                        Publish(testData);
                        Thread.Sleep(2000);
                    }
                }
                //sensor.Disconnect();
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

        public void onData(KObject data)
        {
            GoDataSet dataSet = (GoDataSet)data;
            //var dataWithID = new Dictionary<string, string>();
            for (UInt32 i = 0; i < dataSet.Count; i++)
            {
                GoDataMsg dataObj = (GoDataMsg)dataSet.Get(i);
                switch (dataObj.MessageType)
                {
                    case GoDataMessageType.Stamp:
                        {
                            GoStampMsg stampMsg = (GoStampMsg)dataObj;
                            for (UInt32 j = 0; j < stampMsg.Count; j++)
                            {
                                GoStamp stamp = stampMsg.Get(j);
                                //Console.WriteLine("Frame Index = {0}", stamp.FrameIndex);
                                //Console.WriteLine("Time Stamp = {0}", stamp.Timestamp);
                                //Console.WriteLine("Encoder Value = {0}", stamp.Encoder);
                            }
                        }
                        break;
                    case GoDataMessageType.Measurement:
                        {
                            GoMeasurementMsg measurementMsg = (GoMeasurementMsg)dataObj;


                            for (UInt32 k = 0; k < measurementMsg.Count; ++k)
                            {
                                GoMeasurementData measurementData = measurementMsg.Get(k);
                                //Console.WriteLine("ID: {0}", measurementMsg.Id);
                                //Console.WriteLine("Value: {0}", measurementData.Value);
                                //Console.WriteLine("Decision: {0}", measurementData.Decision);                                
                                //dataWithID.Add(measurementMsg.Id.ToString(), measurementData.Value.ToString());
                            }
                        }
                        break;
                }

            }

            Publish(new LMIData()); //TODO Data Shell

        }

        public class LMIData
        {
            private Measurement myMeasurement;
            private List<double> dummyData;
            public string dummyString;

            public LMIData() { }

            public LMIData(List<double> dummyData)
            {
                this.dummyData = dummyData;
                this.dummyString = dummyString;
            }         
            
            public enum Measurement
            {
                Test
            }

            public Measurement MyMeasurement
            {
                get => myMeasurement;
                set => myMeasurement = value;
            }

            public List<double> DummyData
            {
                get => dummyData;
                set => dummyData = value;
            }


        }
    }
}
