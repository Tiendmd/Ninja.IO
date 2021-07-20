using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public GameDataScrObj gameDataScrObj;

    private void Awake()
    {
        gameDataScrObj = Resources.Load("Data") as GameDataScrObj;
        Instance = this;

    }

    public bool CheckFirstTimePlay()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            return false;
        }
        return true;
    }

    public void SetLevel()
    {
        gameDataScrObj.level++;
    }

    public void SetCoin(int a)
    {
        gameDataScrObj.totalCoin = a;
    }

    public void SaveGameData()
    {
        //PlayerPrefs.SetInt("level", gameDataScrObj.level);
        //PlayerPrefs.SetInt("coin", gameDataScrObj.totalCoin);
    }

    //public int LoadLevelData()
    //{
    //    return PlayerPrefs.GetInt("level");
    //}

    //public int LoadCoinData()
    //{
    //    return PlayerPrefs.GetInt("coin");
    //}
}
