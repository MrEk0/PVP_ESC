using System;
using System.Collections.Generic;
using Abilities;
using Characters;
using Common;
using Enums;
using UnityEngine;

namespace Windows
{
    public class AbilityWindow : AWindow
    {
        public event Action<AbilityTypes> AbilitySelectEvent = delegate { };

        [SerializeField] private RectTransform _abilityPanel;
        [SerializeField] private AbilityWindowItem _poolItem;
        [SerializeField] private RectTransform _parent;
        
        private PlayerCharacter _player;
        private List<AbilityWindowItem> _items = new();

        public override void Init(ServiceLocator serviceLocator)
        {
            _player = serviceLocator.GetService<PlayerCharacter>();

            foreach (var windowItem in _items)
                Destroy(windowItem.gameObject);
            _items.Clear();
            
            foreach (var abilityData in _player.CharacterAbilities.AbilitiesData)
            {
                var item = Instantiate(_poolItem, _parent);
                item.AbilitySelectEvent += OnAbilitySelected;

                InitItem(item, abilityData);
                _items.Add(item);
            }

            _player.StepStartEvent += OnPlayerStepStarted;
        }

        private void OnDestroy()
        {
            _player.StepStartEvent -= OnPlayerStepStarted;
        }

        private void OnPlayerStepStarted()
        {
            _abilityPanel.gameObject.SetActive(true);

            for (var i = 0; i < _player.CharacterAbilities.AbilitiesData.Count; i++)
            {
                if (_items.Count < i || _items.Count == 0)
                    return;
                
                InitItem(_items[i], _player.CharacterAbilities.AbilitiesData[i]);
            }
        }

        private void OnAbilitySelected(AbilityTypes type)
        {
            _abilityPanel.gameObject.SetActive(false);

            AbilitySelectEvent(type);
        }

        private void InitItem(AbilityWindowItem item, CharacterAbilities.AbilityData<Ability> data)
        {
            var windowItemData = new AbilityWindowItemData
            {
                Sprite = data.Sprite,
                ReloadSteps = data.CurrentReloadSteps,
                Type = data.Type
            };
            item.Init(windowItemData);
        }
    }
}
