using EventSourcing.Core.Domain;
using EventSourcing.Core.Events;
using System;

namespace EventSourcing.MongoDbStorage
{
    public abstract class PersonEventBase
    {
        public string Id { get; internal set; } = Guid.NewGuid().ToString();
        public string PersonId { get; internal set; }
        public DateTimeOffset Recorded { get; internal set; } = DateTimeOffset.Now;
        public abstract IEvent<Person> ToEvent();
    }
}
