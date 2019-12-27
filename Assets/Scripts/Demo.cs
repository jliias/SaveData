/* 
 * This class demonstrates how to load and save game date using
 * GameData class. 
 * Author: Juha Liias, WestSloth Games (Dec 2019)
 */

using UnityEngine;
using UnityEngine.UI;
using MyGameData;

public class Demo : MonoBehaviour
{
    // Variables to save ads status, coins and highscore
    public bool adsEnabled;
    public int coinsCollected;
    public int highScore;

    // UI elements for showing/modifying status of ads, coins and highscore
    private Toggle adToggle;
    private Slider coinSlider;
    private Slider scoreSlider;

    // Find all needed UI objects before anything else is done
    private void Awake()
    {
        adToggle = GameObject.Find("ToggleAds").GetComponent<Toggle>();
        coinSlider = GameObject.Find("SliderCoins").GetComponent<Slider>();
        scoreSlider = GameObject.Find("SliderScore").GetComponent<Slider>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        LoadData();
    }

    // This is called by UI toggle element
    public void SetAdsEnabled()
    {
        // get value from UI toggle (no UI text to update)
        adsEnabled = adToggle.isOn;
        PrintStatus();
    }

    // This is called by coins slider UI element
    public void SetCoinsCollected()
    {
        // get value from UI slider and update corresponding UI text
        coinsCollected = (int)coinSlider.value;
        coinSlider.GetComponentInChildren<Text>().text = "" + coinsCollected;
        PrintStatus();
    }

    // This is called by highscore slider UI element
    public void SetHighScore()
    {
        // get value from UI slider and update corresponding UI text
        highScore = (int)scoreSlider.value;
        scoreSlider.GetComponentInChildren<Text>().text = "" + highScore;
        PrintStatus();
    }

    // This is called by Save Data UI button
    public void SaveData()
    {
        // Create new GameData and set values
        GameData newGameData = new GameData();
        newGameData.adsEnabled = adsEnabled;
        newGameData.coinsCollected = coinsCollected;
        newGameData.highScore = highScore;

        // Save data to PlayerPrefs
        GameData.SaveData(newGameData);
    }

    // This is called either in Start() or from Load Data UI button
    public void LoadData()
    {
        // Load game data using GameData class
        GameData myGameData = GameData.LoadData();
        Debug.Log(myGameData);

        if (myGameData == null)
        // If loaded game data is null, set default values
        {
            adsEnabled = false;
            coinsCollected = 5;
            highScore = 10;
        }
        else
        // Otherwise set local variables according to loaded data
        {
            adsEnabled = myGameData.adsEnabled;
            coinsCollected = myGameData.coinsCollected;
            highScore = myGameData.highScore;
        }

        // Update toggle and sliders to current state
        adToggle.isOn = adsEnabled;
        coinSlider.value = coinsCollected;
        scoreSlider.value = highScore;
    }

    // This is called from UI Reset button
    public void ResetData()
    {
        // Set all values to default
        adsEnabled = false;
        coinsCollected = 0;
        highScore = 0;

        // Update toggle and sliders to current state
        // This does not save status automatically!
        adToggle.isOn = adsEnabled;
        coinSlider.value = coinsCollected;
        scoreSlider.value = highScore;
    }

    // For debugging purposes: print variables status
    private void PrintStatus()
    {
        Debug.Log("AdsEnabled: " + adsEnabled + ", coins: " + coinsCollected + ", highscore: " + highScore);
    }
}