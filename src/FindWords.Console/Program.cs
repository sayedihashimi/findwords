using FindWords.Shared;

var wordFinder = new WordTreeBuilder().BuidWordFinderAsync().Result;

while (true)
{
    Console.Write("Enter letters: ");
    var letters = Console.ReadLine();
    Console.Clear();

    if (string.IsNullOrEmpty(letters) || letters.Length <= 2)
    {
        Console.WriteLine("Enter at least 3 letters");
        continue;
    }
    var words = wordFinder.FindWordsInString(letters!);
    if(words!.Count() > 0)
    {
        foreach (var word in words)
        {
            Console.WriteLine($"{word}");
        }
    }
    else
    {
        Console.WriteLine($"No words found for '{letters}'");
    }
}