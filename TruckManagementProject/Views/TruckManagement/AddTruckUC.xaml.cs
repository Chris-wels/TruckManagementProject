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

namespace TruckManagementProject.Views.TruckManagement
{
    /// <summary>
    /// Interaction logic for AddTruckUC.xaml
    /// </summary>
    public partial class AddTruckUC : UserControl
    {
        public AddTruckUC()
        {
            InitializeComponent();
        }

        private void addTruckButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //First Column
                string model = modelTextBox.Text;
                string manufacturer = manufacturerTextBox.Text;
                string size = sizeCombobox.Text;
                int seats = int.Parse(seatsTextBox.Text);
                int doors = int.Parse(doorsTextBox.Text);
                string colour = colourTextBox.Text;
                string registrationNo = registrationNoTextBox.Text;
                DateTime WOFExpiry = DateTime.Parse(WOFExpiryDatePicker.Text);

                //Second Column
                DateTime regisExpiry = DateTime.Parse(regisExpiryDatePicker.Text);
                DateTime dateImported = DateTime.Parse(dateImportedDatePicker.Text);
                int manufacturerYear = int.Parse(manufacturerYearTextbox.Text);
                string status = statusComboBox.Text;
                string fuelType = fuelTypeComboBox.Text;
                string transmission = transmissionTypeComboBox.Text;
                //decimal values
                decimal rentalPrice = decimal.Parse(rentalPriceTextbox.Text);
                decimal advanceDeposit = decimal.Parse(advanceDepositTextbox.Text);

                /*------------- TRUCK MODEL-------------------------------------------------------------------------------*/
                //Creating TruckModel Object to store the inputs
                TruckModel truckM = new TruckModel();
                truckM.Model = model;
                truckM.Manufacturer = manufacturer;
                truckM.Size = size;
                truckM.Seats = seats;
                truckM.Doors = doors;

                /*--------------INDIVIDUAL TRUCK-------------------------------------------------------------------------------*/
                //Creating Individual Truck Object to store the inputs
                IndividualTruck individualT = new IndividualTruck();
                individualT.Colour = colour;
                individualT.RegistrationNumber = registrationNo;
                individualT.WofexpiryDate = WOFExpiry;
                individualT.RegistrationExpiryDate = regisExpiry;
                individualT.DateImported = dateImported;
                individualT.ManufactureYear = manufacturerYear;
                individualT.Status = status;
                individualT.FuelType = fuelType;
                individualT.Transmission = transmission;
                individualT.DailyRentalPrice = rentalPrice;
                individualT.AdvanceDepositRequired = advanceDeposit;
                individualT.TruckModel = truckM;

                /*--------------TRUCK FEATURE-------------------------------------------------------------------------------*/
                //Getting featureID from TruckFeature table
                TruckFeature featureOne = DAO.searchFeatureById(1);
                TruckFeature featureTwo = DAO.searchFeatureById(2);
                TruckFeature featureThree = DAO.searchFeatureById(3);
                TruckFeature featureFour = DAO.searchFeatureById(4);

                //Create list to store each feature with corresponding number selected by the user
                List<TruckFeature> association = new List<TruckFeature>();
                if (airconCheckbox.IsChecked == true)
                {
                    association.Add(featureOne);

                }
                else if (reardoorCheckbox.IsChecked == true)
                {
                    association.Add(featureTwo);
                }
                else if (alarmsystemCheckbox.IsChecked == true)
                {
                    association.Add(featureThree);
                }
                else if (keylesdoorCheckbox.IsChecked == true)
                {
                    association.Add(featureFour);
                }
                else
                {
                    MessageBox.Show("Please select at least one truck feature");
                }

                //Calling addTruck from DAO class and adding truck details + feature list from checkboxes
                DAO.addTruck(individualT, association);
                MessageBox.Show("New truck record added successfully!");

                //Clearing values
                modelTextBox.Clear();
                manufacturerTextBox.Clear();
                sizeCombobox.SelectedIndex = -1;
                seatsTextBox.Clear();
                doorsTextBox.Clear();
                colourTextBox.Clear();
                registrationNoTextBox.Clear();
                WOFExpiryDatePicker.SelectedDate = null;
                regisExpiryDatePicker.SelectedDate = null;
                dateImportedDatePicker.SelectedDate = null;
                manufacturerYearTextbox.Clear();
                statusComboBox.SelectedIndex = -1;
                fuelTypeComboBox.SelectedIndex = -1;
                transmissionTypeComboBox.SelectedIndex = -1;
                rentalPriceTextbox.Clear();
                advanceDepositTextbox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
