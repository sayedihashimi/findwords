using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace FindWords.Shared {
    public class WordTreeBuilder {
        /// <summary>
        /// One word per line, no white space before or after word
        /// </summary>
        public async Task<IWordTree> BuildFromDictionaryFileAsync(string filepath) {
            Debug.Assert(File.Exists(filepath));

            using var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(fileStream);

            IWordTree tree = new WordTree();
            int linesProcessed = 0;
            string currentLine;
            while( (currentLine = await reader.ReadLineAsync()) != null) {
                tree.AddWord(currentLine);
                linesProcessed++;
            }

            Console.WriteLine($"lines: {linesProcessed}");
            return tree;
        }
    }
}
