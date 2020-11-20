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
        DateTime countDownTo;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStyle = WindowStyle.SingleBorderWindow;

            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.WindowState = WindowState.Normal;
            this.Title = "Cronbergs BinaryWatch";

            ShowTimeValue();

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += (s, e) =>
            {
                ShowTimeValue();
            };
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            myMenu.Visibility = Visibility.Visible;
        }

        public void ShowTimeValue()
        {
            string h = "";
            string m = "";
            string s = "";
            if (!option.ShowHex)
            {
                if (option.CountDown)
                {
                    if (countDownTo > DateTime.Now)
                    {
                        h = Convert.ToString((countDownTo - DateTime.Now).Hours, 2).PadLeft(8, '0');
                        m = Convert.ToString((countDownTo - DateTime.Now).Minutes, 2).PadLeft(8, '0');
                        s = Convert.ToString((countDownTo - DateTime.Now).Seconds, 2).PadLeft(8, '0');
                    }
                    else
                    {
                        h = "00000000";
                        m = "00000000";
                        s = "00000000";
                    }
                }
                else
                {
                    h = Convert.ToString(DateTime.Now.Hour, 2).ToString().PadLeft(8, '0');
                    m = Convert.ToString(DateTime.Now.Minute, 2).ToString().PadLeft(8, '0');
                    s = Convert.ToString(DateTime.Now.Second, 2).ToString().PadLeft(8, '0');
                }
            }
            else
            {
                if (option.CountDown)
                {
                    if (countDownTo > DateTime.Now)
                    {

                        h = Convert.ToString((countDownTo - DateTime.Now).Hours, 16).PadLeft(2, '0');
                        m = Convert.ToString((countDownTo - DateTime.Now).Minutes, 16).PadLeft(2, '0');
                        s = Convert.ToString((countDownTo - DateTime.Now).Seconds, 16).PadLeft(2, '0');
                    }
                    else
                    {
                        h = "00";
                        m = "00";
                        s = "00";
                    }

                }
                else
                {
                    h = Convert.ToString(DateTime.Now.Hour, 16).ToString().PadLeft(2, '0');
                    m = Convert.ToString(DateTime.Now.Minute, 16).ToString().PadLeft(2, '0');
                    s = Convert.ToString(DateTime.Now.Second, 16).ToString().PadLeft(2, '0');
                }
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
            this.Topmost = option.TopMost;
            if (!option.ShowTitle)
            {
                this.WindowStyle = WindowStyle.SingleBorderWindow;
            }
            else
            {
                this.WindowStyle = WindowStyle.None;
            }

        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.System && (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt)
            {
                myMenu.Visibility = Visibility.Visible;
            }
        }


        private void menuCountDown(object sender, RoutedEventArgs e)
        {
            option.CountWatch = false;
            option.CountDown = true;
            var s = this.Topmost;
            InputCountDown i = new InputCountDown();
            option.TopMost = false;
            i.Topmost = true;
            i.ShowDialog();
            option.TopMost = s;
            if (i.CountToMinutes > 0)
                countDownTo = DateTime.Now.AddMinutes(i.CountToMinutes + 1);
            if (i.CountToTime != new TimeSpan(0, 0, 0))
                countDownTo = DateTime.Now.Date + i.CountToTime;

            myMenu.Visibility = Visibility.Collapsed;
        }

        private void menuShowTitle(object sender, RoutedEventArgs e)
        {
            option.ShowTitle = !option.ShowTitle;
            myMenu.Visibility = Visibility.Collapsed;
        }

        private void menuExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void menuWatch(object sender, RoutedEventArgs e)
        {
            option.CountWatch = true;
            option.CountDown = false;
            this.Close();
        }
        private void menuShowSeconds(object sender, RoutedEventArgs e)
        {
            option.ShowSeconds = !option.ShowSeconds;
            myMenu.Visibility = Visibility.Collapsed;
            ShowTimeValue();
        }
        private void menuShowHex(object sender, RoutedEventArgs e)
        {
            option.ShowHex = !option.ShowHex;
            myMenu.Visibility = Visibility.Collapsed;
            ShowTimeValue();
        }

        private void menuTopMost(object sender, RoutedEventArgs e)
        {
            option.TopMost = !option.TopMost;
            myMenu.Visibility = Visibility.Collapsed;
            ShowTimeValue();
        }

    }

    class Options
    {
        public bool ShowSeconds { get; set; }
        public bool ShowHex { get; set; }
        public bool TopMost { get; set; } = true;
        public bool CountDown { get; set; }
        public bool CountWatch { get; set; } = true;
        public bool ShowTitle { get; set; }
    }
}
