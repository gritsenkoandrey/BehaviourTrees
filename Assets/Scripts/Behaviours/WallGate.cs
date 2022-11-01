using Helpers;
using Interfaces;
using UnityEngine;

namespace Behaviours
{
    public sealed class WallGate : MonoBehaviour, IPosition, IAnimator
    {
        [SerializeField] private Animator _animator;

        public Vector3 Position => transform.position;
        public Animator Animator => _animator;
        
        private bool _isOpen;

        public void OpenGate()
        {
            if (_isOpen) return;

            _isOpen = true;
            _animator.SetTrigger(Animations.OpenDoor);
        }

        public void CloseDoor()
        {
            if (!_isOpen) return;

            _isOpen = false;
            _animator.SetTrigger(Animations.CloseDoor);
        }
    }
}