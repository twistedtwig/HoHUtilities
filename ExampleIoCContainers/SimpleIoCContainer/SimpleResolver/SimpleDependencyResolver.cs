using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using HoHUtilities.Extensions;

namespace HoHUtilities.SimpleIoCContainer
{
    public class SimpleDependencyResolver : IDependencyResolver
    {
        private IDictionary<Type, Type> Types = new Dictionary<Type, Type>();

        public void Initialize(Dictionary<String, String> typesForIoC)
        {
            foreach (KeyValuePair<string, string> iocType in typesForIoC)
            {
                try
                {
                    Type contract = Type.GetType(iocType.Key);
                    Type implementation = Type.GetType(iocType.Value);

                    Types.Add(contract, implementation);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "Error inserting an IoC variable for: '{0}' - '{1}'", iocType.Key, iocType.Value), ex);
                }
            }
        }

        public T Resolve<T>() where T : class 
        {
            return ResolveWithMultipleConstructors(Types, typeof(T), null) as T;
        }

        public T Resolve<T>(IDictionary<string, object> extraParameters) where T : class 
        {
            return ResolveWithMultipleConstructors(Types, typeof(T), extraParameters) as T;
        }

        public void Register<TContract, TImplementation>() where TContract : class 
        {
            Types[typeof(TContract)] = typeof(TImplementation);
        }


        private object ResolveWithMultipleConstructors(IDictionary<Type, Type> dictionary, Type contract, IDictionary<string, object> extraParameters)
        {
            Type implementation = dictionary[contract];

            Dictionary<ConstructorInfo, IDictionary<string, object>> constructorResults = new Dictionary<ConstructorInfo, IDictionary<string, object>>();
            foreach (ConstructorInfo constructor in implementation.GetConstructors())
            {
                ParameterInfo[] constructorParameters = constructor.GetParameters();

                if (constructorParameters.Length == 0)
                {
                    constructorResults.Add(constructor, new Dictionary<String, object>());
                    continue;
                }

                IDictionary<string, object> parameters = new Dictionary<string, object>(constructorParameters.Length);
                if (extraParameters != null && extraParameters.Keys.Count > 0)
                    parameters.AddRange(extraParameters, false);

                foreach (ParameterInfo parameterInfo in constructorParameters)
                {
                    if (dictionary.ContainsKey(parameterInfo.ParameterType))
                        parameters.Add(parameterInfo.Name, ResolveWithMultipleConstructors(dictionary, parameterInfo.ParameterType, null));
                }

                constructorResults.Add(constructor, parameters);
            }

            if (constructorResults.Count != 0)
                return CreateObject(constructorResults);

            if (extraParameters != null && extraParameters.Keys.Count > 0)
                throw new TypeLoadException(String.Format("Unable to load type: {0}, with the given parameters.", contract.FullName));

            throw new TypeLoadException(String.Format("Unable to load type: {0}.", contract.FullName));
        }

        private object CreateObject(Dictionary<ConstructorInfo, IDictionary<string, object>> constructorResults)
        {
            var sortedConstructors = (from entry in constructorResults orderby entry.Value.Values.Count descending select entry);
            foreach (KeyValuePair<ConstructorInfo, IDictionary<string, object>> constructorInfo in sortedConstructors)
            {
                try
                {
                    if (constructorInfo.Key.GetParameters().Length == constructorInfo.Value.Count)
                    {
                        if (constructorInfo.Key.GetParameters().Length == 0)
                            return Activator.CreateInstance(constructorInfo.Key.ReflectedType);

                        if (CheckDictionaryHasCorrectTypes(constructorInfo.Key, constructorInfo.Value))
                            return constructorInfo.Key.Invoke(CreateConstructorArrayInCorrectOrder(constructorInfo.Key, constructorInfo.Value));
                    }
                }
                catch (TargetParameterCountException parameterCountException)
                {
                    //swallow so others can be tried.
                }
            }

            return null;
        }

        private bool CheckDictionaryHasCorrectTypes(ConstructorInfo constructorInfo, IDictionary<string, object> list)
        {
            ParameterInfo[] parameters = constructorInfo.GetParameters();
            foreach (ParameterInfo parameter in parameters)
            {
                if (!list.ContainsKey(parameter.Name))
                    return false;
            }

            return true;
        }

        private object[] CreateConstructorArrayInCorrectOrder(ConstructorInfo constructorInfo, IDictionary<string, object> list)
        {
            ParameterInfo[] parameters = constructorInfo.GetParameters();
            object[] resultArray = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                resultArray[i] = list[parameters[i].Name];
            }

            return resultArray;
        }
    }
}
