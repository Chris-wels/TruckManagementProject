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
    /// Interaction logic for AddEmployeeUC.xaml
    /// </summary>
    public partial class AddEmployeeUC : UserControl
    {
        public AddEmployeeUC()
        {
            InitializeComponent();
        }

        public void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            string officeAddress = officeAddressTextBox.Text;
            string phoneEXTNum = phoneExtNumTextBox.Text;
            string userName = usernameTextBox.Text;
            string passWord = passWordTextBox.Text;
            string role = roleTextBox.Text;
            string name = nameTextBox.Text;
            string address = addressTextBox.Text;
            string telephone = telephoneTextBox.Text;


            //grabs person class
            TruckPerson p = new TruckPerson();
            p.Name = name;
            p.Address = address;
            p.Telephone = telephone;

            //grabs Employee class
            TruckEmployee em = new TruckEmployee();
            em.EmployeeId = p.PersonId;
            em.OfficeAddress = officeAddress;
            em.PhoneExtensionNumber = phoneEXTNum;
            em.Username = userName;
            em.Password = passWord;
            em.Role = role;

            em.Employee = p;
            try
            {
                DAO.addEmployee(em);
                MessageBox.Show("Employee added successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed");
            }



        }
    }
}
