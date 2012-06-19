using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HeynsLibrary
{
    public static class StringExtensions
    {
        /// <summary>
        /// Encrypts the specified to encrypt.
        /// </summary>
        /// <param name="toEncrypt">To encrypt.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string Encrypt(this string toEncrypt, string key)
        {
            return Cryptography.Cryptography.Encrypt(toEncrypt, key);
        }

        /// <summary>
        /// Decrypts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string Decrypt(this string value, string key)
        {
            return Cryptography.Cryptography.Decrypt(value, key);
        }

        /// <summary>
        /// Converts the byte[] "timestamp" to a string.
        /// </summary>
        /// <param name="binary">The binary.</param>
        /// <returns></returns>
        public static string TimestampToString(this byte[] binary)
        {
            string result = "";
            foreach (byte b in binary)
            {
                result += b.ToString() + "|";
            }
            result = result.Substring(0, result.Length - 1);
            return result;
        }

        /// <summary>
        /// Converts the string to a timestamp value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>byte[]</returns>
        public static byte[] StringToTimestamp(this string value)
        {
            string[] arr = value.Split('|');
            byte[] bytes = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                bytes[i] = Convert.ToByte(arr[i]);
            }
            return bytes;
        }

        /// <summary>
        /// Encrypts the string to MD5Hash.
        /// </summary>
        /// <param name="value">The string to encrypt.</param>
        /// <returns></returns>
        public static string ToMD5Hash(this string value)
        {
            return Cryptography.Cryptography.CreateMD5Hash(value);
        }

        /// <summary>
        /// Determines whether [has unique letters] [the specified word].
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>bool</returns>
        public static bool HasUniqueLetters(this string word)
        {
            int validator = 0;
            for (int i = 0; i < word.Length; i++)
            {
                int val = word.ToCharArray()[i];
                if ((validator & (1 << val)) > 0) return false;
                validator |= (1 << val);
            }
            return true;
        }

        /// <summary>
        /// Removes the duplicate letter in a string.
        /// </summary>
        /// <param name="word">The word.</param>
        public static void RemoveDuplicateLetter(this string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                throw new ArgumentNullException("word");
            if (word.Length < 2)
                throw new ArgumentOutOfRangeException("word");

            int tail = 1;
            for (int i = 0; i < word.Length; i++)
            {
                int j = 0;
                for (; j < tail; j++)
                    if (word.ToCharArray()[i] == word.ToCharArray()[j])
                    {
                        word.ToCharArray()[i] = ' ';
                        break;
                    }
                if (j == tail)
                {
                    word.ToCharArray()[tail] = word.ToCharArray()[i];
                    ++tail;
                }
            }
        }

        /// <summary>
        /// Gets the number of words within a string array.
        /// </summary>
        /// <param name="words">The words.</param>
        /// <returns></returns>
        public static Dictionary<string, int> GetWordCount(this string[] words)
        {
            var wordsWithCount = new Dictionary<string, int>();
            foreach (string word in words)
            {
                var wordLowered = word.ToLowerInvariant();
                if (!string.IsNullOrWhiteSpace(wordLowered.Trim()))
                {
                    if (!wordsWithCount.ContainsKey(wordLowered))
                        wordsWithCount.Add(wordLowered, 0);
                    wordsWithCount.Add(wordLowered, wordsWithCount[wordLowered] + 1);
                }
            }
            return wordsWithCount;
        }

        /// <summary>
        /// Gets the frequency of word in an array of strings.
        /// </summary>
        /// <param name="words">The words.</param>
        /// <param name="word">The word.</param>
        /// <returns></returns>
        public static int GetFrequencyOfWord(this string[] words, string word)
        {
            if (words == null || string.IsNullOrWhiteSpace(word))
                return -1;
            var wordsWithCount = words.GetWordCount();
            if (wordsWithCount.ContainsKey(word))
            {
                return wordsWithCount[word];
            }
            return 0;
        }

        /// <summary>
        /// Obtains the shortest distance between words.
        /// </summary>
        /// <param name="words">The words.</param>
        /// <param name="firstWord">The first word.</param>
        /// <param name="secondWord">The second word.</param>
        /// <param name="orderMatters">Does order matters.</param>
        /// <returns></returns>
        public static int ShortestDistanceBetweenWords(this string[] words, string firstWord, string secondWord, bool orderMatters = false)
        {
            int pos = 0;
            int min = int.MaxValue / 2;
            int firstWordPos = -min;
            int secondWordPos = -min;
            for (int i = 0; i < words.Length; i++)
            {
                string currentWord = words[i];
                if (currentWord.Equals(firstWord))
                {
                    firstWordPos = pos;
                    if (orderMatters)
                    {
                        int distance = firstWordPos - secondWordPos;
                        if (min > distance)
                            min = distance;
                    }
                }
                else if (currentWord.Equals(secondWord))
                {
                    secondWordPos = pos;
                    int distance = secondWordPos - firstWordPos;
                    if (min > distance) min = distance;
                }
                ++pos;
            }
            return min;
        }

        /// <summary>
        /// Appends the given key/value to the url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static string AppendQueryString(this string url, string key, string value)
        {
            // Remove existing item
            var regEx = new Regex(@"(?<pre>(.+)(\?|&)" + key + @"=)(?<existing>[^\?&]+)(?<post>.*)", RegexOptions.IgnoreCase);
            if (regEx.IsMatch(url))
            {
                url = regEx.Replace(url, "${pre}" + value + "${post}");
            }
            else
            {
                // Append
                if (url.IndexOf('?') != -1) { url += "&"; } else { url += "?"; }
                url += key + "=" + value;
            }
            // Return
            return url;
        }

        /// <summary>
        /// Converts the given string to its URL representation.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A string with the formatted URL</returns>
        public static string ToUrl(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) 
                return string.Empty;
            value = value.Trim().ToLowerInvariant();
            value = (!value.StartsWith("http")) ? string.Format("http://{0}", value) : value;
            return value;
        }

        /// <summary>
        /// Converts the given string to its Secure URL representation.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A string with the formatted URL</returns>
        public static string ToSecureUrl(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;
            value = value.Trim().ToLowerInvariant();
            value = (!value.StartsWith("https")) ? string.Format("https://{0}", value) : value;
            return value;
        }
    }
}