using System;
using System.Web.Http;
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

        public static bool IsApiController(Type type)
        {
            return type != null
                   && type.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                   && !type.IsAbstract
                   && typeof(ApiController).IsAssignableFrom(type);
        }
    }
}
