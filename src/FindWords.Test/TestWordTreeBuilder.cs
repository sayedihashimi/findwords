using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FindWords.Shared;
using Xunit;

namespace FindWords.Test {
    public class TestWordTreeBuilder {
        public class TestBuildDictionary {
            [Fact]
            public async System.Threading.Tasks.Task BasicTest01Async() {
                string content = @"accolated
accolent
accoll
accolle
accolled
accollee
accombination
accommodable";
                string tempfile = await new FileHelper().CreateTempFileWithContentAsync(content);
                IWordTree tree = await new WordTreeBuilder().BuildFromDictionaryFileAsync(tempfile);

                Assert.True(tree.IsWord("accolated"));
                Assert.True(tree.IsWord("accolent"));
                Assert.True(tree.IsWord("accommodable"));
                Assert.False(tree.IsWord("accommodableK"));
            }

            [Fact]
            public async Task ReadRealFile() {
                IWordTree tree = await new WordTreeBuilder().BuildFromDictionaryFileAsync(
                    GetPathToRealDictionaryFile());

                Assert.True(tree.IsWord("rubiaceae"));
                Assert.True(tree.IsWord("katsuwonidae"));
                Assert.True(tree.IsWord("ritualistically"));
                Assert.True(tree.IsWord("mac"));
            }

            internal string GetPathToRealDictionaryFile() {
                return Path.Combine(GetApplicationRoot(), "assets/words_alpha.txt");
            }

            public string GetApplicationRoot() {
                var exePath = Path.GetDirectoryName(System.Reflection
                                  .Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                return appRoot;
            }
        }
    }
}
