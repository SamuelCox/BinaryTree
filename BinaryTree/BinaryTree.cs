using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTree
{

    public class BinaryTree<T> : ICollection<T> where T : IComparable
    {
        private readonly InnerBinaryTree<T> _innerTree;
        private int _count;
        public int Count => _count;

        public bool IsReadOnly => false;

        public BinaryTree()
        {
            _innerTree = new InnerBinaryTree<T>();
        }

        public void Add(T item)
        {            
            _innerTree.Add(item);
            _count++;
        }

        public void Clear()
        {
            _innerTree.Clear();
        }

        public bool Contains(T item)
        {
            return _innerTree.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            var removed = _innerTree.Remove(item);
            if (removed)
            {
                _count--;
            }
            return removed;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private class InnerBinaryTree<T> where T : IComparable
        {
            private InnerBinaryTree<T> _left;
            private InnerBinaryTree<T> _right;
            private ValueWrapper<T> _value;

            public void Add(T item)
            {
                var nodeToInsert = new ValueWrapper<T> { Value = item };
                var currentNode = this;
                InnerBinaryTree<T> previousNode = null;

                if (_value == null)
                {
                    _value = nodeToInsert;
                    return;
                }

                while (currentNode != null)
                {
                    previousNode = currentNode;
                    var compareResult = item.CompareTo(currentNode._value.Value);
                    if (compareResult > 0)
                    {
                        currentNode = currentNode._right;
                    }
                    else if (compareResult < 0)
                    {
                        currentNode = currentNode._left;
                    }

                }
                var previousCompare = item.CompareTo(previousNode._value.Value);
                if (previousCompare < 0)
                {
                    previousNode._left = new InnerBinaryTree<T>()
                    {
                        _value = nodeToInsert
                    };
                }
                else
                {
                    previousNode._right = new InnerBinaryTree<T>()
                    {
                        _value = nodeToInsert
                    };
                }
            }

            public void Clear()
            {
                _left = null;
                _right = null;
            }

            public bool Contains(T item)
            {
                var currentNode = this;
                while (currentNode != null && currentNode._value != null)
                {
                    var compareResult = item.CompareTo(currentNode._value.Value);
                    if (compareResult > 0)
                    {
                        currentNode = currentNode._right;
                    }
                    else if (compareResult < 0)
                    {
                        currentNode = currentNode._left;
                    }
                    else
                    {
                        return true;
                    }

                }
                return false;
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public IEnumerator<T> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            public bool Remove(T item)
            {
                var currentNode = this;
                InnerBinaryTree<T> previousNode = null;
                while (currentNode != null && currentNode._value?.Value.CompareTo(item) != 0)
                {
                    previousNode = currentNode;
                    var compareResult = item.CompareTo(currentNode._value.Value);
                    if (compareResult > 0)
                    {
                        currentNode = currentNode._right;
                    }
                    else if (compareResult < 0)
                    {
                        currentNode = currentNode._left;
                    }

                }
                if (currentNode != null && (currentNode._left == null || currentNode._right == null))
                {
                    if (currentNode._left == null)
                    {
                        currentNode._value = currentNode._right?._value;
                        currentNode._left = currentNode._right?._left;
                        currentNode._right = currentNode._right?._right;

                    }
                    else
                    {
                        currentNode._right = currentNode._left?._right;
                        currentNode._value = currentNode._left?._value;
                        currentNode._left = currentNode._left?._left;
                    }

                    if (currentNode._value?.Value.CompareTo(previousNode._value.Value) > 0)
                    {
                        previousNode._right = currentNode;
                        return true;
                    }
                    else
                    {
                        previousNode._left = currentNode;
                        return true;
                    }

                }
                else if (currentNode != null)
                {
                    InnerBinaryTree<T> inOrderPrevious = null;
                    InnerBinaryTree<T> tempNode;

                    tempNode = currentNode._right;
                    while (tempNode._left != null)
                    {
                        inOrderPrevious = tempNode;
                        tempNode = tempNode._left;
                    }

                    if (inOrderPrevious != null)
                    {
                        inOrderPrevious._left = tempNode._right;
                    }
                    else
                    {
                        currentNode._right = tempNode._right;
                    }

                    currentNode._value = tempNode._value;
                    return true;
                }

                return false;
            }            

            private class ValueWrapper<T>
            {
                public T Value { get; set; }
            }
        }
    }

    
}
