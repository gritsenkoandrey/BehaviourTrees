using Enums;

namespace BehaviourTreeNode
{
    public sealed class Selector : Node
    {
        public Selector(string name)
        {
            Name = name;
        }

        public override Status Process()
        {
            Status childStatus = Children[CurrentChild].Process();

            if (childStatus == Status.RUNNING)
            {
                return Status.RUNNING;
            }

            if (childStatus == Status.SUCCESS)
            {
                CurrentChild = 0;
                
                return Status.SUCCESS;
            }

            CurrentChild++;

            if (CurrentChild >= Children.Count)
            {
                CurrentChild = 0;

                return Status.FAILURE;
            }
            
            return Status.RUNNING;
        }
    }
}