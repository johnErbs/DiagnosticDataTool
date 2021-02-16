using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaWareMonitor.Pages
{
    /// <summary>
    /// Interaction logic for LeftSidePanel.xaml
    /// </summary>
    public partial class LeftSidePanel : Page
    {
        
        public LeftSidePanel()
        {
            InitializeComponent();
            Hardware.Cpu = false;
            Hardware.Memory = false;
            Hardware.Tempprature = false;
            Chart.EnableLineChart = true;
            
        }

        private void btn_Click_Ram(object sender, RoutedEventArgs e)
        {
            Chart.ClearChart = true;
            Chart.EnableLineChart = true;
            Hardware.Cpu = false;
            Hardware.Tempprature = false;
            Hardware.Memory = true;
        }

        private void btn_Click_Cpu(object sender, RoutedEventArgs e)
        {
            Chart.ClearChart = true;
            Chart.EnableLineChart = true;
            Hardware.Memory = false;
            Hardware.Tempprature = false;
            Hardware.Cpu = true;
        }
        private void btn_Click_CpuTemprature(object sender, RoutedEventArgs e)
        {
            Chart.ClearChart = true;
            Chart.EnableLineChart = true;
            Hardware.Memory = false;
            Hardware.Cpu = false;
            Hardware.Tempprature = true;
        }
    }
}
