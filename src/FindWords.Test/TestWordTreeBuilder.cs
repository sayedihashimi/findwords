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
                    new FileHelper().GetPathToRealDictionaryFile());
                
                Assert.True(tree.IsWord("rubiaceae"));
                Assert.True(tree.IsWord("katsuwonidae"));
                Assert.True(tree.IsWord("ritualistically"));
                Assert.True(tree.IsWord("mac"));
            }
        }
    }
}
