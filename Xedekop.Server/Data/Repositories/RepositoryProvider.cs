using Xedekop.Server.Data.Interfaces;

namespace Xedekop.Server.Data.Repositories
{
    public class RepositoryProvider : IRepositoryProvider
    {
        #region Properties
        private RepositoryFactories _repositoryFactories;
        public Dictionary<Type, object> Repositories { get; private set; }
        public AppDbContext DbContext { get; set; }
        #endregion Properties

        #region Constructors
        /// <summary>
        /// The RepositoryProvider contructor.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="dbContext">The Db Context of the application.</param>
        public RepositoryProvider(ILoggerFactory loggerFactory, AppDbContext dbContext)
        {
            _repositoryFactories = new RepositoryFactories(loggerFactory);
            Repositories = new Dictionary<Type, object>();
            DbContext = dbContext;
        }
        #endregion Constructors

        #region Methods

        public IPokeRepository<T>? GetRepositoryForEntityType<T>() where T : class
        {
            return GetRepository<IPokeRepository<T>>(_repositoryFactories.GetRepositoryFactoryForEntityType<T>());
        }

        /// <summary>
        /// Gets the repository of the provided type.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="factory">The logger factory.</param>
        /// <returns>Gets the repository of the provided type.</returns>
        public T GetRepository<T>(Func<AppDbContext, object>? factory = null) where T : class
        {
            object repoObj;
            Repositories.TryGetValue(typeof(T), out repoObj);
            if (repoObj != null)
            {
                return (T)repoObj;
            }
            return MakeRepository<T>(factory, DbContext);
        }

        /// <summary>
        /// Makes the repository of the provided type.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="factory">The logger factory.</param>
        /// <param name="dbContext">The Db context of the application.</param>
        /// <returns>Gets the repository of the provided type.</returns>
        /// <exception cref="Exception">When no factory for the repository type.</exception>
        private T MakeRepository<T>(Func<AppDbContext, object>? factory, AppDbContext dbContext) where T : class
        {
            var f = factory ?? _repositoryFactories.GetRepositoryFactory<T>();
            if (f == null)
            {
                throw new Exception("No factory for repository type, " + typeof(T).FullName);
            }
            var repo = (T)f(dbContext);
            Repositories[typeof(T)] = repo;
            return repo;
        }

        /// <summary>
        /// Sets the repository of a type.
        /// </summary>
        /// <typeparam name="T">The type of the repository.</typeparam>
        /// <param name="repository">The repository to set.</param>
        public void SetRepository<T>(T repository) where T : class
        {
            Repositories[typeof(T)] = repository;
        }
        #endregion Methods
    }
}
