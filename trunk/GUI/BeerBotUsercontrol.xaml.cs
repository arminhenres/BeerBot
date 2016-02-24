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

namespace GUI
{
    /// <summary>
    /// Interaktionslogik für BeerBotUsercontrol.xaml
    /// </summary>
    public partial class BeerBotUsercontrol : UserControl
    {
        public BeerBotUsercontrol()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button1.Background = Brushes.YellowGreen;
            Start.Visibility = Visibility.Visible;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            ProgressBar.Visibility = Visibility.Visible;
           
            
        }

        private void NotButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
