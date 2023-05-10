using BikeRepairShop.BL.Domain;
using BikeRepairShop.BL.DTO;
using BikeRepairShop.BL.Exceptions;
using BikeRepairShop.BL.Factories;
using BikeRepairShop.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.BL.Managers {
    public class CustomerManager {
        private ICustomerRepository repo;

        public CustomerManager(ICustomerRepository repo) {
            this.repo = repo;
        }

        public List<BikeInfo> GetBikesInfo() {
            try {
                return repo.GetBikesInfo();
            } catch (Exception ex) { throw new ManagerException("GetBikesInfo", ex); }
        }
        public void AddBike(BikeInfo bikeInfo) {
            try {
                Customer customer = repo.GetCustomer(bikeInfo.Customer.id);
                Bike bike = DomainFactory.NewBike(bikeInfo);
                customer.AddBike(bike);
                repo.AddBike(bike);
                bikeInfo.Id = bike.ID;
            } catch (Exception ex) { throw new ManagerException("AddBike", ex); }
        }
        public int GetLastBikeId() {
            return repo.GetLastBikeId();
        }
        public int GetLastCustomerId() {
            return repo.GetLastCustomerId();
        }
        public void DeleteBike(int? BikeId) {
            try {
                repo.DeleteBike(BikeId);
            } catch (Exception ex) { throw new ManagerException("DeleteBike", ex); }
        }

        public string GetCustomerDescription(int CustomerId) {
            try {
                return repo.GetCustomer(CustomerId).Name + " (" + repo.GetCustomer(CustomerId).Email + ")";
            } catch (Exception ex) {
                throw new ManagerException("GetCustomerDescription", ex);
            }
        }

        public void ChangeBike(BikeInfo bikeInfo) {
            try {
                repo.ChangeBike(bikeInfo);
            } catch (Exception ex) { throw new ManagerException("ChangeBike", ex); }
        }

        public string GetCustomerName(int? id) {
            try {
                return repo.GetCustomer(id).Name;
            } catch (Exception ex) { throw new ManagerException("GetCustomerName", ex); }
        }
        public List<CustomerInfo> GetCustomerInfo() {
            try {
                return repo.GetCustomersInfo();
            } catch (Exception ex) { throw new ManagerException("GetCustomerInfo", ex); }
        }

        public void AddCustomer(CustomerInfo customerInfo) {
            try {
                Customer customer = repo.GetCustomer(customerInfo.ID);
                Customer customer1 = DomainFactory.NewCustomer(customerInfo);
                repo.AddCustomer(customer1);
                customerInfo.ID = customer1.ID;
            } catch (Exception ex) { throw new ManagerException("AddCustomer", ex); }
        }

        public void ChangeCustomer(CustomerInfo customerInfo) {
            try {
                repo.ChangeCustomer(customerInfo);
            } catch (Exception ex) {
                throw new ManagerException("ChangeCustomer", ex);
            }
        }
        public void DeleteCustomer(int? CustomerID) {
            try {
                repo.DeleteCustomer(CustomerID);
            } catch (Exception ex) { throw new ManagerException("DeleteCustomer", ex); }
        }
    }
}
