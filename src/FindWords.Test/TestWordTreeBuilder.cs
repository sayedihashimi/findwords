using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FindWords.Shared;
using Xunit;

namespace FindWords.Test {
    public class TestWordTreeBuilder {
        public class Test_BuildFromFile {
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

        public class Test_BuildFromResource {
            [Fact]
            public async Task Test_NoArgsAsync() {
                var tree = await new WordTreeBuilder().BuildFromResource();

                Assert.NotNull(tree);
                Assert.True(tree.IsWord("abashments"));
                Assert.True(tree.IsWord("enchantingly"));
                Assert.True(tree.IsWord("steeplechases"));
                Assert.True(tree.IsWord("mac"));
            }

            [Fact]
            public async Task Test_Pass_Assembly_And_ResxName_Args() {
                var tree = await new WordTreeBuilder().BuildFromResource(
                    typeof(WordTreeBuilder).GetTypeInfo().Assembly,
                    $"FindWords.Shared.assets.words_alpha.txt");

                Assert.True(tree.IsWord("rubiaceae"));
                Assert.True(tree.IsWord("katsuwonidae"));
                Assert.True(tree.IsWord("ritualistically"));
                Assert.True(tree.IsWord("mac"));
                Assert.True(tree.IsWord("abashments"));
            }
        }
    }
}
