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
                        addCurrentGridView(dataBuffer.ElementAt(countCurrent));
                        countCurrent++;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(this.ToString() + '\n' + ex.Message);
            }
        }
        private void addCurrentGridView(ReceiveLMI1Actor.LMIData lmiData)
        {
            var measurement = lmiData.MyMeasurement;

            switch (measurement)
            {
                case ReceiveLMI1Actor.LMIData.Measurement.Test:

                    dgv_current.Rows.Add(new string[] { lmiData.dummyString });
                    break;
                default:
                    break;
            }
        }

        private void LMIAdvancedView_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

       
    }
}
