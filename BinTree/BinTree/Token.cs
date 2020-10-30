using System.Collections.Generic;

namespace BinTree
{
    class Token<T>
    {
        public int Id;
        public int Root;
        public List<string> Merge = new List<string>();

        public Token(int id, int root)
        {
            this.Id = id;
            this.Root = root;
            this.Merge.Add(string.Empty);
        }
        public Token(int id, int root, T value)
        {
            this.Id = id;
            this.Root = root;
            this.Merge.Add(value.ToString());
        }
    }
}
