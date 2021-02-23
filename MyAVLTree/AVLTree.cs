using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAVLTree
{
    //class Node and method FindNode have to be internal and private
    //IEnumerable?
    //Remove?
    //Balance for remove?
    public class AVLTree<TKey, TValue> //: IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        public int Count { get; private set; }
        private Node<TKey, TValue> _root;

        public AVLTree()
        {
            Count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            var node = new Node<TKey, TValue>(key, value);
            if (_root == null)
            {
                _root = node;
                Count++;
                return;
            }
            var current = _root;
            var parent = _root;
            while (current != null)
            {
                parent = current;
                if (current.Key.CompareTo(node.Key) == 0)
                {
                    throw new ArgumentException("Dublicate key");
                }
                if (current.Key.CompareTo(node.Key) > 0)
                {
                    current = current.Left;
                }
                else if (current.Key.CompareTo(node.Key) < 0)
                {
                    current = current.Right;
                }
            }
            if (parent.Key.CompareTo(node.Key) > 0)
            {
                parent.Left = node;
                parent.LeftHeight++;
            }
            else
            {
                parent.Right = node;
                parent.RightHeight++;
            }
            if (parent.Parent != null)
            {
                if (parent.Parent.Left == parent)
                    parent.Parent.LeftHeight++;
                else
                    parent.Parent.RightHeight++;
            }
            node.Parent = parent;
            Count++;
            Balance(node);
        }

        private void Balance(Node<TKey, TValue> node)
        {
            var grandParent = node.Parent.Parent;
            var parent = node.Parent;
            if (grandParent != null)
            {
                if (grandParent.LeftHeight - grandParent.RightHeight > 1)
                {
                    if (parent.Left == node)
                    {
                        parent.Parent = grandParent.Parent;
                        if (parent.Parent == null)
                            _root = parent;
                        grandParent.Parent = parent;
                        grandParent.Left = null;
                        parent.Right = grandParent;
                        parent.RightHeight++;
                        grandParent.LeftHeight -= 2;
                    }
                    else
                    {
                        node.Parent = grandParent.Parent;
                        if (node.Parent == null)
                            _root = node;
                        parent.Parent = node;
                        grandParent.Parent = node;
                        node.Left = parent;
                        node.Right = grandParent;
                        parent.Right = null;
                        grandParent.Left = null;
                        grandParent.LeftHeight -= 2;
                        parent.RightHeight--;
                        node.LeftHeight++;
                        node.RightHeight++;
                    }
                }
                else if (grandParent.RightHeight - grandParent.LeftHeight > 1)
                {
                    if (parent.Right == node)
                    {
                        parent.Parent = grandParent.Parent;
                        if (parent.Parent == null)
                            _root = parent;
                        grandParent.Parent = parent;
                        grandParent.Right = null;
                        parent.Left = grandParent;
                        parent.LeftHeight++;
                        grandParent.RightHeight -= 2;
                    }
                    else
                    {
                        node.Parent = grandParent.Parent;
                        if (node.Parent == null)
                            _root = node;
                        parent.Parent = node;
                        grandParent.Parent = node;
                        node.Right = parent;
                        node.Left = grandParent;
                        parent.Left = null;
                        grandParent.Right = null;
                        grandParent.RightHeight -= 2;
                        parent.LeftHeight--;
                        node.LeftHeight++;
                        node.RightHeight++;
                    }
                }
            }
        }

        public bool Remove(TKey key)
        {
            var node = FindNode(key);
            if (node == null)
                return false;
            //удаление
            return true;
        }

        public Node<TKey, TValue> FindNode(TKey key)
        {
            var current = _root;
            while (current != null)
            {
                if (current.Key.CompareTo(key) == 0)
                {
                    return current;
                }
                if (current.Key.CompareTo(key) > 0)
                {
                    current = current.Left;
                }
                else if (current.Key.CompareTo(key) < 0)
                {
                    current = current.Right;
                }
            }
            return null;
        }

        public bool ContainsKey(TKey key)
        {
            if (FindNode(key) == null)
                return false;
            return true;
        }

        //public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        //{
        //    return new TreeEnum(this);
        //}
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetEnumerator();
        //}
    }

    //public class TreeEnum<TKey, TValue> : IEnumerator
    //     where TKey : IComparable<TKey>
    //{
    //    AVLTree<TKey, TValue> tree = new AVLTree<TKey, TValue>();
    //    int index = -1;
    //    public TreeEnum(AVLTree<TKey, TValue> aVLTree)
    //    {
    //        tree = aVLTree;
    //    }

    //    public Node<TKey, TValue> Current
    //    {
    //        get
    //        {
    //            return tree[index];
    //        }
    //    }

    //    public bool MoveNext()
    //    {
    //        index++;
    //        return (index < tree.Count);
    //    }

    //    public void Reset()
    //    {
    //        index = -1;
    //    }
    //}
}
