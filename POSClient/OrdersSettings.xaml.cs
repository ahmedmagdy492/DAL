using BLL.Services;
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
    /// Interaction logic for OrdersSettings.xaml
    /// </summary>
    public partial class OrdersSettings : Window
    {
        private readonly IBranchService _branchService;
        public OrdersSettings()
        {
            InitializeComponent();
            background_pnl.Background = new LinearGradientBrush(
                Colors.White,
                Colors.White,
                130
                );
            _branchService = new BranchService();
        }

        private void onlineOrder_settings_Checked(object sender, RoutedEventArgs e)
        {
            // checking if any branch is selected
            if (cmb_branches.SelectedValue != null)
            {
                // changing the online ordering settings
                _branchService.ChangeOnlineStatus(Convert.ToInt32(cmb_branches.SelectedValue), onlineOrder_chck.IsChecked.Value);
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // getting all branches
            ObservableCollection<DAL.Models.Branch> allBranches = null;
            await Task.Run(() =>
            {
                allBranches = new ObservableCollection<DAL.Models.Branch>(_branchService.GetAll());
            });

            cmb_branches.ItemsSource = allBranches;
            cmb_branches.DisplayMemberPath = "Name";
            cmb_branches.SelectedValuePath = "Id";
            cmb_branches.SelectedIndex = 0;
        }

        private void btn_AddBranch_Click(object sender, RoutedEventArgs e)
        {
            Branch branchFrm = new Branch();
            branchFrm.ShowDialog();

            // getting all branches
            cmb_branches.ItemsSource = new ObservableCollection<DAL.Models.Branch>(_branchService.GetAll());
            cmb_branches.DisplayMemberPath = "Name";
            cmb_branches.SelectedValuePath = "Id";
            cmb_branches.SelectedIndex = 0;
        }

        private void btn_TrackOrders_Click(object sender, RoutedEventArgs e)
        {
            // show the tracking orders form
        }

        private void cmb_branches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var branchId = Convert.ToInt32(cmb_branches.SelectedValue);
            var branch = _branchService.GetById(branchId);

            onlineOrder_chck.IsChecked = branch.IsOnline;
        }

        private void onlineOrder_chck_Unchecked(object sender, RoutedEventArgs e)
        {
            // changing the online ordering settings
            _branchService.ChangeOnlineStatus(Convert.ToInt32(cmb_branches.SelectedValue), onlineOrder_chck.IsChecked.Value);
        }
    }
}
