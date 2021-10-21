using DDM_Messwagen.Actors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DDM_Messwagen.Actors;
using System.Collections.Concurrent;
using Akka.Actor;
using System.Threading;

namespace DDM_Messwagen
{
    public partial class LMIAdvancedView : Form, IActorViewModelCreator
    {

        private ConcurrentQueue<ReceiveLMI1Actor.LMIData> dataBuffer = new ConcurrentQueue<ReceiveLMI1Actor.LMIData>();
        private Dictionary<string, List<double>> calculationValues;
        public int countCurrent;

        public IActorRef ViewModel { get; private set; }


        public LMIAdvancedView()
        {
            InitializeComponent();
            countCurrent = 0;
        }

        public IActorRef GetViewModel(ActorSystem actorSystemRef)
        {
            var lmiVMProps = Props.Create(() => new LMIAdvancedViewModel(dataBuffer));
            ViewModel = actorSystemRef.ActorOf(lmiVMProps);

            return ViewModel;
        }

        public void UpdateContentFromViewModel()
        {
            
            try
            {               
                ReceiveLMI1Actor.LMIData lmiData = new ReceiveLMI1Actor.LMIData();
                
                if (dataBuffer.TryPeek(out lmiData))
                {
                    if (countCurrent < dataBuffer.Count)
                    {
                        addCurrentData(dataBuffer.ElementAt(countCurrent));
                        updateCharacterstics();
                        countCurrent++;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(this.ToString() + '\n' + ex.Message);
            }
        }
        private void addCurrentData(ReceiveLMI1Actor.LMIData lmiData)
        {
            var measurement = lmiData.MyMeasurement;

            switch (measurement)
            {
                case ReceiveLMI1Actor.LMIData.Measurement.Daimler:                  
                    var newRow = new string[1000];
                    var index = 1;
                    foreach (var pin in lmiData.Data)
                    {
                        if (pin.Value[0] != "Not found")
                        {
                            chart_Z.Series[pin.Key].Points.AddY(pin.Value[2]);
                            chart_Y.Series[pin.Key].Points.AddY(pin.Value[1]);
                            chart_X.Series[pin.Key].Points.AddY(pin.Value[0]);
                        }
                    }                 
                    break;
                case ReceiveLMI1Actor.LMIData.Measurement.ImprofeSchweiszen:
                    newRow = new string[1000];
                    index = 1;
                    foreach (var pin in lmiData.Data)
                    {
                        if (pin.Value[0] != "Not found")
                        {
                            chart_Z.Series[pin.Key].Points.AddY(pin.Value[2]);
                            chart_Y.Series[pin.Key].Points.AddY(pin.Value[1]);
                            chart_X.Series[pin.Key].Points.AddY(pin.Value[0]);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void updateCharacterstics()
        {
            try
            {
                dataBuffer.TryPeek(out var measurementData);

                switch (measurementData.MyMeasurement)
                {
                    case ReceiveLMI1Actor.LMIData.Measurement.Daimler:

                        #region Daimler Update Characteristics

                        List<List<double>> xValues = new List<List<double>>(8);
                        List<List<double>> yValues = new List<List<double>>(8);
                        List<List<double>> zValues = new List<List<double>>(8);

                        for (int i = 0; i < 8; i++)
                        {
                            xValues.Add(new List<double>());
                            yValues.Add(new List<double>());
                            zValues.Add(new List<double>());
                        }

                        int pinCount = 0;

                        foreach (var row in dataBuffer)
                        {
                            foreach (var pin in row.Data)
                            {
                                if (pin.Value[0] != "Not found")
                                {
                                    xValues[pinCount].Add(Convert.ToDouble(pin.Value[0]));
                                    yValues[pinCount].Add(Convert.ToDouble(pin.Value[1]));
                                    zValues[pinCount].Add(Convert.ToDouble(pin.Value[2]));
                                }
                                pinCount++;
                            }
                            pinCount = 0;
                        }

                        var meanX = calculateMean(xValues);
                        var meanY = calculateMean(yValues);
                        var meanZ = calculateMean(zValues);

                        var minX = calculateMin(xValues);
                        var minY = calculateMin(yValues);
                        var minZ = calculateMin(zValues);

                        var maxX = calculateMax(xValues);
                        var maxY = calculateMax(yValues);
                        var maxZ = calculateMax(zValues);

                        var devX = calculateDeviation(xValues);
                        var devY = calculateDeviation(yValues);
                        var devZ = calculateDeviation(zValues);

                        dgv_X.Rows.Clear();
                        dgv_Y.Rows.Clear();
                        dgv_Z.Rows.Clear();

                        dgv_X.Rows.Add(meanX.ToArray());
                        dgv_X.Rows.Add(minX.ToArray());
                        dgv_X.Rows.Add(maxX.ToArray());
                        dgv_X.Rows.Add(devX.ToArray());

                        dgv_Y.Rows.Add(meanY.ToArray());
                        dgv_Y.Rows.Add(minY.ToArray());
                        dgv_Y.Rows.Add(maxY.ToArray());
                        dgv_Y.Rows.Add(devY.ToArray());

                        dgv_Z.Rows.Add(meanZ.ToArray());
                        dgv_Z.Rows.Add(minZ.ToArray());
                        dgv_Z.Rows.Add(maxZ.ToArray());
                        dgv_Z.Rows.Add(devZ.ToArray());

                        #endregion 

                        break;
                    default:
                        break;
                }
                
            }
            catch(Exception ex)
            {

            }
        
        }

        private string[] calculateMean(List<List<double>> values)
        {
            string[] mean = new string[9];
            int i = 0;
            mean[i] = "Average";
            foreach( var pin in values)
            {
                i++;
                if(pin.Count != 0)
                {
                    mean[i] = pin.Average().ToString();
                }    
            }

            return mean;
        }

        private string[] calculateDeviation(List<List<double>> values)
        {
            string[] dev = new string[9];
            int i = 0;
            dev[i] = "Deviation";
            foreach (var pin in values)
            {
                i++;
                if(pin.Count != 0)
                {
                    double average = pin.Average();
                    double sumOfSquaresOfDifferences = pin.Select(val => (val - average) * (val - average)).Sum();
                    dev[i] = Math.Sqrt(sumOfSquaresOfDifferences / pin.Count).ToString();
                }            
            }

            return dev;
        }

        private string[] calculateMax(List<List<double>> values)
        {
            string[] max = new string[9];
            int i = 0;
            max[i] = "Max";
            foreach (var pin in values)
            {
                i++;
                if (pin.Count != 0)
                {
                    max[i] = pin.Max().ToString();
                }
            }

            return max;
        }

        private string[] calculateMin(List<List<double>> values)
        {
            string[] min = new string[9];
            int i = 0;
            min[i] = "Min";
            foreach (var pin in values)
            {
                i++;
                if (pin.Count != 0)
                {
                    min[i] = pin.Min().ToString();
                }

            }
            return min;
        }

        private void LMIAdvancedView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

       
    }
}
