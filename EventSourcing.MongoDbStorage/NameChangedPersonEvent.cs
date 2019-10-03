using EventSourcing.Core.Domain;
using EventSourcing.Core.Events;

namespace EventSourcing.MongoDbStorage
{
    public class NameChangedPersonEvent : PersonEventBase
    {
        public NameChangedPersonEvent(NameChanged changed)
        {
            this.FirstName = changed.First;
            this.LastName = changed.Last;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override IEvent<Person> ToEvent()
        {
            return new NameChanged(FirstName, LastName);
        }
    }
}