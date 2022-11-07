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
    /// Interaction logic for RentalsByDateUC.xaml
    /// </summary>
    public partial class RentalsByDateUC : UserControl
    {
        public RentalsByDateUC()
        {
            InitializeComponent();
        }

        private void ShowTruckByDateButton_Click(object sender, RoutedEventArgs e)
        {
            try {
                //-------Gets the two selected DateTime Values 
                DateTime firstDate = datePickerOne.SelectedDate.Value;
                DateTime lastDate = datePickerTwo.SelectedDate.Value;

                //--------Creates list of RentalRecords and Checks the getRentalBetweenDates Method
                List<CustomDisplayRentalsByCustomer> rentalsBydates = DAO.getRentalBetweenDates(firstDate, lastDate);
                if(rentalsBydates != null) 
                {
                    dateRentTruckDataGrid.ItemsSource = rentalsBydates;

                }
                else 
                {
                    MessageBox.Show("No records could be found between these two selected Dates");
                }

            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
