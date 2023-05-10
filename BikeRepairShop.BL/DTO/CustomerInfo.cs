using BikeRepairShop.BL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.BL.DTO {
    public class CustomerInfo {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string adress { get; set; }
        public string Email { get; set; }
        private List<Bike> Bikes = new List<Bike>();

        public CustomerInfo(int? iD, string name, string adress, string email, List<Bike> bikes) {
            ID = iD;
            Name = name;
            this.adress = adress;
            Email = email;
            Bikes = bikes;
        }

        public CustomerInfo(int? id, string name, string adress, string email) {
            ID = id;
            Name = name;
            this.adress = adress;
            Email = email;
        }
    }
}
