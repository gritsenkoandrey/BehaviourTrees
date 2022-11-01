using System.Collections.Generic;
using System.Text;
using Enums;
using UnityEngine;

namespace BehaviourTreeNode
{
    public sealed class BehaviourTree : Node
    {
        public BehaviourTree() => Name = "Tree";

        public BehaviourTree(string name) => Name = name;

        public override Status Process() => Children[CurrentChild].Process();

        public void PrintTree()
        {
            StringBuilder print = new StringBuilder();

            Stack<NodeLevel> nodeStack = new Stack<NodeLevel>();

            Node currentNode = this;
            
            nodeStack.Push(new NodeLevel{Level = 0, Node = currentNode});

            while (nodeStack.Count != 0)
            {
                NodeLevel nextNode = nodeStack.Pop();
                
                print.Append('-', nextNode.Level);
                print.Append($"{nextNode.Node.Name}\n");

                for (int i = nextNode.Node.Children.Count - 1; i >= 0; i--)
                {
                    nodeStack.Push(new NodeLevel{Level = nextNode.Level + 1, Node = nextNode.Node.Children[i]});
                }
            }
            
            Debug.Log(print);
        }
    }
}