using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAVLTree
{
    internal class Node<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Node<TKey, TValue> Left, Right;
        public int height = 0;

        public int BalanceFactor
        {
            get
            {
                if (Right == null && Left == null)
                    return 0;
                else if (Right == null)
                    return -Left.height;
                else if (Left == null)
                    return Right.height;
                else
                    return Right.height - Left.height;
            }
        }

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            height = 1;
        }

        public void FixHeight()
        {
            if (Right == null & Left == null)
                height = 1;
            else if (Right == null)
                height = Left.height + 1;
            else if (Left == null)
                height = Right.height + 1;
            else
                height = Math.Max(Left.height, Right.height) + 1;
        }
    }
}