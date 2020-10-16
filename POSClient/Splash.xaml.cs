using BLL.Services;
using SharedLib;
using System;
using System.Collections.Generic;
using System.IO;
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
            // creating directories if are not exist
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string foldername = System.IO.Path.Combine(path, "MigoSystems");
            if (!Directory.Exists(foldername))
            {
                Directory.CreateDirectory(foldername);
            }

            foldername = System.IO.Path.Combine(path, "MigoSystems", "POSFiles");
            if (!Directory.Exists(foldername))
            {
                Directory.CreateDirectory(foldername);
            }

            // determining which form start first
            bool isFirstTime = false;
            try
            {
                isFirstTime = await Task.Run(() =>
                {
                    return _authService.IsFirstTime();
                });
            }
            catch (Exception)
            {
                MessageBox.Show("Database server is currently offline", "An Error has occurred", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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
