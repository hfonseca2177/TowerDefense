using UnityEngine;

namespace TowerDefense.Util
{
    /// <summary>
    /// Assign a serializable reference for obj pool
    /// </summary>
    public class ObjectPoolLoader: MonoBehaviour
    {
        [SerializeField] private ObjectPoolingReference _objectPoolingReference;
        [SerializeField] private ObjectPooling _objectPooling;

        private void Start()
        {
            _objectPoolingReference.Pool = _objectPooling;
        }
    }
}