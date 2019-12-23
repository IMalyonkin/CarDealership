using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using CarDealership.ViewModels;

namespace CarDealership
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Employee employee;
        public Employee Employee
        {
            get { return employee; }
            set { employee = value; }
        }

        public MainWindow()
        {
            InitializeComponent();

            SideBar.Width = 50;

            DataContext = new MainWindowVM(this, Main);
        }

        public MainWindow(Employee employee)
        {
            InitializeComponent();

            this.employee = employee;

            Main.Content = new VehiclesInStockPage(Main, this);
        }

        public void hideSideBar()
        {
            SideBar.Width = 0;
        }

        public void returnSideBar()
        {
            SideBar.Width = 50;
        }

        private RelayCommand menuBtn;
        public RelayCommand MenuBtn
        {
            get
            {
                return menuBtn ??
                  (menuBtn = new RelayCommand(obj =>
                  {
                      if (SideBar.Width == 50)
                          SideBar.Width = 220;
                      else SideBar.Width = 50;
                  }));
            }
        }

        private RelayCommand exitBtn;
        public RelayCommand ExitBtn
        {
            get
            {
                return exitBtn ??
                  (exitBtn = new RelayCommand(obj =>
                  {
                      logInWindow logInWindow = new logInWindow();
                      this.Close();
                      logInWindow.Show();
                  }));
            }
        }

        private RelayCommand vehiclesInStockPageBtn;
        public RelayCommand VehiclesInStockPageBtn
        {
            get
            {
                return vehiclesInStockPageBtn ??
                  (vehiclesInStockPageBtn = new RelayCommand(obj =>
                  {
                      Main.Content = new VehiclesInStockPage(Main, this);
                  }));
            }
        }

        private RelayCommand buildVehiclePageBtn;
        public RelayCommand BuildVehiclePageBtn
        {
            get
            {
                return buildVehiclePageBtn ??
                  (buildVehiclePageBtn = new RelayCommand(obj =>
                  {
                      Main.Content = new BuildVehiclePage(Main, this);
                  }));
            }
        }

        private RelayCommand statisticPageBtn;
        public RelayCommand StatisticPageBtn
        {
            get
            {
                return statisticPageBtn ??
                  (statisticPageBtn = new RelayCommand(obj =>
                  {
                      Main.Content = new StatisticPage();
                  }));
            }
        }

        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            if (SideBar.Width == 50)
                SideBar.Width = 220;
            else SideBar.Width = 50;

            SideBar.InvalidateVisual();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new VehiclesInStockPage(Main, this);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new BuildVehiclePage(Main, this);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new StatisticPage();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            logInWindow logInWindow = new logInWindow();
            this.Close();
            logInWindow.Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
