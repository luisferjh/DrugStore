using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Actions
{
    public interface IRemoveRepository<T, Y> where T : class 
    {
        Task DeleteAsync(T model);
        Task DeleteAsync(Y id);
        Task DeleteAsync(IEnumerable<T> t);
    }
}
