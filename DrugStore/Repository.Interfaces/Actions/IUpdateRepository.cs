using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Actions
{
    public interface IUpdateRepository<T> where T: class
    {
        Task UpdateAsync(T model);
    }
}
