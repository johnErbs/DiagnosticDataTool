using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;

namespace MediaWareMonitor
{
    class Chart : INotifyPropertyChanged
    {
        private static ChartValues<double> chartValue = new ChartValues<double>();
        public static ChartValues<double> ChartValue
        {
            get { return chartValue; }
            set { chartValue = value; }
        }

        private SeriesCollection chartvalues = new SeriesCollection();
        public SeriesCollection Chartvalues
        {
            get { return chartvalues; }
            set { chartvalues = value; NotifyPropertyChanged(); }
        }
        private float maxVal;
        public float MaxValue
        {
            get { return maxVal; }
            set { maxVal = value; NotifyPropertyChanged(); }
        }
        public static bool ClearChart { get; set; }
        public static bool EnableLineChart { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
