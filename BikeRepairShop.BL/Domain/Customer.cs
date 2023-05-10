using BikeRepairShop.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.BL.Domain
{
    public class Customer
    {
        public int? ID { get; private set; }
        public string Name { get; private set; }
        public string adress { get; private set; }
        public string Email { get; private set; }
        private List<Bike> Bikes  = new List<Bike>();
        public IReadOnlyList<Bike> GetBikes() { return Bikes.AsReadOnly(); }
        private List<CustomerRepairOrderInfo> CustomerRepairOrderInfos= new List<CustomerRepairOrderInfo>();

        public Customer(int? iD, string name, string adress, string email) {
            ID = iD;
            Name = name;
            adress = adress;
            Email = email;
        }

        public Customer() {
        }

        public Customer(string name, string adress, string email) {
            Name = name;
            this.adress = adress;
            Email = email;
        }

        public IReadOnlyList<CustomerRepairOrderInfo> GetCustomerRepairOrderInfos() { return CustomerRepairOrderInfos.AsReadOnly();}

        public void AddBike(Bike bike)
        {
            if (bike == null) throw new DomainException("Customer");
            if (Bikes.Contains(bike)) throw new DomainException("customer-addbike");
            Bikes.Add(bike);
            if (bike.Customer!=this) bike.SetCustomer(this);
        }
        public void RemoveBike(Bike bike)
        {
            if (!Bikes.Contains(bike)) throw new DomainException("customer-removebike");
            if (bike.Customer == this) bike.RemoveCustomer();
            Bikes.Remove(bike);
            
        }
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("customer-setname");
            Name = name;
        }
        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new DomainException("customer-setemail");
            Email = email;
        }
        public void Setadress(string adress)
        {
            if (string.IsNullOrWhiteSpace(adress)) throw new DomainException("customer-setadress");
            adress = adress;
        }
        public void SetId(int id)
        {
            if (id <= 0) throw new DomainException("customer-setid");
            ID = id;
        }
    }
}
