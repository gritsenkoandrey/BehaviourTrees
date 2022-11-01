using Unity.AI.Navigation;
using UnityEngine;

namespace Behaviours
{
    public sealed class BuilderNavMesh : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface _navMeshSurface;
        
        private void Awake() => _navMeshSurface.BuildNavMesh();
    }
}