using TowerDefense.Enemies;
using UnityEngine;

namespace TowerDefense.Game
{
    /// <summary>
    /// Game Manager -controls core components and game cycle
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform _playerBase;
        [SerializeField] private TargetReference _targetReference;

        private void Awake()
        {
            _targetReference.Target = _playerBase;
        }

    }
}