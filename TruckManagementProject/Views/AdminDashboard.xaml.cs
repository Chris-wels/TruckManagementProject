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
using System.Windows.Shapes;
using TruckManagementProject.Views.CustomerManagement;
using TruckManagementProject.Views.RentalManagement;
using TruckManagementProject.Views.TruckManagement;

namespace TruckManagementProject.Views
{
    /// <summary>
    /// Interaction logic for AdminDashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Window
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

        private void closeImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void resizeImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)

            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
        private void addEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            centralPanel.Children.Clear();
            centralPanel.Children.Add(new AddEmployeeUC());
        }

        private void updateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            centralPanel.Children.Clear();
            centralPanel.Children.Add(new UpdateEmployeeUC());
        }

        private void updateCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            centralPanel.Children.Clear();
            centralPanel.Children.Add(new UpdateCustomerUC());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Hide();
        }



        private void LogOutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Hide();
        }

        private void returnRentalMenuItem_Click(object sender, RoutedEventArgs e)
        {
            centralPanel.Children.Clear();
            centralPanel.Children.Add(new ReturnTruckUC());
        }

        private void showRentalByUser_Click(object sender, RoutedEventArgs e)
        {
            centralPanel.Children.Clear();
            centralPanel.Children.Add(new RentalsByCustomerUC());
        }

        private void showRentalsByDate_Click(object sender, RoutedEventArgs e)
        {
            centralPanel.Children.Clear();
            centralPanel.Children.Add(new RentalsByDateUC());
        }

        private void rentTruckMenuItem_Click(object sender, RoutedEventArgs e)
        {
            centralPanel.Children.Clear();
            centralPanel.Children.Add(new RentTruckUC());
        }

        private void addCustomerMenuItem_Click(object sender, RoutedEventArgs e)
        {
            centralPanel.Children.Clear();
            centralPanel.Children.Add(new AddCustomerUC());
        }

        private void addTruckMenuItem_Click(object sender, RoutedEventArgs e)
        {
            centralPanel.Children.Clear();
            centralPanel.Children.Add(new AddTruckUC());
        }

        private void updateTruckMenuItem_Click(object sender, RoutedEventArgs e)
        {
            centralPanel.Children.Clear();
            centralPanel.Children.Add(new UpdateTruckUC());
        }

        private void showAllTruckMenuItem_Click(object sender, RoutedEventArgs e)
        {
            centralPanel.Children.Clear();
            centralPanel.Children.Add(new ShowAllTruckUC());
        }

        private void showAvailTruckMenuItem_Click(object sender, RoutedEventArgs e)
        {
            centralPanel.Children.Clear();
            centralPanel.Children.Add(new ShowAvailableTruckUC());
        }

        private void searchTruckByModelMenuItem_Click(object sender, RoutedEventArgs e)
        {
            centralPanel.Children.Clear();
            centralPanel.Children.Add(new SearchTruckByModelUC());
        }

      
    }
}
