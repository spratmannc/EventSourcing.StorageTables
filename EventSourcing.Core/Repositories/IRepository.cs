using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Core.Repositories
{
    public interface IRepository<T>
    {
        void Save(T entity);

        T Find(string id);
    }
}
