using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    public static class StringExtensions
    {
        public static string ToBinary(this string str)
        {
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(str);
            return string.Join("", utf8Bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
        }

        public static string BinaryToString(this string binary)
        {
            List<byte> bytes = new List<byte>();

            for (int i = 0; i < binary.Length; i += 8)
            {
                int endIndex = Math.Min(i + 8, binary.Length);
                string byteString = binary.Substring(i, endIndex - i).PadRight(8, '0');
                byte b = Convert.ToByte(byteString, 2);
                bytes.Add(b);
            }

            return Encoding.UTF8.GetString(bytes.ToArray());
        }
    }
}