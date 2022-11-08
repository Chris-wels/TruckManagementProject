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
        {  //---------Shows Available Trucks for rent by Registration Number in the Regostration Combo Box 
            InitializeComponent();
            TruckRegoComboBox.ItemsSource = DAO.getAvailableTrucks();
            TruckRegoComboBox.DisplayMemberPath = "RegistrationNumber";
            TruckRegoComboBox.SelectedValuePath = "RegistrationNumber";
        }


        //---------Shows the correct License Number According to the Inputed Customer Name
        private void CustomerNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            string customerName = CustomerNameTextBox.Text;
            //DAO.getCustomerName(customerName);
            //string customerLicense = CustomerLiscenseComboBox.Text;
            CustomerLiscenseComboBox.ItemsSource = DAO.getCustomerNameLicense(customerName);
            CustomerLiscenseComboBox.DisplayMemberPath = "LicenseNumber";
            CustomerLiscenseComboBox.SelectedValuePath = "LicenseNumber";
            

            
        }

        //---------- creates an Object of truck rental and adds any inputed values to the Object
        private void RentTruckButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string registraion = TruckRegoComboBox.Text;
                string customerLicense = CustomerLiscenseComboBox.Text;
                int TruckId = DAO.getTruckRego(registraion).TruckId;
                int customerId = DAO.getCustomerLicense(customerLicense).CustomerId;
                DateTime rentDate = DateTime.Parse(DateRentedPicker.SelectedDate.ToString());
                DateTime dueDate = DateTime.Parse(DateDuePicker.SelectedDate.ToString());
                decimal totalPrice = decimal.Parse(priceTextBox.Text);

                TruckRental tr = new TruckRental();
                tr.TruckId = TruckId;
                tr.CustomerId = customerId;
                tr.RentDate = rentDate;
                tr.ReturnDueDate = dueDate;
                tr.TotalPrice = totalPrice;

                DAO.rentTruck(tr, TruckId);
                MessageBox.Show("Truck Rented Successfully");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-----------Get the advanced deposit according to the registration
        private void TruckRegoComboBox_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string registration = TruckRegoComboBox.Text;
                if (registration == null)

                {
                    MessageBox.Show("please select a truck registration");
                    
                
                } else{
                    string deposit = string.Format("{0:F2}", DAO.getTruckRego(registration).AdvanceDepositRequired);//Not sure how to fix this error Make sure to select a truck registration otherwise this error will occur with this event handler
                    DepositTextBox.Text = deposit;
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //----------Calculates Date Rented and Date Due Total price amount
        private void DateDuePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DateDuePicker.SelectedDate< DateTime.Today && DateDuePicker.SelectedDate < DateRentedPicker.SelectedDate) 
            {
                MessageBox.Show("Selected Due Date cannot be before Date Rented");
                DateRentedPicker.Text = "";
            }
            else 
            {
                string rego = TruckRegoComboBox.Text;
                double price = (double)DAO.getTruckRego(rego).DailyRentalPrice;
                DateTime rentDate = DateTime.Parse(DateRentedPicker.SelectedDate.ToString());
                DateTime dueDate = DateTime.Parse(DateDuePicker.SelectedDate.ToString());
                int days = (int)(dueDate - rentDate).TotalDays;
                
                if (dueDate == rentDate)
                {
                    days = 1;
                }
                double totalPrice = days * price;

                priceTextBox.Text = String.Format("{0:f2}",totalPrice);

            }
        }

        //----Sets Rent Date to Today
        private void DateRentedPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateRentedPicker.SelectedDate = DateTime.Today;
        }
    }
}
