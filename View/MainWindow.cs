using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Akka.Actor;
using DDM_Messwagen.Actors;

namespace DDM_Messwagen
{
    public partial class MainWindow : Form
    {

        public ActorSystem ActorSystemRef { get; set; }
        private IActorRef ReceiveBaumer;
        private IActorRef ReceiveLMI1;
        


        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            ActorSystemRef = ActorSystem.Create("MySystem");

            //// Temp
            //lmiView1.Visible = false;
            //.Visible = false;

            // Create View Models
            var baumerViewModel = baumerView2.GetViewModel(ActorSystemRef);
            var lmiViewModel = lmiView1.GetViewModel(ActorSystemRef);
            var ddmViewModel = ddmView1.GetViewModel(ActorSystemRef);


            // Create Actors
            var processBaumerProps = Props.Create(() => new ProcessDataBaumerActor(new List<IActorRef>() { baumerViewModel }));
            var processBaumer = ActorSystemRef.ActorOf(processBaumerProps);

            var receiveBaumerProps = Props.Create(() => new ReceiveBaumerActor(new List<IActorRef>() { processBaumer, ddmViewModel }));
            ReceiveBaumer = ActorSystemRef.ActorOf(receiveBaumerProps);

            var receiveLMIProps = Props.Create(() => new ReceiveLMI1Actor(new List<IActorRef>() { lmiViewModel, ddmViewModel }));
            ReceiveLMI1 = ActorSystemRef.ActorOf(receiveLMIProps);

            // Connect ViewModel sender part
            baumerViewModel.Tell(SubscriptionCommand.AddSubscriber.WithData(ReceiveBaumer));
            baumerViewModel.Tell(SubscriptionCommand.AddSubscriber.WithData(ddmViewModel));
            lmiViewModel.Tell(SubscriptionCommand.AddSubscriber.WithData(ReceiveLMI1));
            lmiViewModel.Tell(SubscriptionCommand.AddSubscriber.WithData(ddmViewModel));



            Task.Run(() =>
            {
                UpdateWithResult();
            });
        }

        private void UpdateWithResult()
        {
            while(true)
            {
                try
                {
                    InvokeIfRequired(this, form =>
                    {
                        baumerView2.UpdateContentFromViewModel();
                        lmiView1.UpdateContentFromViewModel();
                        ddmView1.UpdateContentFromViewModel();
                    });
                }catch (Exception ex)
                {
                    
                }
            }
        }

        public static void InvokeIfRequired(Control c, Action<Control> action)
        {
            if(c.InvokeRequired)
            {
                c.Invoke(new Action(() => action(c)));
            }else
            {
                action(c);
            }
        }

        private void statorvermessungLMIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiveLMI1.Tell(GeneratorCommand.StartGenerator);
        }

        private void baumerKameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiveBaumer.Tell(GeneratorCommand.StartGenerator);
        }
    }
}
