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

            /// <summary>
            /// This can be used to re-create the six letter word file.
            /// Normally this should be commented out.
            /// </summary>
            //[Fact]
            //public async Task CreateSixLetterWordFileAsync() {
            //    string destpath = Path.Combine(
            //        GetApplicationRoot(),
            //        $"test-sixletterwords-{DateTime.Now.ToString("yyyyMMdd-hh-mm-ss")}.txt");
            //    using var readerFileStream = new FileStream(GetPathToRealDictionaryFile(), FileMode.Open,FileAccess.Read);
            //    using var reader = new StreamReader(readerFileStream);

            //    using var outputFileStream = new FileStream(destpath, FileMode.Create, FileAccess.ReadWrite);
            //    using var outputWriter = new StreamWriter(outputFileStream);

            //    string currentLine;
            //    while( (currentLine = await reader.ReadLineAsync()) != null) {
            //        if(currentLine.Length == 6) {
            //            await outputWriter.WriteLineAsync(currentLine);
            //        }
            //    }

            //    await outputWriter.FlushAsync();
            //}

            internal string GetPathToRealDictionaryFile() {
                return Path.Combine(GetApplicationRoot(), "assets/words_alpha.txt");
            }

            internal string GetApplicationRoot() {
                var exePath = Path.GetDirectoryName(System.Reflection
                                  .Assembly.GetExecutingAssembly().CodeBase);
                Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
                var appRoot = appPathMatcher.Match(exePath).Value;
                return appRoot;
            }
        }
    }
}
