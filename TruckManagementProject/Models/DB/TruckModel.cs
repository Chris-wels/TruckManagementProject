using System;
using System.Collections.Generic;

#nullable disable

namespace TruckManagementProject.Models.DB
{
    public partial class TruckModel
    {
        public TruckModel()
        {
            IndividualTrucks = new HashSet<IndividualTruck>();
        }
        //Added Validation for TruckModels
        public int ModelId { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public string Size { get; set; }
        private int seats;
        public int Seats
        {
            get
            {
                return seats;
            }
            set
            {
                if (value > 1)
                {
                    seats = value;
                }
                else
                {
                    throw new Exception("Invalid Seat count. Value must be higher than 1");
                }
            }
        }
        private int doors;
        public int Doors
        {
            get { return doors; }
            set
            {
                if (value > 1)
                {
                    doors = value;
                }
                else
                {
                    throw new Exception("Invalid Door count. Value must be higher than 1");
                }
            }
        }

        public virtual ICollection<IndividualTruck> IndividualTrucks { get; set; }
    }
}
