using EventSourcing.Core.Domain;
using EventSourcing.Core.Events;

namespace EventSourcing.MongoDbStorage
{
    public class CreatedPersonEvent : PersonEventBase
    {
        public CreatedPersonEvent(Created evnt)
        {
            this.PersonId = evnt.PersonId;
        }

        public override IEvent<Person> ToEvent()
        {
            return new Created(PersonId);
        }
    }
}