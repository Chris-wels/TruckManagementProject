using System;
using System.Collections.Generic;

#nullable disable

namespace TruckManagementProject.Models.DB
{
    public partial class IndividualTruck
    {
        public IndividualTruck()
        {
            TruckFeatureAssociations = new HashSet<TruckFeatureAssociation>();
            TruckRentals = new HashSet<TruckRental>();
        }
        //Added Validation for Individual Truck
        public int TruckId { get; set; }
        public string Colour { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime WofexpiryDate { get; set; }
        private DateTime registrationExpiry;
        public DateTime RegistrationExpiryDate
        {
            get { return registrationExpiry; }
            set
            {
                if (value <= DateTime.Today)
                {
                    throw new Exception("Please enter correct registration expiry date");
                }
                else
                {
                    this.registrationExpiry = value;
                }

            }
        }
        private DateTime dateImported;
        public DateTime DateImported
        {
            get { return dateImported; }
            set
            {
                if (value > DateTime.Today)
                {
                    throw new Exception("Please enter correct date imported");
                }
                else
                {
                    this.dateImported = value;
                }
            }
        }
        private int manufactureYear;
        public int ManufactureYear
        {
            get { return manufactureYear; }
            set
            {
                if (value <= DateTime.Now.Year)
                {
                    this.manufactureYear = value;
                }
                else
                {
                    throw new Exception("Please enter correct manufacture year");
                }
            }
        }
        public string Status { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
        private decimal dailyRentalPrice;
        public decimal DailyRentalPrice
        {
            get { return dailyRentalPrice; }
            set
            {
                if (value >= 1000)
                {
                    this.dailyRentalPrice = value;
                }
                else
                {
                    throw new Exception("Daily rent amount must be equal to or higher than $1000");

                }
            }
        }
        private decimal advancedeposit;
        public decimal AdvanceDepositRequired
        {
            get { return advancedeposit; }
            set
            {
                if (value >= 500)
                {
                    this.advancedeposit = value;
                }
                else
                {
                    throw new Exception("Advance deposit amount must be equal to or higher than $500");
                }
            }

        }

        public int TruckModelId { get; set; }

        public virtual TruckModel TruckModel { get; set; }
        public virtual ICollection<TruckFeatureAssociation> TruckFeatureAssociations { get; set; }
        public virtual ICollection<TruckRental> TruckRentals { get; set; }
    }
}
