using TMPro;
using TowerDefense.Events;
using UnityEngine;

namespace TowerDefense.UI
{
    /// <summary>
    /// Displays a message notification to the user
    /// </summary>
    public class NotificationDisplay: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _message;
        [SerializeField] private GameObject _messagePanel;
        [SerializeField] private float _timer;
        [SerializeField] private StringEventAsset _onNotifyUser;

        private void OnEnable()
        {
            _onNotifyUser.OnInvoked.AddListener(OnNotifyUserEvent);
        }

        private void OnDisable()
        {
            _onNotifyUser.OnInvoked.RemoveListener(OnNotifyUserEvent);
        }

        private void OnNotifyUserEvent(string message)
        {
            NotifyMessage(message);
        }

        private void NotifyMessage(string message)
        {
            _message.text = message;
            _messagePanel.SetActive(true);
            Invoke(nameof(HideMessage), _timer);
        }

        private void HideMessage()
        {
            _messagePanel.SetActive(false);
        }


    }
}