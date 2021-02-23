using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAVLTree
{
    public class Node<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Node<TKey, TValue> Left, Right, Parent;
        public int LeftHeight, RightHeight;

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
