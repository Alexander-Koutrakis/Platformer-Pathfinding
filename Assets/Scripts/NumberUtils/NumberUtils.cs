using System.Collections.Generic;

namespace NumberUtility
{
    public static class NumberUtils
    {
        public static bool AreDigitsUnique(uint value)
        {
            List<uint> digits = new List<uint>();
            while (value > 0)
            {
                uint digit = value % 10;
                value /= 10;
                if (digits.Contains(digit))
                {
                    return false;
                }
                else
                {
                    digits.Add(digit);
                }
            }
            return true;
        }

    }
}