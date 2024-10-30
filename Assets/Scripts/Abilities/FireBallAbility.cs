namespace Abilities
{
    public class FireBallAbility : Ability
    {
        private readonly int _applyDamage;
        private readonly int _stepDamage;
        
        public FireBallAbility(int applyDamage, int stepDamage)
        {
            _applyDamage = applyDamage;
            _stepDamage = stepDamage;
        }
        
        public override void Apply()
        {
            Owner.Hp.TakeDamage(_applyDamage);
        }

        public override void MakeStep()
        {
            Owner.Hp.TakeDamage(_stepDamage);
        }
    }
}
