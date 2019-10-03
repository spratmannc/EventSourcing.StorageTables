using EventSourcing.Core.Domain;
using EventSourcing.Core.Events;
using EventSourcing.Core.Repositories;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Linq;

namespace EventSourcing.MongoDbStorage
{
    public class PersonRepository : IRepository<Person>
    {
        private readonly IMongoCollection<PersonEventBase> people;

        public PersonRepository(string connectionString)
        {
            var client = new MongoClient(connectionString);
            this.people = client.GetDatabase("HR").GetCollection<PersonEventBase>("People");
            SetupTypeMappings();
        }

        private void SetupTypeMappings()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(PersonEventBase)))
            {
                BsonClassMap.RegisterClassMap<PersonEventBase>(e =>
                {
                    e.AutoMap();
                    e.MapIdProperty(i => i.Id);
                    e.AddKnownType(typeof(NameChangedPersonEvent));
                    e.AddKnownType(typeof(CreatedPersonEvent));
                    e.AddKnownType(typeof(BirthDateChangedPersonEvent));
                });
            }
        }

        public Person Find(string id)
        {
            var history = people.AsQueryable()
                                .Where(i => i.PersonId == id)
                                .OrderBy(i => i.Recorded)
                                .ToList()
                                .Select(e => e.ToEvent());

            return new Person(history);
        }

        public void Save(Person person)
        {
            var events = person.UnsavedEvents
                               .Select(e => Convert(e))
                               .ToList();

            foreach (var evnt in events)
            {
                evnt.PersonId = person.Id;
            }

            people.InsertMany(events);

            person.Checkpoint();
        }

        private PersonEventBase Convert(IEvent<Person> e) => e switch
        {
            NameChanged nameChange => new NameChangedPersonEvent(nameChange),
            Created created => new CreatedPersonEvent(created),
            BirthDateChanged dob => new BirthDateChangedPersonEvent(dob),
            _ => null
        };
    }
}
