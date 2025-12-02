using Xedekop.Server.Data.Interfaces;

namespace Xedekop.Server.Data.Entities
{
    /// <summary>
    /// OrderItem of an Order.
    /// The thing the user is buying and how much.
    /// </summary>
    public class OrderItem : IEntity
    {
        /// <summary>
        /// The Primary Key of the OrderItem.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The "Product" the user is buying.
        /// </summary>
        public Pokemon Pokemon { get; set; }

        /// <summary>
        /// How much of the Product the user is buying.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The cost of the Product alone.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
