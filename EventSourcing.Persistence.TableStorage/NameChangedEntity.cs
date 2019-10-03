using EventSourcing.Core.Domain;
using EventSourcing.Core.Events;
using Microsoft.Azure.Cosmos.Table;

namespace EventSourcing.TableStorage
{
    internal class NameChangedEntity : TableEntity, IEventEntity<Person>
    {
        public NameChangedEntity(NameChanged changed)
        {
            this.FirstName = changed.First;
            this.LastName = changed.Last;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EventType { get; set; } = nameof(NameChangedEntity);

        public IEvent<Person> ToEvent()
        {
            return new NameChanged(FirstName, LastName);
        }
    }
}