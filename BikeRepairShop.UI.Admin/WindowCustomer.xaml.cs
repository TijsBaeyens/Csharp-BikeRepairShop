using BikeRepairShop.BL.Domain;
using BikeRepairShop.BL.Managers;
using BikeRepairShop.UI.Admin.Mappers;
using BikeRepairShop.UI.Admin.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for WindowCustomer.xaml
    /// </summary>
    public partial class WindowCustomer : Window {
        public CustomerUI Customer { get; set; }
        private bool update;
        private CustomerManager customerManager;
        public WindowCustomer(CustomerManager customerManager, bool update = false) {
            InitializeComponent();
            this.customerManager = customerManager;
            this.update = update;
            if (update) {
                
            } else {
                IDTextBox.Text = "<generated>";
            }
        }

        private void CancelCustomerButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void SaveCustomerButton_Click(object sender, RoutedEventArgs e) {
            if (update) {
                Customer.Name = CustomerNameTextBox.Text;
                Customer.Adress = AdressTextBox.Text;
                Customer.Email = EmailTextBox.Text;
                customerManager.ChangeCustomer(CustomerMapper.ToDTO(Customer));
            } else {
                Customer = new CustomerUI();
                Customer.Name = CustomerNameTextBox.Text;
                Customer.Adress = AdressTextBox.Text;
                Customer.Email = EmailTextBox.Text;
                Customer.Id = customerManager.GetLastCustomerId() + 1;
                customerManager.AddCustomer(CustomerMapper.ToDTO(Customer));
            }
            DialogResult = true;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            if (update) {
                CustomerNameTextBox.Text = Customer.Name;
                AdressTextBox.Text = Customer.Adress;
                IDTextBox.Text = Customer.Id.ToString();
                EmailTextBox.Text = Customer.Email;
            }
        }
    }
}
