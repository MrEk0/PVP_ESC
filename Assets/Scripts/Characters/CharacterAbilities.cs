using System.Collections.Generic;
using System.Linq;
using Abilities;
using Enums;
using UnityEngine;

namespace Characters
{
    public class CharacterAbilities
    {
        public class AbilityData<T> where T : Ability
        {
            public T Ability;
            public Sprite Sprite;
            public int CurrentReloadSteps;
            public int MaxReloadSteps;
            public int DurationSteps;
            public AbilityTypes Type;
        }

        public IReadOnlyList<AbilityData<Ability>> AbilitiesData => _abilitiesData;
        public IReadOnlyList<AbilityTypes> ApplyAbilities => _applyAbilities;

        private readonly List<AbilityData<Ability>> _abilitiesData = new();
        private readonly List<AbilityTypes> _applyAbilities = new();

        public void AddRange(IEnumerable<AbilityData<Ability>> abilitiesData)
        {
            _abilitiesData.AddRange(abilitiesData);
        }

        public void SelectAbility(AbilityTypes type)
        {
            var abilityData = _abilitiesData.FirstOrDefault(o => o.Type == type);
            if (abilityData == null)
                return;

            abilityData.CurrentReloadSteps = abilityData.MaxReloadSteps;
        }

        public void ChangeSteps()
        {
            foreach (var abilityData in _abilitiesData)
            {
                if (_applyAbilities.Contains(abilityData.Type))
                {
                    abilityData.Ability.MakeStep();
                    if (abilityData.DurationSteps >= abilityData.Ability.CurrentSteps)
                    {
                        abilityData.Ability.Remove();
                        _applyAbilities.Remove(abilityData.Type);
                    }
                }

                abilityData.CurrentReloadSteps = abilityData.CurrentReloadSteps > 0 ? abilityData.CurrentReloadSteps - 1 : 0;
            }
        }

        public void Add(Character owner, AbilityTypes type)
        {
            var abilityData = _abilitiesData.FirstOrDefault(o => o.Type == type);
            if (abilityData == null)
                return;

            abilityData.Ability.Owner = owner;
            abilityData.Ability.Apply();

            _applyAbilities.Add(type);
        }

        public void Remove(AbilityTypes type)
        {
            _applyAbilities.RemoveAll(o => o == type);
        }

        public void Restart()
        {
            foreach (var abilityData in _abilitiesData.Where(abilityData => _applyAbilities.Contains(abilityData.Type)))
                abilityData.Ability.Remove();
            _applyAbilities.Clear();

            foreach (var abilityData in _abilitiesData)
                abilityData.CurrentReloadSteps = 0;
        }
    }
}
