using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models;
using CarDealership.BLL;

namespace CarDealership.ViewModels
{
    public class StaticticVM : INotifyPropertyChanged
    {
        CarsContext db;

        private DateTime selectedDate1;
        private DateTime selectedDate2;
        private DateTime baseDate1;
        private DateTime baseDate2;
        private Model selectedModel;
        private Employee selectedEmployee;
        private string sum;

        public ObservableCollection<Model> Models { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<ContractModel> allContracts { get; set; }
        public ObservableCollection<ContractModel> selectedContracts { get; set; }

        public string Sum
        {
            get { return sum; }
            set
            {
                sum = value;
                OnPropertyChanged("Sum");
            }
        }

        public DateTime SelectedDate1
        {
            get { return selectedDate1; }
            set
            {
                selectedDate1 = value;
                OnPropertyChanged("SelectedDate1");

                getContracts();
            }
        }

        public DateTime SelectedDate2
        {
            get { return selectedDate2; }
            set
            {
                selectedDate2 = value;
                OnPropertyChanged("SelectedDate2");

                getContracts();
            }
        }

        public Model SelectedModel
        {
            get { return selectedModel; }
            set
            {
                selectedModel = value;
                OnPropertyChanged("SelectedModel");

                getContracts();
            }
        }

        public Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");

                getContracts();
            }
        }

        private RelayCommand clearDate1;
        public RelayCommand ClearDate1
        {
            get
            {
                return clearDate1 ??
                  (clearDate1 = new RelayCommand(obj =>
                  {
                      SelectedDate1 = baseDate1;
                      getContracts();
                  }));
            }
        }

        private RelayCommand clearDate2;
        public RelayCommand ClearDate2
        {
            get
            {
                return clearDate2 ??
                  (clearDate2 = new RelayCommand(obj =>
                  {
                      SelectedDate2 = baseDate2;
                      getContracts();
                  }));
            }
        }

        private RelayCommand clearModel;
        public RelayCommand ClearModel
        {
            get
            {
                return clearModel ??
                  (clearModel = new RelayCommand(obj =>
                  {
                      SelectedModel = null;
                      getContracts();
                  }));
            }
        }

        private RelayCommand clearEmployee;
        public RelayCommand ClearEmployee
        {
            get
            {
                return clearEmployee ??
                  (clearEmployee = new RelayCommand(obj =>
                  {
                      SelectedEmployee = null;
                      getContracts();
                  }));
            }
        }

        void getContracts()
        {
            selectedContracts.Clear();
            allContracts.ToList().ForEach(i => selectedContracts.Add(i));

            if (SelectedModel != null)
            {
                List<ContractModel> tmp = selectedContracts.ToList().Where(i => i.modelId == selectedModel.Id).ToList();
                selectedContracts.Clear();
                tmp.ForEach(i => selectedContracts.Add(i));
            }

            if (SelectedEmployee != null)
            {
                List<ContractModel> tmp = selectedContracts.ToList().Where(i => i.employeeId == selectedEmployee.Id).ToList();
                selectedContracts.Clear();
                tmp.ForEach(i => selectedContracts.Add(i));
            }

            if (SelectedDate1 != baseDate1)
            {
                List<ContractModel> tmp = selectedContracts.ToList().Where(i => dateCompare(i, selectedDate1)).ToList();
                selectedContracts.Clear();
                tmp.ForEach(i => selectedContracts.Add(i));
            }

            if (SelectedDate2 != baseDate2)
            {
                List<ContractModel> tmp = selectedContracts.ToList().Where(i => !dateCompare(i, selectedDate2)).ToList();
                selectedContracts.Clear();
                tmp.ForEach(i => selectedContracts.Add(i));
            }

            int s = 0;
            foreach (var item in selectedContracts)
            {
                s += (int)(item.contract.Total_Price);
            }
            Sum = s.ToString();
        }

        bool dateCompare(ContractModel i, DateTime selectedDate)
        {
            if (DateTime.Compare(selectedDate, i.contract.Date) <= 0) return true;
            else return false;
        }

        public StaticticVM()
        {
            db = new CarsContext();
            baseDate1 = new DateTime(2019, 12, 20);
            baseDate2 = DateTime.Now;
            selectedDate1 = baseDate1;
            selectedDate2 = baseDate2;
            allContracts = new ObservableCollection<ContractModel>(db.Contract.ToList()
                                                                        .Where(i => i.Type == "Покупка")
                                                                        .Select(i => new ContractModel()
                                                                        {
                                                                            contract = i,
                                                                            model = i.Vehicle.Kit.Model.Name,
                                                                            modelId = i.Vehicle.Kit.Model.Id,
                                                                            kit = i.Vehicle.Kit.Name,
                                                                            client = i.Client.Name,
                                                                            employee = i.Employee.Name,
                                                                            employeeId = i.Employee.Id,
                                                                            date = i.Date.ToString("dd.MM.yyyy")
                                                                        }));
            selectedContracts = new ObservableCollection<ContractModel>();
            allContracts.ToList().ForEach(i => selectedContracts.Add(i));

            int s = 0;
            foreach (var item in selectedContracts)
            {
                s += (int)(item.contract.Total_Price);
            }
            Sum = s.ToString();

            Models = new ObservableCollection<Model>(db.Model.ToList());
            Employees = new ObservableCollection<Employee>(db.Employee.ToList());


        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
