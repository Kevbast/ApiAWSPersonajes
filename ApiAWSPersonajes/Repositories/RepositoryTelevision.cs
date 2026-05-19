using ApiAWSPersonajes.Data;
using ApiAWSPersonajes.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAWSPersonajes.Repositories
{
    public class RepositoryTelevision
    {
        private TelevisionContext context;
        public RepositoryTelevision(TelevisionContext context)
        {
            this.context = context;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            return await this.context.Personajes.ToListAsync();
        }
        public async Task<Personaje> FindPersonajeAsync(int idPersonaje)
        {
            return await this.context.Personajes.FirstOrDefaultAsync(z=>z.IdPersonaje==idPersonaje);
        }
        private async Task<int> GetMaxIdPersonajeAsync()
        {
            return await this.context.Personajes.MaxAsync(x => x.IdPersonaje) + 1;
        }
        public async Task CreatePersonajeAsync(string nombre,string imagen)
        {
            Personaje p = new Personaje();
            p.IdPersonaje = await this.GetMaxIdPersonajeAsync();
            p.Nombre = nombre;
            p.Imagen = imagen;
            await this.context.Personajes.AddAsync(p);
            await this.context.SaveChangesAsync();
            
        }


    }
}
