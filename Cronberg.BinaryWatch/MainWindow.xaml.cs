using System;
using System.Windows;
using System.Windows.Input;

// icon from https://gauger.io/fonticon/

namespace Cronberg.BinaryWatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Options option = new Options();

        public MainWindow()
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.SingleBorderWindow;

            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.WindowState = WindowState.Normal;
            this.Title = "Cronbergs BinaryWatch";
            ShowValue();

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += (s, e) =>
            {
                ShowValue();
            };
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        public void ShowValue()
        {
            string h = "";
            string m = "";
            string s = "";
            if (!option.ShowHex)
            {
                h = Convert.ToString(DateTime.Now.Hour, 2).ToString().PadLeft(8, '0');
                m = Convert.ToString(DateTime.Now.Minute, 2).ToString().PadLeft(8, '0');
                s = Convert.ToString(DateTime.Now.Second, 2).ToString().PadLeft(8, '0');
            }
            else
            {
                h = Convert.ToString(DateTime.Now.Hour, 16).ToString().PadLeft(2, '0');
                m = Convert.ToString(DateTime.Now.Minute, 16).ToString().PadLeft(2, '0');
                s = Convert.ToString(DateTime.Now.Second, 16).ToString().PadLeft(2, '0');
            }
            if (option.ShowSeconds)
                this.txt.Text = $"{h}:{m}:{s}".ToUpper();
            else
                this.txt.Text = ($"{h}:{m}").ToUpper();

            if (!option.ShowHex)
            {
                
                this.Title = "Cronbergs BinaryWatch";
                if (option.ShowSeconds)
                {
                    this.Width = 750;
                }
                else
                {
                    this.Width = 500;
                }
            }
            else
            {
                this.Title = "Cronbergs HexWatch";
                if (option.ShowSeconds)
                {
                    this.Width = 250;
                }
                else
                {
                    this.Width = 200;
                }
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                myMenu.Visibility = Visibility.Visible;
            }
        }


        private void menuExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void menuShowSeconds(object sender, RoutedEventArgs e)
        {
            option.ShowSeconds = !option.ShowSeconds;
            myMenu.Visibility = Visibility.Collapsed;
            ShowValue();
        }
        private void menuShowHex(object sender, RoutedEventArgs e)
        {
            option.ShowHex = !option.ShowHex;
            myMenu.Visibility = Visibility.Collapsed;
            ShowValue();
        }

    }

    class Options
    {
        public bool ShowSeconds { get; set; }
        public bool ShowHex { get; set; }
    }
}
