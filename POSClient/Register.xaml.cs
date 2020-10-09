using BLL.Services;
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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private readonly IRoleService _roleService;
        public Register()
        {
            InitializeComponent();
            _roleService = new RoleService();
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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // loading all roles
            var allRoles = await Task.Run(() =>
            {
                return _roleService.GetRoles();
            });

            cmb_roles.DataContext = allRoles;
        }

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            bool modelValid = CheckValidity();

            if(modelValid)
            {
                // register the user and add it to the database
            }
        }

    }
}
