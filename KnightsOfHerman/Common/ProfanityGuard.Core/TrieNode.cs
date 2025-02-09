using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfanityGuard.Core
{
    /// <summary>
    /// Trie Node datastructure
    /// </summary>
    internal class TrieNode
    {
        public Dictionary<char, TrieNode> Children { get; }
        public bool IsWordEnd { get; set; }

        public TrieNode()
        {
            Children = new Dictionary<char, TrieNode>();
            IsWordEnd = false;
        }
    }
}
