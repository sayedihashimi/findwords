using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace FindWords.Shared {
    public class WordTreeBuilder {
        private const string DefaultWordFile = "cspell-filtered_en_US.txt";
        /// <summary>
        /// One word per line, no white space before or after word
        /// </summary>
        public async Task<IWordTree> BuildFromDictionaryFileAsync(string filepath) {
            Debug.Assert(File.Exists(filepath));

            using var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            return await BuildFrom(fileStream);
        }

        public async Task<IWordTree> BuildFromResource() {
            return await BuildFromResource(
                typeof(WordTreeBuilder).GetTypeInfo().Assembly,
                $"FindWords.Shared.assets.{DefaultWordFile}");
        }

        public async Task<IWordTree>BuildFromResource(Assembly assembly, string resxName) {
            Debug.Assert(assembly != null);
            Debug.Assert(resxName != null);

            using Stream resource = assembly.GetManifestResourceStream(resxName);
            return await BuildFrom(resource);
        }

        public async Task<IWordTree>BuildFrom(Stream stream) {
            Debug.Assert(stream != null);

            using var reader = new StreamReader(stream);

            IWordTree tree = new WordTree();
            int linesProcessed = 0;
            string currentLine;
            while ((currentLine = await reader.ReadLineAsync()) != null) {
                tree.AddWord(currentLine);
                linesProcessed++;
            }

            Console.WriteLine($"lines: {linesProcessed}");
            return tree;
        }
    }
}
