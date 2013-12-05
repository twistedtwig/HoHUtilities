using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;

namespace HoHUtilities.ReflectionUtility
{
    [ReflectionPermission(SecurityAction.Demand, Unrestricted = true)]
    public static class Reflect
    {
        private static MethodInfo FindMethodInfo(object objectToHaveMethodInvoked, string methodName, object[] objectParams)
        {
            if (objectToHaveMethodInvoked == null) return null;
            if (String.IsNullOrWhiteSpace(methodName)) return null;            

            MethodInfo[] methodInfos = objectToHaveMethodInvoked.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            int paramCount = objectParams != null ? objectParams.Length : 0;
            return methodInfos.Where(x => x.Name.Equals(methodName) && x.GetParameters().Count() == paramCount).FirstOrDefault();
        }

        public static object InvokeGenericMethodOnObject(object objectToHaveMethodInvoked, string methodName, Type genericType, object[] objectParams)
        {
            if (genericType == null) { return null; }

            MethodInfo method = FindMethodInfo(objectToHaveMethodInvoked, methodName, objectParams);
            if (method == null) { return null; }

            if (!method.IsGenericMethod) { return null; }
            MethodInfo genericMethod = method.MakeGenericMethod(new Type[] { genericType });

            return genericMethod.Invoke(objectToHaveMethodInvoked, objectParams ?? new object[] { } );
        }

        public static object InvokeMethodOnObject(object objectToHaveMethodInvoked, string methodName, object[] objectParams)
        {
            MethodInfo method = FindMethodInfo(objectToHaveMethodInvoked, methodName, objectParams);
            if (method == null) { return null; }

            return method.Invoke(objectToHaveMethodInvoked, objectParams ?? new object[] { });
        }


        /// <summary>
        /// Checks to see if a field exists for the given object.
        /// </summary>
        /// <param name="objectToCheckAgainst"><c>Object</c> The object that is to be checked for the field</param>
        /// <param name="propName"><c>String</c> The name of the field to check for</param>
        /// <returns></returns>
        public static bool CheckFieldExists(Object objectToCheckAgainst, String propName)
        {
            return GetField(objectToCheckAgainst, propName) != null;
        }

        /// <summary>
        /// Returns a <c>FieldInfo</c> object for the given object and the field name.
        /// </summary>
        /// <param name="objectToCheckAgainst"><c>Object</c> The object that is to be checked for the field</param>
        /// <param name="fieldName"><c>String</c> The name of the field to check for</param>
        /// <returns></returns>
        public static FieldInfo GetField(Object objectToCheckAgainst, String fieldName)
        {
            if (objectToCheckAgainst == null) { throw new ArgumentException("objectToCheckPropertyAgainst was NULL"); }
            if (fieldName == null) { throw new ArgumentException("propName was NULL"); }

            FieldInfo publicFieldInfo = objectToCheckAgainst.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public);
            if(publicFieldInfo != null)
            {
                return publicFieldInfo;
            }

            return objectToCheckAgainst.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
        }

        /// <summary>
        /// Returns the <c>PropertyInfo</c> object for the given object and the property name
        /// </summary>
        /// <param name="objectToGetProperty"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static PropertyInfo GetProperty(Object objectToGetProperty, string propertyName)
        {
            if (objectToGetProperty == null) { throw new ArgumentException("objectToGetProperty was NULL"); }
            if (propertyName == null) { throw new ArgumentException("propertyName was NULL"); }

            PropertyInfo publicPropertyInfo = objectToGetProperty.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
            if(publicPropertyInfo != null)
            {
                return publicPropertyInfo;
            }

            return objectToGetProperty.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic);
        }        

        /// <summary>
        /// Generically returns the value of a field given object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToGetField"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static T GetFieldValue<T>(Object objectToGetField, String fieldName)
        {
            FieldInfo fieldInfo = GetField(objectToGetField, fieldName);
            if (fieldInfo == null)
                return default(T);

            if (!(fieldInfo.FieldType == typeof(T)))
            {
                return default(T);
            }

            return (T) fieldInfo.GetValue(objectToGetField);            
        }

        /// <summary>
        /// Generically returns a property value of a object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToGetProperty"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(object objectToGetProperty, string propertyName)
        {
            PropertyInfo propertyInfo = GetProperty(objectToGetProperty, propertyName);
            if (propertyInfo == null)
                return default(T);

            if(!(propertyInfo.PropertyType == typeof(T)))
            {
                return default(T);
            }

            return (T) propertyInfo.GetValue(objectToGetProperty, null);
        }

        /// <summary>
        /// Returns a field or property value from a given object, (fields are checked first, then properties).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToGetValue"></param>
        /// <param name="valueName"></param>
        /// <returns></returns>
        public static T GetValue<T>(object objectToGetValue, String valueName)
        {
            T fieldValue = GetFieldValue<T>(objectToGetValue, valueName);
            if(fieldValue != null)
            {
                return fieldValue;
            }

            return GetPropertyValue<T>(objectToGetValue, valueName);
        }

        /// <summary>
        /// Tries to get the Type of the field or property for the given object. (checks fields first then properties.
        /// Returns null if no field or property found.
        /// </summary>
        /// <param name="objectToGetTypeOfValue"></param>
        /// <param name="valueName"></param>
        /// <returns></returns>
        public static Type GetValueType(object objectToGetTypeOfValue, string valueName)
        {
            FieldInfo fieldInfo = GetField(objectToGetTypeOfValue, valueName);
            if(fieldInfo != null)
            {
                return fieldInfo.FieldType;
            }

            PropertyInfo propertyInfo = GetProperty(objectToGetTypeOfValue, valueName);
            if (propertyInfo == null)
                return null;

            return propertyInfo.PropertyType;
        }


        /// <summary>
        /// Added the field value to the given object
        /// </summary>
        /// <param name="objectToHaveFieldSet"><c>Object</c> The object to have a field set</param>
        /// <param name="fieldName"><c>String</c> The name of the field</param>
        /// <param name="fieldValue"><c>Object</c> The value of the field</param>
        public static void AssignFieldValue(Object objectToHaveFieldSet, String fieldName, Object fieldValue)
        {
            if (objectToHaveFieldSet == null) { throw new ArgumentException("ObjectToAddTo was NULL"); }
            if (fieldName == null) { throw new ArgumentException("fieldName was NULL"); }


            FieldInfo field = GetField(objectToHaveFieldSet, fieldName);

            if (field == null)
            {
                throw new PropertyNotFoundException("field was not found during Reflection.", objectToHaveFieldSet.GetType().ToString(), fieldName);
            }

            field.SetValue(objectToHaveFieldSet, fieldValue);
        }

        /// <summary>
        /// Added the property value to the given object
        /// </summary>
        /// <param name="objectToHavePropertySet"><c>Object</c> The object to have a property set</param>
        /// <param name="propertyName"><c>String</c> The name of the property</param>
        /// <param name="propertyValue"><c>Object</c> The value of the property</param>
        public static void AssignPropertyValue(Object objectToHavePropertySet, String propertyName, Object propertyValue)
        {
            if (objectToHavePropertySet == null) { throw new ArgumentException("objectToHavePropertySet was NULL"); }
            if (propertyName == null) { throw new ArgumentException("propertyName was NULL"); }


            PropertyInfo property = GetProperty(objectToHavePropertySet, propertyName);

            if (property == null)
            {
                throw new PropertyNotFoundException("property was not found during Reflection.", objectToHavePropertySet.GetType().ToString(), propertyName);
            }

            property.SetValue(objectToHavePropertySet, propertyValue, null);
        }

        /// <summary>
        /// Will try to assign value to field then properties, will throw error if neither field or property found.
        /// </summary>
        /// <param name="objectToHaveValueSet"></param>
        /// <param name="ValueName"></param>
        /// <param name="value"></param>
        public static void AssignValue(Object objectToHaveValueSet, String ValueName, Object value)
        {
            bool fieldErrorThrown = false;
            bool propertyErrorThrown = false;

            try
            {
                AssignFieldValue(objectToHaveValueSet, ValueName, value);
            }
            catch (Exception)
            {
                fieldErrorThrown = true;
            }
            try
            {
                AssignPropertyValue(objectToHaveValueSet, ValueName, value);
            }
            catch (Exception)
            {
                propertyErrorThrown = true;
            }

            if (fieldErrorThrown && propertyErrorThrown)
            {
                throw new ArgumentException(String.Format("object: '{0}' with value name '{1}' was not found", objectToHaveValueSet, ValueName));
            }
        }

        /// <summary>
        /// Will check if the object given inherits from a given generic Interface.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool DoesObjectInheritFromGenericInterface(this object item, Type t)
        {
            if (item == null || t == null) return false;
            if (!t.IsGenericType) return false;
            return item.GetType().GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == t);
        }

        /// <summary>
        /// Determine if the given string is of the given tpye.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static bool Is(this string input, Type targetType)
        {
            try
            {
                TypeDescriptor.GetConverter(targetType).ConvertFromString(input);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
