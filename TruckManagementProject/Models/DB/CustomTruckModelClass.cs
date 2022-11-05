using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//for key
using System.ComponentModel.DataAnnotations;


namespace TruckManagementProject.Models.DB
{
    public class CustomTruckModelClass
    {
        [Key]

        //From: IndividualTruck table
        public int TruckId { get; set; }
        public string Registration_Num { get; set; }
        //From: Model table
        public string Model { get; set; }
        public string Status { get; set; }
        public string Fuel_Type { get; set; }
        public string Transmission { get; set; }
        public string WOFExpiry_Date { get; set; }
        public string RegistrationExpiry_Date { get; set; }
        public string Date_Imported { get; set; }
        public int Manufacture_Year { get; set; }
        public string Rental_Price { get; set; }
        public string Advance_Deposit { get; set; }

        //From: Model table
       
        public string Manufacturer { get; set; }
        public string Size { get; set; }

        public List<string> Features = new List<string>();
        public string AllFeatures
        {
            get { return string.Join(", ", Features); }
        }
    }
}
