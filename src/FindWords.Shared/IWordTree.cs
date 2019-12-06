namespace FindWords.Shared {
    public interface IWordTree {
        int MinWordLength { get; set; }

        void AddWord(string word);
        bool IsWord(string word);
    }
}