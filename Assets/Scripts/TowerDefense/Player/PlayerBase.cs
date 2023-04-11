using System;
using UnityEngine;

namespace TowerDefense.Player
{
    public class PlayerBase : MonoBehaviour
    {
        [SerializeField] private GameObject _visualRepresentation;

        private void Start()
        {
            _visualRepresentation.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            //other.TryGetComponent(out Enemy)
        }
    }
}