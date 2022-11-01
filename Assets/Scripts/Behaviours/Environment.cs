using Enums;
using Interfaces;
using UnityEngine;

namespace Behaviours
{
    public sealed class Environment : MonoBehaviour, IPosition, IHumanType
    {
        [SerializeField] private GameObject _zone;
        [SerializeField] private HumanType _humanType;
        
        public HumanType HumanType => _humanType;
        public Vector3 Position => transform.position;

        private bool _isUnlock;

        public void Unlock()
        {
            if (!_isUnlock)
            {
                _isUnlock = true;
                _zone.SetActive(false);
            }
            else
            {
                _isUnlock = false;
                _zone.SetActive(true);
            }
        }
    }
}