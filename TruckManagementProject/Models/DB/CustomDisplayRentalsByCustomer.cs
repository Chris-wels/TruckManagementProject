using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TruckManagementProject.Models.DB
{
    public class CustomDisplayRentalsByCustomer
    {
        [Key]
        public int RentalId { get; set; }
        public string RegistrationNumber { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string RentDate { get; set; }

        public string ReturnDueDate { get; set; }

        public string ReturnDate { get; set; }

        public string TotalPrice { get; set; }
    }
}
