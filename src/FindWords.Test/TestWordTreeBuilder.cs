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

            // can be used to remove words with special characters from
            // the cspell file
//            [Fact]
//            public async Task PreprocessCspellFileAsync() {
//                var hasNonAlphaCharsPattern = @"[^a-zA-Z]";
//                var fileHelper = new FileHelper();
//                var pathToCspellSourceFile = Path.Combine(
//                    fileHelper.GetApplicationRoot(),
//                    "assets","cspell-en_US.txt");

//                var filteredWordsOutFilePath = Path.GetTempFileName();
//                var removedWordsOutFilePath = Path.GetTempFileName();

//                using var sourceFileStream = new FileStream(pathToCspellSourceFile, FileMode.Open, FileAccess.Read);
//                using var sourceFileReader = new StreamReader(sourceFileStream);
//                using var filteredOutStream = new FileStream(filteredWordsOutFilePath, FileMode.Create, FileAccess.ReadWrite);
//                using var filteredOutWriter = new StreamWriter(filteredOutStream);
//                using var removedOutStream = new FileStream(removedWordsOutFilePath, FileMode.Create, FileAccess.ReadWrite);
//                using var removedOutWriter = new StreamWriter(removedOutStream);

//                var nonAlphaRegex = new Regex(hasNonAlphaCharsPattern, RegexOptions.Compiled);
//                string cspellLine;
//                while( (cspellLine = await sourceFileReader.ReadLineAsync()) != null) {
//                    if (nonAlphaRegex.IsMatch(cspellLine)) {
//                        await removedOutWriter.WriteLineAsync(cspellLine);
//                    }
//                    else {
//                        await filteredOutWriter.WriteLineAsync(cspellLine.ToLowerInvariant());
//                    }
//                }

//                Console.WriteLine($@"
//filtered words file: {filteredWordsOutFilePath}
//removed words file: {removedWordsOutFilePath}
//");
//            }
        }
    }
}
