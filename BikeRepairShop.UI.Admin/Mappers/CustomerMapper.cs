using BikeRepairShop.BL.DTO;
using BikeRepairShop.UI.Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.UI.Admin.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerInfo ToDTO(CustomerUI customerUI)
        {
            return new CustomerInfo(customerUI.Id, customerUI.Name, customerUI.Adress, customerUI.Email);
        }
    }
}
