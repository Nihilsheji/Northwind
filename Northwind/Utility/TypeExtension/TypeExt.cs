using System;
using System.Reflection;

namespace Northwind.Utility.TypeExtension
{
    //Credit goes to StackOverflow user Noctis
    //https://stackoverflow.com/questions/4035719/getmethod-for-generic-method
    public static class TypeExt
    {
        public static MethodInfo GetMethodExt
        (
            this Type thisType,
            string name,
            params Type[] parameterTypes
        )
        {
            return GetMethodExt(
                thisType,
                name,
                BindingFlags.Instance
                | BindingFlags.Static
                | BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.FlattenHierarchy,
                parameterTypes
            );
        }

        public static MethodInfo GetMethodExt
        (
            this Type thisType,
            string name,
            BindingFlags bindingFlags,
            params Type[] parameterTypes
        )
        {
            MethodInfo matchingMethod = null;

            GetMethodExt(ref matchingMethod, thisType, name, bindingFlags, parameterTypes);

            if (matchingMethod == null && thisType.IsInterface)
            {
                foreach (Type interfaceType in thisType.GetInterfaces())
                    GetMethodExt(
                        ref matchingMethod,
                        interfaceType,
                        name,
                        bindingFlags,
                        parameterTypes
                    );
            }

            return matchingMethod;
        }

        private static void GetMethodExt
        (
            ref MethodInfo matchingMethod,
            Type type,
            string name,
            BindingFlags bindingFlags,
            params Type[] parameterTypes
        )
        {
            // Check all methods with the specified name, including in base classes
            foreach (MethodInfo methodInfo in type.GetMember(name,
                                                             MemberTypes.Method,
                                                             bindingFlags))
            {
                // Check that the parameter counts and types match, 
                // with 'loose' matching on generic parameters
                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                if (parameterInfos.Length == parameterTypes.Length)
                {
                    int i = 0;
                    for (; i < parameterInfos.Length; ++i)
                    {
                        if (!parameterInfos[i].ParameterType
                                              .IsSimilarType(parameterTypes[i]))
                            break;
                    }
                    if (i == parameterInfos.Length)
                    {
                        if (matchingMethod == null)
                            matchingMethod = methodInfo;
                        else
                            throw new AmbiguousMatchException(
                                   "More than one matching method found!");
                    }
                }
            }
        }

        private static bool IsSimilarType(this Type thisType, Type type)
        {
            // Ignore any 'ref' types
            if (thisType.IsByRef)
                thisType = thisType.GetElementType();
            if (type.IsByRef)
                type = type.GetElementType();

            // Handle array types
            if (thisType.IsArray && type.IsArray)
                return thisType.GetElementType().IsSimilarType(type.GetElementType());

            // If the types are identical, or they're both generic parameters 
            // or the special 'T' type, treat as a match
            if (thisType == type || ((thisType.IsGenericParameter || thisType == typeof(T))
                                 && (type.IsGenericParameter || type == typeof(T))))
                return true;

            // Handle any generic arguments
            if (thisType.IsGenericType && type.IsGenericType)
            {
                Type[] thisArguments = thisType.GetGenericArguments();
                Type[] arguments = type.GetGenericArguments();
                if (thisArguments.Length == arguments.Length)
                {
                    for (int i = 0; i < thisArguments.Length; ++i)
                    {
                        if (!thisArguments[i].IsSimilarType(arguments[i]))
                            return false;
                    }
                    return true;
                }
            }

            return false;
        }       
    }

    public class T
    { }
}
