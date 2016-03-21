namespace Graph
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GraphConnectedComponents
    {
        public static void Main()
        {
            int numberOfLines = int.Parse(Console.ReadLine());
            CostumGraph<int> costumGraph = new CostumGraph<int>(numberOfLines);
            for (int line = 0; line < numberOfLines; line++)
            {
                int[] neighboursValues;
                try
                {
                    neighboursValues = Console.ReadLine()
                        .Split(' ')
                        .Select(int.Parse)
                        .ToArray();
                }
                catch (FormatException)
                {
                    neighboursValues = null;
                }
                 
                costumGraph.AddNode(new GraphNode<int>(line, neighboursValues));
            }

            FindGraphConnectedComponents(costumGraph);
        }

        private static HashSet<GraphNode<int>> visitedNodes;

        private static void FindGraphConnectedComponents(CostumGraph<int> costumGraph)
        {
            visitedNodes = new HashSet<GraphNode<int>>();

            var startNode = new GraphNode<int>(0);
            for (int startNodeIndex = 0; startNodeIndex < costumGraph.NodesCount; startNodeIndex++)
            {
                startNode = costumGraph.GraphNodesCollection[startNodeIndex];
                if (!visitedNodes.Contains(startNode))
                {
                    Console.Write("Connected component:");
                    Dfs(startNode);
                    Console.WriteLine();
                }
            }
        }

        private static void Dfs(GraphNode<int> node)
        {
            if (!visitedNodes.Contains(node))
            {
                visitedNodes.Add(node);
                foreach (var neighbour in node.Neighbours)
                {
                    if (!visitedNodes.Contains(neighbour))
                    {
                        Dfs(neighbour);
                    }
                    
                }

                Console.Write(" " + node.Value);
            }
        }
    }
}
