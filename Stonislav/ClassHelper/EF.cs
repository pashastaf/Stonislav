using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stonislav.ClassHelper
{
    internal class EF
    {
        public static DataBase.Entities Context { get; } = new DataBase.Entities();
    }
}
