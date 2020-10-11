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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POSClient
{
    /// <summary>
    /// Interaction logic for categoryFrm.xaml
    /// </summary>
    public partial class categoryFrm : Window
    {
        private readonly ICategoryService _categoryService;
        public categoryFrm()
        {
            InitializeComponent();
            _categoryService = new CategoryService();
        }

        private bool CheckValidity()
        {
            bool isValid = true;

            if(string.IsNullOrWhiteSpace(txt_name.Text))
            {
                isValid = false;
            }
            return isValid;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            // getting all categories
            dgv_categories.ItemsSource = new ObservableCollection<Category>(_categoryService.GetAll());
        }

        private async void btn_addCategory_Click(object sender, RoutedEventArgs e)
        {
            bool isModelValid = CheckValidity();

            if(isModelValid)
            {
                btn_addCategory.Content = "Adding...";
                btn_addCategory.IsEnabled = false;

                var category = new Category
                {
                    Name = txt_name.Text
                };

                await Task.Run(() =>
                {
                    _categoryService.Add(category);
                });

                btn_addCategory.Content = "Add";
                btn_addCategory.IsEnabled = true;

                // getting all categories

                ObservableCollection<Category> categories = null;

                await Task.Run(() =>
                {
                    categories = new ObservableCollection<Category>(_categoryService.GetAll());
                });

                dgv_categories.ItemsSource = categories;
            }
        }
    }
}
