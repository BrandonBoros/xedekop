using System.Drawing.Printing;
using Xedekop.Server.Data.Entities;

namespace Xedekop.Server.Data.Interfaces
{
    public interface IPokePokemonRepository : IPokeRepository<Pokemon> {
        public Task<PaginatedList<Pokemon>> GetPaginatedPokemon(int pageIndex, int pageSize, string filterType = "null", string sort = "null");
    }
}
