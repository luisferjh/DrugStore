using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Actions
{
    public interface IReadRepository<T> where T: class
    {
        Task<IEnumerable<T>> List();
    }
}
