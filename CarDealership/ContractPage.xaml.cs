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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarDealership.Models;
using CarDealership.BLL;

namespace CarDealership
{
    /// <summary>
    /// Логика взаимодействия для ContractPage.xaml
    /// </summary>
    public partial class ContractPage : Page
    {
        public ContractPage(AppViewModel vm, VehicleModel v)
        {
            InitializeComponent();

            DataContext = new ContractVM(vm, v);
        }

        public ContractPage(BuildVehicleVM vm, VehicleModel v)
        {
            InitializeComponent();

            DataContext = new ContractVM(vm, v);
        }
    }
}
