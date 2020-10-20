using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleGarage.Models.ViewModels
{
    public class EditViewModel
    {
        public int Id { get; set; }

        public VehicleType VehicleType { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Color { get; set; }

        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 2)]
        public string Brand { get; set; }

        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 1)]
        public string Model { get; set; }

        [Required]
        [DisplayName("Number of Wheels")]
        [Range(0, 20)]
        public int NumWheels { get; set; }
    }
}
