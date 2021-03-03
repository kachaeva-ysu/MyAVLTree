using System;
using System.Collections;
using System.Collections.Generic;

namespace MyAVLTree
{
    public class AVLTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        public int Count { get; private set; }
        private Node<TKey, TValue> _root;

        public AVLTree()
        {
            Count = 0;
        }

        #region balancing

        private Node<TKey, TValue> RotateRight(Node<TKey, TValue> p)
        {
            var q = p.Left;
            p.Left = q.Right;
            q.Right = p;
            p.FixHeight();
            if (p == _root)
            {
                _root = q;
            }
            return q;
        }

        private Node<TKey, TValue> RotateLeft(Node<TKey, TValue> q)
        {
            var p = q.Right;
            q.Right = p.Left;
            p.Left = q;
            q.FixHeight();
            if (q == _root)
                _root = p;
            return p;
        }

        private Node<TKey, TValue> Balance(Node<TKey, TValue> p)
        {
            p.FixHeight();
            if (p.BalanceFactor == 2)
            {
                if (p.Right.BalanceFactor < 0)
                    p.Right = RotateRight(p.Right);
                return RotateLeft(p);
            }
            if (p.BalanceFactor == -2)
            {
                if (p.Left.BalanceFactor > 0)
                    p.Left = RotateLeft(p.Left);
                return RotateRight(p);
            }
            return p;
        }

        #endregion balancing

        #region addition

        public void Add(TKey key, TValue value)
        {
            _root = Insert(key, value, _root);
        }

        private Node<TKey, TValue> Insert(TKey key, TValue value, Node<TKey, TValue> p)
        {
            var node = new Node<TKey, TValue>(key, value);
            if (p == null)
            {
                Count++;
                return node;
            }
            if (key.CompareTo(p.Key) < 0)
                p.Left = Insert(key, value, p.Left);
            else
                p.Right = Insert(key, value, p.Right);
            return Balance(p);
        }

        #endregion addition

        #region removing

        public bool Remove(TKey key)
        {
            var result = Remove(_root, key);
            if (result == null)
                return false;
            return true;
        }

        private Node<TKey, TValue> FindMin(Node<TKey, TValue> p)
        {
            if (p.Left == null)
                return p;
            return FindMin(p.Left);
        }

        private Node<TKey, TValue> RemoveMin(Node<TKey, TValue> p)
        {
            if (p.Left == null)
                return p.Right;
            p.Left = RemoveMin(p.Left);
            return Balance(p);
        }

        private Node<TKey, TValue> Remove(Node<TKey, TValue> p, TKey key)
        {
            if (p == null)
            {
                return null;
            }
            if (key.CompareTo(p.Key) < 0)
                p.Left = Remove(p.Left, key);
            else if (key.CompareTo(p.Key) > 0)
                p.Right = Remove(p.Right, key);
            else
            {
                Node<TKey, TValue> q = p.Left;
                Node<TKey, TValue> r = p.Right;
                p = null;
                if (r == null)
                    return q;
                Node<TKey, TValue> min = FindMin(r);
                min.Right = RemoveMin(r);
                min.Left = q;
                Count--;
                return Balance(min);
            }
            return Balance(p);
        }

        #endregion removing

        #region search

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

        public TValue this[TKey key]
        {
            get
            {
                var result = FindNode(key);
                if (result == null)
                    throw new ArgumentNullException();
                return result.Value;
            }
            set
            {
                var result = FindNode(key);
                if (result == null)
                    throw new ArgumentNullException();
                result.Value = value;
            }
        }

        #endregion search

        #region enumeration

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

        #endregion enumeration
    }
}