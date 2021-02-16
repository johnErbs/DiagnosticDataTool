using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MediaWareMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static LoggMonitor logM = new LoggMonitor();
        readonly static DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.Render);
        protected static bool MaximizedWindow { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            //XML mainwindow components and functionality. -->
            MaximizedWindow = true;
            MainLogger.DataContext = logM;
            InitializeXMLPages();
            StartMonitorFlow();
        }

        private void InitializeXMLPages()
        {
            MonitorWindow.Content = new Pages.MainMonitorPage();
            RightSidePanel.Content = new Pages.RightSidePannel();
            LeftSidePanel.Content = new Pages.LeftSidePanel();
        }

        private void StartMonitorFlow()
        {
            _timer.Tick += new EventHandler(MonitorHartbeat);
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Start();
        }

        private void MonitorHartbeat(object sender, EventArgs e)
        {
            LoggMonitor.DT = DateTime.Now;

            logM.Time = ($"Log time: {LoggMonitor.DT.Hour,0:D2}:{LoggMonitor.DT.Minute,0:D2}:{LoggMonitor.DT.Second,0:D2}");
            logM.Date = ($"Log date: {LoggMonitor.DT.Day}/{LoggMonitor.DT.Month}/{LoggMonitor.DT.Year}");
            _timer.Stop();
            _timer.Start();
        }

        private void CommandBinding_CanExecute_WindowControl(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed_Close(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void CommandBinding_Executed_Max(object sender, ExecutedRoutedEventArgs e)
        {
            if (!MaximizedWindow)
            {
                SystemCommands.MaximizeWindow(this);
                MaximizedWindow = true;
            }
            else
            {
                SystemCommands.RestoreWindow(this);
                MaximizedWindow = false;
            }
        }

        private void CommandBinding_Executed_Min(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }
    }
}
