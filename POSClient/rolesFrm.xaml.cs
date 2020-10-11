using BLL.Services;
using DAL.Models;
using SharedLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for rolesFrm.xaml
    /// </summary>
    public partial class rolesFrm : Window
    {
        private readonly IRoleService _roleService;
        public rolesFrm()
        {
            InitializeComponent();
            _roleService = new RoleService();
        }

        private bool CheckValidity()
        {
            bool isValid = true;

            if(string.IsNullOrWhiteSpace(txt_roleName.Text))
            {
                isValid = false;
            }
            return isValid;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // loading all roles
            dgv_roles.ItemsSource = new ObservableCollection<Role>(_roleService.GetRoles());
        }

        private async void btn_addRole_Click(object sender, RoutedEventArgs e)
        {
            bool modelValid = CheckValidity();

            if(modelValid)
            {
                // adding the role to the database
                btn_addRole.IsEnabled = false;
                btn_addRole.Content = "Adding...";

                var role = new Role 
                {
                    Name = txt_roleName.Text
                };

                await Task.Run(() =>
                {
                    _roleService.AddRole(role);
                });

                btn_addRole.IsEnabled = true;
                btn_addRole.Content = "Add";

                // getting all roles
                ObservableCollection<Role> roles = null;

                await Task.Run(() =>
                {
                    roles = new ObservableCollection<Role>(_roleService.GetRoles());
                });

                dgv_roles.ItemsSource = roles;
            }
        }
    }
}
