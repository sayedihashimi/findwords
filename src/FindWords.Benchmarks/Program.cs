using BenchmarkDotNet.Running;
using FindWords.Benchmarks;
using FindWords.Shared;
using FindWords.Test;

var arg1 = string.Empty;
if(args.Length > 0) {
	arg1 = args[0] != null ? args[0].ToLower() : null;
}

switch (arg1) {
	case "benchmarks":
		RunBenchmarks();
		break;
	case "wordtree":
		await RunWordTreeAsync();
		break;
	case "memwordtree":
		await RunMemEfficientWordTreeAsync();
		break;
	default:
		RunBenchmarks();
		break;
}

void RunBenchmarks() {
	var summary = BenchmarkRunner.Run<WordTreeBenchmarks>();
	Console.WriteLine(summary);
}
async Task RunWordTreeAsync() {
	var tree = await new WordTreeBuilder().BuildFromDictionaryFileAsync(
	new FileHelper().GetPathToRealDictionaryFile());
	tree.IsWord("rubiaceae");
	tree.IsWord("katsuwonidae");
	tree.IsWord("ritualistically");
	tree.IsWord("mac");
	tree.IsWord("efficient");
}
async Task RunMemEfficientWordTreeAsync() {
	var tree = await new WordTreeBuilder().BuildFromDictionaryFileAsync(
	new FileHelper().GetPathToRealDictionaryFile(), memEfficientTree: true);
	tree.IsWord("rubiaceae");
	tree.IsWord("katsuwonidae");
	tree.IsWord("ritualistically");
	tree.IsWord("mac");
	tree.IsWord("efficient");
}