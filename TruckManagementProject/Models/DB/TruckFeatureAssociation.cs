using System;
using System.Collections.Generic;

#nullable disable

namespace TruckManagementProject.Models.DB
{
    public partial class TruckFeatureAssociation
    {
        public int TruckId { get; set; }
        public int FeatureId { get; set; }

        public virtual TruckFeature Feature { get; set; }
        public virtual IndividualTruck Truck { get; set; }
    }
}
