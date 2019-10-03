using EventSourcing.Core.Domain;
using EventSourcing.Core.Events;
using System;

namespace EventSourcing.MongoDbStorage
{
    public class BirthDateChangedPersonEvent : PersonEventBase
    {
        public BirthDateChangedPersonEvent(BirthDateChanged changed)
        {
            this.DateOfBirth = changed.DateOfBirth;
        }

        public DateTimeOffset DateOfBirth { get; private set; }

        public override IEvent<Person> ToEvent()
        {
            return new BirthDateChanged(DateOfBirth);
        }
    }
}