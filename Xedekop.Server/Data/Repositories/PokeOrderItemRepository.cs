using Microsoft.EntityFrameworkCore;
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

        public OrderItem CreateOrderItem(Pokemon pokemon, decimal unitPrice)
        {
            OrderItem orderItem = new OrderItem()
            {
                Pokemon = pokemon,
                Quantity = 1,
                UnitPrice = unitPrice,
            };

            _dbSet.AddAsync(orderItem);

            return orderItem;
        }

        public OrderItem? UpdateOrderItem(int id, Pokemon? pokemon = null, decimal? unitPrice = null, int? quantity = null)
        {
            OrderItem? oldOrderItem = _dbSet.Find(GetByID(id));
            OrderItem newOrderItem;

            if (oldOrderItem == null && pokemon != null && unitPrice != null)
            {
                newOrderItem = CreateOrderItem(pokemon, (decimal)unitPrice);
            }
            else if (oldOrderItem == null)
            {
                return null;
            }
            else
            {
                newOrderItem = new OrderItem()
                {
                    Pokemon = pokemon ?? oldOrderItem.Pokemon,
                    Quantity = quantity ?? oldOrderItem.Quantity,
                    UnitPrice = unitPrice ?? oldOrderItem.UnitPrice,
                };

                _dbSet.Update(newOrderItem);
            }

            return newOrderItem;
        }

        public OrderItem? DeleteOrderItem(int id)
        {
            OrderItem? deletedOrderItem = _dbSet.Find(GetByID(id));

            if (deletedOrderItem == null)
            {
                return null;
            }

            _dbSet.Remove(deletedOrderItem);

            return deletedOrderItem;
        }

    }
}
