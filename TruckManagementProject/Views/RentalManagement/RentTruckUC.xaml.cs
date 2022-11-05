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

namespace TruckManagementProject.Views.RentalManagement
{
    /// <summary>
    /// Interaction logic for RentTruckUC.xaml
    /// </summary>
    public partial class RentTruckUC : UserControl
    {
        public RentTruckUC()
        {  //Shows Available Trucks for rent by Registration Number in the Regostration Combo Box 
            InitializeComponent();
            TruckRegoComboBox.ItemsSource = DAO.getIndividualTrucks();
            TruckRegoComboBox.DisplayMemberPath = "RegistrationNumber";
            TruckRegoComboBox.SelectedValuePath = "RegistrationNumber";
        }


        //Shows the correct License Number According to the Inputed Customer Name
        private void CustomerNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            string customerName = CustomerNameTextBox.Text;
            //DAO.getCustomerName(customerName);
            //string customerLicense = CustomerLiscenseComboBox.Text;
            CustomerLiscenseComboBox.ItemsSource = DAO.getCustomerNameLicense(customerName);
            CustomerLiscenseComboBox.DisplayMemberPath = "LicenseNumber" + DAO.getCustomerName(customerName);
            CustomerLiscenseComboBox.SelectedValuePath = "LicenseNumber" + DAO.getCustomerName(customerName);
        }

        // creates an Object of truck rental and adds any inputed values to the Object
        private void RentTruckButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string registraion = TruckRegoComboBox.Text;
                string customerLicense = CustomerLiscenseComboBox.Text;
                int TruckId = DAO.getTruckRego(registraion).TruckId;
                int customerId = DAO.getCustomerLicense(customerLicense).CustomerId;
                DateTime rentDate = DateTime.Parse(DateRentedPicker.Text);
                DateTime dueDate = DateTime.Parse(DateDuePicker.Text);
                decimal totalPrice = decimal.Parse(priceTextBox.Text);

                TruckRental tr = new TruckRental();
                tr.TruckId = TruckId;
                tr.CustomerId = customerId;
                tr.RentDate = rentDate;
                tr.ReturnDueDate = dueDate;
                tr.TotalPrice = totalPrice;

                DAO.rentTruck(tr, TruckId);
                MessageBox.Show("success");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Get the advanced deposit according to the registration
        private void TruckRegoComboBox_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string registration = TruckRegoComboBox.Text;
                if (registration != null)
                {
                    string deposit = string.Format("{0:F2}", DAO.getTruckRego(registration).AdvanceDepositRequired);
                    DepositTextBox.Text = deposit;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
