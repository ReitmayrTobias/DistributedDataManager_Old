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

namespace DDM_Messwagen
{
    public partial class DDMView : UserControl,IActorViewModelCreator
    {
        private ConcurrentQueue<DDMViewModel.ViewData> dataBuffer = new ConcurrentQueue<DDMViewModel.ViewData>();



        public IActorRef ViewModel { get; private set; }


        public DDMView()
        {
            InitializeComponent();
            IntializeCHARTControl();
        }

        private void IntializeCHARTControl()
        {
            



        }

        public IActorRef GetViewModel(ActorSystem actorSystemRef)
        {
            var ddmVMProps = Props.Create(() => new DDMViewModel(dataBuffer));
            ViewModel = actorSystemRef.ActorOf(ddmVMProps);

            return ViewModel;
        }

        public void UpdateContentFromViewModel()
        {
            try
            {

                DDMViewModel.ViewData act, last = null;
                while (dataBuffer.TryDequeue(out act))
                {
                    if(act.WenglorState != DDMViewModel.ViewData.State.Unknown)
                    {
                        if(act.WenglorState == DDMViewModel.ViewData.State.Running)
                        {
                            lb_WenglorState.Text = "Running ...";
                            lb_WenglorState.ForeColor = Color.Green;
                        }
                        if (act.WenglorState == DDMViewModel.ViewData.State.Paused)
                        {
                            lb_WenglorState.Text = "Paused ...";
                            lb_WenglorState.ForeColor = Color.Orange;
                        }
                        if (act.WenglorState == DDMViewModel.ViewData.State.Stopped)
                        {
                            lb_WenglorState.Text = "Stopped ...";
                            lb_WenglorState.ForeColor = Color.Black;
                        }                     
                    }
                    if (act.LmiState != DDMViewModel.ViewData.State.Unknown)
                    {
                        if (act.LmiState == DDMViewModel.ViewData.State.Running)
                        {
                            lb_LMIState.Text = "Running ...";
                            lb_LMIState.ForeColor = Color.Green;
                        }
                        if (act.LmiState == DDMViewModel.ViewData.State.Paused)
                        {
                            lb_LMIState.Text = "Paused ...";
                            lb_LMIState.ForeColor = Color.Orange;
                        }
                        if (act.LmiState == DDMViewModel.ViewData.State.Stopped)
                        {
                            lb_LMIState.Text = "Stopped ...";
                            lb_LMIState.ForeColor = Color.Black;
                        }
                    }

                }
            }
            catch (Exception ex)
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
