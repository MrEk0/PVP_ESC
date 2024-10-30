using System.Linq;
using Windows;
using Abilities;
using Characters;
using Configs;
using UnityEngine;

namespace Common
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameSettingsData _gameSettingsData;
        [SerializeField] private GameWindow _gameWindow;
        [SerializeField] private AbilityWindow _abilityWindow;
        [SerializeField] private CharacterHpView _playerHpView;
        [SerializeField] private CharacterHpView _enemyHpView;

        private bool _isPlayerStep = true;
        
        public EnemyCharacter EnemyCharacter { get; private set; }
        public PlayerCharacter PlayerCharacter { get; private set; }

        private void Start()
        {
            var serviceLocator = new ServiceLocator();
            serviceLocator.AddService(_abilityWindow);
            serviceLocator.AddService(_gameSettingsData);
            serviceLocator.AddService(this);

            PlayerCharacter = new PlayerCharacter(serviceLocator);
            PlayerCharacter.StepCompleteEvent += OnStepCompletedEvent;
            InitCharacter(PlayerCharacter, _playerHpView);

            EnemyCharacter = new EnemyCharacter(serviceLocator);
            EnemyCharacter.StepCompleteEvent += OnStepCompletedEvent;
            InitCharacter(EnemyCharacter, _enemyHpView);

            serviceLocator.AddService(PlayerCharacter);
            _abilityWindow.Init(serviceLocator);
            
            _gameWindow.RestartEvent += OnGameRestarted;

            MakeFirstStep();
        }

        private void OnDestroy()
        {
            PlayerCharacter.StepCompleteEvent -= OnStepCompletedEvent;
            EnemyCharacter.StepCompleteEvent -= OnStepCompletedEvent;
            _gameWindow.RestartEvent -= OnGameRestarted;
        }

        private void InitCharacter(Character character, CharacterHpView hpView)
        {
            var abilitiesData = _gameSettingsData.AvailableAbilities.Select(ability =>
                new CharacterAbilities.AbilityData<Ability>
                {
                    Ability = ability.Ability,
                    Sprite = ability.Sprite,
                    CurrentReloadSteps = 0,
                    MaxReloadSteps = ability.ReloadSteps,
                    DurationSteps = ability.DurationSteps,
                    Type = ability.Type
                }).ToList();
            
            character.CharacterAbilities.AddRange(abilitiesData);
            character.Hp.DeathEvent += OnGameRestarted;

            hpView.Init(character);
        }

        private void MakeFirstStep()
        {
            PlayerCharacter.Step();
        }

        private void OnGameRestarted()
        {
            PlayerCharacter.Restart();
            EnemyCharacter.Restart();

            MakeFirstStep();
        }

        private void OnStepCompletedEvent()
        {
            _isPlayerStep = !_isPlayerStep;
            
            if (_isPlayerStep)
                PlayerCharacter.Step();
            else
                EnemyCharacter.Step();
        }
    }
}
