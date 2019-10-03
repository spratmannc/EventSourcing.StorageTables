using EventSourcing.Core.Domain;
using EventSourcing.Core.Events;
using Microsoft.Azure.Cosmos.Table;
using System;

namespace EventSourcing.TableStorage
{
    internal class BirthDateChangedEntity : TableEntity, IEventEntity<Person>
    {

        public DateTimeOffset DateOfBirth { get; set; }
        public string EventType { get; set; } = nameof(BirthDateChangedEntity);

        public BirthDateChangedEntity(BirthDateChanged dob)
        {
            this.DateOfBirth = dob.DateOfBirth;
        }

        public IEvent<Person> ToEvent()
        {
            return new BirthDateChanged(DateOfBirth);
        }
    }
}