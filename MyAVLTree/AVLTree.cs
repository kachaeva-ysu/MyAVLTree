using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAVLTree
{
    //remove?
    public class AVLTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
    {
        public int Count { get; private set; }
        private Node<TKey, TValue> _root;

        public AVLTree()
        {
            Count = 0;
        }

        #region balance
        Node<TKey, TValue> RotateRight(Node<TKey, TValue> p)
        {
            var q = p.Left;
            p.Left = q.Right;
            q.Right = p;
            if (p == _root)
                _root = q;
            return q;
        }
        Node<TKey, TValue> RotateLeft(Node<TKey, TValue> q)
        {
            var p = q.Right;
            q.Right=p.Left;
            p.Left = q;
            if (q == _root)
                _root = p;
            return p;
        }
        Node<TKey,TValue> Balance(Node<TKey,TValue> p)
        {
            p.FixHeight();
            if(p.balanceFactor==2)
            {
                if (p.Right.balanceFactor < 0)
                    p.Right = RotateRight(p.Right);
                return RotateLeft(p);
            }
            if(p.balanceFactor==-2)
            {
                if (p.Left.balanceFactor > 0)
                    p.Left = RotateLeft(p.Left);
                return RotateRight(p);
            }
            return p;
        }
        #endregion
        public void Add(TKey key, TValue value)
        {
            _root=Insert(key, value, _root);
        }
        private Node<TKey,TValue> Insert(TKey key, TValue value,Node<TKey,TValue> p)
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