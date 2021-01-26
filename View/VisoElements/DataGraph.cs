using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;

namespace DistributedDataManager.View.VisoElements
{
    public partial class DataGraph : UserControl
    {
        private Dictionary<string, string> data;
        private string name;



        public DataGraph()
        {
            InitializeComponent();

            //Chart Init
            chartCont.Series["Series1"].Points.Clear();
            chartCont.Series["Series1"].Color = Color.IndianRed;
            chartCont.Series["Series1"].BorderWidth = 5;

            chartCont.ChartAreas[0].AxisY.Maximum = 10;
            chartCont.ChartAreas[0].AxisY.Minimum = 0;
            chartCont.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chartCont.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chartCont.ChartAreas[0].AxisY.MajorGrid.Enabled = true;

            //Data Init
            data = new Dictionary<string, string>();


        }

        public void UpdateData()
        {
            
            foreach(var pin in data)
            {
                chartCont.Series["Series1"].Points.Add(Convert.ToDouble(pin.Key), Convert.ToDouble(pin.Value));
            }         
        }

        private string Name
        {
            get => name;
            set => name = value;
        }


    }
}
