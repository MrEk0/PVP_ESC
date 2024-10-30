using System;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Windows
{
    public class AbilityWindowItem : AWindowItem<AbilityWindowItemData>
    {
        public event Action<AbilityTypes> AbilitySelectEvent = delegate { };
        
        [SerializeField] private Image _abilityImage;
        [SerializeField] private TMP_Text _reloadText;
        [SerializeField] private RectTransform _blockImage;
        [SerializeField] private Button _selectButton;

        private AbilityTypes _type;
        
        public override void Init(AbilityWindowItemData data)
        {
            _abilityImage.sprite = data.Sprite;
            _reloadText.text = data.ReloadSteps.ToString();
            _type = data.Type;

            _blockImage.gameObject.SetActive(data.ReloadSteps > 0);
            _selectButton.interactable = data.ReloadSteps == 0;
        }

        private void OnEnable()
        {
            _selectButton.onClick.AddListener(OnSelectButtonClicked);
        }

        private void OnDisable()
        {
            _selectButton.onClick.RemoveListener(OnSelectButtonClicked);
        }

        private void OnSelectButtonClicked()
        {
            AbilitySelectEvent(_type);
        }
    }
}
