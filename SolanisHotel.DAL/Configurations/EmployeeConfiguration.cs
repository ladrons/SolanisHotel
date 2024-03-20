using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SolanisHotel.DAL.Configurations.Common;
using SolanisHotel.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolanisHotel.DAL.Configurations
{
    public class EmployeeConfiguration : BaseConfiguration<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(x => x.Salary).HasPrecision(18, 2);
        }
    }
}