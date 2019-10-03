using EventSourcing.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Core.Domain
{
    public class Person
    {
        private IList<IEvent<Person>> unsavedEvents = new List<IEvent<Person>>();

        public string FirstName { get; internal set; }
        public string LastName { get; internal set; }
        public DateTimeOffset DateOfBirth { get; internal set; }
        public IEnumerable<IEvent<Person>> UnsavedEvents => unsavedEvents.AsEnumerable();

        public string Id { get; internal set; }

        public Person()
        {
            Apply(new Created(Guid.NewGuid().ToString()));
        }

        public Person(IEnumerable<IEvent<Person>> history)
        {
            foreach (var item in history)
            {
                item.Apply(this);
            }
        }

        private void Apply(IEvent<Person> evnt)
        {
            evnt.Apply(this);
            unsavedEvents.Add(evnt);
        }

        public void SetName(string first, string second)
        {
            Apply(new NameChanged(first, second));
        }

        public void SetDOB(DateTimeOffset date)
        {
            Apply(new BirthDateChanged(date));
        }
    }

}
