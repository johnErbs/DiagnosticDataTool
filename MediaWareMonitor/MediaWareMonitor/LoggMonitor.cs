using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MediaWareMonitor
{
    class LoggMonitor : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static DateTime dt = new DateTime();
        public static DateTime DT
        {
            get { return dt; }
            set { dt = value; }
        }

        private string time;
        public string Time
        {
            get { return time; }
            set
            {
                time = value;
                NotifyPropertyChanged();
            }
        }

        private string date;
        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                NotifyPropertyChanged();
            }
        }

        private string hardwareInfo;
        public string HardwareInfo
        {
            get { return hardwareInfo; }
            set
            {
                hardwareInfo = value;
                NotifyPropertyChanged();
            }
        }

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
