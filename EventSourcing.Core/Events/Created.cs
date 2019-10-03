using EventSourcing.Core.Domain;
using System;

namespace EventSourcing.Core.Events
{
    public class Created : PersonEventBase
    {
        public Created(string personId)
        {
            PersonId = personId;
        }

        public override void Apply(Person person)
        {
            person.Id = PersonId;
            base.Apply(person);
        }
    }
}