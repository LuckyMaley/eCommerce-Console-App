using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainCode.Repository
{
    internal interface ILogin<T>
    {
        bool Login(string username);

    }
}
