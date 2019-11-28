using System;
using System.Collections;
using System.Collections.Generic;

namespace FindWords.Shared {
    public class WordTreeNode {
        public WordTreeNode(char character, WordTreeNode previous, bool isWord) {
            Character = character;
            Previous = previous;
            IsWord = isWord;
        }
        public char Character { get; set; }
        public WordTreeNode Previous { get; set; }
        public IList<WordTreeNode> Next { get; set; } = new List<WordTreeNode>();
        public bool IsWord{get;set;}
    }
}
