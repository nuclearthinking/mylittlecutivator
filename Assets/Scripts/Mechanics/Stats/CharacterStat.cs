using System;
using System.Collections.Generic;

namespace Mechanics.Stats
{
    public class CharacterStats
    {
        public float baseValue;

        public float value
        {
            get
            {
                if (isDirty)
                {
                    lastValue = CalculateFinalValue();
                    isDirty = false;
                }

                return lastValue;
            }
        }

        private bool isDirty = true;
        private float lastValue;
        private float percentSum;
        
        private readonly List<StatModifier> statModifiers;

        public CharacterStats(float baseValue)
        {
            this.baseValue = baseValue;
            statModifiers = new List<StatModifier>();
        }

        public void AddModifier(StatModifier modifier)
        {
            isDirty = true;
            statModifiers.Add(modifier);
            statModifiers.Sort(CompareModifiers);
        }

        int CompareModifiers(StatModifier a, StatModifier b)
        {
            if (a.order < b.order)
                return -1;
            return a.order > b.order ? 1 : 0;
        }

        public void RemoveModifier(StatModifier modifier)
        {
            isDirty = true;
            statModifiers.Remove(modifier);
        }

        protected float CalculateFinalValue()
        {
            float finalValue = baseValue;

            foreach (var modifier in statModifiers)
            {
                if (modifier.type == StatModifierType.Float)
                {
                    finalValue += modifier.value;
                }
                else if (modifier.type == StatModifierType.Percent)
                {
                    percentSum += modifier.value;
                }
            }
            finalValue *= 1 + percentSum;
            return (float) Math.Round(finalValue, 4);
        }
    }
}