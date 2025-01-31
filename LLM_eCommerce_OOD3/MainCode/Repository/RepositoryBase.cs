using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository
{
    public abstract class RepositoryBase<T>
    {
        public abstract bool AddEntity(T entity);
        public abstract bool DeleteRow(int id);
        public abstract bool UpdateEntity(T entity);
    }
}
