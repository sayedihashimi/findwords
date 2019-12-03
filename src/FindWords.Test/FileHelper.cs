using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FindWords.Test {
    public class FileHelper {
        public async Task<string> CreateTempFileWithContentAsync(string content) {
            string tempfilepath = Path.GetTempFileName();

            using var fileStream = new FileStream(tempfilepath, FileMode.OpenOrCreate, FileAccess.Write);
            using var fileWriter = new StreamWriter(fileStream);

            string[] contentLines = content.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            foreach(var line in contentLines) {
                await fileWriter.WriteLineAsync(line);
            }

            return tempfilepath;
        }

        public string GetPathToRealDictionaryFile() {
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
