namespace Abilities
{
    public class RegenerationAbility : Ability
    {
        private readonly int _healAmount;
        
        public RegenerationAbility(int healAmount)
        {
            _healAmount = healAmount;
        }
        
        public override void Apply()
        {
            Owner.Hp.Heal(_healAmount);
        }

        public override void MakeStep()
        {
            Owner.Hp.Heal(_healAmount);
        }
    }
}
