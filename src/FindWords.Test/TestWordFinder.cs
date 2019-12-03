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

                var found = finder.FindWordsInString(str);

                Assert.NotNull(found);
                Assert.Equal(35, found.Count);
            }

            private async Task<IWordTree> BuildWordTreeAsync() {
                return await new WordTreeBuilder().BuildFromDictionaryFileAsync(
                    new FileHelper().GetPathToRealDictionaryFile());
            }
        }
    }
}
