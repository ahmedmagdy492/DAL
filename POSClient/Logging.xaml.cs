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
    /// Interaction logic for Logging.xaml
    /// </summary>
    public partial class Logging : Window
    {
        private readonly ILoggerService _loggerService;
        public Logging()
        {
            InitializeComponent();
            _loggerService = new LoggerService();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DAL.Models.Logging> loggings = null;
            await Task.Run(() =>
            {
                loggings = new ObservableCollection<DAL.Models.Logging>(_loggerService.GetAll());
            });

            dgv_logs.ItemsSource = loggings;
        }
    }
}
