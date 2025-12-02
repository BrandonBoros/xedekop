using Xedekop.Server.Controllers.Base;
using Xedekop.Server.Data;
using Xedekop.Server.Data.Entities;
using Xedekop.Server.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xedekop.Server.Data.Repositories;
using System.Threading.Tasks;
using System.Drawing.Printing;

namespace Xedekop.Server.Controllers
{
    [Route("api/[controller]")]
    public class PokemonController : BaseController<Pokemon>
    {       
        private IUnitOfWork _unitOfWork;
        public PokemonController(ILogger<PokemonController> logger, IUnitOfWork unitOfWork) : base(logger, unitOfWork.GetRepository<IPokeRepository<Pokemon>>())
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{pageIndex}/{pageSize}/{type}/{sort}")]
        public async Task<IActionResult> GetPaginatedPokemon(int pageIndex, int pageSize, string type, string sort)
        {
            List<object> returnResults = new List<object>();

            var pokeRepo = _unitOfWork.GetRepository<IPokePokemonRepository>();

            var results = await pokeRepo.GetPaginatedPokemon(pageIndex, pageSize, type, sort);

            returnResults.Add(results);
            returnResults.Add(results.TotalPages);

            return Ok(returnResults);
        }
    }
}
