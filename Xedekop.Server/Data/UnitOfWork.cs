using Xedekop.Server.Data.Interfaces;

namespace Xedekop.Server.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties
        /// <summary>
        /// The Db context of the application.
        /// </summary>
        private AppDbContext _context;

        /// <summary>
        /// The Repository provider that gives access to the repositories.
        /// </summary>
        private readonly IRepositoryProvider _repositoryProvider;
        #endregion

        #region Constructors
        /// <summary>
        /// The constructor for UnitOfWork.
        /// Manages the Repositories of the application.
        /// </summary>
        /// <param name="context">The Db context of the application.</param>
        /// <param name="repositoryProvider">The Repository provider that gives access to the repositories.</param>
        /// <param name="factory">The ILoggerFactory that creates the required ILogger</param>
        public UnitOfWork(AppDbContext context, IRepositoryProvider repositoryProvider, ILoggerFactory factory)
        {
            _context = context;
            _repositoryProvider = repositoryProvider;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Gets Repository of specific type.
        /// </summary>
        /// <typeparam name="T">The generic type of the Repository.</typeparam>
        /// <returns>The type of the Repository.</returns>
        public T GetRepository<T>() where T : class
        {
            return _repositoryProvider.GetRepository<T>();
        }


        #endregion Methods

        #region IDisposable
        private bool disposedValue;
        /// <summary>
        /// Disposes of instance in memory (protected).
        /// </summary>
        /// <param name="disposing">Whether it's currently disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Disposes of instance in memory (public).
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion IDisposable
    }
}
