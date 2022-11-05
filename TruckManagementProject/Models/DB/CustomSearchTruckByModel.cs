using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//for key
using System.ComponentModel.DataAnnotations;
namespace TruckManagementProject.Models.DB
{
    internal class CustomSearchTruckByModel
    {
        [Key]
        public string Model { get; set; } = null!;
        public int TruckId { get; set; }
        public string Colour { get; set; } = null!;
        public string Size { get; set; } = null!;
        public decimal DailyRentalPrice { get; set; }
        public string Status { get; set; } = null!;

        public List<string> Features = new List<string>();
        public string AllFeatures
        {
            get { return string.Join(", ", Features); }
        }
    }
}
