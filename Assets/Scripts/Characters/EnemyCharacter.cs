using System;
using Common;
using Configs;

namespace Characters
{
    public class EnemyCharacter : Character
    {
        public event Action StepCompleteEvent = delegate { };

        public EnemyCharacter(ServiceLocator serviceLocator) : base(serviceLocator) { }

        public override void Step()
        {
            var data = ServiceLocator.GetService<GameSettingsData>();
            var gameManager = ServiceLocator.GetService<GameManager>();

            var rndIndex = UnityEngine.Random.Range(0, CharacterAbilities.AbilitiesData.Count);
            var abilityData = CharacterAbilities.AbilitiesData[rndIndex];

            if (data.IsAbilityEnemyApplied(abilityData.Type))
                CharacterAbilities.Add(gameManager.PlayerCharacter, abilityData.Type);
            else
                CharacterAbilities.Add(this, abilityData.Type);

            StepCompleteEvent();
        }
    }
}
