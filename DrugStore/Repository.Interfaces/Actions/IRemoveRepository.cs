using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Actions
{
    public interface IRemoveRepository<T, Y> where T : class 
    {
        Task Delete(T model);
        Task Delete(Y id);
        Task Delete(IEnumerable<T> t);
    }
}
