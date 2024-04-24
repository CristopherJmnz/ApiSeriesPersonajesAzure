using ApiSeriesPersonajesAzure.Data;
using ApiSeriesPersonajesAzure.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiSeriesPersonajesAzure.Repositories
{
    public class PersonajesSeriesRepository
    {
        private PersonajesSeriesContext context;
        public PersonajesSeriesRepository(PersonajesSeriesContext context)
        {
            this.context = context;
        }

        public async Task<List<PersonajesSeries>> GetAllPersonajesAsync()
        {
            return await this.context.PersonajesSeries
                .ToListAsync();
        }

        public async Task<PersonajesSeries> FindPersonajeAsync(int idpersonaje)
        {
            return await this.context.PersonajesSeries
                .FirstOrDefaultAsync(x => x.IdPersonaje == idpersonaje);
        }


        public async Task<List<PersonajesSeries>>
            GetPersonajesBySerieAsync(string serie)
        {
            List<PersonajesSeries> personajes = await this.context.PersonajesSeries
                .Where(x => x.Serie == serie).ToListAsync();
            return personajes.Count == 0 ? null : personajes;
        }

        public async Task<List<string>> GetSeriesAsync()
        {
            var query = from datos in this.context.PersonajesSeries
                        select datos.Serie;
            return await query.ToListAsync();
        }

        private async Task<int> GetMaxIdPersonaje()
        {
            return await this.context.PersonajesSeries
                .MaxAsync(x => x.IdPersonaje) + 1;
        }
        public async Task InsertPersonajeAsync(string nombre, string imagen, string serie)
        {
            PersonajesSeries personaje = new PersonajesSeries()
            {
                IdPersonaje = await this.GetMaxIdPersonaje(),
                Serie = serie,
                Imagen = imagen,
                Nombre = nombre
            };
            await this.context.PersonajesSeries.AddAsync(personaje);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdatePersonajeAsync(int idpersonaje, string nombre, string imagen, string serie)
        {
            PersonajesSeries personaje = await this.FindPersonajeAsync(idpersonaje);
            personaje.Serie = serie;
            personaje.Nombre = nombre;
            personaje.Imagen = imagen;
            await this.context.SaveChangesAsync();
        }

        public async Task DeletePersonajeAsync(int idpersonaje)
        {
            PersonajesSeries personaje = await this.FindPersonajeAsync(idpersonaje);
            this.context.PersonajesSeries.Remove(personaje);
            await this.context.SaveChangesAsync();
        }

    }
}
