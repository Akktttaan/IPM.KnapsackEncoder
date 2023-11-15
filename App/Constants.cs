﻿using System.Collections.Generic;

namespace App
{
    public class Constants
    {
        public static int[] PrimeNumbers = {
            2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103,
            107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223,
            227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347,
            349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463,
            467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607,
            613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743,
            751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883,
            887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997
        };
        
        public static Dictionary<string, int> RussianLetters = new Dictionary<string, int>
        {
            { "А", 1 },
            { "Б", 2 },
            { "В", 3 },
            { "Г", 4 },
            { "Д", 5 },
            { "Е", 6 },
            { "Ё", 7 },
            { "Ж", 8 },
            { "З", 9 },
            { "И", 10 },
            { "Й", 11 },
            { "К", 12 },
            { "Л", 13 },
            { "М", 14 },
            { "Н", 15 },
            { "О", 16 },
            { "П", 17 },
            { "Р", 18 },
            { "С", 19 },
            { "Т", 20 },
            { "У", 21 },
            { "Ф", 22 },
            { "Х", 23 },
            { "Ц", 24 },
            { "Ч", 25 },
            { "Ш", 26 },
            { "Щ", 27 },
            { "Ъ", 28 },
            { "Ы", 29 },
            { "Ь", 30 },
            { "Э", 31 },
            { "Ю", 32 },
            { "Я", 33 },
            { "а", 34 },
            { "б", 35 },
            { "в", 36 },
            { "г", 37 },
            { "д", 38 },
            { "е", 39 },
            { "ё", 40 },
            { "ж", 41 },
            { "з", 42 },
            { "и", 43 },
            { "й", 44 },
            { "к", 45 },
            { "л", 46 },
            { "м", 47 },
            { "н", 48 },
            { "о", 49 },
            { "п", 50 },
            { "р", 51 },
            { "с", 52 },
            { "т", 53 },
            { "у", 54 },
            { "ф", 55 },
            { "х", 56 },
            { "ц", 57 },
            { "ч", 58 },
            { "ш", 59 },
            { "щ", 60 },
            { "ъ", 61 },
            { "ы", 62 },
            { "ь", 63 },
            { "э", 64 },
            { "ю", 65 },
            { "я", 66 }
        };
        
        public static Dictionary<int, string> RussianLettersReverse = new Dictionary<int, string>
        {
            { 1, "А" },
            { 2, "Б" },
            { 3, "В" },
            { 4, "Г" },
            { 5, "Д" },
            { 6, "Е" },
            { 7, "Ё" },
            { 8, "Ж" },
            { 9, "З" },
            { 10, "И" },
            { 11, "Й" },
            { 12, "К" },
            { 13, "Л" },
            { 14, "М" },
            { 15, "Н" },
            { 16, "О" },
            { 17, "П" },
            { 18, "Р" },
            { 19, "С" },
            { 20, "Т" },
            { 21, "У" },
            { 22, "Ф" },
            { 23, "Х" },
            { 24, "Ц" },
            { 25, "Ч" },
            { 26, "Ш" },
            { 27, "Щ" },
            { 28, "Ъ" },
            { 29, "Ы" },
            { 30, "Ь" },
            { 31, "Э" },
            { 32, "Ю" },
            { 33, "Я" },
            { 34, "а" },
            { 35, "б" },
            { 36, "в" },
            { 37, "г" },
            { 38, "д" },
            { 39, "е" },
            { 40, "ё" },
            { 41, "ж" },
            { 42, "з" },
            { 43, "и" },
            { 44, "й" },
            { 45, "к" },
            { 46, "л" },
            { 47, "м" },
            { 48, "н" },
            { 49, "о" },
            { 50, "п" },
            { 51, "р" },
            { 52, "с" },
            { 53, "т" },
            { 54, "у" },
            { 55, "ф" },
            { 56, "х" },
            { 57, "ц" },
            { 58, "ч" },
            { 59, "ш" },
            { 60, "щ" },
            { 61, "ъ" },
            { 62, "ы" },
            { 63, "ь" },
            { 64, "э" },
            { 65, "ю" },
            { 66, "я" }
        };
    }
}