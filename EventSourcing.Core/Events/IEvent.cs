namespace EventSourcing.Core.Events
{
    public interface IEvent<in T>
    {
        void Apply(T entity);
    }
}
