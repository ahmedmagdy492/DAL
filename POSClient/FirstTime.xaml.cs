using BLL.Services;
using DAL.Models;
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
    /// Interaction logic for FirstTime.xaml
    /// </summary>
    public partial class FirstTime : Window
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public FirstTime()
        {
            InitializeComponent();
            this._roleService = new RoleService();
            _userService = new UserService();
        }

        private bool CheckValidity()
        {
            bool isValid = true;

            if(string.IsNullOrWhiteSpace(txt_username.Text))
            {
                isValid = false;
            }
            if(string.IsNullOrWhiteSpace(txt_password.Password))
            {
                isValid = false;
            }
            
            return isValid;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // checking the validity
            bool modelValid = CheckValidity();

            if(modelValid)
            {
                btn_Submit.IsEnabled = false;
                btn_Submit.Foreground = new SolidColorBrush(Colors.Black);
                btn_Submit.Content = "Submitting...";

                var role = new Role { Name = "Admin" };
                _roleService.AddRole(role);

                var createdRole = _roleService.GetRoleByName("Admin");

                var user = new User
                {
                    Name = txt_username.Text,
                    Username = txt_username.Text,
                    Password = txt_password.Password,
                    RoleId = createdRole.Id
                };

                await Task.Run(() =>
                {
                    // adding a new user
                    _userService.AddUser(user);
                });

                btn_Submit.IsEnabled = true;
                btn_Submit.Foreground = new SolidColorBrush(Colors.White);
                btn_Submit.Content = "Submit";

                // showing the admin panel form
                AdminPanel adminPanel = new AdminPanel(user.Username);
                adminPanel.Show();
                this.Hide();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // closing the first window
            Application.Current.Windows[0].Close();
        }
    }
}
