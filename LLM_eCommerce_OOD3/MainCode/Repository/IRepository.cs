using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository
{
    public interface IRepository<T>
    {
        List<T> ReadGetAllRows();
        T ReadRowByID(int id);
        bool CheckIfIdExists(int id);

    }
}
