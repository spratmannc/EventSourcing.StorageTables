using EventSourcing.Core.Domain;

namespace EventSourcing.Core.Events
{
    public class Created : IEvent<Person>
    {
        public Created(string personId)
        {
            PersonId = personId;
        }

        public string PersonId { get; private set; }

        public void Apply(Person person)
        {
            person.Id = PersonId;
        }
    }
}