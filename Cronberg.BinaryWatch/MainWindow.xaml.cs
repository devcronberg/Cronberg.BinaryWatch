using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cronberg.BinaryWatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.SingleBorderWindow;
            
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.WindowState = WindowState.Normal;
            this.Title = "Cronbergs BinaryWatch";
            SetBinaryValue();

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += (s, e) => {
                SetBinaryValue();
            };
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        public void SetBinaryValue() {
            string h = Convert.ToString(DateTime.Now.Hour, 2).ToString().PadLeft(8, '0');
            string m = Convert.ToString(DateTime.Now.Minute, 2).ToString().PadLeft(8, '0');
            string s = Convert.ToString(DateTime.Now.Second, 2).ToString().PadLeft(8, '0');
            this.txt.Text = $"{h}:{m}:{s}";
        }
    }
}
