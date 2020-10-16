using BLL.Services;
using DAL.Models;
using SharedLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Documents;

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
        private readonly ImageService _imageService;
        private readonly UserService _userService;
        private readonly IOrderItemService _orderItemService;
        private IEnumerable<Product> products;
        private List<CartItem> cart;
        public invoice(string username, DAL.Models.Branch branch)
        {
            InitializeComponent();
            this.username = username;
            _productService = new ProductService();
            _orderService = new OrderService();
            _imageService = new ImageManager();
            _userService = new UserService();
            _orderItemService = new OrderItemService();
            lbl_username.Content = username;
            this.branch = branch;
            lbl_branch.Content = branch.Name;
            cart = new List<CartItem>();
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
        
        private void Init()
        {
            cart.Clear();
            dgv_orderItems.ItemsSource = new ObservableCollection<CartItem>(cart);
            txt_orderId.Text = "";
            txt_search.Text = string.Empty;
            // focus on the search
            txt_search.Focus();

            // setting default values
            txt_totalValue.Text = "0";

            // getting the order id
            int orderId = 0;

            orderId = _orderService.GetAll().OrderByDescending(o => o.Id).FirstOrDefault() == null ? 1 : _orderService.GetAll().OrderByDescending(o => o.Id).FirstOrDefault().Id + 1;
            txt_orderId.Text = Convert.ToString(orderId);
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

        private void FillProducts(IEnumerable<Product> products)
        {
            // clearing out the grid from all the controls
            products_container.Children.Clear();

            for(int i = 0; i < products.Count(); i++)
            {
                StackPanel productPanel = new StackPanel
                {
                    Name = $"product_{products.ToList()[i].Id}",
                    Tag = products.ToList()[i].Id,
                    Margin = new Thickness(0, 0, 5, 10),
                    Cursor = System.Windows.Input.Cursors.Hand,
                    Effect = new System.Windows.Media.Effects.DropShadowEffect
                    {
                        Color = Colors.LightGray
                    }
                };

                productPanel.MouseDown += ProductPanel_MouseDown;

                // getting the product image
                var allImages = _imageService.GetAll().ToList();
                var id = products.ToList()[i].Id;
                DAL.Models.Image productImage = allImages.FirstOrDefault(a => a.ProductId == id);

                if (productImage != null)
                {
                    BitmapImage prodImg = new BitmapImage();
                    prodImg.BeginInit();
                    prodImg.UriSource = new Uri(productImage.Path);
                    prodImg.EndInit();

                    System.Windows.Controls.Image image = new System.Windows.Controls.Image
                    {
                        Height = 100,
                        Width = 100,
                        Margin = new Thickness(0, 2, 0, 0),
                        Source = prodImg,
                        MaxHeight = 100,
                        MaxWidth = 100
                    };

                    productPanel.Children.Add(image);
                    TextBlock txt = new TextBlock
                    {
                        Text = products.ToList()[i].Name,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    };

                    TextBlock price = new TextBlock
                    {
                        Text = string.Format("{0:c}", products.ToList()[i].Price),
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    };
                    productPanel.Children.Add(txt);
                    productPanel.Children.Add(price);
                    products_container.Children.Add(productPanel);
                }
            }
        }

        private void ProductPanel_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // adding the clicked product to the list of products to be added
            int productId = Convert.ToInt32(((StackPanel)sender).Tag);

            var product = products.FirstOrDefault(p => p.Id == productId);
            var cartItem = new CartItem
            {
                ProductId = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = 1,
                Total = 1 * product.Price
            };

            var foundCartItem = cart.FirstOrDefault(c => c.ProductId == cartItem.ProductId);
            if(foundCartItem == null)
            {
                cart.Add(cartItem);
            }
            else
            {
                // increasing the product quantity
                foundCartItem.Quantity += 1;
                foundCartItem.Total = foundCartItem.Quantity * foundCartItem.Price;
            }

            dgv_orderItems.ItemsSource = new ObservableCollection<CartItem>(cart);

            // calculating the total value of the invoice
            double totalValue = 0;
            foreach (CartItem item in dgv_orderItems.ItemsSource)
            {
                totalValue += item.Total;
            }
            txt_totalValue.Text = totalValue.ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Windows[1].Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();

            // loading all products
            products = _productService.GetAll();
            FillProducts(products);
        }

        private async void btn_submitPrint_Click(object sender, RoutedEventArgs e)
        {
            bool isModelValid = CheckValidity();

            if(isModelValid)
            {
                btn_submitPrint.IsEnabled = false;
                btn_submitPrint.Foreground = new SolidColorBrush(Colors.ForestGreen);
                btn_submitPrint.Content = "Submitting...";
                // creating an order and submitting the order
                var currentTime = DateTime.Now;
                var currentUser = await _userService.GetUserByUsername(this.username);

                var order = new Order
                {
                    BranchId = branch.Id,
                    OrderDate = currentTime,
                    UserId = currentUser.Id,
                    TotalInvoiceValue = Convert.ToDouble(txt_totalValue.Text)
                };
                _orderService.Add(order);
                foreach (CartItem item in dgv_orderItems.ItemsSource)
                {
                    var orderItem = new OrderItems
                    {
                        OrderId = Convert.ToInt32(txt_orderId.Text),
                        ProductId = item.ProductId,
                        ProductPrice = item.Price,
                        Quantity = (int)item.Quantity
                    };
                    _orderItemService.Add(orderItem);
                }

                // printing the invoice
                PrintDialog pd = new PrintDialog();
                FlowDocument doc = new FlowDocument();
                doc.Name = "POSInvoice";

                pd.PrintVisual(dgv_orderItems, "DGV");
                pd.PrintVisual(txt_orderId, "");
                pd.PrintVisual(txt_totalValue, "");

                Init();
                btn_submitPrint.IsEnabled = true;
                btn_submitPrint.Foreground = new SolidColorBrush(Colors.White);
                btn_submitPrint.Content = "Submit and Print";
            }
        }

        private void txt_search_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // filtering by the product name
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                if(!string.IsNullOrWhiteSpace(txt_search.Text))
                {
                    string searchTxt = txt_search.Text.ToLower();
                    var filteredProds = this.products.Where(p => p.Name.ToLower().Contains(searchTxt));
                    FillProducts(filteredProds);
                }
            }

            if(string.IsNullOrWhiteSpace(txt_search.Text))
            {
                FillProducts(products);
            }
        }
    }
}
