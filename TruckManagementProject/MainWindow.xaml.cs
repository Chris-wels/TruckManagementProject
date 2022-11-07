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
using TruckManagementProject.Controller;
using TruckManagementProject.Models.DB;
using TruckManagementProject.Views;

namespace TruckManagementProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string pwd = passwordTextBox.Text;
            TruckEmployee em = DAO.userLogin(username, pwd);
            if (em == null)
            {
                MessageBox.Show(" Sorry Nothing was found");
            }
            else if (em.Role == "admin")
            {
                AdminDashboard adminForm = new AdminDashboard();
                adminForm.Show();
                this.Hide();

            }
            else if (em.Role == "staff")
            {
                AdminDashboard form = new AdminDashboard();
                form.DashboardLabel.Text = "Welcome Staff Members";
                form.EmployeeMenu.Visibility = Visibility.Collapsed;
                form.addTruckMenuItem.Visibility = Visibility.Collapsed;
                form.updateTruckMenuItem.Visibility = Visibility.Collapsed;
                form.showAllTruckMenuItem.Visibility = Visibility.Collapsed;
                form.Show();
                this.Hide();

            }
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

        private void usernameTextBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            usernameTextBox.Text = "";

        }

        private void passwordTextBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            passwordTextBox.Text = "";

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
