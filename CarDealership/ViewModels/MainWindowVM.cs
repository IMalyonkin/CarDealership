using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CarDealership.ViewModels
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        MainWindow window;
        Frame main;

        public MainWindowVM(MainWindow window, Frame main)
        {
            this.window = window;
            this.main = main;
        }

        private RelayCommand menuBtn;
        public RelayCommand MenuBtn
        {
            get
            {
                return menuBtn ??
                  (menuBtn = new RelayCommand(obj =>
                  {
                      if (window.SideBar.Width == 50)
                          window.SideBar.Width = 220;
                      else window.SideBar.Width = 50;
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
                      window.Close();
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
                      main.Content = new VehiclesInStockPage(main, window);
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
                      main.Content = new BuildVehiclePage(main, window);
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
                      main.Content = new StatisticPage();
                  }));
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
