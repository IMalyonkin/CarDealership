using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models;

namespace CarDealership.BLL
{
    public class OptionModel : INotifyPropertyChanged
    {
        public Option option { get; set; }

        private bool isselected;
        public bool IsSelected
        {
            get { return isselected; }
            set
            {
                isselected = value;
                OnPropertyChanged();
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
