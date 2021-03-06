using System;
using FindWords.Shared;
using Xunit;

namespace FindWords.Test {
    public class TestWordTree {
        [Fact]
        public void TestRemoveFirstChar01() {
            char[] sample = "sample".ToCharArray();
            char[] result = new WordTree().RemoveFirstChar(sample);

            Assert.Equal("ample".ToCharArray(), result);
        }

        [Fact]
        public void TestIsWord01() {
            var tree = new WordTree();
            tree.AddWord("cat");
            Assert.True(tree.IsWord("cat"));
            Assert.False(tree.IsWord("cato"));
            Assert.False(tree.IsWord("c"));
            Assert.False(tree.IsWord("ca"));
        }

        [Fact]
        public void TestIsWordReturnsFalse() {
            var tree = new WordTree();
            Assert.False(tree.IsWord("cat"));
            Assert.False(tree.IsWord("dog"));
        }

        [Fact]
        public void TestIsCarWordWhenCareIsInsertedBeforeCar() {
            var tree = new WordTree();
            tree.AddWord("cat");
            tree.AddWord("care");
            tree.AddWord("car");
            Assert.True(tree.IsWord("car"));
            Assert.True(tree.IsWord("care"));
        }
    }
}
