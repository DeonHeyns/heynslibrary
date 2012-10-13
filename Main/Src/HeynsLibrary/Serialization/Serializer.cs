using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HeynsLibrary.Serialization
{
    public static class Serializer
    {
        public static string XmlSerialize<T>(T obj)
        {
            return XmlSerializeAsync(obj).Result;
        }

        public static async Task<string> XmlSerializeAsync<T>(T obj)
        {
            CheckNullArgument(obj);
            if (!typeof(T).CanSerialize())
            {
                throw new ArgumentException(
                   string.Format("{0} is not a Serializable type.", typeof(T).Name));
            }
            var xml = string.Empty;
            var xs = new XmlSerializer(typeof(T));
            
            await Task.Factory.StartNew(() =>
            {
                using (var ms = new MemoryStream())
                {
                    xs.Serialize(ms, obj);
                    var bytes = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(bytes, 0, bytes.Length);
                    xml = Encoding.UTF8.GetString(bytes);
                }
            });

            return xml;
        }

        public static T XmlDeserialize<T>(string xml)
        {
            return XmlDeserializeAsync<T>(xml).Result;
        }

        public static async Task<T> XmlDeserializeAsync<T>(string xml)
        {
            var xs = new XmlSerializer(typeof(T));
            object obj = null;

            await Task.Factory.StartNew(() =>
            {
                obj = xs.Deserialize(new StringReader(xml));
                if (obj == null)
                    throw new Exception(
                        string.Format("Deserialization of {0} failed", typeof(T)));
            });

            return (T)obj;
        }

        public static byte[] BinarySerialize<T>(T obj)
        {
            CheckNullArgument(obj);
            if (!typeof(T).CanSerialize())
            {
                throw new ArgumentException(
                   string.Format("{0} is not a Serializable type.", typeof(T).Name));
            }
            return Serialize(obj, new BinaryFormatter());
        }

        public static T BinaryDeserialize<T>(byte[] binary)
        {
            T obj;
            using (var ms = new MemoryStream(binary))
            {
                obj = Deserialize<T>(new BinaryFormatter(), ms);
            }
            return obj;
        }

        public static string SoapSerialize<T>(T obj)
        {
            CheckNullArgument(obj);
            if (!typeof(T).CanSerialize())
            {
                throw new ArgumentException(
                   string.Format("{0} is not a Serializable type.", typeof(T).Name));
            }
            var bytes = Serialize(obj, new SoapFormatter());
            var soap = Encoding.UTF8.GetString(bytes);
            return soap;
        }

        public static T SoapDeserialize<T>(string soapEnvelope)
        {
            T obj;
            using (var ms = new MemoryStream())
            {
                using (var stream = new StreamWriter(ms))
                {
                    stream.Write(soapEnvelope);
                    obj = Deserialize<T>(new SoapFormatter(), ms);
                }
            }
            return obj;
        }

        public static string DataContractSerilize<T>(T obj)
        {
            CheckNullArgument(obj);
            if (!typeof(T).CanSerialize())
            {
                throw new ArgumentException(
                   string.Format("{0} is not a Serializable type.", typeof(T).Name));
            }
            string xml;
            var dcs = new DataContractSerializer(typeof(T));
            using (var ms = new MemoryStream())
            {
                dcs.WriteObject(ms, obj);
                var bytes = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(bytes, 0, bytes.Length);
                xml = Encoding.UTF8.GetString(bytes);
            }
            return xml;
        }

        public static T DataContractDeserialize<T>(string xml)
        {
            var dcs = new DataContractSerializer(typeof(T));
            var obj = dcs.ReadObject(System.Xml.XmlReader.Create(new StringReader(xml)));
            if (obj == null)
                throw new Exception(
                    string.Format("Deserialization of {0} failed", typeof(T)));
            return (T)obj;
        }
        
        private static void CheckNullArgument<T>(T obj)
        {
            if (ReferenceEquals(obj, null)) throw new ArgumentNullException("obj");
        }

        private static byte[] Serialize<T>(T serializeType, IFormatter formatter)
        {
            byte[] byteData;
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, serializeType);
                var bytes = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(bytes, 0, bytes.Length);
                byteData = bytes;
            }
            return byteData;
        }

        private static T Deserialize<T>(IFormatter formatter, Stream stream)
        {
            var obj = (T)formatter.Deserialize(stream);
            return obj;
        }
    }
}