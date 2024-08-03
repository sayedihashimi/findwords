using FindWords.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FindWords.Test {
	public class TestMemEfficientWordTree {
		[Fact]
		public void TestRemoveFirstChar01() {
			ReadOnlySpan<char> sample = "sample";
			var result = new MemEfficientWordTree().RemoveFirstChar(sample);

			Assert.Equal("ample", result.ToString());
		}

		[Fact]
		public void TestIsWord01() {
			var tree = new MemEfficientWordTree();
			tree.AddWord("cat");
			Assert.True(tree.IsWord("cat"));
			Assert.False(tree.IsWord("cato"));
			Assert.False(tree.IsWord("c"));
			Assert.False(tree.IsWord("ca"));
		}

		[Fact]
		public void TestIsWordReturnsFalse() {
			var tree = new MemEfficientWordTree();
			Assert.False(tree.IsWord("cat"));
			Assert.False(tree.IsWord("dog"));
		}

		[Fact]
		public void TestIsCarWordWhenCareIsInsertedBeforeCar() {
			var tree = new MemEfficientWordTree();
			tree.AddWord("cat");
			tree.AddWord("care");
			tree.AddWord("car");
			Assert.True(tree.IsWord("car"));
			Assert.True(tree.IsWord("care"));
		}
	}
}
