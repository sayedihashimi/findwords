using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FindWords.Shared;
using Xunit;

namespace FindWords.Test {
    public class TestCommonWords {
        [Fact]
        public async Task Test_Read_GetCommonSixLetterWordsAsync() {
            var cw = new CommonWords();
            List<string> words = await cw.GetCommonSixLetterWordsAsync();

            Assert.NotNull(words);
            Assert.True(words.Count > 1000);
        }

    }
}
