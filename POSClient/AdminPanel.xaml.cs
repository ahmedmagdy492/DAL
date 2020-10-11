using SharedLib;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            background_pnl.Background = new LinearGradientBrush(
                Color.FromRgb(Theme.BackColor.R1, Theme.BackColor.G1, Theme.BackColor.B1),
                Color.FromRgb(Theme.BackColor.R2, Theme.BackColor.G2, Theme.BackColor.B2),
                190
                );
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
            this.Close();
            Application.Current.Windows[1].Show();
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Application.Current.Windows[1].Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Windows[1].Show();
        }

        private void btn_products_MouseEnter(object sender, MouseEventArgs e)
        {
            ((Grid)sender).Background = new SolidColorBrush(Color.FromRgb(128, 203, 196));
        }

        private void btn_products_MouseLeave(object sender, MouseEventArgs e)
        {
            ((Grid)sender).Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OrdersSettings orderFrm = new OrdersSettings();
            orderFrm.ShowDialog();
        }

        private void btn_users_MouseDown(object sender, MouseButtonEventArgs e)
        {
            usersFrm usersFrm = new usersFrm();
            usersFrm.ShowDialog();
        }

        private void btn_roles_MouseDown(object sender, MouseButtonEventArgs e)
        {
            rolesFrm rolesFrm = new rolesFrm();
            rolesFrm.ShowDialog();
        }

        private void btn_products_MouseDown(object sender, MouseButtonEventArgs e)
        {
            productsFrm productsFrm = new productsFrm();
            productsFrm.ShowDialog();
        }
    }
}
