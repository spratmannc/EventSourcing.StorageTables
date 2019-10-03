using EventSourcing.Core.Events;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Persistence
{
    public interface IEventEntity<T> : ITableEntity
    {
        string EventType { get; set; }
        IEvent<T> ToEvent();
    }
}
