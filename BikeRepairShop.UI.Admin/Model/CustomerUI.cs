using BikeRepairShop.BL.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeRepairShop.UI.Admin.Model
{
    public class CustomerUI : INotifyPropertyChanged
    {
        public int? Id { get; set; }
        private string _Name;
        public string Name { get { return _Name; } set { _Name = value; OnPropertyChanged(); } }
        private string _Adress;
        public string Adress { get { return _Adress; } set { _Adress = value; OnPropertyChanged(); } }
        private string _Email;
        public string Email { get { return _Email; } set { _Email = value; OnPropertyChanged(); } }


        public CustomerUI() {
        }

        public CustomerUI(int? id, string name, string adress, string email) {
            Id = id;
            Name = name;
            Adress = adress;
            Email = email;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name=null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
