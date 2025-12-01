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

        public async Task<PaginatedList<Pokemon>> GetPaginatedPokemon(int pageIndex, int pageSize, string? filterType = null, string? sort = null)
        {

            if (filterType == "null" && sort == "null")
                return await PaginatedList<Pokemon>.CreateAsync(_dbSet, pageIndex, pageSize);
            else if (filterType == "null")
            {
                if (sort == "priceASC")
                    return await PaginatedList<Pokemon>.CreateAsync(_dbSet.OrderBy(pk => pk.Price), pageIndex, pageSize);
                else if (sort == "priceDESC")
                    return await PaginatedList<Pokemon>.CreateAsync(_dbSet.OrderByDescending(pk => pk.Price), pageIndex, pageSize);
            }
            else if (sort == "null")
                return await PaginatedList<Pokemon>.CreateAsync(_dbSet.Where(pk => pk.Type1 == filterType), pageIndex, pageSize);
            else
            {
                if (sort == "priceASC")
                    return await PaginatedList<Pokemon>.CreateAsync(_dbSet.Where(pk => pk.Type1 == filterType).OrderBy(pk => pk.Price), pageIndex, pageSize);
                else if (sort == "priceDESC")
                    return await PaginatedList<Pokemon>.CreateAsync(_dbSet.Where(pk => pk.Type1 == filterType).OrderByDescending(pk => pk.Price), pageIndex, pageSize);
            }
            
            // just in case
            return await PaginatedList<Pokemon>.CreateAsync(_dbSet, pageIndex, pageSize);
        }
    }
}
