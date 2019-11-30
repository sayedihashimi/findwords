using System;
using System.Linq;
using FindWords.Shared;
using Xunit;

namespace FindWords.Test {
    public class TestStringPermuter {
        [Fact]
        public void TestGetThreeLetterPermutations() {
            var permutations = new StringPermuter().Permute("cat").ToList();

            Assert.Equal(6, permutations.Count);
            Assert.True(permutations.Contains("cat"));
            Assert.True(permutations.Contains("cta"));
            Assert.True(permutations.Contains("act"));
            Assert.True(permutations.Contains("atc"));
            Assert.True(permutations.Contains("tca"));
            Assert.True(permutations.Contains("tac"));

        }

        [Fact]
        public void TestGetFourLetterPermutations() {
            var permutations = new StringPermuter().Permute("rude").ToList();

            Assert.Equal(24, permutations.Count);
            Assert.True(permutations.Contains("rude"));
            Assert.True(permutations.Contains("rued"));
            Assert.True(permutations.Contains("rdue"));
            Assert.True(permutations.Contains("rdeu"));
            Assert.True(permutations.Contains("redu"));
            Assert.True(permutations.Contains("reud"));
            Assert.True(permutations.Contains("urde"));
            Assert.True(permutations.Contains("ured"));
            Assert.True(permutations.Contains("udre"));
            Assert.True(permutations.Contains("uder"));
            Assert.True(permutations.Contains("uedr"));
            Assert.True(permutations.Contains("uerd"));
            Assert.True(permutations.Contains("dure"));
            Assert.True(permutations.Contains("duer"));
            Assert.True(permutations.Contains("drue"));
            Assert.True(permutations.Contains("dreu"));
            Assert.True(permutations.Contains("deru"));
            Assert.True(permutations.Contains("deur"));
            Assert.True(permutations.Contains("eudr"));
            Assert.True(permutations.Contains("eurd"));
            Assert.True(permutations.Contains("edur"));
            Assert.True(permutations.Contains("edru"));
            Assert.True(permutations.Contains("erdu"));
            Assert.True(permutations.Contains("erud"));
        }

        [Fact]
        public void TestGetSixLetterPermutations() {
            var permutations = new StringPermuter().Permute("scared").ToList();

            Assert.True(permutations.Contains("scared"));
            Assert.True(permutations.Contains("csared"));
            Assert.True(permutations.Contains("dsreca"));
            Assert.True(permutations.Contains("arecds"));
            Assert.True(permutations.Contains("raesdc"));
            Assert.True(permutations.Contains("ecrads"));
            Assert.True(permutations.Contains("dcears"));
            Assert.True(permutations.Contains("scdare"));
        }

    }
}
