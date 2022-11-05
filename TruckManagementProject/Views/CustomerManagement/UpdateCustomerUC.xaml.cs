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

namespace TruckManagementProject.Views.CustomerManagement
{
    /// <summary>
    /// Interaction logic for UpdateCustomerUC.xaml
    /// </summary>
    public partial class UpdateCustomerUC : UserControl
    {
        TruckCustomer cust = null;
        TruckPerson cp;
        public UpdateCustomerUC()
        {
            InitializeComponent();
        }

        private void SearchCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            //Searching employee using the Id
            int cid = int.Parse(customerIDTextBox.Text);
            // Uses the DAO controller method to search for the customer by ID and retrieve all details
            cust = DAO.searchCustomer(cid);
            if (cust == null)//if no records are found show message
            {
                MessageBox.Show("sorry no customer found");
            }
            else
            {
                //if details are found assign each textbox the corresponding values

                licenseNumberTextBox.Text = cust.LicenseNumber;
                ageTextBox.Text = cust.Age.ToString();
                expiryDatePicker.SelectedDate = cust.LicenseExpiryDate;

                nameTextBox.Text = cust.Customer.Name;
                addressTextBox.Text = cust.Customer.Address;
                telephoneTextBox.Text = cust.Customer.Telephone;
            }
        }

        private void UpdateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            TruckPerson cp = cust.Customer;
            cust.LicenseNumber = licenseNumberTextBox.Text;
            cust.Age = int.Parse(ageTextBox.Text);
            cp.Name = nameTextBox.Text;
            cp.Address = addressTextBox.Text;
            cp.Telephone = telephoneTextBox.Text;
            DAO.updateCustomer(cust, cp);
            MessageBox.Show("Updated Successfully");
        }
    }
}
