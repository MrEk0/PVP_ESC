using System;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using Enums;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/GameSettingsData")]
    public class GameSettingsData : ScriptableObject
    {
        public enum AbilityOwnerType
        {
            Other,
            Myself
        }
        
        [Serializable]
        public class AbilitySettings<T> where T : Ability
        {
            [SerializeField] private AbilityTypes _type;
            [SerializeField] private AbilityOwnerType _ownerType;
            [SerializeField] private Sprite _sprite;
            [SerializeField] private int _reloadSteps;
            [SerializeField] private int _durationSteps;
            [SerializeField] private int _damage;
            [SerializeField] private int _healAmount;
            [SerializeField] private int _stepDamage;
            
            public AbilityTypes Type => _type;
            public AbilityOwnerType OwnerType => _ownerType;
            public Sprite Sprite => _sprite;
            public int ReloadSteps => _reloadSteps;
            public int DurationSteps => _durationSteps;
            public int Damage => _damage;
            public int HealAmount => _healAmount;
            public int StepDamage => _stepDamage;
            public T Ability { get; set; }
        }
        
        [SerializeField] private int _maxHp;
        [SerializeField] private AbilitySettings<Ability>[] _availableAbilities = Array.Empty<AbilitySettings<Ability>>();

        public int MaxHp => _maxHp;

        public IReadOnlyList<AbilitySettings<Ability>> AvailableAbilities => _availableAbilities;

        private void OnValidate()
        {
            foreach (var availableAbility in _availableAbilities)
            {
                availableAbility.Ability = availableAbility.Type switch
                {
                    AbilityTypes.BarrierAbility => new BarrierAbility(availableAbility.Damage),
                    AbilityTypes.DamageAbility => new DamageAbility(availableAbility.Damage),
                    AbilityTypes.FireBallAbility => new FireBallAbility(availableAbility.Damage,
                        availableAbility.StepDamage),
                    AbilityTypes.PurificationAbility => new PurificationAbility(),
                    AbilityTypes.RegenerationAbility => new RegenerationAbility(availableAbility.HealAmount),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public bool IsAbilityEnemyApplied(AbilityTypes type)
        {
            var abilitySettings = _availableAbilities.FirstOrDefault(o => o.Type == type);
            if (abilitySettings == null)
                return false;

            return abilitySettings.OwnerType == AbilityOwnerType.Other;
        }
    }
}