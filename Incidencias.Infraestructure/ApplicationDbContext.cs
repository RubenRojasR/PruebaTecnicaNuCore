using Incidencias.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incidencias.Infraestructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Responsible> Responsible{ get; set; }
        public DbSet<Incident> Incident { get; set; }
        public DbSet<IncidentHistory> IncidentHistory { get; set; }
    }
}
