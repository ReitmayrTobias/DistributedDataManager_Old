using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;
using DDM_Messwagen.Actors;
using Akka.Actor;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Forms.DataVisualization.Charting;

namespace DDM_Messwagen
{
    public partial class BaumerView : UserControl, IActorViewModelCreator
    {

        private ConcurrentQueue<ReceiveBaumerActor.BaumerData> dataBuffer = new ConcurrentQueue<ReceiveBaumerActor.BaumerData>();                                                                                                    
        
        

        public IActorRef ViewModel { get; private set; }


        public BaumerView()
        {
            InitializeComponent();
            IntializeCHARTControl();
        }

        private void IntializeCHARTControl()
        {           
            
        }

        public IActorRef GetViewModel(ActorSystem actorSystemRef)
        {
            var baumerVMProps = Props.Create(() => new BaumerViewModel(dataBuffer));
            ViewModel = actorSystemRef.ActorOf(baumerVMProps);

            return ViewModel;
        }

        public void UpdateContentFromViewModel()
        {
            try
            {

                ReceiveBaumerActor.BaumerData act, last = null;
                while(dataBuffer.TryDequeue(out act))
                {
                    
                }
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(this.ToString() + '\n' + ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ViewModel.Tell(GeneratorCommand.PauseGenerator);
        }

        private void btn_Continue_Click(object sender, EventArgs e)
        {
            ViewModel.Tell(GeneratorCommand.ContinueGenerator);
        }
    }
}
