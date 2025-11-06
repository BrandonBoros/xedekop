using Xedekop.Server.Data.Entities;
using Xedekop.Server.Data.Interfaces;

namespace Xedekop.Server.Data.Repositories
{
    public class PokeOrderRepository : PokeGenericRepository<Order>, IPokeOrderRepository
    {
        /// <summary>
        /// The constructor for the OrderRepository.
        /// </summary>
        /// <param name="db">The Db context of the application.</param>
        /// <param name="logger">The logger for the PokeRepository.</param>
        public PokeOrderRepository(AppDbContext db, ILogger<PokeOrderRepository> logger) : base(db, logger) { }
    }
}
