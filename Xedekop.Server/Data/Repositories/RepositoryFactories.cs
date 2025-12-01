using Xedekop.Server.Data.Entities;
using Xedekop.Server.Data.Interfaces;
using Type = System.Type;

namespace Xedekop.Server.Data.Repositories
{
    public class RepositoryFactories
    {
        #region Properties
        /// <summary>
        /// The ILoggerFactory.
        /// </summary>
        private ILoggerFactory _loggerFactory;

        /// <summary>
        /// The Dictionary for all the repository factories.
        /// </summary>
        private readonly IDictionary<Type, Func<AppDbContext, object>> _repositoryFactories;

        /// <summary>
        /// Returns the repository based on provided type.
        /// </summary>
        /// <returns></returns>
        private IDictionary<Type, Func<AppDbContext, object>> GetPokeFactories()
        {
            return new Dictionary<Type, Func<AppDbContext, object>>
            {
                {typeof(IPokeRepository<Pokemon>), dbContext => new PokePokemonRepository(dbContext, new Logger<PokePokemonRepository>(_loggerFactory)) },
                {typeof(IPokeRepository<Order>), dbContext => new PokeOrderRepository(dbContext, new Logger<PokeOrderRepository>(_loggerFactory)) },
                {typeof(IPokeRepository<OrderItem>), dbContext => new PokeOrderItemRepository(dbContext, new Logger<PokeOrderItemRepository>(_loggerFactory)) },

                {typeof(IPokePokemonRepository), dbContext => new PokePokemonRepository(dbContext, new Logger<PokePokemonRepository>(_loggerFactory)) },
            };
        }
        #endregion Properties

        #region Contructors
        /// <summary>
        /// The RepositoryFactories constructor.
        /// Generates the required Repositories.
        /// </summary>
        /// <param name="loggerFactory">The logger factory to generate loggers.</param>
        public RepositoryFactories(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _repositoryFactories = GetPokeFactories();
        }
        #endregion Contructors

        #region Methods
        /// <summary>
        /// Gets the repository factory of the provided type.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <returns>Gets the repository of the provided type.</returns>
        public Func<AppDbContext, object> GetRepositoryFactory<T>()
        {
            Func<AppDbContext, object> factory;
            _repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        /// <summary>
        /// Gets the repository factory of the entity type.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <returns>Gets the repository factory for the entity type.</returns>
        public Func<AppDbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class, IEntity
        {
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        /// <summary>
        /// Gets the generic repository factory.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <returns>Gets the default repository of the provided type.</returns>
        protected virtual Func<AppDbContext, object> DefaultEntityRepositoryFactory<T>() where T : class, IEntity
        {
            return dbContext => new PokeGenericRepository<T>(dbContext, new Logger<PokeGenericRepository<T>>(_loggerFactory));
        }
        #endregion Methods
    }
}
