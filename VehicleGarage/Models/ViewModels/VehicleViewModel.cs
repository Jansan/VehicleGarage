using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleGarage.Models.ViewModels
{
    public class VehicleViewModel
    {
        public int Id { get; set; }

        [DisplayName("Vehicle Type")]
        public VehicleType VehicleType { get; set; }

        [DisplayName("Registration Number")]
        public string RegNum { get; set; }

        [DisplayName("Arrival Time")]
        public DateTime ArrivalTime { get; set; }

        
    }
}
