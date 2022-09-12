using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StringUtility{
    public class StringUtils
    {
        public static void SortLetters(ref byte[] inputAndOutput, byte[] sortOrder)
        {
            byte[] sortedArray = new byte[inputAndOutput.Length];
            int index = 0;
            for (int i = 0; i < sortOrder.Length; i++)
            {
                for (int j = 0; j < inputAndOutput.Length; j++)
                {
                    if (sortOrder[i] == inputAndOutput[j])
                    {
                        sortedArray[index] = inputAndOutput[j];
                        index++;
                    }
                }
            }
            inputAndOutput = sortedArray;
        }

        public static void SortLetters2(ref byte[] inputAndOutput, byte[] sortOrder)
        {
            Dictionary<byte, int> byteMap = new Dictionary<byte, int>();

            for(int i = 0; i < sortOrder.Length; i++)
            {
                byte b = sortOrder[i];
                byteMap.Add(b, 0);
            }

            for (int i = 0; i < inputAndOutput.Length; i++)
            {
                byte b = inputAndOutput[i];
                byteMap[b]++;
            }

            int index = 0;
            foreach (byte b in byteMap.Keys)
            {
                for (int i = 0; i < byteMap[b]; i++)
                {
                    inputAndOutput[index] = b;
                    index++;
                }
            }

        }

        public static void DebugTest(byte[] bytearray)
        {
            string result = "";
            foreach (byte b in bytearray)
            {
                char c = Convert.ToChar(b);
                result += "," + c;
            }
            UnityEngine.Debug.Log(result);
        }
    }
}
