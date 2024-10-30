namespace Abilities
{
    public class DamageAbility : Ability
    {
        private readonly int _damage; 
        
        public DamageAbility(int damage)
        {
            _damage = damage;
        }
        
        public override void Apply()
        {
            Owner.Hp.TakeDamage(_damage);
        }
    }
}
