using Microsoft.EntityFrameworkCore;
using Prototype.OpenTelematics.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Telematics.DataSimulator
{
    class SimulatorContext : TelematicsContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=OpenTelematics;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
     }
}
