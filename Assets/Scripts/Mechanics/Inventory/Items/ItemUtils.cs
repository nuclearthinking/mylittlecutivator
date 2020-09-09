using Enums;
using UnityEngine;

namespace Mechanics.Inventory.Items
{
    public static class ItemUtils
    {
        public static Color ColorByQuality(ItemQuality quality)
        {
            switch (quality)
            {
                case ItemQuality.Epic:
                    return new Color(1, 0, 1, 1);
                case ItemQuality.Good:
                    return new Color(0,1,1,1);
                case ItemQuality.Junk:
                    return Color.gray;
                case ItemQuality.Normal:
                    return Color.white;
                case ItemQuality.Rare:
                    return Color.blue;
                case ItemQuality.Legendary:
                    return new Color(1, 0.15f,0,1);
                case ItemQuality.Unique:
                    return Color.green;
                default:
                    return Color.white;
            }
        }
    }
}