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
        public async Task<IWordTree> BuildFromDictionaryFileAsync(string filepath, bool memEfficientTree = false) {
            Debug.Assert(File.Exists(filepath));

            using var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            return await BuildFrom(fileStream, memEfficientTree);
        }

        /// <summary>
        /// This will return the tree built from a word file that is an embedded resource.
        /// </summary>
        /// <returns></returns>
        public async Task<IWordTree> BuildFromResource(bool memEfficientTree = false) {
            return await BuildFromResource(
                            typeof(WordTreeBuilder).GetTypeInfo().Assembly,
                            $"FindWords.Shared.assets.{DefaultWordFile}", memEfficientTree);
        }

        public async Task<IWordTree>BuildFromResource(Assembly assembly, string resxName, bool memEfficientTree = false) {
            Debug.Assert(assembly != null);
            Debug.Assert(resxName != null);

            using Stream resource = assembly.GetManifestResourceStream(resxName);
            return await BuildFrom(resource, memEfficientTree);
        }

        public async Task<IWordTree> BuildFrom(Stream stream, bool memEfficientTree = false) {
            Debug.Assert(stream != null);

            using var reader = new StreamReader(stream);

            IWordTree tree = memEfficientTree ? new MemEfficientWordTree() : new WordTree();

            string currentLine;
            while ((currentLine = await reader.ReadLineAsync()) != null) {
                tree.AddWord(currentLine);
            }
            return tree;
        }

        public async Task<IWordFinder> BuidWordFinderAsync(bool memEfficientTree = false) {
            return new WordFinder(await BuildFromResource(memEfficientTree));
        }
    }
}
