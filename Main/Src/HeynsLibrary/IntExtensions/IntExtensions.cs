using System;
using System.Collections;
using System.Text;

namespace HeynsLibrary
{
    public static class IntExtensions
    {
        /// <summary>
        /// Converts the an integer to the English word.
        /// </summary>
        /// <param name="num">The num.</param>
        /// <returns></returns>
        public static string ConvertIntToEnglishWord(this int num)
        {
            StringBuilder sb = new StringBuilder();
            int len = 1;
            while (Math.Pow((double)10, (double)len) < num)
            {
                len++;
            }

            string[] wordarr1 = { "", "One ", "Two ", "Three", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] wordarr11 = { "", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] wordarr10 = { "", "Ten ", "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] wordarr100 = { "", "Hundred ", "Thousand " };

            int tmp = 0;

            if (num == 0)
            {
                sb.Append("Zero");
            }
            else
            {
                if (len > 3 && len % 2 == 0)
                {
                    len++;
                }
                do
                {
                    // Number greater than 999
                    if (len > 3)
                    {
                        tmp = (num / (int)Math.Pow((double)10, (double)len - 2));
                        // If tmp is 2 digit number and not a multiple of 10
                        if (tmp / 10 == 1 && tmp % 10 != 0)
                        {
                            sb.Append(wordarr11[tmp % 10]);
                        }
                        else
                        {
                            sb.Append(wordarr10[tmp / 10]);
                            sb.Append(wordarr1[tmp % 10]);
                        }
                        if (tmp > 0)
                        {
                            sb.Append(wordarr100[len / 2]);
                        }
                        num = num % (int)(Math.Pow((double)10, (double)len - 2));
                        len = len - 2;
                    }
                    else
                    { // Number is less than 1000
                        tmp = num / 100;
                        if (tmp != 0)
                        {
                            sb.Append(wordarr1[tmp]);
                            sb.Append(wordarr100[len / 2]);
                        }
                        tmp = num % 100;
                        if (tmp / 10 == 1 && tmp % 10 != 0)
                        {
                            sb.Append(wordarr11[tmp % 10]);
                        }
                        else
                        {
                            sb.Append(wordarr10[tmp / 10]);
                            sb.Append(wordarr1[tmp % 10]);
                        }
                        len = 0;
                    }
                } while (len > 0);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Algorithm to find all pairs of integers within an array which sum to a specified value
        /// </summary>
        /// <param name="array"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static IEnumerable ReturnPairSums(this int[] array, int sum)
        {
            if (array.Length > 0 || array == null)
                throw new ArgumentNullException("array");
            Array.Sort(array);
            int first = 0;
            int last = array.Length - 1;
            while (first < last)
            {
                int s = array[first] + array[last];
                if (s == sum)
                {
                    yield return array[first] + " " + array[last];
                    ++first;
                    --last;
                }
                else
                {
                    if (s < sum) ++first;
                    else --last;
                }
            }
        }

        public static int AddWithoutUsingAddOperator(this int a, int b)
        {
            if (b == 0) return a;
            int sum = a ^ b; // an Add without carrying
            int carry = (a & b) << 1; // Carry, but we don’t add
            return AddWithoutUsingAddOperator(sum, carry); // Recursive call
        }
    }
}