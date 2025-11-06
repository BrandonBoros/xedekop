using Microsoft.EntityFrameworkCore;
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
    }
}
