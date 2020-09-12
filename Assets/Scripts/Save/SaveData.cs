namespace Save
{    
    [System.Serializable]
    public class SaveData
    {
        // PLAYER
        public int level;
        public int currentExp;
        public int currentHealth;
        
        
        //INVENTORY
        public int[] inventory;

        //EQUIPMENT
        public int[] equipment;
    }
}