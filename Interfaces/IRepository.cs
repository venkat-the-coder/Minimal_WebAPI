namespace Minimal_WebAPI.Interfaces
{
    public interface IRepository<T> where T : class
    {
        List<T> Get();
        T GetById(int? id);
    }
}
