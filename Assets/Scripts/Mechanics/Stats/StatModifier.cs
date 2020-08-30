namespace Mechanics.Stats
{
    public enum StatModifierType
    {
        Float,
        Percent,
    }
    public class StatModifier
    {
        public readonly float value;
        public readonly StatModifierType type;
        public readonly int order;

        public StatModifier(float value, StatModifierType type, int order)
        {
            this.value = value;
            this.type = type;
            this.order = order;
        }
        
        public StatModifier (float value, StatModifierType type): this (value, type, (int)type) {}
    }
}