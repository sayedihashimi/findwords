using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FindWords.Shared {
    public class StringPermuter {

        public IEnumerable<string> Permute(string str) {
            Debug.Assert(str != null);
            return Permute(str, 0, str.Length - 1);
        }
        // https://www.geeksforgeeks.org/c-program-to-print-all-permutations-of-a-given-string-2/
        private IEnumerable<string> Permute(string str, int l, int r) {
            List<string> results = new List<string>();
            if (l == r) {
                Console.WriteLine(str);
                results.Add(str);
                //yield return str;
            }
            else {
                for (int i = l; i <= r; i++) {
                    str = Swap(str, l, i);
                    results.AddRange(Permute(str, l + 1, r));
                    str = Swap(str, l, i);
                }
            }

            return results;
        }

        /** 
        * Swap Characters at position 
        * @param a string value 
        * @param i position 1 
        * @param j position 2 
        * @return swapped string 
        */
        public String Swap(string a, int i, int j) {
            char temp;
            char[] charArray = a.ToCharArray();
            temp = charArray[i];
            charArray[i] = charArray[j];
            charArray[j] = temp;
            string s = new string(charArray);
            return s;
        }

    }
}
