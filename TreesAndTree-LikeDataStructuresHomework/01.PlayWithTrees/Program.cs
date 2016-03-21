namespace _01.PlayWithTrees
{
    using System;

    public class Program
    {
        private static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            var tree = FillTree(n);
            int pathSumToSeek = int.Parse(Console.ReadLine());
            var leafNodesValuesCollection = tree.GetLeafNodesValues();
            var middleNodesValuesCollection = tree.GetMiddleNodesValues();
            leafNodesValuesCollection.Sort();
            middleNodesValuesCollection.Sort();

            Console.WriteLine("Root node: " + tree.Value);
            Console.WriteLine("Leaf nodes: " + string.Join(", ", leafNodesValuesCollection));
            Console.WriteLine("Middle nodes: " + string.Join(", ", middleNodesValuesCollection));
            var longestPath = tree.GetLongestPath();
            longestPath.Reverse();
            Console.WriteLine("Longest path: {0} (length = {1})", string.Join(" -> ", longestPath), longestPath.Count);
            var pathsWithGivenSum = tree.GetValuesOfPathWithSum(pathSumToSeek);
            foreach (var pathValues in pathsWithGivenSum)
            {
                Console.WriteLine(string.Join(" -> ", pathValues));
            }
        }

        private static RecursiveTree<int> FillTree(int n)
        {
            RecursiveTree<int> tree = null;
            for (int line = 1; line < n; line++)
            {
                string[] nodeValues = Console.ReadLine().Split(' ');

                int parentNodeValue = int.Parse(nodeValues[0]);
                int childNodeValue = int.Parse(nodeValues[1]);
                if (line == 1)
                {
                    tree = new RecursiveTree<int>(parentNodeValue, new RecursiveTree<int>(childNodeValue, null));
                }
                else
                {
                    tree.AddNodes(parentNodeValue, childNodeValue);
                }
            }

            return tree;
        }
    }
}
