using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CarDealership.Models;

namespace CarDealership
{
    public class logInVM : INotifyPropertyChanged
    {
        logInWindow logInWindow;
        private CarsContext db;
        private string login;
        private string password;

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

        private RelayCommand check;
        public RelayCommand Check
        {
            get
            {
                return check ??
                  (check = new RelayCommand(obj =>
                  {
                      findEmp();
                  }));
            }
        }

        private void findEmp()
        {
            List<Employee> employees = new List<Employee>();
            employees = db.Employee.ToList().Where(i => i.Login == login && i.Password == password).ToList();

            if (employees.FirstOrDefault() != null)
            {
                MainWindow mainWindow = new MainWindow(employees.FirstOrDefault());
                logInWindow.Close();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        public logInVM(logInWindow logInWindow)
        {
            this.logInWindow = logInWindow;
            db = new CarsContext();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
