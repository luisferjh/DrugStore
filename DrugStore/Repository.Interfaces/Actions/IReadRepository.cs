using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Actions
{
    public interface IReadRepository<T, Y> where T : class
    {
        Task<IEnumerable<T>> List();
        Task<T> Get(Y id);
    }
}
