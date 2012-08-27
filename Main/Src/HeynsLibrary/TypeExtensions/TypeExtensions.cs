using System;
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
            var fullName = typeof (T).FullName;
            if (fullName != null)
            {
                var @interface = type.GetInterface(fullName, true);
                return @interface != null;
            }
            return false;
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