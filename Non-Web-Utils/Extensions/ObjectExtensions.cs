using System;

namespace HoHUtilities.Extensions
{
    public static class ObjectExtensions
    {

        /// <summary>
        /// Checks to see if the one object is really the same as another.  Tries to check both value and reference types, but complex types that do not
        /// override equals will not match even if they should be seen as the same object.
        /// </summary>
        /// <param name="me"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool CompareIfSame(this object me, object other)
        {
            if (me == null && other == null) { return true; }
            if (me == null || other == null) { return false; }

            if (me.GetType().IsValueType && other.GetType().IsValueType)
            {
                if (me == other)
                    return true;
            }

            if (me is IComparable && other is IComparable)
            {
                return ((IComparable) me).CompareTo(other) == 0;
            }

            //see if it implements generic Icomparable.. if so try and to compareto and return result == 0;
            if (ReflectionUtility.Reflect.DoesObjectInheritFromGenericInterface(me, typeof(IComparable<>)) &&
                ReflectionUtility.Reflect.DoesObjectInheritFromGenericInterface(other, typeof(IComparable<>)))
            {
                var result = ReflectionUtility.Reflect.InvokeMethodOnObject(me, "CompareTo", new object[] { other });
                int resultVal = -1;
                if (result != null)
                {
                    int.TryParse(result.ToString(), out resultVal);
                    return resultVal == 0;
                }
            }

            if (me.Equals(other))
                return true;

            return false;
        }
    }
}
