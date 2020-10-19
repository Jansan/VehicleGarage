using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleGarage.Models;

namespace VehicleGarage.Data
{
    public class VehicleGarageContext : DbContext
    {
        public VehicleGarageContext (DbContextOptions<VehicleGarageContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleGarage.Models.Vehicle> Vehicle { get; set; }
    }
}
