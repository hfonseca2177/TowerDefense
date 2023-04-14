using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Util
{
    /// <summary>
    /// Basic Object Pooling
    /// </summary>
    public class ObjectPooling : MonoBehaviour
    {
        [Tooltip("Prefab to be instantiated")]
        [SerializeField] private GameObject _objectPrefab;
        [Tooltip("Create this amount instances at start")]
        [SerializeField] private int _warmUpQuantity;
        
        private List<GameObject> _pooledObjects;
        
        [Header("Pool statistics")]
        [SerializeField] private int _activeObjects;
        [SerializeField] private int _disabledObjects { get { return _pooledObjects?.Count ?? 0;} }

        private void Start()
        {
            _activeObjects = 0;
            _pooledObjects = new List<GameObject>(_warmUpQuantity);
            WarmUp();
        }

        private void WarmUp()
        {
            for (int i = 0; i < _warmUpQuantity; i++)
            {
                InstantiateObject();
            }
        }

        private GameObject InstantiateObject()
        {
            var obj = Instantiate(_objectPrefab);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
            return obj;
        }

        public GameObject Get()
        {
            _activeObjects++;
            var obj =  _pooledObjects.Find(g => !g.activeInHierarchy);
            if (obj == null)
            {
                obj = InstantiateObject();
            }
            return obj;
        }

        public void Release(GameObject obj)
        {
            _activeObjects--;
            obj.SetActive(false);
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void Dispose()
        {
            _pooledObjects.ForEach(Destroy);
            _pooledObjects.Clear();
            _activeObjects = 0;
        }

    }
}