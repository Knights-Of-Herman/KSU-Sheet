using System.Reflection;

namespace ProfanityGuard.Core
{
    /// <summary>
    /// Class that handles checking a string for profanity
    /// Should probably move this to an open-source API that anyone can call
    /// </summary>
    public static class ProfanityChecker
    {
        /// <summary>
        /// Initializes the list of banned words
        /// </summary>
        public static void Initialize()
        {
            _profanityTrie = new();
            ReadBannedwords();
        }

        private static Trie _profanityTrie;
        
        /// <summary>
        /// Loads the banned words list into the Trie
        /// </summary>
        private static void ReadBannedwords()
        {
            var assembly = Assembly.GetExecutingAssembly();

            string resourcePath = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith("banned_words.txt"));

            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.ToLower();
                        line = line.Replace(" ", "");
                        _profanityTrie.Insert(line);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a string contains profanity in any substring
        /// </summary>
        /// <param name="input">String to check</param>
        /// <returns>True if profanity is found</returns>
        public static bool Check(string input)
        {
            input = input.ToLower().Replace(" ", "");

            for (int start = 0; start < input.Length; start++)
            {
                for (int end = start + 1; end <= input.Length; end++)
                {
                    var segment = input.Substring(start, end - start);
                    if (_profanityTrie.Search(segment))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
