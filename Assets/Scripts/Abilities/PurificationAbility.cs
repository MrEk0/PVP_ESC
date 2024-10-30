using System.Linq;
using Enums;

namespace Abilities
{
    public class PurificationAbility : Ability
    {
        public override void Apply()
        {
            if (Owner.CharacterAbilities.ApplyAbilities.Any(o => o == AbilityTypes.FireBallAbility))
            {
                Owner.CharacterAbilities.Remove(AbilityTypes.FireBallAbility);
            }
        }
    }
}
