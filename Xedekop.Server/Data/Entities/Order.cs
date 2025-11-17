using Xedekop.Server.Data.Interfaces;

namespace Xedekop.Server.Data.Entities
{
    /// <summary>
    /// Pokemon order.
    /// Contains Order Id, time of Order, and ICollection of OrderItems.
    /// </summary>
    public class Order : IEntity
    {
        /// <summary>
        /// Primary key of the Order.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// DateTime the Order took place.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Collection of the items in the Order.
        /// </summary>
        public ICollection<OrderItem> Items { get; set; }
    }
}
