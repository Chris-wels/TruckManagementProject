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
    /// Interaction logic for SearchTruckByModelUC.xaml
    /// </summary>
    public partial class SearchTruckByModelUC : UserControl
    {
        IndividualTruck indiTruck = null;
        public SearchTruckByModelUC()
        {
            InitializeComponent();
            //Display all the truckdetails starting with Model
            datagrid.ItemsSource = DAO.getTruckModelFullDetails();
        }

        private void searchTruckModelButton_Click(object sender, RoutedEventArgs e)
        {
            string truckM = truckModelTextBox.Text;
            TruckModel truckMod = DAO.searchModelByName(truckM);
            if (truckMod == null)
            {
                MessageBox.Show("Matching truck model was not found");
            }
            else
            {
                //Changing the datagrid items if the truck model entered was found within database
                datagrid.ItemsSource = DAO.searchTruckModel(truckM);
            }
        }
    }
}
