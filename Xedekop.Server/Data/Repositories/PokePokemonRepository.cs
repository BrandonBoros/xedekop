using Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Threading.Tasks;
using Xedekop.Server.Data.Entities;
using Xedekop.Server.Data.Interfaces;

namespace Xedekop.Server.Data.Repositories
{
    public class PokePokemonRepository : PokeGenericRepository<Pokemon>, IPokePokemonRepository
    {
        /// <summary>
        /// The constructor for the PokemonRepository.
        /// </summary>
        /// <param name="db">The Db context of the application.</param>
        /// <param name="logger">The logger for the PokeRepository.</param>
        public PokePokemonRepository(AppDbContext db, ILogger<PokePokemonRepository> logger) : base(db, logger) { }

        public async Task<PaginatedList<Pokemon>> GetPaginatedPokemon(int pageIndex, int pageSize)
        {
            return await PaginatedList<Pokemon>.CreateAsync(_dbSet, pageIndex, pageSize);
        }
    }
}
