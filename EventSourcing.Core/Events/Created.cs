using EventSourcing.Core.Domain;
using System;

namespace EventSourcing.Core.Events
{
    public class Created : IEvent<Person>
    {
        public Created(string personId)
        {
            PersonId = personId;
        }

        public string PersonId { get; }

        public void Apply(Person person)
        {
            person.Id = PersonId;
        }
    }
}