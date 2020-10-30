using System;
using System.IO;
using System.Text;

namespace BinTree
{
    class Program
    {
        static readonly Random random = new Random();

        static BinTree<int> Generation(int minCount, int maxCount, int minValue, int maxValue)
        {
            BinTree<int> tree = new BinTree<int>();

            int count = random.Next(minCount, maxCount);
            for (int i = 0; i < count; i++)
            {
                int value = random.Next(minValue, maxValue);
                tree.Add(value);
            }

            return tree;
        }

        static void Main()
        {
            const string PathFile = "./output.txt";

            const int MinCount = 1;
            const int MaxCount = 64;
            const int MinValue = 1;
            const int MaxValue = 1_000_000;

            BinTree<int> tree = Generation(MinCount, MaxCount, MinValue, MaxValue);

            using (StreamWriter file = new StreamWriter(PathFile, false, Encoding.UTF8))
                tree.Output(file.WriteLine);
        }
    }
}
