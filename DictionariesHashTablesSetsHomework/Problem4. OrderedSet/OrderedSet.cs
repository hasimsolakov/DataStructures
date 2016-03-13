namespace Problem4.OrderedSet
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class OrderedSet<T> : IEnumerable<T> where T : IComparable<T>
    {
#region Constructors
        public OrderedSet(OrderedSet<T> set) : this(set.Value, set.LeftChild, set.RightChild, set.RootAdded)
        {
        }

        public OrderedSet() : this(default(T), null, null, false)
        {
        }

        private OrderedSet(T value, OrderedSet<T> leftChild = null, OrderedSet<T> rightChild = null, bool rootAdded = true)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
            this.RootAdded = rootAdded;
        }
#endregion

#region Properties
        public int Count
        {
            get
            {
                return (this.LeftChild?.Count ?? 0) + (this.RightChild?.Count ?? 0) + ((this.Value == null) ? 0 : 1);
            }
        }

        private int Height
        {
            get
            {
                return (((this.RightChild?.Height ?? 0) + 1) >= ((this.LeftChild?.Height ?? 0) + 1))
                    ? (this.RightChild?.Height ?? 0) + 1
                    : (this.LeftChild?.Height ?? 0) + 1;
            }
        }

        private int Balance
        {
            get { return (this.RightChild?.Height ?? 0) - (this.LeftChild?.Height ?? 0); }
        }

        private bool RootAdded { get; set; }

        private T Value { get; set; }

        private OrderedSet<T> LeftChild { get; set; }

        private OrderedSet<T> RightChild { get; set; }
#endregion

        public void Add(T element)
        {
            if (!this.RootAdded)
            {
                this.Value = element;
                this.RootAdded = true;
                return;
            }

            if (this.Value.CompareTo(element) == 1)
            {
                if (this.LeftChild == null)
                {
                    this.LeftChild = new OrderedSet<T>(element);
                }
                else
                {
                    this.LeftChild.Add(element);
                    this.RebalanceIfNeeded();
                    return;
                }
            }
            else if (this.Value.CompareTo(element) == -1)
            {
                if (this.RightChild == null)
                {
                    this.RightChild = new OrderedSet<T>(element);
                }
                else
                {
                    this.RightChild.Add(element);
                    this.RebalanceIfNeeded();
                    return;
                }
            }

            this.RebalanceIfNeeded();
        }

        public bool Contains(T element)
        {
            if (this.Value.Equals(element))
            {
                return true;
            }

            if (this.Value.CompareTo(element) == 1)
            {
                if (this.LeftChild != null)
                {
                    return this.LeftChild.Contains(element);
                }
            }

            if (this.Value.CompareTo(element) == -1)
            {
                if (this.RightChild != null)
                {
                    return this.RightChild.Contains(element);
                }
            }

            return false;
        }

        public bool Remove(T element, OrderedSet<T> predecessor = null)
        {
            bool elementIsRemoved = false;
            if (this.Value.Equals(element))
            {
                var resultOfTheSearch = this.LeftChild?.GetRightMost(null, this);
                if (resultOfTheSearch == null)
                {
                    resultOfTheSearch = new OrderedSet<T>[] { this.RightChild, this };
                }

                if (resultOfTheSearch[0] == null && predecessor != null)
                {
                    if (predecessor.LeftChild?.Value.Equals(element) ?? false)
                    {
                        predecessor.LeftChild = null;
                        elementIsRemoved = true;
                    }
                    else if (predecessor.RightChild?.Value.Equals(element) ?? false)
                    {
                        predecessor.RightChild = null;
                        elementIsRemoved = true;
                    }
                }
                else
                {
                    var parentOfTheFutureRootNode = resultOfTheSearch[1];
                    var futureRootNode = resultOfTheSearch[0];
                    if (parentOfTheFutureRootNode.LeftChild?.Value.Equals(futureRootNode.Value) ?? false)
                    {
                        parentOfTheFutureRootNode.LeftChild = null;
                    }

                    if (parentOfTheFutureRootNode.RightChild?.Value.Equals(futureRootNode.Value) ?? false)
                    {
                        parentOfTheFutureRootNode.RightChild = null;
                    }

                    this.Value = futureRootNode.Value;
                    elementIsRemoved = true;
                }       
            }

            if (!elementIsRemoved && this.Value.CompareTo(element) == 1)
            {
                if (this.LeftChild != null)
                {
                    elementIsRemoved = this.LeftChild.Remove(element, this);
                }
            }

            if (!elementIsRemoved && this.Value.CompareTo(element) == -1)
            {
                if (this.RightChild != null)
                {
                   elementIsRemoved = this.RightChild.Remove(element, this);
                }
            }

            if (elementIsRemoved)
            {
                this.RebalanceIfNeeded();
            }

            return elementIsRemoved;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.LeftChild != null)
            {
                foreach (var value in this.LeftChild)
                {
                    yield return value;
                }
            }

            yield return this.Value;

            if (this.RightChild != null)
            {
                foreach (var value in this.RightChild)
                {
                    yield return value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private OrderedSet<T>[] GetRightMost(OrderedSet<T> setToReturn = null, OrderedSet<T> parentOfTheSetToReturn = null)
        {
            OrderedSet<T>[] result = new OrderedSet<T>[2];
            if (this.LeftChild != null)
            {
                result = this.LeftChild.GetRightMost(setToReturn, this);
            }

            result[0] = this;
            result[1] = parentOfTheSetToReturn;
            if (this.RightChild != null)
            {
                result = this.RightChild.GetRightMost(setToReturn, this);
            }

            return result;
        }

        private void RotateLeft()
        {
            var initialRightChild = this.RightChild;
            this.RightChild = initialRightChild.LeftChild;
            this.LeftChild = new OrderedSet<T>(this);
            this.Value = initialRightChild.Value;
            this.RightChild = initialRightChild.RightChild;
        }

        private void RotateRight()
        {
            var initialLeftChild = this.LeftChild;
            this.LeftChild = initialLeftChild.RightChild;
            this.RightChild = new OrderedSet<T>(this);
            this.Value = initialLeftChild.Value;
            this.LeftChild = initialLeftChild.LeftChild;
        }

        private void RebalanceIfNeeded()
        {
            bool needRebalancing = Math.Abs((this.LeftChild?.Height ?? 0) - (this.RightChild?.Height ?? 0)) > 1;

            if (needRebalancing)
            {
                if ((this.LeftChild?.Height ?? 0) > (this.RightChild?.Height ?? 0))
                {
                    if (this.LeftChild.Balance < 1)
                    {
                        this.RotateRight();
                    }
                    else
                    {
                        this.LeftChild.RotateLeft();
                        this.RotateRight();
                    }
                }
                else
                {
                    if (this.RightChild.Balance >= 0)
                    {
                        this.RotateLeft();
                    }
                    else
                    {
                        this.RightChild.RotateRight();
                        this.RotateLeft();
                    }
                }
            }
        }
    }
}