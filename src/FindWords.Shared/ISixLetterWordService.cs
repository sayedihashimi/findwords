using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindWords.Shared {
    public interface ISixLetterWordService {
        bool Randomize { get; set; }

        Task<List<string>> GetCommonSixLetterWordsAsync();
        string GetNextWord();
        string GetNextWordRandom();
        string GetPreviousWord();
        string GetPreviousWordRandom();
    }
}