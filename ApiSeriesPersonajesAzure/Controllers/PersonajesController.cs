using ApiSeriesPersonajesAzure.Models;
using ApiSeriesPersonajesAzure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiSeriesPersonajesAzure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonajesController : ControllerBase
    {
        private PersonajesSeriesRepository repo;
        public PersonajesController(PersonajesSeriesRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonajesSeries>>> Personajes()
        {
            return await this.repo.GetAllPersonajesAsync();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<string>>> Series()
        {
            return await this.repo.GetSeriesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonajesSeries>> FindPersonaje(int id)
        {
            PersonajesSeries personaje = await this.repo.FindPersonajeAsync(id);
            return personaje==null ? NotFound() : personaje;
        }

        [HttpGet("[action]/{serie}")]
        public async Task<ActionResult<List<PersonajesSeries>>>
            PersonajesSeries(string serie)
        {
            return await this.repo.GetPersonajesBySerieAsync(serie);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> InsertPersonaje(PersonajesSeries personaje)
        {
            await this.repo.InsertPersonajeAsync
                (personaje.Nombre, personaje.Imagen, personaje.Serie);
            return Ok();
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdatePersonaje(PersonajesSeries personaje)
        {
            await this.repo.UpdatePersonajeAsync
                (personaje.IdPersonaje, personaje.Nombre, personaje.Imagen, personaje.Serie);
            return Ok();
        }

        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<PersonajesSeries>> DeletePersonaje(int id)
        {
            await this.repo.DeletePersonajeAsync(id);
            return Ok();
        }
    }
}
