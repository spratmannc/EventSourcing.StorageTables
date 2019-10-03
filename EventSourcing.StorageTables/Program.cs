using EventSourcing.Core.Domain;
using EventSourcing.Persistence;
using System;

namespace EventSourcing.StorageTables
{
    class Program
    {
        static void Main(string[] args)
        {
            string CONNECTION_STRING = Environment.GetEnvironmentVariable("StorageConnection");

            // create a person
            Person person = new Person();
            person.SetName("The", "Person");
            person.SetDOB(DateTimeOffset.Now.AddYears(-12));

            var repository = new PersonRepository(CONNECTION_STRING);

            // save it
            repository.Save(person);

            // get it back
            person = repository.Find(person.Id);

            Console.WriteLine($"{person.Id} => {person.FirstName} {person.LastName} born on {person.DateOfBirth.ToString("d")}");

            Console.ReadLine();
        }
    }
}
