using System;
using System.Linq;
using System.Text;
using MathNet.Numerics;

namespace App
{
    public class KnapsackEncoder
    {
        private readonly int _keyDimension;
        private readonly string _text;

        public KnapsackEncoder(int keyDimension, string text)
        {
            _keyDimension = keyDimension;
            _text = text;
        }

        private int[] GenerateSuperincreasingSequence()
        {
            var rnd = new Random();
            var sequence = new int[_keyDimension];
            sequence[0] = rnd.Next(0, 10);

            for (var i = 1; i < _keyDimension; i++)
            {
                sequence[i] = rnd.Next(sequence.Sum() * 2 + 1, sequence.Sum() * 2 + rnd.Next(2, 20));
            }

            return sequence;
        }

        public (int[], int, int) Encrypt()
        {
            var rnd = new Random();
            var privateKey = GenerateSuperincreasingSequence();
            var m = rnd.Next(privateKey.Sum() + 1,
                privateKey.Sum() + 100);
            var n = Constants.PrimeNumbers.First();
            var i = 1;
            while (Euclid.GreatestCommonDivisor(n, m) != 1)
            {
                n = Constants.PrimeNumbers[i];
                i++;
            }

            var openKey = privateKey.Select(x => x * n % m).ToArray();

            var binaryText = ConvertTextToBinary(_text);
            var encryptedValues = new int[binaryText.Length / _keyDimension + 1];
            for (i = 0; i < binaryText.Length / _keyDimension; i++)
            {
                var startIndex = i * _keyDimension;
                var remainingLength = Math.Min(_keyDimension, binaryText.Length - startIndex);
                var chunk = binaryText.Substring(startIndex, remainingLength);
                if (chunk.Length < _keyDimension) chunk.PadLeft(_keyDimension, '0');
                encryptedValues[i] = SumNumbersByBits(openKey, chunk);
            }

            return (encryptedValues, n, m);
        }

        public string Decrypt(int[] encryptedValues, int n, int m)
        {
            var inverseN = ModInverse(n, m);
            var binariyes = encryptedValues.Select(x => x * inverseN % m).ToArray();

            return ConvertBinaryToText(binariyes);
        }

        private string ConvertTextToBinary(string text)
        {
            var binaryStringBuilder = new StringBuilder();

            foreach (var binary in text.Select(c => Convert.ToString(Constants.RussianLetters[c.ToString()], 2)))
            {
                binaryStringBuilder.Append(binary);
            }

            return binaryStringBuilder.ToString();
        }

        public string ConvertBinaryToText(int[] binaryText)
        {
            var textBuilder = new StringBuilder();

            for (var i = 0; i < binaryText.Length; i += 8)
            {
                textBuilder.Append(Constants.RussianLettersReverse[binaryText[i]]); // Добавление символа в текст
            }

            return textBuilder.ToString();
        }
     

        public int SumNumbersByBits(int[] numbers, string bits)
        {
            if (bits.Length != numbers.Length)
            {
                throw new ArgumentException("Длина массива битов должна быть равна длине массива чисел.");
            }

            var sum = 0;
            for (var i = 0; i < bits.Length; i++)
            {
                if (bits[i] == '1')
                {
                    sum += numbers[i];
                }
            }

            return sum;
        }

        // Метод для вычисления модульного обратного
        public int ModInverse(int n, int m)
        {
            int a = n, b = m;
            int x = 0, y = 1, lastX = 1, lastY = 0;
            int temp;

            while (b != 0)
            {
                var quotient = a / b;
                var remainder = a % b;

                a = b;
                b = remainder;

                temp = x;
                x = lastX - quotient * x;
                lastX = temp;

                temp = y;
                y = lastY - quotient * y;
                lastY = temp;
            }

            if (lastX < 0)
            {
                lastX += m;
            }

            return lastX;
        }
    }
}