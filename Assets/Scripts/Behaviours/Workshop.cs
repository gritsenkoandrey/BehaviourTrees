using Interfaces;
using UnityEngine;

namespace Behaviours
{
    public sealed class Workshop : MonoBehaviour, IPosition
    {
        public Vector3 Position => transform.position;
    }
}