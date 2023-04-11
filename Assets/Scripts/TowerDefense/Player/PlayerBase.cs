using TowerDefense.Enemies;
using TowerDefense.Events;
using UnityEngine;

namespace TowerDefense.Player
{
    /// <summary>
    /// PLayer base - goal line where enemies try to reach to deal damage to the player 
    /// </summary>
    public class PlayerBase : MonoBehaviour
    {
        [Tooltip("Player baseline stats like Hit Points")]
        [SerializeField] private PlayerBaseStats _playerBaseStats;
        [Header("Events")]
        [Tooltip("Notifies current player Hit Points")]
        [SerializeField] private IntEventAsset _onPlayerHpUpdateNotify;
        [Tooltip("Notifies player damage taken")]
        [SerializeField] private IntEventAsset _onPlayerDamageTakenNotify;
        [Tooltip("Visual representation to show where is the base position")]
        [SerializeField] private GameObject _visualRepresentation;
        
        //run time HP state
        private int _currentHP;

        private void Awake()
        {
            _currentHP = _playerBaseStats.HitPoints;
        }

        private void Start()
        {
            _visualRepresentation.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                int damageTaken = enemy.HitPlayer();
                _currentHP -= damageTaken;
                _onPlayerDamageTakenNotify.Invoke(damageTaken);
                _onPlayerHpUpdateNotify.Invoke(_currentHP);
            }
        }
    }
}