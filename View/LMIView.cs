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

namespace DDM_Messwagen
{
    public partial class LMIView : UserControl, IActorViewModelCreator
    {
        private ConcurrentQueue<ReceiveLMI1Actor.LMIData> dataBuffer = new ConcurrentQueue<ReceiveLMI1Actor.LMIData>();
        private int progress;


        public IActorRef ViewModel { get; private set; }


        public LMIView()
        {
            InitializeComponent();
            IntializeCHARTControl();
        }

        private void IntializeCHARTControl()
        {
            progress = 0;
            chartPie.Titles.Add("Progress");
            chartPie.Series["Series1"].Points.AddXY("Measured", "0");
            chartPie.Series["Series1"].Points.AddXY("Not Measured", "100");



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

                ReceiveLMI1Actor.LMIData act, last = null;
                while (dataBuffer.TryDequeue(out act))
                {                  
                    chartPie.Series["Series1"].Points.Clear();
                    txb_Pin1.Text = act.DummyData[0].ToString();
                    txb_Pin2.Text = act.DummyData[1].ToString();
                    txb_Pin3.Text = act.DummyData[2].ToString();
                    txb_Pin4.Text = act.DummyData[3].ToString();
                    progress += 5;
                    chartPie.Series["Series1"].Points.AddXY("Measured", progress.ToString());
                    chartPie.Series["Series1"].Points.AddXY("Not Measured", (100 - progress).ToString());
                    chartPie.Series["Series1"].Points[0].Color = Color.LawnGreen;
                    chartPie.Series["Series1"].Points[1].Color = Color.LightGray;

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
    }
}
