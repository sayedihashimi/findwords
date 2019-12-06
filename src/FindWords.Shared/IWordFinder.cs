using System.Collections.Generic;

namespace FindWords.Shared {
    public interface IWordFinder {
        IWordTree WordTree { get; set; }
        int MinWordLength { get; set; }

        List<string> FindWordsInString(string str);
    }
}