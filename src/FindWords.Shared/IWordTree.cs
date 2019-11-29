namespace FindWords.Shared {
    public interface IWordTree {
        void AddWord(string word);
        bool IsWord(string word);
    }
}