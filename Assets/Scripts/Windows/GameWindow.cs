using System;
using UnityEngine;
using UnityEngine.UI;

namespace Windows
{
    public class GameWindow : MonoBehaviour
    {
        public event Action RestartEvent = delegate { };

        [SerializeField] private Button _restartButton;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
        }

        private void OnRestartButtonClicked()
        {
            RestartEvent();
        }
    }
}
