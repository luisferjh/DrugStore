using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interfaces.Actions
{
    public interface IUpdateRepository<T> where T : class
    {
        void Update(T model);
    }
}
