namespace _01.PlayWithTrees
{
    using System;
    using System.Collections.Generic;

    public class RecursiveTree<T>
    {
        private static IDictionary<T, RecursiveTree<T>> nodesCollection = new Dictionary<T, RecursiveTree<T>>();

        public RecursiveTree(T value, params RecursiveTree<T> [] children)
        {
            this.Value = value;
            this.Children = new List<RecursiveTree<T>>();

            if (children == null)
            {
                return;
            }

            foreach (var child in children)
            {
                child.Parent = this;
                this.Children.Add(child);
            }
        }

        public T Value { get; set; }

        public IList<RecursiveTree<T>> Children { get; private set; }

        public RecursiveTree<T> Parent { get; set; }

        public List<T> GetLongestPath()
        {
            int minNumber = 0;
            RecursiveTree<T> lastNode = new RecursiveTree<T>(default(T));
            this.GetLongestPathLastNode(ref minNumber, ref lastNode);
            List<T> longestPath = new List<T>();

            while (lastNode != null)
            {
                longestPath.Add(lastNode.Value);
                lastNode = lastNode.Parent;
            }

            return longestPath;
        }

        public List<List<T>> GetValuesOfPathWithSum(int sum)
        {
            List<RecursiveTree<T>> lastNodesCollection = new List<RecursiveTree<T>>();
            this.GetLastNodeOfPathWithGivenSum(sum, ref lastNodesCollection);
            List<List<T>> pathsWithGivenSum = new List<List<T>>();
            for (int path = 0; path < lastNodesCollection.Count; path++)
            {
                var currentLastNode = lastNodesCollection[path];
                List<T> pathValues = new List<T>();
                while (currentLastNode != null)
                {
                    pathValues.Add(currentLastNode.Value);
                    currentLastNode = currentLastNode.Parent;
                }

                pathValues.Reverse();
                pathsWithGivenSum.Add(pathValues);
            }

            return pathsWithGivenSum;
        }

        public List<T> GetMiddleNodesValues()
        {
            List<T> middleNodesValues = new List<T>();

            if (this.Children.Count != 0 && this.Parent != null)
            {
                middleNodesValues.Add(this.Value);
                return middleNodesValues;
            }

            foreach (var child in this.Children)
            {
                var result = child.GetMiddleNodesValues();
                middleNodesValues.AddRange(result);
            }

            return middleNodesValues;
        }

        public List<T> GetLeafNodesValues()
        {
            List<T> leafNodesValues = new List<T>();

            if (this.Children.Count == 0)
            {
                 leafNodesValues.Add(this.Value);
                return leafNodesValues;
            }

            foreach (var child in this.Children)
            {
                var result = child.GetLeafNodesValues();
                leafNodesValues.AddRange(result);
            }

            return leafNodesValues;
        }

        public void AddNodes(T parentNodeValue, T childNodeValue)
        {
            var result = this.GetNodeByValue(parentNodeValue);
            if (result == null)
            {
                this.Value = parentNodeValue;
                var child = new RecursiveTree<T>(childNodeValue, null);
                child.Parent = this;
                this.Children.Add(child);
            }
            else
            {
                var childToAdd = new RecursiveTree<T>(childNodeValue, null);
                childToAdd.Parent = result;
                result.Children.Add(childToAdd);
            }
        }

        /// <param name="valueOfTheNodeToGet"></param>
        /// <returns>Returns null if there is no node with given value</returns>
        private RecursiveTree<T> GetNodeByValue(T valueOfTheNodeToGet)
        {            
            if (this.Value.Equals(valueOfTheNodeToGet))
            {
                return this;
            }

            foreach (var child in this.Children)
            {
                var result = child.GetNodeByValue(valueOfTheNodeToGet);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        private RecursiveTree<T> GetLongestPathLastNode(ref int maxLength, ref RecursiveTree<T> lastNode, int length = 0)
        {
            if (this.Children.Count == 0)
            {
                length++;
                if (length > maxLength)
                {
                    lastNode = this;
                    maxLength = length;
                }
            }

            foreach (var child in this.Children)
            {
                child.GetLongestPathLastNode(ref maxLength, ref lastNode, length + 1);
            }

            return lastNode;
        }

        private void GetLastNodeOfPathWithGivenSum(int sum, ref List<RecursiveTree<T>> lastNodesCollection)
        {
            int currentValue = Convert.ToInt32(this.Value);
            if (sum - currentValue == 0)
            {
                lastNodesCollection.Add(this);
            }

            foreach (var child in this.Children)
            {
                child.GetLastNodeOfPathWithGivenSum(sum - currentValue, ref lastNodesCollection);
            }
        }
    }
}
