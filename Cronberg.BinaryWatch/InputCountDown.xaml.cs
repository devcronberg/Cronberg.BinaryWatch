using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cronberg.BinaryWatch
{
    /// <summary>
    /// Interaction logic for InputCountDown.xaml
    /// </summary>
    public partial class InputCountDown : Window
    {
        public int CountToMinutes { get; set; }
        public TimeSpan CountToTime { get; set; }
        public InputCountDown()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(txtMinutes.Text !="")
                    CountToMinutes = Convert.ToInt32(txtMinutes.Text);
                if(txtCountTo.Text!="")
                    CountToTime = new TimeSpan(Convert.ToInt32(txtCountTo.Text.Split(":")[0]), Convert.ToInt32(txtCountTo.Text.Split(":")[1]),0);
            }
            catch (Exception)
            {

                throw;
            }
            this.Close();
        }
    }
}
