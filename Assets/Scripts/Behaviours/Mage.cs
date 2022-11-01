using BehaviourTreeNode;
using Enums;
using Helpers;
using UnityEngine;

namespace Behaviours
{
    public sealed class Mage : Human
    {
        [SerializeField] private WallGate _gate;
        [SerializeField] private Environment _archerEnv;
        [SerializeField] private Environment _mageEnv;
        [SerializeField] private Workshop _workshop;
        
        [SerializeField] private Human _human;

        private BehaviourTree _tree;

        protected override void Initialize()
        {
            Agent.enabled = true;

            HumanType = HumanType.Mage;
            
            State = ActionState.Idle;

            Status = Status.SUCCESS;
            
            InitializeBehaviourTree();

            Action += ChangeAnimation;
        }

        private void InitializeBehaviourTree()
        {
            _tree = new BehaviourTree();

            Sequence steel = new Sequence("Steal");

            Leaf goToGate = new Leaf("Go To Gate", GoToGate);
            Leaf goToWorkshop = new Leaf("Go To Workshop", GoToWorkshop);
            Leaf openDoor = new Leaf("Open Door", OpenDoor);
            Leaf closeDoor = new Leaf("Close Door", CloseDoor);

            Selector unlockEnvironment = new Selector("Unlock Environment");

            Leaf archerEnvironment = new Leaf("Archer Environment", GoToArcherEnvironment);
            Leaf mageEnvironment = new Leaf("Mage Environment", GoToMageEnvironment);

            unlockEnvironment.AddChild(mageEnvironment);
            unlockEnvironment.AddChild(archerEnvironment);

            steel.AddChild(goToGate);
            steel.AddChild(openDoor);
            steel.AddChild(unlockEnvironment);
            steel.AddChild(goToGate);
            steel.AddChild(closeDoor);
            steel.AddChild(goToWorkshop);

            _tree.AddChild(steel);

            _tree.PrintTree();
        }

        protected override void Tick()
        {
            if (_human.Status == Status.SUCCESS)
            {
                Status = Status.RUNNING;
            }
            
            if (Status != Status.SUCCESS)
            {
                Status = _tree.Process();
            }
        }

        private Status GoToArcherEnvironment() => GoToEnvironment(_archerEnv);
        private Status GoToMageEnvironment() => GoToEnvironment(_mageEnv);
        private Status GoToWorkshop() => GoToPosition(_workshop.Position);
        private Status GoToGate() => GoToPosition(_gate.Position);

        private Status OpenDoor()
        {
            _gate.OpenGate();

            return Status.SUCCESS;
        }

        private Status CloseDoor()
        {
            _gate.CloseDoor();

            return Status.SUCCESS;
        }
        
        private Status GoToEnvironment(Environment environment)
        {
            Status status = GoToPosition(environment.Position);

            if (status == Status.SUCCESS)
            {
                if (environment.HumanType == HumanType)
                {
                    environment.Unlock();
                    
                    return Status.SUCCESS;
                }
                
                return Status.FAILURE;
            }

            return status;
        }
        
        private void ChangeAnimation(ActionState state)
        {
            if (state == ActionState.Idle)
            {
                State = ActionState.Idle;
                
                Animator.SetBool(Animations.IsIdle, true);
                Animator.SetBool(Animations.IsRun, false);
            }
            else
            {
                State = ActionState.Walking;

                Animator.SetBool(Animations.IsIdle, false);
                Animator.SetBool(Animations.IsRun, true);
            }
        }
    }
}