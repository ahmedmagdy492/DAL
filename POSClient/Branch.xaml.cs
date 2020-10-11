using BLL.Services;
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
    /// Interaction logic for Branch.xaml
    /// </summary>
    public partial class Branch : Window
    {
        private readonly IBranchService _branchService;
        public Branch()
        {
            InitializeComponent();
            background_pnl.Background = new LinearGradientBrush(
                Colors.White,
                Colors.White,
                130
                );
            _branchService = new BranchService();
        }

        public ObservableCollection<DAL.Models.Branch> branches { get; private set; }

        private bool CheckValidity()
        {
            bool isValid = true;

            if(string.IsNullOrWhiteSpace(txt_name.Text))
            {
                isValid = false;
            }
            if(string.IsNullOrWhiteSpace(txt_location.Text))
            {
                isValid = false;
            }
            return isValid;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // loading all branches
            var allBranches = _branchService.GetAll();

            dgv_branches.ItemsSource = new ObservableCollection<DAL.Models.Branch>(allBranches);
        }

        private async void btn_add_Click(object sender, RoutedEventArgs e)
        {
            // checking the validity
            var modelValid = CheckValidity();

            if(modelValid)
            {
                // adding a new branch
                btn_add.IsEnabled = false;
                btn_add.Content = "loading...";

                var branch = new DAL.Models.Branch
                {
                    Name = txt_name.Text,
                    IsOnline = chk_onlineOrders.IsChecked.Value,
                    Location = txt_location.Text
                };

                await Task.Run(() =>
                {
                    _branchService.Add(branch);
                });

                // getting all branches
                branches = null;

                await Task.Run(() =>
                {
                    branches = new ObservableCollection<DAL.Models.Branch>(_branchService.GetAll());
                });
                dgv_branches.ItemsSource = branches;

                btn_add.IsEnabled = true;
                btn_add.Content = "Add";
            }
        }
    }
}
