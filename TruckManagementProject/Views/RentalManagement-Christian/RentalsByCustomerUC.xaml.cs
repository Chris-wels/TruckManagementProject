using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// Interaction logic for RentalsByCustomerUC.xaml
    /// </summary>
    public partial class RentalsByCustomerUC : UserControl
    {
        public RentalsByCustomerUC()
        {
            //-------Gets all customers by License Number and displays their license Numbers in the combo box
            InitializeComponent();
            customerLicenseComboBox.ItemsSource = DAO.getAllCustomers();
            customerLicenseComboBox.DisplayMemberPath = "LicenseNumber";
            customerLicenseComboBox.SelectedValuePath = "LicenseNumber";
        }

        //---------Creats List of Customers That Have a rental Record and displays the Rentals in the Datagrid
        private void ShowRentalsButton_Click(object sender, RoutedEventArgs e)
        {
            string customerLicense = customerLicenseComboBox.Text;
            TruckCustomer cl = DAO.getCustomerLicense(customerLicense);
            int id = cl.CustomerId;
            try 
            {
                List<CustomDisplayRentalsByCustomer> rentals = DAO.displayRentalsByCustomer(id);
                if(rentals.Count > 0) 
                {
                    rentalCustomerTruckDataGrid.ItemsSource = rentals;
                  
                }
                else 
                {
                    MessageBox.Show("No records on this License could be found");
                }
            }catch(Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
