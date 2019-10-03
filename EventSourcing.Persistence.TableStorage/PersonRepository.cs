using EventSourcing.Core.Domain;
using EventSourcing.Core.Events;
using EventSourcing.Core.Repositories;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace EventSourcing.TableStorage
{
    public class PersonRepository : IRepository<Person>
    {
        private CloudTable table;

        public PersonRepository(string connectionString)
        {
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudTableClient();
            this.table = client.GetTableReference("people");
        }

        public Person Find(string id)
        {
            var query = new TableQuery()
                            .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id))
                            .OrderBy("Timestamp");

            var history = table.ExecuteQuery(query, PersonEventResolver)
                               .Select(item => item.ToEvent());

            return new Person(history);
        }

        public void Save(Person person)
        {
            var batch = new TableBatchOperation();

            foreach (var evnt in person.UnsavedEvents)
            {
                ITableEntity entity = ConvertEventToEntity(evnt);

                if (entity != null)
                {
                    entity.RowKey = Guid.NewGuid().ToString();
                    entity.PartitionKey = person.Id;
                    batch.Add(TableOperation.InsertOrMerge(entity));
                }
            }

            table.ExecuteBatchAsync(batch).Wait();
        }

        private static IEventEntity<Person> PersonEventResolver(string partitionKey, string rowKey, DateTimeOffset timeStamp, IDictionary<string, EntityProperty> props, string etag)
        {
            var type = props["EventType"].StringValue;
            Type t = ConvertNameToEntityType(type);

            TableEntity evnt = FormatterServices.GetUninitializedObject(t) as TableEntity;

            evnt.PartitionKey = partitionKey;
            evnt.RowKey = rowKey;
            evnt.Timestamp = timeStamp;
            evnt.ETag = etag;
            evnt?.ReadEntity(props, null);

            return evnt as IEventEntity<Person>;
        }

        private static ITableEntity ConvertEventToEntity(IEvent<Person> evnt) => evnt switch
        {
            Created created => new CreatedEntity(created),
            NameChanged changed => new NameChangedEntity(changed),
            BirthDateChanged dob => new BirthDateChangedEntity(dob),
            _ => default
        };

        private static Type ConvertNameToEntityType(string type)
        {
            return type switch
            {
                nameof(BirthDateChangedEntity) => typeof(BirthDateChangedEntity),
                nameof(NameChangedEntity) => typeof(NameChangedEntity),
                nameof(CreatedEntity) => typeof(CreatedEntity),
                _ => default
            };
        }
    }
}
