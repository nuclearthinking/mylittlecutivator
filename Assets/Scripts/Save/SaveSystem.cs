using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Core;
using Mechanics.Inventory;
using Model;
using UnityEngine;

namespace Save
{
    public static class SaveSystem
    {
        public static string GetSaveFilPath()
        {
            return Application.persistentDataPath + "/save.dat";
        }
        
        public static void Save(SaveData saveData)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = GetSaveFilPath();
            FileStream stream = new FileStream(path, FileMode.Create);
            
            formatter.Serialize(stream, saveData);
            stream.Close();
        }

        public static SaveData Load()
        {
            string path = GetSaveFilPath();
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                SaveData saveData = (SaveData) formatter.Deserialize(stream);
                stream.Close();
                return saveData;
            }
            else
            {
                Debug.LogError($"Cant load save file {path}");
                return null;
            }

        }

    }
}