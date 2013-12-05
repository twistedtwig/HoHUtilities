using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoHUtilities.ReflectionUtility.Test
{
    public class ReflectTestClass
    {
        public ReflectTestClass() {  }

        public ReflectTestClass(string privateName)
        {
            NamePropertyPrivate = privateName;
            NameFieldPrivate = privateName;
        }

        public String NamePropertyPublic { get; set; }
        private String NamePropertyPrivate { get; set; }
        
        public string NameFieldPublic;
        private string NameFieldPrivate;

        public bool HasMethodExecuted { get; set; }

        public void ExecuteMe<T>(T p1)
        {
            if(p1 != null) {HasMethodExecuted = true;}
        }

        public T ExecuteMe<T>() where T : ReflectTestClass, new()
        {
            HasMethodExecuted = true;
            return new T();
        }

        public T ExecuteMe<T>(T p1, T p2) where T : ReflectTestClass, new()
        {
            if (p1 != null && p2 != null) { HasMethodExecuted = true; }
            return new T();
        }

    }
}
