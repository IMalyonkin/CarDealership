using System;
using System.Windows.Controls;
using System.Drawing;
using System.Windows;
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
    public class BuildVehicleVM : INotifyPropertyChanged
    {
        private BuildVehiclePage buildVehiclePage;
        private MainWindow window;
        private Frame main;
        private CarsContext db;

        private VehicleModel vehicle;

        private Brand selectedBrand;
        private Model selectedModel;
        private Engine selectedEngine;
        private Models.Color selectedColor;
        private string totalPrice;
        private byte[] vehicleImage;

        public ObservableCollection<Brand> Brands { get; set; }
        public ObservableCollection<Model> Models { get; set; }
        public ObservableCollection<Engine> Engines { get; set; }
        public ObservableCollection<Models.Color> Colors { get; set; }


        private Option selectedHeadlights;
        private Option selectedWheels;
        private Option selectedMirrors;
        private Option selectedWing;
        private Option selectedSeat;
        private Option selectedUpholstery;
        private Option selectedSteering;
        private Option selectedHeating;
        private Option selectedSills;
        private Option selectedLighting;
        private List<OptionModel> selectedOthers;

        public ObservableCollection<Option> Headlights { get; set; }
        public ObservableCollection<Option> Wheels { get; set; }
        public ObservableCollection<Option> Mirrors { get; set; }
        public ObservableCollection<Option> Wings { get; set; }
        public ObservableCollection<Option> Seats { get; set; }
        public ObservableCollection<Option> Upholsteries { get; set; }
        public ObservableCollection<Option> Steerings { get; set; }
        public ObservableCollection<Option> Heatings { get; set; }
        public ObservableCollection<Option> Sills { get; set; }
        public ObservableCollection<Option> Lightings { get; set; }
        public ObservableCollection<OptionModel> Others { get; set; }

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

                calcTotalPrice();
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

                    Headlights.Clear();
                    selectedHeadlights = null;
                    SelectedModel.Option
                        .Where(i => i.OptionTypeFK == 1).ToList()
                        .ForEach(i => Headlights.Add(i));

                    Wheels.Clear();
                    selectedWheels = null;
                    SelectedModel.Option
                        .Where(i => i.OptionTypeFK == 2).ToList()
                        .ForEach(i => Wheels.Add(i));

                    Mirrors.Clear();
                    selectedMirrors = null;
                    SelectedModel.Option
                        .Where(i => i.OptionTypeFK == 3).ToList()
                        .ForEach(i => Mirrors.Add(i));

                    Wings.Clear();
                    selectedWing = null;
                    SelectedModel.Option
                        .Where(i => i.OptionTypeFK == 4).ToList()
                        .ForEach(i => Wings.Add(i));

                    Seats.Clear();
                    selectedSeat = null;
                    SelectedModel.Option
                        .Where(i => i.OptionTypeFK == 5).ToList()
                        .ForEach(i => Seats.Add(i));

                    Upholsteries.Clear();
                    selectedUpholstery = null;
                    SelectedModel.Option
                        .Where(i => i.OptionTypeFK == 6).ToList()
                        .ForEach(i => Upholsteries.Add(i));

                    Steerings.Clear();
                    selectedSteering = null;
                    SelectedModel.Option
                        .Where(i => i.OptionTypeFK == 7).ToList()
                        .ForEach(i => Steerings.Add(i));

                    Heatings.Clear();
                    selectedHeating = null;
                    SelectedModel.Option
                        .Where(i => i.OptionTypeFK == 8).ToList()
                        .ForEach(i => Heatings.Add(i));

                    Sills.Clear();
                    selectedSills = null;
                    SelectedModel.Option
                        .Where(i => i.OptionTypeFK == 9).ToList()
                        .ForEach(i => Sills.Add(i));

                    Lightings.Clear();
                    selectedLighting = null;
                    SelectedModel.Option
                        .Where(i => i.OptionTypeFK == 10).ToList()
                        .ForEach(i => Lightings.Add(i));

                    Others.Clear();
                    selectedOthers = new List<OptionModel>();
                    SelectedModel.Option
                        .Where(i => i.OptionTypeFK == 11)
                        .Where(i => i.Price > 0)
                        .Select(i => new OptionModel() { option = i }).ToList()
                        .ForEach(i => Others.Add(i));

                    VehicleImage = SelectedModel.Model_Color.ToList().Select(i => i.Image).FirstOrDefault();

                    calcTotalPrice();
                }
                catch { }
            }
        }

        public Engine SelectedEngine
        {
            get { return selectedEngine; }
            set
            {
                selectedEngine = value;
                OnPropertyChanged("SelectedEngine");

                calcTotalPrice();
            }
        }

        public Models.Color SelectedColor
        {
            get { return selectedColor; }
            set
            {
                selectedColor = value;
                OnPropertyChanged("SelectedColor");

                calcTotalPrice();
            }
        }



        public Option SelectedHeadlights
        {
            get { return selectedHeadlights; }
            set
            {
                selectedHeadlights = value;
                OnPropertyChanged("SelectedHeadlights");

                calcTotalPrice();
            }
        }

        public Option SelectedWheels
        {
            get { return selectedWheels; }
            set
            {
                selectedWheels = value;
                OnPropertyChanged("SelectedWheels");

                calcTotalPrice();
            }
        }

        public Option SelectedMirrors
        {
            get { return selectedMirrors; }
            set
            {
                selectedMirrors = value;
                OnPropertyChanged("SelectedMirrors");

                calcTotalPrice();
            }
        }

        public Option SelectedWing
        {
            get { return selectedWing; }
            set
            {
                selectedWing = value;
                OnPropertyChanged("SelectedWing");

                calcTotalPrice();
            }
        }

        public Option SelectedSeat
        {
            get { return selectedSeat; }
            set
            {
                selectedSeat = value;
                OnPropertyChanged("SelectedSeat");

                calcTotalPrice();
            }
        }

        public Option SelectedUpholstery
        {
            get { return selectedUpholstery; }
            set
            {
                selectedUpholstery = value;
                OnPropertyChanged("SelectedUpholstery");

                calcTotalPrice();
            }
        }

        public Option SelectedSteering
        {
            get { return selectedSteering; }
            set
            {
                selectedSteering = value;
                OnPropertyChanged("SelectedSteering");

                calcTotalPrice();
            }
        }

        public Option SelectedHeating
        {
            get { return selectedHeating; }
            set
            {
                selectedHeating = value;
                OnPropertyChanged("SelectedHeating");

                calcTotalPrice();
            }
        }

        public Option SelectedSills
        {
            get { return selectedSills; }
            set
            {
                selectedSills = value;
                OnPropertyChanged("SelectedSills");

                calcTotalPrice();
            }
        }

        public Option SelectedLighting
        {
            get { return selectedLighting; }
            set
            {
                selectedLighting = value;
                OnPropertyChanged("SelectedLighting");

                calcTotalPrice();
            }
        }

        public List<OptionModel> SelectedOthers
        {
            get { return selectedOthers; }
            set
            {
                selectedOthers = value;
                OnPropertyChanged("SelectedOthers");

                calcTotalPrice();
            }
        }

        public string TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        public byte[] VehicleImage
        {
            get { return vehicleImage; }
            set
            {
                vehicleImage = value;
                OnPropertyChanged("VehicleImage");
            }
        }

        private RelayCommand selectCommand;
        public RelayCommand SelectCommand
        {
            get
            {
                if (selectCommand == null) selectCommand = new RelayCommand(x => MultiSelect());
                return selectCommand;
            }
            set { selectCommand = value; }
        }
        private void MultiSelect()
        {
            SelectedOthers.Clear();

            foreach (var item in Others.Where(x => x.IsSelected).ToList())
                selectedOthers.Add(item);

            calcTotalPrice();
        }

        private void calcTotalPrice()
        {
            int price = 0;
            vehicle = new VehicleModel();
            vehicle.vehicle = new Vehicle();
            vehicle.options = new List<Option>();

            if (selectedModel != null)
            {
                price += Convert.ToInt32(selectedModel.Price);
                vehicle.model = selectedModel.Name;
            }
            if (selectedEngine != null)
            {
                price += Convert.ToInt32(selectedEngine.Price);
                vehicle.engineName = selectedEngine.Name;
                vehicle.enginePower = selectedEngine.Power;
                vehicle.engineType = selectedEngine.Type;
                vehicle.vehicle.EngineFK = selectedEngine.Id;
            }
            if (selectedColor != null)
            {
                price += Convert.ToInt32(selectedColor.Price);
                vehicle.color = selectedColor.Name;
                vehicle.vehicle.ColorFK = selectedColor.Id;
            }
            if (selectedHeadlights != null)
            {
                price += Convert.ToInt32(selectedHeadlights.Price);
                vehicle.options.Add(selectedHeadlights);
            }
            if (selectedWheels != null)
            {
                price += Convert.ToInt32(selectedWheels.Price);
                vehicle.options.Add(selectedWheels);
            }
            if (selectedMirrors != null)
            {
                price += Convert.ToInt32(selectedMirrors.Price);
                vehicle.options.Add(selectedMirrors);
            }
            if (selectedWing != null)
            {
                price += Convert.ToInt32(selectedWing.Price);
                vehicle.options.Add(selectedWing);
            }
            if (selectedSeat != null)
            {
                price += Convert.ToInt32(selectedSeat.Price);
                vehicle.options.Add(selectedSeat);
            }
            if (selectedUpholstery != null)
            {
                price += Convert.ToInt32(selectedUpholstery.Price);
                vehicle.options.Add(selectedUpholstery);
            }
            if (selectedSteering != null)
            {
                price += Convert.ToInt32(selectedSteering.Price);
                vehicle.options.Add(selectedSteering);
            }
            if (selectedHeating != null)
            {
                price += Convert.ToInt32(selectedHeating.Price);
                vehicle.options.Add(selectedHeating);
            }
            if (selectedSills != null)
            {
                price += Convert.ToInt32(selectedSills.Price);
                vehicle.options.Add(selectedSills);
            }
            if (selectedLighting != null)
            {
                price += Convert.ToInt32(selectedLighting.Price);
                vehicle.options.Add(selectedLighting);
            }
            if (selectedOthers != null)
            {
                foreach (var item in selectedOthers)
                {
                    price += Convert.ToInt32(item.option.Price);
                    vehicle.options.Add(item.option);
                }
            }

            vehicle.totalPrice = TotalPrice = price.ToString();
        }

        private bool isVehicleBuild()
        {
            return (selectedEngine != null && selectedColor != null && selectedWheels != null && selectedHeadlights != null && 
                selectedMirrors != null && selectedWing != null && selectedSeat != null && selectedUpholstery != null && 
                selectedSteering != null && selectedHeating != null && selectedSills != null && selectedLighting != null && selectedOthers.FirstOrDefault() != null);
        }

        public Frame Main
        {
            get { return main; }
            set { main = value; }
        }

        public MainWindow Window
        {
            get { return window; }
            set { window = value; }
        }

        public BuildVehiclePage BuildVehiclePage
        {
            get { return buildVehiclePage; }
            set { buildVehiclePage = value; }
        }

        private RelayCommand contract;
        public RelayCommand Contract
        {
            get
            {
                return contract ??
                  (contract = new RelayCommand(obj =>
                  {
                      main.Content = new ContractPage(this, vehicle);
                      window.hideSideBar();
                  },
                  obj => isVehicleBuild() == true));
            }
        }

        public BuildVehicleVM(Frame main, MainWindow window, BuildVehiclePage buildVehiclePage)
        {
            this.buildVehiclePage = buildVehiclePage;
            this.window = window;
            this.main = main;
            db = new CarsContext();

            Engines = new ObservableCollection<Engine>(db.Engine.ToList());
            Colors = new ObservableCollection<Models.Color>(db.Color.ToList());
            Headlights = new ObservableCollection<Option>(db.Option.ToList());
            Wheels = new ObservableCollection<Option>();
            Mirrors = new ObservableCollection<Option>();
            Wings = new ObservableCollection<Option>();
            Seats = new ObservableCollection<Option>();
            Upholsteries = new ObservableCollection<Option>();
            Steerings = new ObservableCollection<Option>();
            Heatings = new ObservableCollection<Option>();
            Sills = new ObservableCollection<Option>();
            Lightings = new ObservableCollection<Option>();
            Others = new ObservableCollection<OptionModel>();
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
