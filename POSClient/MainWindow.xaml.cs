using BLL.Services;
using SharedLib;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace POSClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IAuthService _authService;
        private readonly IRoleService _roleService;

        public MainWindow()
        {
            InitializeComponent();
            _authService = new AuthService(new Sha256Hashing());
            _roleService = new RoleService();

            background_pnl.Background = new LinearGradientBrush(
                Color.FromRgb(Theme.BackColor.R1, Theme.BackColor.G1, Theme.BackColor.B1),
                Color.FromRgb(Theme.BackColor.R2, Theme.BackColor.G2, Theme.BackColor.B2),
                190
                );
        }

        private bool CheckValidity()
        {
            bool isValid = true;

            if(string.IsNullOrEmpty(txt_username.Text))
            {
                isValid = false;
            }
            if(string.IsNullOrEmpty(txt_password.Password))
            {
                isValid = false;
            }

            return isValid;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void DimForm()
        {
            btn_login.IsEnabled = false;
            btn_login.Foreground = new SolidColorBrush(Color.FromRgb(0, 178, 66));
            btn_login.Content = "Loging in...";
            err_msg.Visibility = Visibility.Hidden;
        }

        private void UnDimForm()
        {
            btn_login.IsEnabled = true;
            btn_login.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            btn_login.Content = "Login";
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            DimForm();

            bool isModelValid = CheckValidity();

            if(isModelValid)
            {
                var user = await _authService.Login(txt_username.Text, txt_password.Password);

                await Task.Run(() =>
                {
                    Thread.Sleep(2000);
                });
                if(user == null)
                {
                    err_msg.Visibility = Visibility.Visible;
                }
                else
                {
                    // user is logged in successfully
                    this.Hide();

                    // checking the user role
                    var isExist = _roleService.IsUserExistInRole("Admin", user.Id);

                    if(isExist)
                    {
                        // if user is admin
                        AdminPanel adminPanel = new AdminPanel(user.Username);
                        adminPanel.Show();
                    }
                    else
                    {
                        // normal user
                    }
                }
                UnDimForm();
            }
            else
            {
                MessageBox.Show("Error: Invalid Data was provided");
                UnDimForm();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // closing the first window
            Application.Current.Windows[0].Close();
        }
    }
}
