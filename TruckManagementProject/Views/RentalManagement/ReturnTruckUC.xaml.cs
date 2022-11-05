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
        }

        private void ReturnTruckButton_Click(object sender, RoutedEventArgs e)
        {
            string registration = TruckRegostrationTextBox.Text;
            TruckRental rental = DAO.getRentalRecordByRego(registration);
            rental.ReturnDate = dateReturnedPicker.SelectedDate.Value;
        }
    }
}
