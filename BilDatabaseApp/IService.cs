using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilDatabaseApp
{
    public interface IService<T>
    {
        List<T> GetAll();
        T GetByString(string txt);
        T Create(T newItem);
        T Delete(string txt);
        T Modify(string txt, T modifiedItem);

    }
}
