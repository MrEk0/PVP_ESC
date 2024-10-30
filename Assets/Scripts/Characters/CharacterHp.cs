using System;
using Interfaces;

namespace Characters
{
    public class CharacterHp : IDamagable
    {
        public event Action DeathEvent = delegate { };
        public event Action UpdateEvent = delegate { };
        
        private float _hp;
        private float _blockDamage;
        private readonly float _maxHp;

        public float GetHpRate => _hp / _maxHp;

        public CharacterHp(float maxHp)
        {
            _hp = maxHp;
            _maxHp = maxHp;
        }

        public void SetBlock(float blockAmount)
        {
            _blockDamage = blockAmount;
        }

        public void RemoveBlock()
        {
            _blockDamage = 0f;
        }

        public void Heal(float hp)
        {
            _hp = MathF.Min(_hp + hp, _maxHp);
            
            UpdateEvent();
        }

        public void TakeDamage(float damageValue)
        {
            _blockDamage = MathF.Max(0f, _blockDamage - damageValue);
            
            _hp = MathF.Max(0f, _blockDamage > 0f ? _hp : _hp - damageValue);

            UpdateEvent();
            
            if (_hp <= 0f)
                DeathEvent();
        }
    }
}