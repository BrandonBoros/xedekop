using Microsoft.EntityFrameworkCore;
using Xedekop.Server.Data.Interfaces;

namespace Xedekop.Server.Data.Repositories
{
    public class PokeGenericRepository<T> : IPokeRepository<T> where T: class
    {
        #region Properties
        protected readonly AppDbContext _db;
        protected readonly DbSet<T> _dbSet;
        protected readonly ILogger<PokeGenericRepository<T>> _logger;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a PokeGenericRepository that the other Repositories will use/inherit.
        /// </summary>
        /// <param name="db">The Db context of the app.</param>
        /// <param name="logger">The logger for PokeGenericRepository.</param>
        public PokeGenericRepository(AppDbContext db, ILogger<PokeGenericRepository<T>> logger)
        {
            _db = db;
            _dbSet = db.Set<T>();
            _logger = logger;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets all from the appropriate table.
        /// </summary>
        /// <returns>Enumarable of the results.</returns>
        public IEnumerable<T> GetAll()
        {
            // Gets all from Db. Logs result.
            try
            {
                _logger.LogInformation("GetAll was called...");

                return _dbSet.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products: {ex}");
                return null;
            }
        }
        #endregion Methods
    }
}
