using Xedekop.Server.Controllers.Base;
using Xedekop.Server.Data;
using Xedekop.Server.Data.Entities;
using Xedekop.Server.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
