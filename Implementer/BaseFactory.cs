using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementer
{
    class BaseFactory<T> where T:class
    {
        public static T Current
        {
            get
            {
                return Program.provider.GetService(typeof(T)) as T;
            }
        }
    }
}
