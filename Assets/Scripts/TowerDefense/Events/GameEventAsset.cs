using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense.Events
{
    /// <summary>
    /// Base Serializable Game Event
    /// </summary>
    public abstract class GameEventAsset<T> : ScriptableObject
    {
        [SerializeField] private bool _isDebugEnabled;
        public UnityEvent<T> OnInvoked;

        public void Invoke(T param)
        {
            if(_isDebugEnabled) Debug.Log($" Object:{name} event invoked: {param}", this);
            OnInvoked.Invoke(param);
        }
    }
}
