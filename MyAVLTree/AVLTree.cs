using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAVLTree
{
    //Balance for insert and remove?
    //class Node has to be internal, _root has to be private
    public class AVLTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        public int Count { get; private set; }
        public Node<TKey, TValue> _root;

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
            //Balance(node);
        }

        //private void Balance(Node<TKey, TValue> node)
        //{
        //    var grandParent = node.Parent.Parent;
        //    var parent = node.Parent;
        //    if (grandParent != null)
        //    {
        //        if (grandParent.LeftHeight - grandParent.RightHeight > 1)
        //        {
        //            if (parent.Left == node)
        //            {
        //                parent.Parent = grandParent.Parent;
        //                if (parent.Parent == null)
        //                    _root = parent;
        //                grandParent.Parent = parent;
        //                grandParent.Left = null;
        //                parent.Right = grandParent;
        //                parent.RightHeight++;
        //                grandParent.LeftHeight -= 2;
        //            }
        //            else
        //            {
        //                node.Parent = grandParent.Parent;
        //                if (node.Parent == null)
        //                    _root = node;
        //                parent.Parent = node;
        //                grandParent.Parent = node;
        //                node.Left = parent;
        //                node.Right = grandParent;
        //                parent.Right = null;
        //                grandParent.Left = null;
        //                grandParent.LeftHeight -= 2;
        //                parent.RightHeight--;
        //                node.LeftHeight++;
        //                node.RightHeight++;
        //            }
        //        }
        //        else if (grandParent.RightHeight - grandParent.LeftHeight > 1)
        //        {
        //            if (parent.Right == node)
        //            {
        //                parent.Parent = grandParent.Parent;
        //                if (parent.Parent == null)
        //                    _root = parent;
        //                grandParent.Parent = parent;
        //                grandParent.Right = null;
        //                parent.Left = grandParent;
        //                parent.LeftHeight++;
        //                grandParent.RightHeight -= 2;
        //            }
        //            else
        //            {
        //                node.Parent = grandParent.Parent;
        //                if (node.Parent == null)
        //                    _root = node;
        //                parent.Parent = node;
        //                grandParent.Parent = node;
        //                node.Right = parent;
        //                node.Left = grandParent;
        //                parent.Left = null;
        //                grandParent.Right = null;
        //                grandParent.RightHeight -= 2;
        //                parent.LeftHeight--;
        //                node.LeftHeight++;
        //                node.RightHeight++;
        //            }
        //        }
        //    }
        //}

        public bool Remove(TKey key)
        {
            var node = FindNode(key);
            if (node == null)
                return false;
            if (node.Left == null && node.Right == null)
            {
                if (node.Parent.Key.CompareTo(node.Key) < 0)
                    node.Parent.Right = null;
                else
                    node.Parent.Left = null;
            }
            else if (node.Left == null && node.Right != null)
            {
                if (node == _root)
                {
                    node.Right = _root;
                    node.Right.Parent = null;
                }
                else
                {
                    if (node.Parent.Key.CompareTo(node.Key) < 0)
                        node.Parent.Right = node.Right;
                    else
                        node.Parent.Left = node.Right;
                    node.Right.Parent = node.Parent;
                }
            }
            else if (node.Left != null && node.Right == null)
            {
                if (node == _root)
                {
                    node.Left = _root;
                    node.Left.Parent = null;
                }
                else
                {
                    if (node.Parent.Key.CompareTo(node.Key) < 0)
                        node.Parent.Right = node.Left;
                    else
                        node.Parent.Left = node.Left;
                    node.Left.Parent = node.Parent;
                }
            }
            else if (node.Left != null && node.Right != null)
            {
                var proxy = node.Left;
                while (proxy.Right != null)
                    proxy = proxy.Right;
                Remove(proxy.Key);
                if (node == _root)
                {
                    _root = proxy;
                    proxy.Parent = null;
                }
                else
                {
                    if (node.Parent.Key.CompareTo(node.Key) < 0)
                        node.Parent.Right = proxy;
                    else
                        node.Parent.Left = proxy;
                    proxy.Parent = node.Parent;
                }
                node.Left.Parent = proxy;
                node.Right.Parent = proxy;
                proxy.Left = node.Left;
                proxy.Right = node.Right;
            }

            return true;
        }

        private Node<TKey, TValue> FindNode(TKey key)
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

        public IEnumerable<KeyValuePair<TKey, TValue>> Traverse()
        {
            return Traverse(_root);
        }

        private IEnumerable<KeyValuePair<TKey, TValue>> Traverse(Node<TKey, TValue> node)
        {
            var list = new List<KeyValuePair<TKey, TValue>>();
            if (node != null)
            {
                list.AddRange(Traverse(node.Left));
                list.Add(new KeyValuePair<TKey, TValue>(node.Key, node.Value));
                list.AddRange(Traverse(node.Right));
            }
            return list;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var item in Traverse(_root))
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Traverse(_root).GetEnumerator();
        }
    }
}