using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FindWords.Shared {
    public class WordFinder : IWordFinder {
        public WordFinder(IWordTree wordTree) {
            WordTree = wordTree;
            MinWordLength = 3;
        }

        public IWordTree WordTree { get; set; }
        public int MinWordLength { get; set; }


        public List<string> FindWordsInString(string str) {
            Debug.Assert(str != null);

            List<char[]> found = FindWordsIn(new char[0], str.ToCharArray());

            List<string> foundAsStr = new List<string>();
            foreach (var chstr in found) {
                foundAsStr.Add(new string(chstr));
            }

            // remove any duplictes
            return foundAsStr.Distinct<string>().ToList();
        }

        protected List<char[]> FindWordsIn(char[] prefix, char[] remaining) {
            Debug.Assert(prefix != null);
            Debug.Assert(remaining != null);

            if (remaining.Length == 0) {
                return new List<char[]>();
            }

            // foreach letter in remainig
            // combine with prefix and then call again
            List<char[]> found = new List<char[]>();

            for (int index = 0; index < remaining.Length; index++) {
                char ch = remaining[index];
                char[] newprefix = new char[prefix.Length + 1];
                Array.Copy(prefix, newprefix, prefix.Length);
                newprefix[newprefix.Length - 1] = ch;

                // Console.WriteLine(newprefix);
                // TODO: Update wortree to use char[]
                var newprefixstr = new string(newprefix);
                if (newprefixstr.Length >= MinWordLength &&
                    WordTree.IsWord(newprefixstr)) {
                    found.Add(newprefix);
                }

                char[] newremaining = new char[remaining.Length - 1];
                int newremindex = 0;
                for (int i = 0; i < remaining.Length; i++) {
                    if (i == index) {
                        // skip this one
                        continue;
                    }
                    newremaining[newremindex] = remaining[i];
                    newremindex++;
                }

                found.AddRange(FindWordsIn(newprefix, newremaining));
            }

            //foreach(char ch in remaining) {
            //    char[] newprefix = new char[prefix.Length + 1];
            //    Array.Copy(prefix, newprefix, prefix.Length);
            //    newprefix[newprefix.Length - 1] = ch;

            //    Console.WriteLine(newprefix);
            //    // TODO: Update wortree to use char[]
            //    if (WordTree.IsWord(new string(newprefix))) {
            //        found.Add(newprefix);
            //    }

            //    char[] newremaining = new char[remaining.Length - 1];
            //    newremaining = remaining[1..];

            //    found.AddRange(FindWordsIn(newprefix, newremaining));
            //}

            return found;
        }
    }
}
