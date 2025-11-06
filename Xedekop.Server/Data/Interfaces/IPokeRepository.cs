namespace Xedekop.Server.Data.Interfaces
{
    public interface IPokeRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
    }
}
