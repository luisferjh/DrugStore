using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces.Actions
{
    public interface ICreateRepository<T> where T : class
    {
        void Create(T model);
    }
}
