using BikeRepairShop.BL.Domain;
using BikeRepairShop.BL.DTO;
using BikeRepairShop.BL.Interfaces;
using BikeRepairShop.BL.Managers;
using BikeRepairShop.DL.Repositories;
using BikeRepairShop.UI.Admin.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BikeRepairShop.UI.Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CustomerManager customerManager;
        private ICustomerRepository customerRepo;
        private ObservableCollection<BikeUI> bikes;
        private ObservableCollection<CustomerUI> customers;
        public MainWindow()
        {
            InitializeComponent();
            string conn = ConfigurationManager.ConnectionStrings["ADOconnSQL"].ConnectionString;
            customerRepo =new CustomerRepository(conn);
            customerManager = new CustomerManager(customerRepo);
            bikes=new ObservableCollection<BikeUI>(customerManager.GetBikesInfo().Select(x=>new BikeUI(x.Id,x.Description,x.BikeType,x.PurchaseCost,x.Customer.id,x.Customer.description)));
            customers = new ObservableCollection<CustomerUI>(customerManager.GetCustomerInfo().Select(x => new CustomerUI(x.ID, x.Name, x.adress, x.Email)));
            BikeDataGrid.ItemsSource = bikes;
            BikeDataGrid.IsReadOnly= true;
            CustomerDataGrid.ItemsSource = customers;
            CustomerDataGrid.IsReadOnly = true;
        }

        private void MenuItemAddBike_Click(object sender, RoutedEventArgs e)
        {
            WindowBike w=new WindowBike(customerManager);
            w.CustomerComboBox.ItemsSource = customers;
            w.CustomerComboBox.SelectedIndex = 0;
            if (w.ShowDialog() == true)
            {
                bikes.Add(w.Bike);
            }
        }

        private void MenuItemDeleteBike_Click(object sender, RoutedEventArgs e)
        {
            BikeUI bike = (BikeUI)BikeDataGrid.SelectedItem;
            if (bike == null) MessageBox.Show("no selection", "Bike");
            else {
                if (MessageBox.Show("Are you sure you want to delete this bike?", "Bike", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                    customerManager.DeleteBike(bike.Id);
                    bikes.Remove(bike);
                }
            }
        }

        private void MenuItemUpdateBike_Click(object sender, RoutedEventArgs e)
        {
            BikeUI bike = (BikeUI)BikeDataGrid.SelectedItem;
            if (bike == null) MessageBox.Show("no selection", "Bike");
            else
            {
                WindowBike w = new WindowBike(customerManager,true);
                w.Bike = bike;
                w.ShowDialog();
            }
        }

        private void MenuItemAddCustomer_Click(object sender, RoutedEventArgs e) {
            WindowCustomer w = new WindowCustomer(customerManager);
            if (w.ShowDialog() == true) {
                customers.Add(w.Customer);
            }
        }
        private void MenuItemDeleteCustomer_Click(object sender, RoutedEventArgs e) {
            CustomerUI customer = (CustomerUI)CustomerDataGrid.SelectedItem;
            if (customer == null) MessageBox.Show("no selection", "Customer");
            else {
                if (MessageBox.Show("Are you sure you want to delete this customer?", "Customer", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                    customerManager.DeleteCustomer(customer.Id);
                    customers.Remove(customer);
                }
            }
        }
        private void MenuItemUpdateCustomer_Click(object sender, RoutedEventArgs e) {
            CustomerUI customer = (CustomerUI)CustomerDataGrid.SelectedItem;
            if (customer == null) MessageBox.Show("no selection", "Bike");
            else {
                WindowCustomer w = new WindowCustomer(customerManager, true);
                w.Customer = customer;
                w.ShowDialog();
            }
        }
        private void MenuItemShowBikes_Click(object sender, RoutedEventArgs e) {
            CustomerUI customer = (CustomerUI)CustomerDataGrid.SelectedItem;
            if (customer == null) MessageBox.Show("no selection", "Bike");
            else {
                WindowBikePerCustomer w = new WindowBikePerCustomer(customerManager, customer.Id);
                w.ShowDialog();
            }
        }
    }
}
