using UnityEngine;

namespace Helpers
{
    public static class Animations
    {
        public static int IsIdle => Animator.StringToHash(nameof(IsIdle));
        public static int IsRun => Animator.StringToHash(nameof(IsRun));
        public static int Attack => Animator.StringToHash(nameof(Attack));
        public static int OpenDoor => Animator.StringToHash(nameof(OpenDoor));
        public static int CloseDoor => Animator.StringToHash(nameof(CloseDoor));
    }
}