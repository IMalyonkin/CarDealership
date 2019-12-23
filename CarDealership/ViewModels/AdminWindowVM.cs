using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CarDealership.Models;

namespace CarDealership.ViewModels
{
    public class AdminWindowVM : INotifyPropertyChanged
    {
        private CarsContext db;

        private Brand selectedBrand;
        private Model selectedModel;
        private string vin;
        private Kit selectedKit;
        private Engine selectedEngine;
        private Models.Color selectedColor;
        private string name;
        private string login;
        private string password;

        public ObservableCollection<Brand> Brands { get; set; }
        public ObservableCollection<Model> Models { get; set; }
        public ObservableCollection<Kit> Kits { get; set; }
        public ObservableCollection<Engine> Engines { get; set; }
        public ObservableCollection<Models.Color> Colors { get; set; }

        public Brand SelectedBrand
        {
            get { return selectedBrand; }
            set
            {
                selectedBrand = value;
                OnPropertyChanged("SelectedBrand");

                Models.Clear();
                selectedBrand.Model.ToList().ForEach(i => Models.Add(i));
                try
                {
                    SelectedModel = Models.First();
                }
                catch { }
            }
        }

        public Model SelectedModel
        {
            get { return selectedModel; }
            set
            {
                selectedModel = value;
                OnPropertyChanged("SelectedModel");

                try
                {
                    Kits.Clear();
                    selectedKit = null;
                    db.Kit.Where(i => i.ModelFK == selectedModel.Id).ToList().ForEach(i => Kits.Add(i));

                    Engines.Clear();
                    selectedEngine = null;
                    SelectedModel.Model_Engine
                        .Join(db.Engine, me => me.EngineFK, e => e.Id, (me, e) => e).ToList()
                        .ForEach(i => Engines.Add(i));

                    Colors.Clear();
                    selectedColor = null;
                    SelectedModel.Model_Color
                        .Join(db.Color, mc => mc.ColorFK, c => c.Id, (mc, c) => c).ToList()
                        .ForEach(i => Colors.Add(i));
                }
                catch (Exception e)
                {

                }
            }
        }

        public string VIN
        {
            get { return vin; }
            set
            {
                vin = value;
                OnPropertyChanged("VIN");
            }
        }

        public Kit SelectedKit
        {
            get { return selectedKit; }
            set
            {
                selectedKit = value;
                OnPropertyChanged("SelectedKit");
            }
        }

        public Engine SelectedEngine
        {
            get { return selectedEngine; }
            set
            {
                selectedEngine = value;
                OnPropertyChanged("SelectedEngine");
            }
        }

        public Models.Color SelectedColor
        {
            get { return selectedColor; }
            set
            {
                selectedColor = value;
                OnPropertyChanged("SelectedColor");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private RelayCommand addVehicle;
        public RelayCommand AddVehicle
        {
            get
            {
                return addVehicle ??
                  (addVehicle = new RelayCommand(obj =>
                  {
                      db.Vehicle.Add(new Vehicle
                      {
                          VIN = vin,
                          EngineFK = selectedEngine.Id,
                          StatusFK = 1,
                          KitFK = selectedKit.Id,
                          ColorFK = selectedColor.Id
                      });

                      if (db.SaveChanges() > 0)
                          MessageBox.Show("Автомобиль добавлен");
                  },
                  obj => isVehiclefilled()));
            }
        }

        private RelayCommand addEmployee;
        public RelayCommand AddEmployee
        {
            get
            {
                return addEmployee ??
                  (addEmployee = new RelayCommand(obj =>
                  {
                      db.Employee.Add(new Employee
                      {
                          Name = name,
                          Login = login,
                          Password = password,
                          Role = "Сотрудник"
                      });

                      if (db.SaveChanges() > 0)
                          MessageBox.Show("Сотрудник добавлен");
                  },
                  obj => isEmployeefilled()));
            }
        }

        private bool isVehiclefilled()
        {
            if (selectedBrand != null && selectedModel != null && vin != "" && selectedKit != null && selectedEngine != null && selectedColor != null)
                return true;
            else
                return false;
        }

        private bool isEmployeefilled()
        {
            if (name != "" && login != "" && password != "")
                return true;
            else
                return false;
        }

        public AdminWindowVM()
        {
            db = new CarsContext();
            Kits = new ObservableCollection<Kit>();
            Engines = new ObservableCollection<Engine>();
            Colors = new ObservableCollection<Models.Color>();
            Models = new ObservableCollection<Model>();
            Brands = new ObservableCollection<Brand>(db.Brand.ToList());
            SelectedBrand = Brands.First();

            name = "";
            login = "";
            password = "";
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
