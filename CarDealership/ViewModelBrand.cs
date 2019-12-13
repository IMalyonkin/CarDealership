using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace CarDealership
{
    public class ViewModelBrand : INotifyPropertyChanged
    {
        private Brand brand;

        public ViewModelBrand(Brand b)
        {
            brand = b;
        }

        public int Id
        {
            get { return brand.Id; }
            set
            {
                brand.Id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return brand.Name; }
            set
            {
                brand.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
