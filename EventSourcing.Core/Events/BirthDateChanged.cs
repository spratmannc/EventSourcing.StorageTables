using EventSourcing.Core.Domain;
using System;

namespace EventSourcing.Core.Events
{
    public class BirthDateChanged : PersonEventBase
    {
        public BirthDateChanged(DateTimeOffset dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
        }

        public DateTimeOffset DateOfBirth { get; private set; }

        public override void Apply(Person person)
        {
            person.DateOfBirth = DateOfBirth;
            base.Apply(person);
        }
    }
}