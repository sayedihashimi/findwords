﻿using System;
using System.Diagnostics;

namespace FindWords.Shared {
	public class MemEfficientWordTree : IWordTree {
		protected WordTreeNode RootNode = new WordTreeNode(char.MinValue, null, false);

		public int MinWordLength { get; set; } = 3;

		public void AddWord(string word) =>
			InsertAfter(RootNode, word.ToCharArray(), true);

		public bool IsWord(string word) {
			Debug.Assert(!string.IsNullOrWhiteSpace(word));
			if (word.Length < MinWordLength) { return false; }
			var result = FindNode(word);
			return result == null ? false : result.IsWord;
		}

		private WordTreeNode InsertAfter(WordTreeNode currentNode, ReadOnlySpan<char> word, bool isWord) {
			Debug.Assert(currentNode != null);

			if (word == null || word.Length == 0) {
				return null;
			}

			WordTreeNode targetNode = null;
			// see if the currentNode has a next pointer for next letter
			char charToFind = word[0];
			foreach (var node in currentNode.Next) {
				if (charToFind.Equals(node.Character)) {
					targetNode = node;
					break;
				}
			}

			// if null create a new node
			if (targetNode == null) {
				targetNode = new WordTreeNode(charToFind, currentNode, word.Length == 1);
				currentNode.Next.Add(targetNode);
			}

			// corner case when AddWord("care") is called before AddWord("car")
			targetNode.IsWord = targetNode.IsWord | word.Length == 1;

			// remove the first char and then continue
			return InsertAfter(targetNode, RemoveFirstChar(word), word.Length == 1);
		}

		public ReadOnlySpan<char> RemoveFirstChar(ReadOnlySpan<char> letters) => 
			letters[1..];

		public WordTreeNode FindNode(string letters) => 
			FindNodeAfter(RootNode, letters.ToCharArray());
		
		private WordTreeNode FindNodeAfter(WordTreeNode currentNode, ReadOnlySpan<char> letters) {
			Debug.Assert(currentNode != null);

			if (letters.Length == 0) {
				return currentNode;
			}

			// look to see if the first letter is in next pointer of current node
			char currentletter = letters[0];
			foreach (WordTreeNode nextnode in currentNode.Next) {
				if (currentletter.Equals(nextnode.Character)) {
					return FindNodeAfter(nextnode, RemoveFirstChar(letters));
				}
			}

			return null;
		}
	}
}
