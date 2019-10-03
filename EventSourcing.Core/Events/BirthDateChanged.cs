using EventSourcing.Core.Domain;
using System;

namespace EventSourcing.Core.Events
{
    public class BirthDateChanged : IEvent<Person>
    {
        public BirthDateChanged(DateTimeOffset dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
        }

        public DateTimeOffset DateOfBirth { get; private set; }

        public void Apply(Person person)
        {
            person.DateOfBirth = DateOfBirth;
        }
    }
}