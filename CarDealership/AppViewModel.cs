using System;
using System.Drawing;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Data.Entity;
using CarDealership.Models;
using CarDealership.BLL;

namespace CarDealership
{
    public class AppViewModel : INotifyPropertyChanged
    {
        MainWindow window;
        Frame main;
        private CarsContext db;

        private Brand selectedBrand;
        private Model selectedModel;
        private Kit selectedKit;
        private Engine selectedEngine;
        private Models.Color selectedColor;
        private VehicleModel selectedVehicle;

        public ObservableCollection<Brand> Brands { get; set; }
        public ObservableCollection<Model> Models { get; set; }
        public ObservableCollection<Kit> Kits { get; set; }
        public ObservableCollection<Engine> Engines { get; set; }
        public ObservableCollection<Models.Color> Colors { get; set; }
        public ObservableCollection<VehicleModel> allVehicles { get; set; }
        public ObservableCollection<VehicleModel> Vehicles { get; set; }

        public Brand SelectedBrand
        {
            get { return selectedBrand; }
            set
            {
                selectedBrand = value;
                OnPropertyChanged("SelectedBrand");

                Models.Clear();
                selectedBrand.Model.ToList().ForEach(i => Models.Add(i));
                SelectedModel = Models.First();
            }
        }

        public Model SelectedModel
        {
            get { return selectedModel; }
            set
            {
                selectedModel = value;
                OnPropertyChanged("SelectedModel");

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

                allVehicles.Clear();
                Vehicles.Clear();
                SelectedModel.Kit.ToList()
                    .Join(db.Vehicle, k => k.Id, v => v.KitFK, (k, v) => v)
                    .Where(i => i.StatusFK == 1)
                    .Select(i => new VehicleModel()
                    {
                        vehicle = new Vehicle() { EngineFK = i.EngineFK, StatusFK = i.StatusFK, KitFK = i.KitFK, ColorFK = i.ColorFK },
                        engineName = i.Engine.Name,
                        engineType = i.Engine.Type,
                        enginePower = i.Engine.Power,
                        kit = i.Kit.Name,
                        color = i.Color.Name,
                        image = SelectedModel.Model_Color.ToList()
                                .Where(j => j.ColorFK == i.ColorFK)
                                .Select(j => j.Image)
                                .FirstOrDefault(),
                        totalPrice = calcTotalPrice(i),
                        options = db.Kit_Option.ToList()
                                .Where(j => j.KitFK == i.KitFK)
                                .Join(db.Option, ko => ko.OptionFK, o => o.Id, (ko, o) => o)
                                .ToList()
                    })
                    .ToList().ForEach(i => allVehicles.Add(i));

                allVehicles.ToList().ForEach(i => Vehicles.Add(i)); 
            }
        }

        public Kit SelectedKit
        {
            get { return selectedKit; }
            set
            {
                selectedKit = value;
                OnPropertyChanged("SelectedKit");

                getVehicles();
            }
        }

        public Engine SelectedEngine
        {
            get { return selectedEngine; }
            set
            {
                selectedEngine = value;
                OnPropertyChanged("SelectedEngine");

                getVehicles();
            }
        }

        public Models.Color SelectedColor
        {
            get { return selectedColor; }
            set
            {
                selectedColor = value;
                OnPropertyChanged("SelectedColor");

                getVehicles();
            }
        }

        public VehicleModel SelectedVehicle
        {
            get { return selectedVehicle; }
            set
            {
                selectedVehicle = value;
                OnPropertyChanged("SelectedVehicle");
            }
        }

        private RelayCommand clearKits;
        public RelayCommand ClearKits
        {
            get
            {
                return clearKits ??
                  (clearKits = new RelayCommand(obj =>
                  {
                      SelectedKit = null;
                      getVehicles();
                  }));
            }
        }

        private RelayCommand clearEngines;
        public RelayCommand ClearEngines
        {
            get
            {
                return clearEngines ??
                  (clearEngines = new RelayCommand(obj =>
                  {
                      SelectedEngine = null;
                      getVehicles();
                  }));
            }
        }

        private RelayCommand clearColors;
        public RelayCommand ClearColors
        {
            get
            {
                return clearColors ??
                  (clearColors = new RelayCommand(obj =>
                  {
                      SelectedColor = null;
                      getVehicles();
                  }));
            }
        }

        private void getVehicles()
        {
            Vehicles.Clear();
            allVehicles.ToList().ForEach(i => Vehicles.Add(i));

            if (SelectedKit != null)
            {
                List<VehicleModel> tmp = Vehicles.ToList()
                    .Where(i => i.vehicle.KitFK == selectedKit.Id)
                    .ToList();

                Vehicles.Clear();
                tmp.ForEach(i => Vehicles.Add(i));
            }

            if (SelectedEngine != null)
            {
                List<VehicleModel> tmp = Vehicles.ToList()
                    .Where(i => i.vehicle.EngineFK == selectedEngine.Id)
                    .ToList();

                Vehicles.Clear();
                tmp.ForEach(i => Vehicles.Add(i));
            }

            if (SelectedColor != null)
            {
                List<VehicleModel> tmp = Vehicles.ToList()
                    .Where(i => i.vehicle.ColorFK == selectedColor.Id)
                    .ToList();

                Vehicles.Clear();
                tmp.ForEach(i => Vehicles.Add(i));
            }
        }

        private string calcTotalPrice(Vehicle v)
        {
            var p1 = db.Engine.ToList()
                                .Where(i => i.Id == v.EngineFK)
                                .Select(i => i.Price)
                                .FirstOrDefault();

            var p2 = db.Kit.ToList()
                        .Where(i => i.Id == v.KitFK)
                        .Select(i => i.Price)
                        .FirstOrDefault();

            var p3 = db.Color.ToList()
                        .Where(i => i.Id == v.ColorFK)
                        .Select(i => i.Price)
                        .FirstOrDefault();

            var p4 = SelectedModel.Price;

            return (Convert.ToInt32(p1) + Convert.ToInt32(p2) + Convert.ToInt32(p3) + Convert.ToInt32(p4)).ToString();
        }

        private RelayCommand contract;
        public RelayCommand Contract
        {
            get
            {
                return contract ??
                  (contract = new RelayCommand(obj =>
                  {
                      main.Content = new ContractPage();
                      window.hideSideBar();
                  }));
            }
        }

        public AppViewModel(Frame main, MainWindow window)
        {
            this.window = window;
            this.main = main;
            db = new CarsContext();

            Kits = new ObservableCollection<Kit>(db.Kit.ToList());
            Engines = new ObservableCollection<Engine>();
            Colors = new ObservableCollection<Models.Color>(db.Color.ToList());
            Vehicles = new ObservableCollection<VehicleModel>();
            allVehicles = new ObservableCollection<VehicleModel>();
            Models = new ObservableCollection<Model>(db.Model.ToList());
            Brands = new ObservableCollection<Brand>(db.Brand.ToList());
            SelectedBrand = Brands.First();
            SelectedModel = Models.First();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
