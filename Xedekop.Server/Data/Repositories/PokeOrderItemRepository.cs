using Xedekop.Server.Data.Entities;
using Xedekop.Server.Data.Interfaces;

namespace Xedekop.Server.Data.Repositories
{
    public class PokeOrderItemRepository : PokeGenericRepository<OrderItem>, IPokeOrderItemRepository
    {
        /// <summary>
        /// The constructor for the OrderItemRepository.
        /// </summary>
        /// <param name="db">The Db context of the application.</param>
        /// <param name="logger">The logger for the PokeRepository.</param>
        public PokeOrderItemRepository(AppDbContext db, ILogger<PokeGenericRepository<OrderItem>> logger) : base(db, logger) { }
    }
}
