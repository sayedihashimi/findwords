using BenchmarkDotNet.Attributes;
using FindWords.Shared;
using FindWords.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindWords.Benchmarks;
[MemoryDiagnoser(true)]
public class WordTreeBenchmarks {
	[Benchmark]
	public async Task LoadWordTreeAndTest5WordsAsync() {
		var tree = await new WordTreeBuilder().BuildFromDictionaryFileAsync(
			new FileHelper().GetPathToRealDictionaryFile());
		tree.IsWord("rubiaceae");
		tree.IsWord("katsuwonidae");
		tree.IsWord("ritualistically");
		tree.IsWord("mac");
		tree.IsWord("efficient");
	}
	[Benchmark]
	public async Task LoadMemEfficientWordTreeAndTest5WordsAsync() {
		var tree = await new WordTreeBuilder().BuildFromDictionaryFileAsync(
			new FileHelper().GetPathToRealDictionaryFile(),memEfficientTree: true);
		tree.IsWord("rubiaceae");
		tree.IsWord("katsuwonidae");
		tree.IsWord("ritualistically");
		tree.IsWord("mac");
		tree.IsWord("efficient");
	}
}

