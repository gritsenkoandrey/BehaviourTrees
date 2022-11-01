using Enums;

namespace BehaviourTreeNode
{
    public sealed class Leaf : Node
    {
        public delegate Status Tick();

        private readonly Tick _processMethod;

        public Leaf() { }

        public Leaf(string name, Tick processMethod)
        {
            Name = name;
            
            _processMethod = processMethod;
        }

        public override Status Process()
        {
            if (_processMethod != null)
            {
                return _processMethod();
            }

            return Status.FAILURE;
        }
    }
}