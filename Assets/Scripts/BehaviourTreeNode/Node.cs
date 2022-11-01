using System.Collections.Generic;
using Enums;

namespace BehaviourTreeNode
{
    public abstract class Node
    {
        protected int CurrentChild = 0;
        
        public Status Status;
        public readonly List<Node> Children = new ();
        public string Name;

        protected Node() { }

        protected Node(string name) => Name = name;

        public void AddChild(Node node) => Children.Add(node);
        public virtual Status Process() => Children[CurrentChild].Process();
    }
}