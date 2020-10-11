using BLL.Services;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace POSClient
{
    /// <summary>
    /// Interaction logic for invoice.xaml
    /// </summary>
    public partial class invoice : Window
    {
        private readonly string username;
        private readonly DAL.Models.Branch branch;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public invoice(string username, DAL.Models.Branch branch)
        {
            InitializeComponent();
            this.username = username;
            _productService = new ProductService();
            _orderService = new OrderService();
            lbl_username.Content = username;
            this.branch = branch;
            lbl_branch.Content = branch.Name;
        }

        private bool CheckValidity()
        {
            bool isValid = true;

            if(string.IsNullOrWhiteSpace(txt_orderId.Text))
            {
                isValid = false;
            }
            if(string.IsNullOrWhiteSpace(txt_totalValue.Text))
            {
                isValid = false;
            }
            if(dgv_orderItems.Items.Count == 0)
            {
                isValid = false;
            }

            return isValid;
        }

        private bool Contains(int id)
        {
            bool isExist = false;
            foreach(ItemsControl item in dgv_orderItems.Items)
            {
                if(Convert.ToInt32(item.GetValue(null)) == id)
                {
                    isExist = true;
                }
            }
            return isExist;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Windows[1].Show();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // setting default values
            txt_totalValue.Text = "0";

            // loading all products
            dgv_products.ItemsSource = new ObservableCollection<Product>(_productService.GetAll());

            // getting the order id
            int orderId = 0;

            await Task.Run(() =>
            {
                orderId = _orderService.GetAll().OrderByDescending(o => o.Id).FirstOrDefault() == null ? 1 : _orderService.GetAll().OrderByDescending(o => o.Id).FirstOrDefault().Id + 1;
            });

            txt_orderId.Text = Convert.ToString(orderId);
        }

        private void btn_submitPrint_Click(object sender, RoutedEventArgs e)
        {
            bool isModelValid = CheckValidity();

            if(isModelValid)
            {
                // creating an order and submitting the order
                // printing the invoice
            }
        }

        private void dgv_products_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(dgv_products.CurrentCell != null && dgv_products.CurrentItem != null)
            {
                // checking if the product is already exist in the order items or not
                Product product = (Product)dgv_products.CurrentItem;

                if(!Contains(product.Id))
                {
                    // if it does not contain it
                    // add the current selected product to the other grid view
                }
                else
                {
                    // otherwise increase the quantity
                }
            }
        }
    }
}
