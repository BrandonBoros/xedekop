namespace Xedekop.Server.Data.Interfaces
{
    public interface IRepositoryProvider
    {
        /// <summary>
        /// The Db Context of the application.
        /// </summary>
        AppDbContext DbContext { get; set; }

        /// <summary>
        /// The IPokeRepository for a specific T.
        /// </summary>
        /// <typeparam name="T">The type of the IPokeRepositoy we need.</typeparam>
        /// <returns>The correct IPokeRepository.</returns>
        IPokeRepository<T>? GetRepositoryForEntityType<T>() where T : class;

        /// <summary>
        /// Gets the Repository of a targeted type.
        /// </summary>
        /// <typeparam name="T">The type to get the Repository of.</typeparam>
        /// <param name="factory">The appropriate factory.</param>
        /// <returns>Type of the Repository.</returns>
        T GetRepository<T>(Func<AppDbContext, object>? factory = null) where T : class;

        /// <summary>
        /// Sets the Repository of specific type.
        /// </summary>
        /// <typeparam name="T">The type of the Repository.</typeparam>
        /// <param name="repository">The Repository of type T.</param>
        void SetRepository<T>(T repository) where T : class;
    }
}
