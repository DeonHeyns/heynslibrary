using System;
using System.Linq;
using System.Xml.Serialization;

namespace HeynsLibrary
{
    public static class TypeExtensions
    {
        public static T GenerateInstance<T>(this Type type)
        {
            return (T)Activator.CreateInstance(type);
        }

        public static bool HasInterface<T>(this Type type)
        {
            var @interface = type.GetInterface(typeof(T).FullName, true);
            return @interface != null;
        }

        public static bool CanSerialize(this Type type)
        {
            var serializable = false;
            var hasIXmlSerializable = type.HasInterface<IXmlSerializable>();
            if (hasIXmlSerializable)
                serializable = true;
            if (type.IsSerializable)
                serializable = true;
            return serializable;
        }
    }
}