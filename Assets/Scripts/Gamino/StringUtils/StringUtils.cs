/* |--------------------------------------------------------|
 * |              Pathfinding Algorith Test Results         |
 * |--------------------------------------------------------| 
 * |        Method      |   Speed   |   Tests   |String Size|
 * |--------------------------------------------------------|        
 * |      SortLetters   |  5.610ms  |  1000000  |186000chars|
 * |    SortLetters2    |  14.065ms |  1000000  |186000chars|
 * |--------------------------------------------------------| 
 * |Specs                                                   |
 * |CPU:    Intel(R) Core(TM) i5-10600K CPU @ 4.10GHz       |
 * |GPU:    Radeon RX 480 4gb                               |
 * |Ram:    DD4 16gb 4200MHz                                |
 * |--------------------------------------------------------|
 * 
 * Even though SortLettersHasMap is of complexity O(n), the use
 * of Dictionary seems slower than iterating through the arrays.
 * Thus SortLetter method O(n^2), was chosen for the solution
 */




using System;
using System.Collections.Generic;

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


        public static void SortLettersHashMap(ref byte[] inputAndOutput, byte[] sortOrder)
        {
            Dictionary<byte, int> byteMap = new Dictionary<byte, int>();

            for(int i = 0; i < sortOrder.Length; i++)
            {
                byteMap.Add(sortOrder[i], 0);
            }

            for (int i = 0; i < inputAndOutput.Length; i++)
            {
                byteMap[inputAndOutput[i]]++;
            }

            int index = 0;
            for (int i = 0; i < sortOrder.Length; i++)
            {
                for (int j = 0; j < byteMap[sortOrder[i]]; j++)
                {
                    inputAndOutput[index] = sortOrder[i];
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
