using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wixapol_Utils
{
    public static class Extensions
    {
        public static IEnumerable<Type> GetImplementingTypes(this Type itype)
    => AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
           .Where(t => t.GetInterfaces().Contains(itype));
    }
}
