using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace FindWords.Test {
    /// <summary>
    /// This class contains tools that are disquised as unit tests.
    /// These tests should be skipped on CI (or minimally ignoring the results of these tests).
    /// If needed we can comment out the Fact attribute to avoid running these unnecessarily.
    /// </summary>
    public class ToolTestCases {
        /// <summary>
        /// This can be used to re-create the six letter word file.
        /// It is not needed to run these tests locally or on CI, just on an as needed basis.
        /// </summary>
        // [Fact]
        public async Task CreateSixLetterWordFileAsync() {
            var fileHelper = new FileHelper();
            string destpath = Path.Combine(
                fileHelper.GetApplicationRoot(),
                $"test-sixletterwords-{DateTime.Now.ToString("yyyyMMdd-hh-mm-ss")}.txt");
            string path = fileHelper.GetPathToRealDictionaryFile();
            path = @"/Users/sayedhashimi/data/mycode/OSS/google-10000-english/google-10000-english-usa-no-swears-medium.txt";
            using var readerFileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(readerFileStream);

            using var outputFileStream = new FileStream(destpath, FileMode.Create, FileAccess.ReadWrite);
            using var outputWriter = new StreamWriter(outputFileStream);

            string currentLine;
            while ((currentLine = await reader.ReadLineAsync()) != null) {
                if (currentLine.Length == 6) {
                    await outputWriter.WriteLineAsync(currentLine);
                }
            }

            await outputFileStream.FlushAsync();
            await outputWriter.FlushAsync();
        }

        // can be used to remove words with special characters from
        // the cspell file
        [Fact]
        public async Task PreprocessCspellFileAsync() {
            var hasNonAlphaCharsPattern = @"[^a-zA-Z]";
            var fileHelper = new FileHelper();
            var pathToCspellSourceFile = Path.Combine(
                fileHelper.GetApplicationRoot(),
                "assets", "cspell-en_US.txt");

            var filteredWordsOutFilePath = Path.GetTempFileName();
            var removedWordsOutFilePath = Path.GetTempFileName();

            using var sourceFileStream = new FileStream(pathToCspellSourceFile, FileMode.Open, FileAccess.Read);
            using var sourceFileReader = new StreamReader(sourceFileStream);
            using var filteredOutStream = new FileStream(filteredWordsOutFilePath, FileMode.Create, FileAccess.ReadWrite);
            using var filteredOutWriter = new StreamWriter(filteredOutStream);
            using var removedOutStream = new FileStream(removedWordsOutFilePath, FileMode.Create, FileAccess.ReadWrite);
            using var removedOutWriter = new StreamWriter(removedOutStream);

            var nonAlphaRegex = new Regex(hasNonAlphaCharsPattern, RegexOptions.Compiled);
            string cspellLine;
            while ((cspellLine = await sourceFileReader.ReadLineAsync()) != null) {
                if (nonAlphaRegex.IsMatch(cspellLine)) {
                    await removedOutWriter.WriteLineAsync(cspellLine);
                }
                else {
                    await filteredOutWriter.WriteLineAsync(cspellLine.ToLowerInvariant());
                }
            }

            Console.WriteLine($@"
filtered words file: {filteredWordsOutFilePath}
removed words file: {removedWordsOutFilePath}
");

            Assert.NotNull(nonAlphaRegex);
        }
    }
}
