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

        /// <summary>
        /// Gets all items from the db for a specific entity
        /// </summary>
        /// <returns>A Status 200 reponse containing all of the entities in the database.</returns>
        [HttpGet]
        public virtual IActionResult GetAll()
        {
            var results = _repository.GetAll();

            return Ok(results);            
        }

        /// <summary>
        /// Gets an item from the db that has a specific Id.
        /// </summary>
        /// <param name="id">The id to match.</param>
        /// <returns>A Status 200 reponse containing the matching item, or a 404 response if the item isn't found.</returns>
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
