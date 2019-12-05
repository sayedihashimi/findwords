using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace FindWords.Shared {
    public class CommonWords : ICommonWords {
        private List<string> _commonSixLetterWords;

        public async Task<List<string>> GetCommonSixLetterWordsAsync() {
            if (_commonSixLetterWords == null) {
                _commonSixLetterWords = await ReadSixLetterWordsFromResx();
            }

            return _commonSixLetterWords;
        }

        protected async Task<List<string>> ReadSixLetterWordsFromResx() {
            var assembly = typeof(CommonWords).GetTypeInfo().Assembly;
            using Stream stream = assembly.GetManifestResourceStream(Strings.SixLetterWordFileResxName);

            using var reader = new StreamReader(stream);
            List<string> words = new List<string>();
            string currentLine;
            while ((currentLine = await reader.ReadLineAsync()) != null) {
                words.Add(currentLine);
            }

            return words;
        }
    }

    internal class Strings {
        internal const string SixLetterWordFileResxName = "FindWords.Shared.assets.common-six-letter-words.txt";
    }
}
