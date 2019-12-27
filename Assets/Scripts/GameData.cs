using UnityEngine;
using System;

namespace MyGameData
{
    /* 
     * This class contains game variables that are saved in JSON format
     * Class is serializable so it can be serialized to JSON string and
     * store in playerprefs. 
     */

    [Serializable]
    public class GameData
    {
        public const string StorageKey = "Game_SaveData";

        // Game save data. You can add here whatever you want. 
        public bool adsEnabled;
        public int coinsCollected;
        public int highScore;

        // To JSON string.
        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }

        // Convert this object to JSON and store to PlayerPrefs with the provided key.
        public void Save(string key)
        {
            PlayerPrefs.SetString(key, ToString());
        }

        // Save data to PlayerPrefs as JSON with storage key.
        public static void SaveData(GameData gameData)
        {
            if (gameData != null)
            {
                Debug.Log("Saving data to PlayerPrefs...");
                Debug.Log(JsonUtility.ToJson(gameData));
                gameData.Save(StorageKey);
            }
        }

        // Load data from PlayerPrefs. 
        // Returns null if nothing stored.
        public static GameData LoadData()
        {
            string json = PlayerPrefs.GetString(StorageKey, null);

            if (!string.IsNullOrEmpty(json))
            {
                Debug.Log("Loading data from PlayerPrefs...");
                return JsonUtility.FromJson<GameData>(json);
            }
            else
                return null;
        }


    }
}