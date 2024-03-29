﻿using EventSourcing.Core.Domain;

namespace EventSourcing.Core.Events
{
    public class NameChanged : IEvent<Person>
    {
        public NameChanged(string first, string last)
        {
            First = first;
            Last = last;
        }

        public string First { get; private set; }
        public string Last { get; private set; }

        public void Apply(Person person)
        {
            person.FirstName = First;
            person.LastName = Last;
        }
    }
}