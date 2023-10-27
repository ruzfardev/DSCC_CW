namespace DSCC_API.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        object GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}