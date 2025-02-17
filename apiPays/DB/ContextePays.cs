using ApiPays.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPays.DB
{
    public class ContextePays : DbContext
    {
        public DbSet<Pays> Pays { get; set; }
        public DbSet<Ville> Villes { get; set; }

        public DbSet<Personne> Personnes { get; set; }

        public DbSet<Trajet> Trajets { get; set; }

        public DbSet<PersonneTrajet> PersonneTrajets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=db_countries; Trusted_Connection=True;");
        }
    }
}
