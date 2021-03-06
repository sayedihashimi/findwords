﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using FindWords.Shared.Extensions;
using System.Linq;

namespace FindWords.Shared {
    public class SixLetterWordService : ISixLetterWordService {
        private List<string> _commonSixLetterWords;
        private List<int> _randomIndex;
        private int _currentIndex = 0;
        private int _currentRandomIndex = 0;

        public bool Randomize { get; set; }

        public SixLetterWordService() {
            _commonSixLetterWords = ReadSixLetterWordsFromResx().Result;
            _randomIndex = CreateIndexForRandomAccess();
        }

        public string GetNextWord() {
            if (Randomize) {
                _randomIndex ??= CreateIndexForRandomAccess();
                _currentRandomIndex = (_currentRandomIndex + 1) % (_randomIndex.Count);
                return _commonSixLetterWords[_randomIndex[_currentRandomIndex]];
            }
            else {
                _currentIndex = (_currentIndex + 1) % (_commonSixLetterWords.Count);
                return _commonSixLetterWords[_currentIndex];
            }
        }

        public string GetPreviousWord() {
            if (Randomize) {
                _randomIndex ??= CreateIndexForRandomAccess();
                _currentRandomIndex = _currentRandomIndex - 1;
                if (_currentRandomIndex < 0) { _currentRandomIndex = _randomIndex.Count - 1; }

                return _commonSixLetterWords[_currentRandomIndex];
            }
            else {
                _currentIndex = _currentIndex - 1;
                if (_currentIndex < 0) { _currentIndex = _commonSixLetterWords.Count - 1; }

                return _commonSixLetterWords[_currentIndex];
            }
        }

        public string GetNextWordRandom() {
            _randomIndex ??= CreateIndexForRandomAccess();
            _currentRandomIndex = (_currentRandomIndex + 1) % (_randomIndex.Count);
            return _commonSixLetterWords[_randomIndex[_currentRandomIndex]];
        }

        public string GetPreviousWordRandom() {
            _randomIndex ??= CreateIndexForRandomAccess();
            _currentRandomIndex = _currentRandomIndex - 1;
            if(_currentRandomIndex < 0) { _currentRandomIndex = _randomIndex.Count - 1; }

            return _commonSixLetterWords[_currentRandomIndex];
        }

        public async Task<List<string>> GetCommonSixLetterWordsAsync() {
            _commonSixLetterWords ??= await ReadSixLetterWordsFromResx();

            return _commonSixLetterWords;
        }

        protected async Task<List<string>> ReadSixLetterWordsFromResx() {
            var assembly = typeof(SixLetterWordService).GetTypeInfo().Assembly;
            using Stream stream = assembly.GetManifestResourceStream(Strings.SixLetterWordFileResxName);

            using var reader = new StreamReader(stream);
            List<string> words = new List<string>();
            string currentLine;
            while ((currentLine = await reader.ReadLineAsync()) != null) {
                words.Add(currentLine);
            }

            return words;
        }

        protected List<int> CreateIndexForRandomAccess() {
            var indexList = new List<int>(_commonSixLetterWords.Count);
            for(var i = 0; i < _commonSixLetterWords.Count; i++) {
                indexList.Add(i);
            }

            indexList = indexList.Randomize().ToList();

            return indexList;
        }
    }

    internal class Strings {
        internal const string SixLetterWordFileResxName = "FindWords.Shared.assets.common-six-letter-words.txt";
    }
}
