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
            InitializeComponent();
            TruckRegostrationComboBox.ItemsSource = DAO.getRentedOutTrucks();
            TruckRegostrationComboBox.DisplayMemberPath = "RegistrationNumber";
            TruckRegostrationComboBox.SelectedValuePath = "RegistrationNumber";
        }

        private void ReturnTruckButton_Click(object sender, RoutedEventArgs e)
        {
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
