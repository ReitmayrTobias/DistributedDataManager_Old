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
           
        }

        public override void StartGenerator(object data = null)
        {
            KApiLib.Construct();
            GoSdkLib.Construct();
            system = new GoSystem();
            dataSet = new GoDataSet();

            // Connect to sensor and set data handler
            //KIpAddress ipAdress = KIpAddress.Parse("192.168.5.30");

            //sensor = system.FindSensorByIpAddress(ipAdress);
            sensor = system.FindSensorById(89550);
            sensor.Connect();
            system.EnableData(true);
            system.SetDataHandler(onData);

            Publish(new DDMViewModel.ViewData(null, this));
            Generator = Task.Run(() =>
            {
                try
                {
                    system.Start();
                    system.EnableData(true);
                }
                catch(Exception ex)
                {

                }
                while (CurrentState != State.Stopped)
                {
                    if (CurrentState == State.Running)
                    {
                        // FOr Testing
                        //Random random = new Random();

                        #region Create Dummy Data

                        //List<string> pin1 = new List<string> { random.Next(69, 81).ToString(), random.Next(10, 20).ToString(), random.Next(39, 50).ToString() };
                        //List<string> pin2 = new List<string> { random.Next(72, 84).ToString(), random.Next(10, 20).ToString(), random.Next(39, 50).ToString() };
                        //List<string> pin3 = new List<string> { random.Next(76, 88).ToString(), random.Next(10, 20).ToString(), random.Next(39, 50).ToString() };
                        //List<string> pin4 = new List<string> { random.Next(80, 92).ToString(), random.Next(10, 20).ToString(), random.Next(39, 50).ToString() };
                        //List<string> pin5 = new List<string> { random.Next(84, 96).ToString(), random.Next(10, 20).ToString(), random.Next(39, 50).ToString() };
                        //List<string> pin6 = new List<string> { random.Next(88, 100).ToString(), random.Next(10, 20).ToString(), random.Next(39, 50).ToString() };
                        //List<string> pin7 = new List<string> { random.Next(92, 104).ToString(), random.Next(10, 20).ToString(), random.Next(39, 50).ToString() };
                        //List<string> pin8 = new List<string> { random.Next(96, 108).ToString(), random.Next(10, 20).ToString(), random.Next(39, 50).ToString() };

                        //List<string> stringData = new List<string>() { pin1[0], pin1[1], pin1[2], pin2[0], pin2[1], pin2[2], pin3[0], pin3[1], pin3[2], pin4[0], pin4[1], pin4[2], pin5[0], pin5[1], pin5[2], pin6[0], pin6[1], pin6[2], pin7[0], pin7[1], pin7[2], pin8[0], pin8[1], pin8[2] };

                        #endregion

                        //LMIData testData = new LMIData(stringData);

                        //Publish(testData);
                        //Thread.Sleep(2000);
                    }
                }
                //sensor.Disconnect();
            });
        }
        public override void StopGenerator(object data = null)
        {
            Publish(new DDMViewModel.ViewData(null, this));
            system.Stop();
            system.EnableData(false);
        }
        public override void PauseGenerator(object data = null)
        {
            Publish(new DDMViewModel.ViewData(null, this));
            system.Stop();
            system.EnableData(false);
        }
        public override void ContinueGenerator(object data = null)
        {
            Publish(new DDMViewModel.ViewData(null, this));
            system.Start();
            system.EnableData(true);
        }

        public void onData(KObject data)
        {
            GoDataSet dataSet = (GoDataSet)data;
            List<string> dataString = new List<string>();
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
                                dataString.Add(measurementData.Value.ToString());
                            }
                        }
                        break;
                }
            }
            Publish(new LMIData(dataString));

        }

        public class LMIData
        {
            private Measurement myMeasurement;
            private Dictionary<string, List<string>> data;

            public LMIData() { }

            public LMIData(List<string> rawData)
            {
                myMeasurement = Measurement.ImprofeSchweiszen; //Abhängig pro Programm
                int jumper = 0;
                int pinNumber = 1;
                data = new Dictionary<string, List<string>>();

                switch (myMeasurement)
                {
                    case Measurement.Daimler:
                        while (data.Count != rawData.Count / 3)
                        {
                            var xValue = rawData[jumper];
                            var yValue = rawData[jumper + 1];
                            var zValue = rawData[jumper + 2];
                            if (xValue == "-1,79769313486232E+308" || yValue == "-1,79769313486232E+308" || zValue == "-1,79769313486232E+308")
                            {
                                xValue = yValue = zValue = "Not found";
                            }

                            List<string> pinData = new List<string>() { xValue, yValue, zValue };
                            data.Add("Pin " + pinNumber.ToString(), pinData);
                            pinNumber++;
                            jumper = jumper + 3;
                        }
                        break;
                    case Measurement.ImprofeSchweiszen:
                        while (data.Count != rawData.Count / 3)
                        {
                            var xValue = rawData[jumper];
                            var yValue = rawData[jumper + 1];
                            var zValue = rawData[jumper + 2];
                            if (xValue == "-1,79769313486232E+308" || yValue == "-1,79769313486232E+308" || zValue == "-1,79769313486232E+308")
                            {
                                xValue = yValue = zValue = "Not found";
                            }

                            List<string> pinData = new List<string>() { xValue, yValue, zValue };
                            data.Add("Pin " + pinNumber.ToString(), pinData);
                            pinNumber++;
                            jumper = jumper + 3;
                        }
                        break;
                    default:
                        break;
                }              
            }         
            
            public enum Measurement
            {
                Daimler,
                ImprofeSchweiszen
            }

            public Measurement MyMeasurement
            {
                get => myMeasurement;
                set => myMeasurement = value;
            }

            public Dictionary<string, List<string>> Data
            {
                get => data;
                set => data = value;
            }


        }
    }
}
