using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Actions
{
    public interface ICreateRepository<T> where T: class
    {
        Task AddAsync(T model);
    }
}
