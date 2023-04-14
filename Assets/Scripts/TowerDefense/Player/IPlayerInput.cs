using UnityEngine;

namespace TowerDefense.Player
{
    /// <summary>
    /// Contract for possible players inputs
    /// </summary>
    public interface IPlayerInput
    {
        //Returns pointer position
        Vector3 GetPointerPosition();
        //whenever player click or touch game 
        void OnPointerDown(Vector3 worldPosition);
    }
}