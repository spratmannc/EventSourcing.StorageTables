using EventSourcing.Core.Domain;
using EventSourcing.Core.Events;
using Microsoft.Azure.Cosmos.Table;

namespace EventSourcing.TableStorage
{
    internal class CreatedEntity : TableEntity, IEventEntity<Person>
    {
        public CreatedEntity(Created created)
        {
            this.PersonId = created.PersonId;
        }

        public string PersonId { get; set; }
        public string EventType { get; set; } = nameof(CreatedEntity);

        public IEvent<Person> ToEvent()
        {
            return new Created(PersonId);
        }
    }
}