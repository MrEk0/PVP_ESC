using Characters;

namespace Abilities
{
    public abstract class Ability
    {
        public Character Owner { get; set; }
        public int CurrentSteps { get; private set; }

        public abstract void Apply();

        public virtual void MakeStep()
        {
            CurrentSteps++;
        }

        public virtual void Remove() { }
    }
}
