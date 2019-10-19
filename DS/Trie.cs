using System.Collections.Generic;
public class Trie {

    public class TrieNode {
        public Dictionary<char, TrieNode> childrens;
        public bool isWord;
        int val;

        public TrieNode () {
            childrens = new Dictionary<char, TrieNode> ();
        }

        TrieNode (int val) {
            //Really??????
            // TrieNode ndoe = new TrieNode ();
            // ndoe.val = val;
            childrens = new Dictionary<char, TrieNode> ();
            this.val = val;
        }

        public bool TryAdd (char prefix) {
            return childrens.TryAdd (prefix, new TrieNode (prefix));
        }
        public bool Contains (char prefix) {
            return childrens.ContainsKey (prefix);
        }
    }

    TrieNode root;
    /** Initialize your data structure here. */
    public Trie () {
        root = new TrieNode ();
    }

    /** Inserts a word into the trie. */
    public void Insert (string word) {
        var cur = root;
        foreach (var c in word) {
            cur.TryAdd (c);
            cur = cur.childrens[c];
        }
        cur.isWord = true;
    }

    /** Returns if the word is in the trie. */
    public bool Search (string word) {
        var cur = SearchNode (word);
        return cur != null && cur.IsWord ();
    }

    /** Returns if there is any word in the trie that starts with the given prefix. */
    public bool StartsWith (string prefix) {
        return SearchNode (prefix) != null;
    }

    TrieNode SearchNode (string prefix) {
        var cur = root;
        foreach (var c in word) {
            if (cur.Contains (c)) {
                cur = cur.childrens[c];
            } else {
                return null;
            }
        }
        return cur;
    }
}

/**
 * Your Trie object will be instantiated and called as such:
 * Trie obj = new Trie();
 * obj.Insert(word);
 * bool param_2 = obj.Search(word);
 * bool param_3 = obj.StartsWith(prefix);
 */