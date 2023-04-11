using UnityEngine;

namespace TowerDefense.Towers
{
    /// <summary>
    /// Tower Upgrade Manager
    /// </summary>
    public class TowerUpgradePanel : MonoBehaviour
    {
        
        [SerializeField] private GameObject _upgradeButton;

        public void OnUnitySelect()
        {
            _upgradeButton.SetActive(!_upgradeButton.activeSelf);
        }

        public void UpgradeUnity()
        {
            Debug.Log("Upgraded");
            _upgradeButton.SetActive(false);
        }
    }
}