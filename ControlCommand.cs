using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDM_Messwagen
{
    public class ControlCommand<T> where T : ControlCommand<T>, new()
    {

        private string name = "Unnamed";
        public object Data { get; set;  }

        public ControlCommand()
        {

        }
        public ControlCommand(string name)
        {
            this.name = name;
        }
        public T WithData(object data)
        {
            return new T() { name = this.ToString(), Data = data };
        }
        public override string ToString()
        {
            return name;
        }

        //Override equality operator and all accociated methods
        public static bool operator ==(ControlCommand<T> cc1, ControlCommand<T> cc2)
        {
            return cc1.ToString().Equals(cc2.ToString());
        }
        public static bool operator !=(ControlCommand<T> cc1, ControlCommand<T> cc2)
        {
            return !cc1.ToString().Equals(cc2.ToString());
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class SubscriptionCommand : ControlCommand<SubscriptionCommand>
    {
        public SubscriptionCommand()
        {

        }
        protected SubscriptionCommand(string name) : base(name) { }

        //Dynamic subscription Commands
        public static SubscriptionCommand AddSubscriber = new SubscriptionCommand("Subscribe");
        public static SubscriptionCommand RemoveSubscriber = new SubscriptionCommand("UnSubscribe");
    }

    public class GeneratorCommand : ControlCommand<GeneratorCommand>
    {
        public GeneratorCommand()
        {

        }
        protected GeneratorCommand(string name) : base(name) { }

        //Generator Commands
        public static GeneratorCommand StartGenerator = new GeneratorCommand("StartGenerator");
        public static GeneratorCommand StopGenerator = new GeneratorCommand("StopGenerator");
        public static GeneratorCommand PauseGenerator = new GeneratorCommand("PauseGenerator");
        public static GeneratorCommand ContinueGenerator = new GeneratorCommand("ContinueGenerator");
    }
}
