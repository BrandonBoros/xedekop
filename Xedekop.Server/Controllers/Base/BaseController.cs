using Microsoft.AspNetCore.Mvc;
using Xedekop.Server.Data.Interfaces;

namespace Xedekop.Server.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<T> : Controller where T : class
    {
        internal readonly ILogger<BaseController<T>> _logger;
        internal readonly IPokeRepository<T> _repository;

        public BaseController(ILogger<BaseController<T>> logger, IPokeRepository<T> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: BaseController
        [HttpGet]
        public virtual IActionResult GetAll()
        {
            var results = _repository.GetAll();

            return Ok(results);            
        }

        [HttpGet("{id}")]
        public virtual IActionResult GetById(int id)
        {
            T item = _repository.GetByID(id);

            if (item is not null)
                return Ok(item);
            else
                return NotFound("Could not find item in database.");
        }
    }
}
