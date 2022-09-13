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
                uint digit = value % 10;//mod by 10 to get the last digit
                value /= 10;//divide by 10 to remove the last digit
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