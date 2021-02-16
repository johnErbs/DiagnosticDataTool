using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace MediaWareMonitor.Pages
{
    /// <summary>
    /// Interaction logic for LineChartWindow.xaml
    /// </summary>
    public partial class LineChartWindow : Page
    {
        private static Chart chart = new Chart();
        private static LoggMonitor log = new LoggMonitor();
        private static LineSeries lineSerie = new LineSeries();
        private static Stack<double> chartPoints = new Stack<double>();
        private static PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private static PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        readonly static DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.Render);

        public LineChartWindow()
        {
            InitializeComponent();
            chart.MaxValue = 100;
            this.data_Chart.DataContext = chart;
            this.txtCpuCounts.DataContext = log;
            BootstrapTimer();
        }

        public void BootstrapTimer()
        {
            _timer.Tick += (RunDataFlow);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            _timer.Start();
        }

        private void RunDataFlow(object sender, EventArgs e)
        {
            if (Chart.EnableLineChart)
            {
                if (Hardware.Cpu)
                {
                    chart.MaxValue = 100;
                    if (Chart.ClearChart)
                    {
                        Chart.ChartValue.Clear();
                        Chart.ClearChart = false;
                    }
                    float cpuCount = cpuCounter.NextValue();
                    log.HardwareInfo = ($"Cpu performance in use: {cpuCount,0:000.000} %");
                    chartPoints.Push(cpuCount);
                    Chart.ChartValue.Add(cpuCount);
                }
                if (Hardware.Memory)
                {
                    chart.MaxValue = 8;
                    if (Chart.ClearChart)
                    {
                        Chart.ChartValue.Clear();
                        Chart.ClearChart = false;
                    }
                    float ramCount = ramCounter.NextValue() / 1000;
                    log.HardwareInfo = ($"Available ram: {ramCount,0:000.000} MB");
                    chartPoints.Push(ramCount);
                    Chart.ChartValue.Add(ramCount);
                }
                if (Hardware.Tempprature)
                {
                    chart.MaxValue = 100;
                    if (Chart.ClearChart)
                    {
                        Chart.ChartValue.Clear();
                        Chart.ClearChart = false;
                    }
                    float tempratureCount = 0;
                    log.HardwareInfo = ($"System temprature: {tempratureCount,0:000.000} °");
                    chartPoints.Push(tempratureCount);
                    Chart.ChartValue.Add(tempratureCount);
                }



                lineSerie.Values = Chart.ChartValue;
                chart.Chartvalues.Add(lineSerie);

                if (lineSerie.Values.Count > 5)
                {
                    Chart.ChartValue.Clear();

                    for (int i = 0; i < chartPoints.Count; i++)
                    {
                        Chart.ChartValue.Add(chartPoints.Pop());
                    }
                    _timer.Stop();
                    _timer.Start();
                }
            }
            else
            {
                Chart.ChartValue.Clear();
                data_Chart.IsEnabled = false;
                _timer.Stop();

            }
        }
    }
}
