using Xedekop.Server.Data.Interfaces;

namespace Xedekop.Server.Data.Entities
{
    public enum Type
    {
        Normal,
        Fire,
        Water,
        Electric,
        Grass,
        Ice,
        Fighting,
        Poison,
        Ground,
        Flying,
        Psychic,
        Bug,
        Rock,
        Ghost,
        Dragon,
        Dark,
        Steel,
        Fairy
    }

    public class Pokemon : IEntity
    {
        /// <summary>
        /// The Primary Key of the Pokemon (pokedex number).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The Name of the Pokemon.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The First Type of the Pokemon.
        /// </summary>
        public string Type1 { get; set; }

        /// <summary>
        /// The Second Type, if exists of the Pokemon.
        /// </summary>
        public string? Type2 { get; set; }

        /// <summary>
        /// The price of the Pokemon.
        /// </summary>
        public decimal Price { get; set; }

        // pokemon image url
    }
}
