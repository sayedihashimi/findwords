using System;
using System.IO;
using System.Reflection;
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
                if (string.IsNullOrEmpty(line)) { continue; }
                await fileWriter.WriteLineAsync(line.Trim());
            }

            return tempfilepath;
        }

        public string GetPathToRealDictionaryFile() {
            return Path.Combine(GetApplicationRoot(), "assets","words_alpha.txt");
        }

        public string GetPathToCspellDictionaryFile() {
            return Path.Combine(GetApplicationRoot(), "assets", "cspell-filtered_en_US.txt");
        }

        public string GetApplicationRoot() {
            var exePath = typeof(FileHelper).GetTypeInfo().Assembly.Location;

            return Path.GetDirectoryName(exePath);
        }
    }
}
