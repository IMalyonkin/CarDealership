using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CarDealership.BLL;
using CarDealership.Models;

namespace CarDealership
{
    public class ContractVM : INotifyPropertyChanged
    {
        private string contractType;
        CarsContext db;
        AppViewModel appViewModel;
        BuildVehicleVM buildVehicleVM;
        VehicleModel vehicle;
        string clientName;
        string clientNumber;
        Employee employee;
        DateTime date;

        public string ContractType
        {
            get { return contractType; }
        }

        public VehicleModel Vehicle
        {
            get { return vehicle; }
            set
            {
                vehicle = value;
                OnPropertyChanged("Vehicle");
            }
        }

        public string Kit
        {
            get { return vehicle.kit != null ? vehicle.kit : "Custom"; }
            set
            {
                vehicle.kit = value;
                OnPropertyChanged("Kit");
            }
        }

        public string ClientName
        {
            set { clientName = value; }
        }

        public string ClientNumber
        {
            set { clientNumber = value; }
        }

        public Employee Employee
        {
            get { return employee; }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }

        public string Date
        {
            get { return date.ToString("dd.MM.yyyy"); }
        }

        private RelayCommand save;
        public RelayCommand Save
        {
            get
            {
                return save ??
                  (save = new RelayCommand(obj =>
                  {
                      if (appViewModel != null)
                      {
                          db.Contract.Add(new Contract
                          {
                              Total_Price = Int32.Parse(vehicle.totalPrice),
                              Date = date,
                              Type = "Покупка",
                              VehicleFK = vehicle.vehicle.Id,
                              Client = new Client { Name = clientName, PhoneNumber = clientNumber },
                              EmployeeFK = employee.Id
                          });

                          db.Vehicle.Find(vehicle.vehicle.Id).StatusFK = 2;
                      }
                      else
                      {
                          db.Contract.Add(new Contract
                          {
                              Total_Price = Int32.Parse(vehicle.totalPrice),
                              Date = date,
                              Type = "Заказ",
                              Vehicle = new Vehicle
                              {
                                  EngineFK = vehicle.vehicle.EngineFK,
                                  ColorFK = vehicle.vehicle.ColorFK,
                                  StatusFK = 3,
                                  KitFK = 1,
                                  Vehicle_Option = convert()
                              },
                              Client = new Client { Name = clientName, PhoneNumber = clientNumber },
                              EmployeeFK = employee.Id
                          });
                      }

                      if (appViewModel != null)
                      {
                          appViewModel.Main.Content = new VehiclesInStockPage(appViewModel.Main, appViewModel.Window);
                          appViewModel.Window.returnSideBar();
                      }
                      else
                      {
                          buildVehicleVM.Main.Content = new BuildVehiclePage(buildVehicleVM.Main, buildVehicleVM.Window);
                          buildVehicleVM.Window.returnSideBar();
                      }

                      if (db.SaveChanges() > 0)
                          MessageBox.Show("Договор оформлен"); 
                  },
                  obj => clientName != null && clientNumber != null));
            }
        }

        private List<Vehicle_Option> convert()
        {
            List<Vehicle_Option> list = new List<Vehicle_Option>();
            foreach (var item in vehicle.options)
                list.Add(new Vehicle_Option { OptionFK = item.Id, Price = item.Price });
            return list;
        }

        private RelayCommand _return;
        public RelayCommand _Return
        {
            get
            {
                return _return ??
                  (_return = new RelayCommand(obj =>
                  {
                      if (appViewModel != null)
                      {
                          appViewModel.Main.Content = appViewModel.VehiclesInStockPage;
                          appViewModel.Window.returnSideBar();
                      }
                      else
                      {
                          buildVehicleVM.Main.Content = buildVehicleVM.BuildVehiclePage;
                          buildVehicleVM.Window.returnSideBar();
                      }
                  }));
            }
        }

        public ContractVM(AppViewModel vm, VehicleModel v)
        {
            db = new CarsContext();
            contractType = "покупку";
            appViewModel = vm;
            vehicle = v;
            employee = vm.Window.Employee;
            date = DateTime.Now;
        }

        public ContractVM(BuildVehicleVM vm, VehicleModel v)
        {
            db = new CarsContext();
            contractType = "заказ";
            buildVehicleVM = vm;
            vehicle = v;
            employee = vm.Window.Employee;
            date = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
