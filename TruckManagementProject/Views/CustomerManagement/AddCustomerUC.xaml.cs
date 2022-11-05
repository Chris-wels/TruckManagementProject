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
using TruckManagementProject.Models.DB;
using TruckManagementProject.Controller;

namespace TruckManagementProject.Views.CustomerManagement
{
    /// <summary>
    /// Interaction logic for AddCustomerUC.xaml
    /// </summary>
    public partial class AddCustomerUC : UserControl
    {
        public AddCustomerUC()
        {
            InitializeComponent();
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            string licenseNumber = licenseNumberTextBox.Text;
            int age = int.Parse(ageTextBox.Text);
            DateTime licenseExpiryDate = expiryDatePicker.SelectedDate.Value;
            string name = nameTextBox.Text;
            string address = addressTextBox.Text;
            string telephone = telephoneTextBox.Text;

            //grabs person class
            TruckPerson p = new TruckPerson();
            p.Name = name;
            p.Address = address;
            p.Telephone = telephone;

            //grabs Customer class
            TruckCustomer cust = new TruckCustomer();
            cust.CustomerId = p.PersonId;
            cust.LicenseNumber = licenseNumber;
            cust.Age = age;
            cust.LicenseExpiryDate = licenseExpiryDate;

            cust.Customer = p;
            try
            {
                DAO.addCustomer(cust);
                MessageBox.Show("New record added ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
