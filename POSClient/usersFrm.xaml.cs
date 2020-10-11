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
    /// Interaction logic for usersFrm.xaml
    /// </summary>
    public partial class usersFrm : Window
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        private readonly IBranchService _branchService;
        public usersFrm()
        {
            InitializeComponent();
            _roleService = new RoleService();
            _userService = new UserService();
            _branchService = new BranchService();
            background_pnl.Background = new LinearGradientBrush(
                Color.FromRgb(Theme.BackColor.R1, Theme.BackColor.G1, Theme.BackColor.B1),
                Color.FromRgb(Theme.BackColor.R2, Theme.BackColor.G2, Theme.BackColor.B2),
                190
                );
        }

        private bool CheckValidity()
        {
            bool isValid = true;

            if(string.IsNullOrWhiteSpace(txt_Name.Text))
            {
                isValid = false;
            }
            if(string.IsNullOrWhiteSpace(txt_username.Text))
            {
                isValid = false;
            }
            if(string.IsNullOrWhiteSpace(txt_password.Password))
            {
                isValid = false;
            }
            if(cmb_roles.SelectedIndex == -1)
            {
                isValid = false;
            }
            if (cmb_branches.SelectedIndex == -1)
            {
                isValid = false;
            }
            return isValid;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // loading all roles
            cmb_roles.ItemsSource = new ObservableCollection<Role>(_roleService.GetRoles());
            cmb_roles.DisplayMemberPath = "Name";
            cmb_roles.SelectedValuePath = "Id";
            cmb_roles.SelectedIndex = 0;

            // loading all branches
            cmb_branches.ItemsSource = new ObservableCollection<DAL.Models.Branch>(_branchService.GetAll());
            cmb_branches.DisplayMemberPath = "Name";
            cmb_branches.SelectedValuePath = "Id";
            cmb_branches.SelectedIndex = 0;

            // loading all users
            ObservableCollection<User> users = null;

            await Task.Run(() =>
            {
                users = new ObservableCollection<User>(_userService.GetUsers().Where(u => u.IsCustomer == false).ToList());
            });
            dgv_users.ItemsSource = users;
        }

        private async void btn_addUser_Click(object sender, RoutedEventArgs e)
        {
            bool modelValid = CheckValidity();

            if(modelValid)
            {
                var user = new User
                {
                    Name = txt_Name.Text,
                    Password = txt_password.Password,
                    RoleId = Convert.ToInt32(cmb_roles.SelectedValue),
                    Address = txt_address.Text,
                    Username = txt_username.Text,
                    BranchId = Convert.ToInt32(cmb_branches.SelectedValue),
                    IsCustomer = false
                };

                btn_addUser.IsEnabled = false;
                btn_addUser.Content = "Adding...";
                // add the user to the database
                await Task.Run(() =>
                {
                    _userService.AddUser(user);
                });

                btn_addUser.IsEnabled = true;
                btn_addUser.Content = "Add User";

                // loading all users
                ObservableCollection<User> users = null;

                await Task.Run(() =>
                {
                    users = new ObservableCollection<User>(_userService.GetUsers());
                });

                dgv_users.ItemsSource = users;
            }
        }
    }
}
