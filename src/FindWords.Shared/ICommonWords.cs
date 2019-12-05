using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindWords.Shared {
    public interface ICommonWords {
        Task<List<string>> GetCommonSixLetterWordsAsync();
    }
}