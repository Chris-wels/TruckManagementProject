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

namespace TruckManagementProject.Views.RentalManagement
{
    /// <summary>
    /// Interaction logic for ReturnTruckUC.xaml
    /// </summary>
    public partial class ReturnTruckUC : UserControl
    {
        public ReturnTruckUC()
        {
            //---------Gets Trucks that have been rented and displays their registration Number
            InitializeComponent();
            TruckRegostrationComboBox.ItemsSource = DAO.getRentedOutTrucks();
            TruckRegostrationComboBox.DisplayMemberPath = "RegistrationNumber";
            TruckRegostrationComboBox.SelectedValuePath = "RegistrationNumber";
        }

        private void ReturnTruckButton_Click(object sender, RoutedEventArgs e)
        {
            //------Gets the Record of an individual truck by registration number and then calls the ReturnTruck Method which changes the availability status of that selected truck record back to Avaialable for rent 
            try
            {
                string registration = TruckRegostrationComboBox.Text;
                TruckRental rental = DAO.getRentalRecordByRego(registration);
                rental.ReturnDate = dateReturnedPicker.SelectedDate.Value;
                DAO.ReturnTruck(rental);
                MessageBox.Show("Truck Returned Successfully");
            }
            catch (Exception ex)
            { 
            MessageBox.Show(ex.Message);
            } 
          }
    }
}
