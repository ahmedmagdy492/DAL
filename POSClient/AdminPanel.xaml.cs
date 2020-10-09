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
using System.Windows.Shapes;

namespace POSClient
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        private string userName;
        public AdminPanel(string userName)
        {
            InitializeComponent();
            this.userName = userName;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lbl_username.Text = userName;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(menu_items.Visibility == Visibility.Hidden)
            {
                menu_items.Visibility = Visibility.Visible;
            }
            else
            {
                menu_items.Visibility = Visibility.Hidden;
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Application.Current.Windows[1].Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
