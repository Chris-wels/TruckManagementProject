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

namespace TruckManagementProject.Views.CustomerManagement
{
    /// <summary>
    /// Interaction logic for UpdateEmployeeUC.xaml
    /// </summary>
    public partial class UpdateEmployeeUC : UserControl
    {
        TruckEmployee em = null;
        TruckPerson ep;
        public UpdateEmployeeUC()
        {
            InitializeComponent();
        }

        private void SearchEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            int eid = int.Parse(employeeIDTextBox.Text);
            em = DAO.searchEmployee(eid);
            if (em == null)
            {
                MessageBox.Show("No record found");
            }
            else
            {

                officeAddressTextBox.Text = em.OfficeAddress;
                phoneExtNumTextBox.Text = em.PhoneExtensionNumber;
                usernameTextBox.Text = em.Username;
                passWordTextBox.Text = em.Password;
                roleTextBox.Text = em.Role;
                nameTextBox.Text = em.Employee.Name;
                addressTextBox.Text = em.Employee.Address;
                telephoneTextBox.Text = em.Employee.Telephone;
            }
        }
        private void UpdateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            TruckPerson ep = em.Employee;
            em.OfficeAddress = officeAddressTextBox.Text;
            em.PhoneExtensionNumber = phoneExtNumTextBox.Text;
            em.Username = usernameTextBox.Text;
            em.Password = passWordTextBox.Text;
            em.Role = roleTextBox.Text;
            ep.Name = nameTextBox.Text;
            ep.Address = addressTextBox.Text;
            ep.Telephone = telephoneTextBox.Text;
            DAO.updateEmployee(em, ep);
            MessageBox.Show("update employee record successfully");
        }

    }
}
