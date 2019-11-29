using System.Collections.Generic;

namespace FindWords.Shared {
    public interface IWordTreeNode {
        char Character { get; set; }
        WordTreeNode Previous { get; set; }
        IList<WordTreeNode> Next { get; set; }
        bool IsWord { get; set; }
    }
}