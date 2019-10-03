using EventSourcing.Core.Events;
using Microsoft.Azure.Cosmos.Table;

namespace EventSourcing.TableStorage
{
    public interface IEventEntity<T> : ITableEntity
    {
        string EventType { get; set; }
        IEvent<T> ToEvent();
    }
}
