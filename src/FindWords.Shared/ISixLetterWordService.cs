using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindWords.Shared {
    public interface ISixLetterWordService {
        Task<List<string>> GetCommonSixLetterWordsAsync();
        string GetNextWord();
        string GetNextWordRandom();
        string GetPreviousWord();
        string GetPreviousWordRandom();
    }
}