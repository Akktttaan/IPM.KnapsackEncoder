using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace App
{
    public static class BigIntegerExtensions
    {
        public static BigInteger Sum(this IEnumerable<BigInteger> arr)
        {
            return arr.Aggregate<BigInteger, BigInteger>(0, (current, item) => current + item);
        } 
    }
}