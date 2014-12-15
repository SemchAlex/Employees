using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Employees.Model;

namespace Employees.Repository
{
    public class DataContext:DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Specialty> Specialties { get; set; } 

        public DataContext():base()
        {
        }

        public DataContext(string connString):base(connString)
        {
            this.Configuration.AutoDetectChangesEnabled = false;
        }
    }
}
