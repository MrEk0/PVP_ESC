namespace Abilities
{
    public class BarrierAbility : Ability
    {
        private readonly int _blockDamage;
        
        public BarrierAbility(int blockDamage)
        {
            _blockDamage = blockDamage;
        }
        
        public override void Apply()
        {
            Owner.Hp.SetBlock(_blockDamage);
        }

        public override void Remove()
        {
            Owner.Hp.RemoveBlock();
        }
    }
}
