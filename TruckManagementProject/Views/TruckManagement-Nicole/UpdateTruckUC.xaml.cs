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
    /// Interaction logic for UpdateTruckUC.xaml
    /// </summary>
    public partial class UpdateTruckUC : UserControl
    {
        IndividualTruck indiTruck = null;
        public UpdateTruckUC()
        {
            InitializeComponent();
            featureComboBox.ItemsSource = DAO.getFeatures();
            featureComboBox.DisplayMemberPath = "Description";
            featureComboBox.SelectedValuePath = "Description";
            //calling hideControls function to hide truck details upon execution
            hideControls(true);
            listbox1.ItemsSource = DAO.getFeatures();
        }
        private void hideControls(bool value)
        {
            if (value)
            {
                modelLabel.Visibility = Visibility.Hidden;
                modelTextBox.Visibility = Visibility.Hidden;
                manufacturerLabel.Visibility = Visibility.Hidden;
                manufacturerTextBox.Visibility = Visibility.Hidden;
                sizeLabel.Visibility = Visibility.Hidden;
                sizeCombobox.Visibility = Visibility.Hidden;
                seatsLabel.Visibility = Visibility.Hidden;
                seatsTextBox.Visibility = Visibility.Hidden;
                doorsLabel.Visibility = Visibility.Hidden;
                doorsTextBox.Visibility = Visibility.Hidden;
                colourLabel.Visibility = Visibility.Hidden;
                colourTextBox.Visibility = Visibility.Hidden;
                registrationNoLabel.Visibility = Visibility.Hidden;
                registrationNoTextBox.Visibility = Visibility.Hidden;
                WOFExpiryDateLabel.Visibility = Visibility.Hidden;
                WOFExpiryDatePicker.Visibility = Visibility.Hidden;
                regisExpiryDateLabel.Visibility = Visibility.Hidden;
                regisExpiryDatePicker.Visibility = Visibility.Hidden;
                dateImportedLabel.Visibility = Visibility.Hidden;
                dateImportedDatePicker.Visibility = Visibility.Hidden;

                //Second Column
                manufactureYearLabel.Visibility = Visibility.Hidden;
                manufactureYearTextbox.Visibility = Visibility.Hidden;
                statusLabel.Visibility = Visibility.Hidden;
                statusComboBox.Visibility = Visibility.Hidden;
                fuelTypeLabel.Visibility = Visibility.Hidden;
                fuelTypeComboBox.Visibility = Visibility.Hidden;
                transmissionTypeLabel.Visibility = Visibility.Hidden;
                transmissionTypeComboBox.Visibility = Visibility.Hidden;
                rentalPriceLabel.Visibility = Visibility.Hidden;
                rentalPriceTextbox.Visibility = Visibility.Hidden;
                advanceDepositLabel.Visibility = Visibility.Hidden;
                advanceDepositTextbox.Visibility = Visibility.Hidden;
                featuresLabel.Visibility = Visibility.Hidden;
                updateButton.Visibility = Visibility.Hidden;
                featureComboBox.Visibility = Visibility.Hidden;
                listbox1.Visibility = Visibility.Hidden;
                addFeatureButton.Visibility = Visibility.Hidden;
                removeFeatureButton.Visibility = Visibility.Hidden;
            }
            else
            {
                modelLabel.Visibility = Visibility.Visible;
                modelTextBox.Visibility = Visibility.Visible;
                manufacturerLabel.Visibility = Visibility.Visible;
                manufacturerTextBox.Visibility = Visibility.Visible;
                sizeLabel.Visibility = Visibility.Visible;
                sizeCombobox.Visibility = Visibility.Visible;
                seatsLabel.Visibility = Visibility.Visible;
                seatsTextBox.Visibility = Visibility.Visible;
                doorsLabel.Visibility = Visibility.Visible;
                doorsTextBox.Visibility = Visibility.Visible;
                colourLabel.Visibility = Visibility.Visible;
                colourTextBox.Visibility = Visibility.Visible;
                registrationNoLabel.Visibility = Visibility.Visible;
                registrationNoTextBox.Visibility = Visibility.Visible;
                WOFExpiryDateLabel.Visibility = Visibility.Visible;
                WOFExpiryDatePicker.Visibility = Visibility.Visible;
                regisExpiryDateLabel.Visibility = Visibility.Visible;
                regisExpiryDatePicker.Visibility = Visibility.Visible;
                dateImportedLabel.Visibility = Visibility.Visible;
                dateImportedDatePicker.Visibility = Visibility.Visible;

                //Second Column
                manufactureYearLabel.Visibility = Visibility.Visible;
                manufactureYearTextbox.Visibility = Visibility.Visible;
                statusLabel.Visibility = Visibility.Visible;
                statusComboBox.Visibility = Visibility.Visible;
                fuelTypeLabel.Visibility = Visibility.Visible;
                fuelTypeComboBox.Visibility = Visibility.Visible;
                transmissionTypeLabel.Visibility = Visibility.Visible;
                transmissionTypeComboBox.Visibility = Visibility.Visible;
                rentalPriceLabel.Visibility = Visibility.Visible;
                rentalPriceTextbox.Visibility = Visibility.Visible;
                advanceDepositLabel.Visibility = Visibility.Visible;
                advanceDepositTextbox.Visibility = Visibility.Visible;
                featuresLabel.Visibility = Visibility.Visible;
                updateButton.Visibility = Visibility.Visible;
                featureComboBox.Visibility = Visibility.Visible;
                listbox1.Visibility = Visibility.Visible;
                addFeatureButton.Visibility = Visibility.Visible;
                removeFeatureButton.Visibility = Visibility.Visible;
            }
        }
        private void searchRegistrationNoButton_Click(object sender, RoutedEventArgs e)
        {
            string regisNo = searchRegistrationNoTextBox.Text;
            if (regisNo.Length == 0)
            {
                MessageBox.Show("Please enter truck registration number");
                hideControls(true);
            }
            else
            {
                hideControls(false);
                //Object of IndividualTruck
                //declared at top
                indiTruck = DAO.searchRegisNo(regisNo);
                if (indiTruck == null)
                {
                    hideControls(true);
                    MessageBox.Show("Sorry no truck is found with the same registration number");
                }
                else
                {
                    //From IndividualTruck --> TruckModel
                    modelTextBox.Text = indiTruck.TruckModel.Model;
                    manufacturerTextBox.Text = indiTruck.TruckModel.Manufacturer;
                    sizeCombobox.Text = indiTruck.TruckModel.Size;
                    //ToString() cause TruckModel.Seats and Doors are int
                    seatsTextBox.Text = indiTruck.TruckModel.Seats.ToString();
                    doorsTextBox.Text = indiTruck.TruckModel.Doors.ToString();
                    //From Individual Truck Table
                    colourTextBox.Text = indiTruck.Colour;
                    registrationNoTextBox.Text = indiTruck.RegistrationNumber;
                    WOFExpiryDatePicker.Text = indiTruck.WofexpiryDate.ToString();
                    regisExpiryDatePicker.Text = indiTruck.RegistrationExpiryDate.ToString();
                    dateImportedDatePicker.Text = indiTruck.DateImported.ToString();

                    manufactureYearTextbox.Text = indiTruck.ManufactureYear.ToString();
                    statusComboBox.Text = indiTruck.Status;
                    fuelTypeComboBox.Text = indiTruck.FuelType;
                    transmissionTypeComboBox.Text = indiTruck.Transmission;
                    rentalPriceTextbox.Text = indiTruck.DailyRentalPrice.ToString();
                    advanceDepositTextbox.Text = indiTruck.AdvanceDepositRequired.ToString();
                    listbox1.ItemsSource = DAO.displayTruckFeaturesOfTruck(indiTruck.TruckId);

                }
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            string regisNo = searchRegistrationNoTextBox.Text;
            //searchRegisNo --> searches if input regis no == regis no in database
            try
            {
                indiTruck = DAO.searchRegisNo(regisNo);

                //Unchangeable details of truck
                modelTextBox.IsReadOnly = true;
                sizeCombobox.IsReadOnly = true;
                seatsTextBox.IsReadOnly = true;
                doorsTextBox.IsReadOnly = true;
                manufacturerTextBox.IsReadOnly = true;
                fuelTypeComboBox.IsReadOnly = true;

                //get the truckModel value of Individual Truck
                TruckModel model = indiTruck.TruckModel;
                //Changeable details of truck           
                indiTruck.Status = statusComboBox.Text;
                indiTruck.WofexpiryDate = DateTime.Parse(WOFExpiryDatePicker.Text);
                indiTruck.AdvanceDepositRequired = decimal.Parse(advanceDepositTextbox.Text);
                indiTruck.DailyRentalPrice = decimal.Parse(rentalPriceTextbox.Text);
                indiTruck.RegistrationExpiryDate = DateTime.Parse(regisExpiryDatePicker.Text);
                indiTruck.ManufactureYear = int.Parse(manufactureYearTextbox.Text);
                indiTruck.Colour = colourTextBox.Text;

                //calling updateTruck from DAO class
                DAO.updateTruck(indiTruck, model);
                //DAO.addTruckFeature(tfa);
                MessageBox.Show("Truck details has been successfully updated!");
                searchRegistrationNoTextBox.Clear();
                hideControls(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void addFeatureButton_Click(object sender, RoutedEventArgs e)
        {
            //gets the selected item in the combobox
            TruckFeature selectedFeature = (TruckFeature)(featureComboBox.SelectedItem);
            var features = DAO.displayTruckFeaturesOfTruck(indiTruck.TruckId);
            if (selectedFeature != null)
            {
                TruckFeatureAssociation tfa = new TruckFeatureAssociation();
                tfa.TruckId = indiTruck.TruckId;
                tfa.FeatureId = selectedFeature.FeatureId;

                MessageBox.Show("Feature added successfully!");
                DAO.addTruckFeatures(tfa);
                //refreshes listbox to show the new feature added
                listbox1.ItemsSource = DAO.displayTruckFeaturesOfTruck(indiTruck.TruckId);
            }
            else
            {
                MessageBox.Show("Please select a feature first");
            }
        }

        private void removeFeatureButton_Click(object sender, RoutedEventArgs e)
        {
            //gets the selected item in the combobox
            TruckFeature selectedFeature = (TruckFeature)(featureComboBox.SelectedItem);
            var features = DAO.displayTruckFeaturesOfTruck(indiTruck.TruckId);
            if (selectedFeature != null)
            {
                if (features.Count != 0)
                {
                    TruckFeatureAssociation tfa = new TruckFeatureAssociation();
                    tfa.TruckId = indiTruck.TruckId;
                    tfa.FeatureId = selectedFeature.FeatureId;

                    MessageBox.Show("Feature removed successfully!");
                    DAO.deleteExistingFeature(tfa);

                    listbox1.ItemsSource = DAO.displayTruckFeaturesOfTruck(indiTruck.TruckId);
                }
                else
                {
                    MessageBox.Show("Truck " + indiTruck.RegistrationNumber + " does not contain any features");
                }

            }
            else
            {
                MessageBox.Show("Please select the feature you want to remove first from combobox");
            }


            featureComboBox.SelectedIndex = -1;
        }
    }
}