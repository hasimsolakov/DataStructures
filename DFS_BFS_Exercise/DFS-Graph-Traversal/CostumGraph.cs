namespace Graph
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Security.AccessControl;

    public class CostumGraph<T>
    {
        public CostumGraph(int numberOfExpectedNodes) : this(new List<GraphNode<T>>(numberOfExpectedNodes))
        {
            
        }

        public CostumGraph(IList<GraphNode<T>> nodesCollection)
        {
            this.GraphNodesCollection = nodesCollection;
        }

        public int NodesCount
        {
            get { return this.GraphNodesCollection.Count; }
        }

        public void AddNode(GraphNode<T> nodeToAdd)
        {
            var nodeWithGivenValue = this.GetNodeByValue(nodeToAdd.Value);
            if (nodeWithGivenValue != null)
            {
                foreach (var neighbour in nodeToAdd.Neighbours)
                {
                    var neighbourToAdd = this.GetNodeByValue(neighbour.Value);
                    nodeWithGivenValue.Neighbours.Add(neighbourToAdd);
                }

                this.GraphNodesCollection.Add(nodeWithGivenValue);
            }
            else
            {
                var neighbours = new List<GraphNode<T>>();
                foreach (var neighbour in nodeToAdd.Neighbours)
                {
                    var neighbourToAdd = this.GetNodeByValue(neighbour.Value);
                    if (neighbourToAdd != null)
                    {
                        neighbours.Add(neighbourToAdd);
                    }
                    else
                    {
                        neighbours.Add(neighbour);
                    }
                    
                }

                nodeToAdd.Neighbours = neighbours;
                this.GraphNodesCollection.Add(nodeToAdd);
            }
        }

        private GraphNode<T> GetNodeByValue(T value)
        {
            GraphNode<T> nodeToReturn = null;
            foreach (var graphNode in this.GraphNodesCollection)
            {
                if (graphNode.Value.Equals(value))
                {
                    nodeToReturn = graphNode;
                }
                else
                {
                    foreach (var neighbour in graphNode.Neighbours)
                    {
                        if (neighbour.Value.Equals(value))
                        {
                            nodeToReturn = neighbour;
                        }
                    }
                }
            }

            return nodeToReturn;
        } 

        public IList<GraphNode<T>> GraphNodesCollection { get; set; }
        
    }

    public class GraphNode<T>
    {
        public GraphNode(T value, params T[] neighboursValues)
        {
            this.Value = value;
            this.Neighbours = new List<GraphNode<T>>();

            if (neighboursValues != null)
            {
                foreach (var neighbourValue in neighboursValues)
                {
                    this.Neighbours.Add(new GraphNode<T>(neighbourValue));
                }
            }
        }

        public T Value { get; set; }

        public IList<GraphNode<T>> Neighbours { get; set; }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var toCompare = (GraphNode<T>)obj;
            return this.Value.Equals(toCompare.Value);
        }
    }
}