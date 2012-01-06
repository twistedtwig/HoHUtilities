using System;
using System.Collections.Generic;

namespace ClassLocator
{
    /// <summary>
    /// Holds the implementation information for a given baseClass
    /// </summary>
    public class ClassSettings
    {
        public Type BaseClass { get; set; }
        public IList<Type> Implemenations { get; set; }
    }
}