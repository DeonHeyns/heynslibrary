using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using HeynsLibrary.Serialization;

namespace HeynsLibrary.Compression
{
    public static class Compressor
    {
        public static byte[] Compress<T>(T obj)
        {
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                using (var gzip = new GZipStream(ms, CompressionMode.Compress, false))
                {
                    var objToSerialize = Serializer.BinarySerialize<T>(obj);
                    gzip.Write(objToSerialize, 0, objToSerialize.Length);
                }
                bytes = ms.ToArray();
            }
            return bytes;
        }

        public static T Decompress<T>(byte[] compressedObject)
        {
            T decompressed = default(T);
            GZipStream gzip = null;
            using (var ms = new MemoryStream())
            {
                ms.Write(compressedObject, 0, compressedObject.Length);

                // Reset the position
                ms.Position = 0;

                using (gzip = new GZipStream(ms,
                    CompressionMode.Decompress, false))
                using (var outputMemoryStream = new MemoryStream())
                {
                    // Buffer the read
                    var buffer = new byte[1024];
                    var byteRead = -1;
                    byteRead = gzip.Read(buffer, 0, buffer.Length);
                    while (byteRead > 0)
                    {
                        outputMemoryStream.Write(buffer, 0, byteRead);
                        byteRead = gzip.Read(buffer, 0, buffer.Length);
                    }
                    decompressed = Serializer.BinaryDeserialize<T>(outputMemoryStream.ToArray());
                }
            }
            return decompressed;
        }
    }
}