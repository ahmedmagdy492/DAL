using BLL.Services;
using SharedLib;
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
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        private readonly IAuthService _authService;
        public Splash()
        {
            InitializeComponent();
            _authService = new AuthService(new Sha256Hashing());
            background_pnl.Background = new LinearGradientBrush(
                Color.FromRgb(Theme.BackColor.R1, Theme.BackColor.G1, Theme.BackColor.B1),
                Color.FromRgb(Theme.BackColor.R2, Theme.BackColor.G2, Theme.BackColor.B2),
                190
                );
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // determining which form start first
            var isFirstTime = await Task.Run(() =>
            {
                return _authService.IsFirstTime();
            });

            this.Hide();
            if(isFirstTime)
            {
                FirstTime firstTimeFrm = new FirstTime();
                firstTimeFrm.Show();
            }
            else
            {
                MainWindow mainWindowFrm = new MainWindow();
                mainWindowFrm.Show();
            }
        }
    }
}
