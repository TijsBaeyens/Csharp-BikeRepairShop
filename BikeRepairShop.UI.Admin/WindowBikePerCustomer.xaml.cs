using BikeRepairShop.BL.Managers;
using BikeRepairShop.UI.Admin.Model;
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

namespace BikeRepairShop.UI.Admin {
    /// <summary>
    /// Interaction logic for WindowBikePerCustomer.xaml
    /// </summary>
    public partial class WindowBikePerCustomer : Window {
        private ObservableCollection<BikeUI> bikes;
        private ObservableCollection<BikeUI> newBikes = new ObservableCollection<BikeUI>();
        private CustomerManager customerManager;
        public WindowBikePerCustomer(CustomerManager customerManager, int? customerId) {
            InitializeComponent();
            this.customerManager = customerManager;
            bikes = new ObservableCollection<BikeUI>(customerManager.GetBikesInfo().Select(x => new BikeUI(x.Id, x.Description, x.BikeType, x.PurchaseCost, x.Customer.id, x.Customer.description)));
            foreach (BikeUI bikeUI in bikes) {
                if (bikeUI.CustomerId == customerId) {
                    newBikes.Add(bikeUI);
                }
            }
            BikeDataGrid.ItemsSource = newBikes;
        }
    }
}
