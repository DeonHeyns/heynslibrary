using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HeynsLibrary.Cryptography
{
    public class Cryptography
    {
        /// <summary>
        /// Encrypts the specified clear data value.
        /// </summary>
        /// <param name="clearData">The clear data value.</param>
        /// <param name="key">The key.</param>
        /// <param name="iv">The iv.</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] clearData, byte[] key, byte[] iv)
        {
            byte[] encryptedData = null;
            using (var ms = new MemoryStream())
            {
                var alg = Rijndael.Create();
                alg.Key = key;
                alg.IV = iv;

                using (var cs = new CryptoStream(ms, alg.CreateEncryptor(),
                                                 CryptoStreamMode.Write))
                {
                    cs.Write(clearData, 0, clearData.Length);
                    cs.Close();
                    encryptedData = ms.ToArray();
                }
            }
            return encryptedData;
        }

        /// <summary>
        /// Encrypts the specified clear text value.
        /// </summary>
        /// <param name="clearText">The clear text value.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static string Encrypt(string clearText, string password)
        {
            var clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            var pdb = new PasswordDeriveBytes(password,
                                              new byte[]
                                              {
                                                  0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
                                                  0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                                              });

            var encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// Encrypts the byte[].
        /// </summary>
        /// <param name="clearData">The data.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] clearData, string password)
        {
            var pdb = new PasswordDeriveBytes(password,
                                              new byte[]
                                              {
                                                  0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
                                                  0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                                              });
            return Encrypt(clearData, pdb.GetBytes(32), pdb.GetBytes(16));
        }

        /// <summary>
        /// Encrypts the specified file in and copies the data to disk specified by the fileOut parameter.
        /// </summary>
        /// <param name="fileIn">The file in.</param>
        /// <param name="fileOut">The file out.</param>
        /// <param name="password">The password.</param>
        public static void Encrypt(string fileIn, string fileOut, string password)
        {
            using (var fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read))
            {
                using (var fsOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var pdb = new PasswordDeriveBytes(password,
                                                      new byte[]
                                                      {
                                                          0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
                                                          0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                                                      });

                    var alg = Rijndael.Create();
                    alg.Key = pdb.GetBytes(32);
                    alg.IV = pdb.GetBytes(16);

                    using (var cs = new CryptoStream(fsOut, alg.CreateEncryptor(),
                                                     CryptoStreamMode.Write))
                    {
                        int bufferLen = 4096;
                        byte[] buffer = new byte[bufferLen];
                        int bytesRead;

                        do
                        {
                            bytesRead = fsIn.Read(buffer, 0, bufferLen);
                            cs.Write(buffer, 0, bytesRead);
                        } while (bytesRead != 0);
                    }
                }
            }
        }

        /// <summary>
        /// Decrypts the specified data.
        /// </summary>
        /// <param name="cipherData">The ciphered data.</param>
        /// <param name="key">The key.</param>
        /// <param name="iv">The iv.</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] cipherData, byte[] key, byte[] iv)
        {
            byte[] decryptedData = null;
            using (var ms = new MemoryStream())
            {
                var alg = Rijndael.Create();
                alg.Key = key;
                alg.IV = iv;
                using (var cs = new CryptoStream(ms, alg.CreateDecryptor(),
                                                 CryptoStreamMode.Write))
                {
                    cs.Write(cipherData, 0, cipherData.Length);
                    cs.Close();
                    decryptedData = ms.ToArray();
                }
            }
            return decryptedData;
        }

        /// <summary>
        /// Decrypts the specified text.
        /// </summary>
        /// <param name="cipherText">The ciphered text.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public static string Decrypt(string cipherText, string password)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);
            var pdb = new PasswordDeriveBytes(password,
                                              new byte[]
                                              {
                                                  0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
                                                  0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                                              });

            var decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }

        /// <summary>
        /// Decrypts the specified byte[] of data.
        /// </summary>
        /// <param name="cipherData">The ciphered data.</param>
        /// <param name="Password">The password.</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] cipherData, string Password)
        {
            var pdb =
            new PasswordDeriveBytes(Password,
                                    new byte[]
                                    {
                                        0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
                                        0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                                    });
            return Decrypt(cipherData, pdb.GetBytes(32), pdb.GetBytes(16));
        }

        /// <summary>
        /// Decrypts the specified file in and copies the data to disk specified by the fileOut parameter.
        /// </summary>
        /// <param name="fileIn">The file in.</param>
        /// <param name="fileOut">The file out.</param>
        /// <param name="password">The password.</param>
        public static void Decrypt(string fileIn, string fileOut, string password)
        {
            using (var fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read))
            {
                using (var fsOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    var pdb = new PasswordDeriveBytes(password, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    var alg = Rijndael.Create();
                    alg.Key = pdb.GetBytes(32);
                    alg.IV = pdb.GetBytes(16);

                    using (var cs = new CryptoStream(fsOut, alg.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        int bufferLen = 4096;
                        var buffer = new byte[bufferLen];
                        int bytesRead;
                        do
                        {
                            bytesRead = fsIn.Read(buffer, 0, bufferLen);
                            cs.Write(buffer, 0, bytesRead);
                        } while (bytesRead != 0);
                    }
                }
            }
        }

        /// <summary>
        /// Creates the MD5 hash for the stringToHash.
        /// </summary>
        /// <param name="stringToHash">The string to hash.</param>
        /// <returns></returns>
        public static string CreateMD5Hash(string stringToHash)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(stringToHash));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// Verifies the MD5 hash of the input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        public static bool VerifyMD5Hash(string input, string hash)
        {
            string hashOfInput = CreateMD5Hash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}