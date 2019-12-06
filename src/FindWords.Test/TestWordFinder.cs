using System;
using System.Threading.Tasks;
using FindWords.Shared;
using Xunit;

namespace FindWords.Test {
    public class TestWordFinder {
        public class TestFindWordsIn {

            [Fact]
            public async Task TestFindWordsIn_FourLetters_01Async() {
                string str = "made";
                WordFinder finder = new WordFinder(await BuildWordTreeAsync());
                finder.MinWordLength = 1;
                var found = finder.FindWordsInString(str);

                Assert.NotNull(found);
                Assert.True(found.Count > 10);
            }

            private async Task<IWordTree> BuildWordTreeAsync() {
                return await new WordTreeBuilder().BuildFromDictionaryFileAsync(
                    new FileHelper().GetPathToRealDictionaryFile());
            }
        }
    }
}
