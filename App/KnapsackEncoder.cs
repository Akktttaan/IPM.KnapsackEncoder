using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using MathNet.Numerics;
using Microsoft.CSharp.RuntimeBinder;

namespace App
{
    public class KnapsackEncoder
    {
        private readonly int _keyDimension;
        private readonly BigInteger _n;
        private readonly BigInteger _m;
        private readonly List<BigInteger> _privateKey;
        private readonly List<BigInteger> _openKey;
        private readonly string _text;

        public KnapsackEncoder(int keyDimension, string text, BigInteger n, BigInteger m, BigInteger[] privateKey, BigInteger[] openKey)
        {
            _keyDimension = keyDimension;
            _text = text;
            _n = n;
            _m = m;
            _privateKey = privateKey.ToList();
            _openKey = openKey.ToList();
        }

        private static BigInteger[] GenerateSuperincreasingSequence(int keyDimension)
        {
            var rnd = new Random();
            var sequence = new BigInteger[keyDimension];
            BigInteger sum = 0;

            for (var i = 0; i < keyDimension; i++)
            {
                var randomNumber = GenerateRandomBigInteger(sum + 1, sum + 20);
                sum += randomNumber;
                sequence[i] = sum;
            }

            return sequence;
        }

        public static (BigInteger[], BigInteger[], BigInteger, BigInteger) Generate(int keyDimension)
        {
            var rnd = new Random();
            var privateKey = GenerateSuperincreasingSequence(keyDimension);
            var m = GenerateRandomBigInteger(privateKey.Sum() + 1, privateKey.Sum() + 100);
            BigInteger n = 2;
            while (Euclid.GreatestCommonDivisor(n, m) != 1)
            {
                n++;
            }

            var openKey = privateKey.Select(x => x * n % m).ToArray();

            return (privateKey, openKey, m, n);
        }
        
        public static BigInteger GenerateRandomBigInteger(BigInteger minValue, BigInteger maxValue)
        {
            Random random = new Random();
            byte[] bytes = new byte[maxValue.ToByteArray().Length];
            random.NextBytes(bytes);

            BigInteger result = new BigInteger(bytes);
            result = BigInteger.Abs(result % (maxValue - minValue)) + minValue;

            return result;
        }

        public BigInteger[] Encrypt()
        {
            var binaryText = _text.ToBinary();
            var encryptedValues = new List<BigInteger>();
            var chunkSize = _openKey.Count;
            for (var i = 0; i < binaryText.Length; i += chunkSize)
            {
                int endIndex = Math.Min(i + chunkSize, binaryText.Length);

                string block = binaryText.Substring(i, endIndex - i).PadRight(chunkSize, '0');

                BigInteger blockValue = 0;

                for (int j = 0; j < chunkSize; j++)
                {
                    int bit = int.Parse(block[j].ToString());
                    blockValue += bit * _openKey[j];
                }

                encryptedValues.Add(blockValue);
            }

            return encryptedValues.ToArray();
        }

        public string Decrypt(BigInteger[] encryptedValues, BigInteger n, BigInteger m, BigInteger[] w)
        {
            StringBuilder binaryResult = new StringBuilder();
            var inverseN = ModInverse(n, m);

            foreach (BigInteger encryptedBlock in encryptedValues)
            {
                // Расшифровываем блок
                BigInteger decryptedValue = encryptedBlock * inverseN % m;

                List<BigInteger> selectedElements = new List<BigInteger>();

                // Раскладываем decryptedValue на элементы w
                foreach (BigInteger wElement in w.OrderByDescending(x => x))
                {
                    while (decryptedValue >= wElement)
                    {
                        decryptedValue -= wElement;
                        selectedElements.Add(wElement);
                    }
                }

                // Преобразование выбранных элементов w в двоичную строку
                foreach (BigInteger element in w)
                {
                    if (selectedElements.Contains(element))
                    {
                        binaryResult.Append("1");
                    }
                    else
                    {
                        binaryResult.Append("0");
                    }
                }
            }

            return binaryResult.ToString().BinaryToString();
        }
     
        // Метод для вычисления модульного обратного
        public BigInteger ModInverse(BigInteger n, BigInteger m)
        {
            BigInteger m0 = m;
            BigInteger y = 0, x = 1;

            while (n > 1)
            {
                BigInteger q = n / m;
                BigInteger t = m;

                m = n % m;
                n = t;
                t = y;

                y = x - q * y;
                x = t;
            }

            return x < 0 ? x + m0 : x;
        }
    }
}