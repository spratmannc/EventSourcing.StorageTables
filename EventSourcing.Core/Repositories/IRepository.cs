namespace EventSourcing.Core.Repositories
{
    public interface IRepository<T>
    {
        void Save(T entity);

        T Find(string id);
    }
}
