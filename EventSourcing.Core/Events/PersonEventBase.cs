using EventSourcing.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Core.Events
{
    public abstract class PersonEventBase : IEvent<Person>
    {
        public string PersonId { get; protected set; }

        public virtual void Apply(Person entity)
        {
            this.PersonId = entity.Id;
        }
    }
}
