using BikeRepairShop.BL.Domain;
using BikeRepairShop.BL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.BL.Interfaces
{
    public interface ICustomerRepository
    {
        void AddBike(Bike bike);
        List<BikeInfo> GetBikesInfo();
        Customer GetCustomer(int? id);
        int GetLastBikeId();
        int GetLastCustomerId();
        void DeleteBike(int? BikeId);
        void ChangeBike(BikeInfo bikeInfo);
        List<CustomerInfo> GetCustomersInfo();
        void AddCustomer(Customer customerInfo);
        void ChangeCustomer(CustomerInfo customerInfo);
        void DeleteCustomer(int? customerID);
    }
}
