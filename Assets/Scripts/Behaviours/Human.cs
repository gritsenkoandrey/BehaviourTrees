using System;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours
{
    public abstract class Human : MonoBehaviour, IAgent, IAnimator, IPosition, IHumanType, IStatus
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;

        public NavMeshAgent Agent => _agent;
        public Animator Animator => _animator;
        public Vector3 Position => transform.position;
        public HumanType HumanType { get; protected set; }
        public Status Status { get; protected set; }

        protected ActionState State { get; set; }
        protected Action<ActionState> Action { get; set; }

        private void Start() => Initialize();
        private void Update() => Tick();

        protected abstract void Initialize();
        protected abstract void Tick();
        
        protected Status GoToPosition(Vector3 destination)
        {
            float distanceToTarget = Vector3.Distance(destination, Position);

            if (State == ActionState.Idle)
            {
                _agent.SetDestination(destination);

                Action.Invoke(ActionState.Walking);
            }
            else if (Vector3.Distance(_agent.pathEndPosition, destination) >= 2f)
            {
                Action.Invoke(ActionState.Idle);

                return Status.FAILURE;
            }
            else if (distanceToTarget < 2f)
            {
                Action.Invoke(ActionState.Idle);

                return Status.SUCCESS;
            }

            return Status.RUNNING;
        }
    }
}