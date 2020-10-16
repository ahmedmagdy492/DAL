using BLL.Services;
using DAL.Models;
using Microsoft.Win32;
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
    /// Interaction logic for productsFrm.xaml
    /// </summary>
    public partial class productsFrm : Window
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ImageService _imageService;
        public productsFrm()
        {
            InitializeComponent();
            _categoryService = new CategoryService();
            _productService = new ProductService();
            _imageService = new ImageManager();
        }

        public void Clear()
        {
            txt_description.Text = string.Empty;
            txt_image.Text = string.Empty;
            txt_price.Text = string.Empty;
            txt_prodName.Text = string.Empty;
            cmb_category.SelectedIndex = 0;
        }

        private bool CheckValidity()
        {
            bool isValid = true;

            if(string.IsNullOrWhiteSpace(txt_prodName.Text))
            {
                isValid = false;
            }
            if(string.IsNullOrWhiteSpace(txt_price.Text))
            {
                isValid = false;
            }
            if(cmb_category.SelectedIndex == -1)
            {
                isValid = false;
            }
            if(string.IsNullOrWhiteSpace(txt_image.Text))
            {
                isValid = false;
            }
            return isValid;
        }

        private void txt_price_KeyDown(object sender, KeyEventArgs e)
        {
            // enabling only chars
            foreach(var c in txt_price.Text)
            {
                if(!char.IsDigit(c))
                {
                    e.Handled = true;
                    txt_price.Text = string.Empty;
                    break;
                }
                else
                {
                    e.Handled = false;
                }
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // loading all categories
            var categories = new ObservableCollection<Category>();
            await Task.Run(() =>
            {
                categories = new ObservableCollection<Category>(_categoryService.GetAll());
            });
            cmb_category.ItemsSource = categories;
            cmb_category.DisplayMemberPath = "Name";
            cmb_category.SelectedValuePath = "Id";
            cmb_category.SelectedIndex = 0;

            // loading all products
            dgv_products.ItemsSource = new ObservableCollection<Product>(_productService.GetAll());
        }

        private async void btn_addProduct_Click(object sender, RoutedEventArgs e)
        {
            bool isModelValid = CheckValidity();

            if(isModelValid)
            {
                btn_addProduct.IsEnabled = false;
                btn_addProduct.Content = "Adding...";

                var product = new Product
                {
                    Name = txt_prodName.Text,
                    Description = txt_description.Text,
                    Price = Convert.ToDouble(txt_price.Text),
                    CategoryId = Convert.ToInt32(cmb_category.SelectedValue)
                };

                await Task.Run(() =>
                {
                    product = _productService.Add(product);
                });

                var image = new DAL.Models.Image
                {
                    Path = ImageSaver.GetFileName(txt_image.Text),
                    ProductId = product.Id
                };

                await Task.Run(() =>
                {
                    _imageService.Add(image);
                });

                btn_addProduct.IsEnabled = true;
                btn_addProduct.Content = "Add";

                // loading all products again
                ObservableCollection<Product> productsCollection = null;

                await Task.Run(() =>
                {
                    productsCollection = new ObservableCollection<Product>(_productService.GetAll());
                });

                dgv_products.ItemsSource = productsCollection;
            }
        }

        private async void btn_addCategory_Click(object sender, RoutedEventArgs e)
        {
            categoryFrm category = new categoryFrm();
            category.ShowDialog();

            // loading new categories

            ObservableCollection<Category> categoryCollection = null;
            await Task.Run(() =>
            {
                categoryCollection = new ObservableCollection<Category>(_categoryService.GetAll());
            });

            cmb_category.DisplayMemberPath = "Name";
            cmb_category.SelectedValuePath = "Id";
            cmb_category.SelectedIndex = 0;
        }

        private void btn_browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog
            {
                Filter = "PNG Images|*.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Title = "Choose an image"
            };

            var dialogResult = opf.ShowDialog();
            if(dialogResult.Value == true)
            {
                txt_image.Text = opf.FileName;
            }
        }
    }
}
