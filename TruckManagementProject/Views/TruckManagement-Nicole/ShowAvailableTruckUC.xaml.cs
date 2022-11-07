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

namespace TruckManagementProject.Views.TruckManagement
{
    /// <summary>
    /// Interaction logic for ShowAvailableTruckUC.xaml
    /// </summary>
    public partial class ShowAvailableTruckUC : UserControl
    {
        public ShowAvailableTruckUC()
        {
            InitializeComponent();
        }

        private void showAvailButton_Click(object sender, RoutedEventArgs e)
        {
            availableTruckDataGrid.ItemsSource = DAO.displayAllAvailableTrucks();

        }
    }
}
