using EventSourcing.Core.Domain;
using EventSourcing.Core.Repositories;
using System;

namespace EventSourcing
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a person
            Person person = new Person();
            person.SetName("The", "Person");
            person.SetDOB(DateTimeOffset.Now.AddYears(-12));

            // un-comment the line for the type of repository you would like
            //      NOTE: naturally, this should be obtained via DI
            // IRepository<Person> repository = GetTableStorageRepository();
            IRepository<Person> repository = GetMongoStorageRepository();

            // save it
            repository.Save(person);

            // get it back
            person = repository.Find(person.Id);

            // write something to the screen
            Console.WriteLine($"{person.Id} => {person.FirstName} {person.LastName} born on {person.DateOfBirth.ToString("d")}");
            Console.ReadLine();
        }

        static IRepository<Person> GetTableStorageRepository()
        {
            string storageConnectionString = Environment.GetEnvironmentVariable("StorageConnection");
            return new TableStorage.PersonRepository(storageConnectionString);
        }

        static IRepository<Person> GetMongoStorageRepository()
        {
            string mongoConnectionString = Environment.GetEnvironmentVariable("MongoDbConnection");
            return new MongoDbStorage.PersonRepository(mongoConnectionString);
        }
    }
}
