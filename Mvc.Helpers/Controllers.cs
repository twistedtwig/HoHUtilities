using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace HoHUtilities.Mvc.Helpers
{
    public class Controllers
    {
        public static bool IsController(Type type)
        {
            return type != null
                   && type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                   && !type.IsAbstract
                   && typeof(IController).IsAssignableFrom(type);
        }
    }
}
