using ApiSeriesPersonajesAzure.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSeriesPersonajesAzure.Data
{
    public class PersonajesSeriesContext : DbContext
    {
        public PersonajesSeriesContext
            (DbContextOptions<PersonajesSeriesContext>options)
            :base(options)
        {
            
        }

        public DbSet<PersonajesSeries> PersonajesSeries { get; set;}
    }
}
