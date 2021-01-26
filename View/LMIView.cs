using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DDM_Messwagen.Actors;
using System.Collections.Concurrent;
using Akka.Actor;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace DDM_Messwagen
{
    public partial class LMIView : UserControl, IActorViewModelCreator
    {
        private IActorRef LMIAdvancedViewModel;
        private LMIAdvancedView lmiAdvancedView1;
        private ConcurrentQueue<ReceiveLMI1Actor.LMIData> dataBuffer = new ConcurrentQueue<ReceiveLMI1Actor.LMIData>();
        private ConcurrentQueue<ReceiveLMI1Actor.LMIData> savedDataBuffer = new ConcurrentQueue<ReceiveLMI1Actor.LMIData>();
        private int countCurrent;
        private int countSaved;
        private int countAdvanced;
        private int progress;


        public IActorRef ViewModel { get; private set; }


        public LMIView()
        {
            InitializeComponent();
            IntializeCHARTControl();

            countCurrent = 0;
            countSaved = 0;
            countAdvanced = 0;

        }

        private void IntializeCHARTControl()
        {

        }

        public IActorRef GetViewModel(ActorSystem actorSystemRef)
        {
            var lmiVMProps = Props.Create(() => new LMIViewModel(dataBuffer));
            ViewModel = actorSystemRef.ActorOf(lmiVMProps);

            return ViewModel;
        }

        public void UpdateContentFromViewModel()
        {
            try
            {
                ReceiveLMI1Actor.LMIData lmiData = new ReceiveLMI1Actor.LMIData();

                // Auto Mode
                if (cB_saveData.Checked == true)
                {
                    while (dataBuffer.Count != 0)
                    {
                        dataBuffer.TryDequeue(out lmiData);
                        savedDataBuffer.Enqueue(lmiData);                       
                    }
                    countCurrent = 0;
                    if(dgv_current.Rows.Count > 1)
                    {
                        dgv_current.Rows.Clear();
                    }
                }
               
                // Add Current Grid View
                if (dataBuffer.TryPeek(out lmiData))
                {
                    if (countCurrent < dataBuffer.Count && cB_saveData.Checked == false)
                    {
                        addCurrentGridView(dataBuffer.Last());
                        countCurrent++;
                    }                   
                }
                
                // Add Saved Grid View
                if (savedDataBuffer.TryPeek(out lmiData))
                {
                    if (countSaved < savedDataBuffer.Count)
                    {
                        addSavedGridView(savedDataBuffer.ElementAt(countSaved));                      
                        countSaved++;
                    }
                }

                //Send to Advanced

                if (lmiAdvancedView1 != null)
                {
                    int toSend = 0;
                    while (countAdvanced + toSend < savedDataBuffer.Count)
                    {                       
                        LMIAdvancedViewModel.Tell(savedDataBuffer.ElementAt(toSend + countAdvanced));
                        toSend++;
                    }
                    countAdvanced += toSend;
                }

                if (cB_saveData.Checked == true)
                {
                    btn_saveData.Enabled = false;
                }else
                {
                    btn_saveData.Enabled = true;
                }

                if (lmiAdvancedView1 != null)
                {
                    lmiAdvancedView1.UpdateContentFromViewModel();
                }

                if (lmiAdvancedView1 != null)
                {
                    if (lmiAdvancedView1.IsDisposed == true)
                    {
                        lmiAdvancedView1 = null;
                        countAdvanced = 0;
                        btn_AdvViso.Enabled = true;
                    }
                }

                




            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(this.ToString() + '\n' + ex.Message);
            }
        }

        private void btn_Continue_Click(object sender, EventArgs e)
        {
            ViewModel.Tell(GeneratorCommand.ContinueGenerator);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ViewModel.Tell(GeneratorCommand.PauseGenerator);
        }

        private void btn_AdvViso_Click(object sender, EventArgs e)
        {
            startAdvancedView();
        }

        private void addCurrentGridView(ReceiveLMI1Actor.LMIData lmiData)
        {
            var measurement = lmiData.MyMeasurement;

            switch (measurement)
            {
                case ReceiveLMI1Actor.LMIData.Measurement.Daimler:

                    var newRow = new string[1000];
                    var index = 1;
                    foreach (var pin in lmiData.Data)
                    {
                        newRow[index] = pin.Value[0];
                        newRow[index + 1] = pin.Value[1];
                        newRow[index + 2] = pin.Value[2];

                        index = index + 3;
                    }
                    dgv_current.Rows.Add(newRow.ToArray());
                    break;
                default:
                    break;
            }
        }

        private void addSavedGridView(ReceiveLMI1Actor.LMIData lmiData)
        {
            var measurement = lmiData.MyMeasurement;

            switch (measurement)
            {
                case ReceiveLMI1Actor.LMIData.Measurement.Daimler:

                    var newRow = new string[1000];
                    var index = 1;
                    foreach (var pin in lmiData.Data)
                    {
                        newRow[index] = pin.Value[0];
                        newRow[index + 1] = pin.Value[1];
                        newRow[index + 2] = pin.Value[2];

                        index = index + 3;
                    }
                    dgv_savedValues.Rows.Add(newRow.ToArray());
                    break;
                default:
                    break;
            }
        }

        private void startAdvancedView ()
        {

            lmiAdvancedView1 = new LMIAdvancedView();
            var actorSystemRef = ActorSystem.Create("MySystem");           
            LMIAdvancedViewModel = lmiAdvancedView1.GetViewModel(actorSystemRef);
            lmiAdvancedView1.Show();
            btn_AdvViso.Enabled = false;
        }

        public static void InvokeIfRequired(Control c, Action<Control> action)
        {
            if (c.InvokeRequired)
            {
                c.Invoke(new Action(() => action(c)));
            }
            else
            {
                action(c);
            }
        }

        private void btn_saveData_Click(object sender, EventArgs e)
        {
            while(dataBuffer.Count != 0)
            {
                dataBuffer.TryDequeue(out var lmiData);
                savedDataBuffer.Enqueue(lmiData);                
            }
            countCurrent = 0;
            dgv_current.Rows.Clear();
        }
    }
}
