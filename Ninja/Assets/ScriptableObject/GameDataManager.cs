using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public GameDataScrObj gameDataScrObj;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        int coin = PlayerPrefs.GetInt("coin");
    }

    public void UpdateLevel()
    {
        gameDataScrObj.level++;
    }

    public void UpdateCoin(int a)
    {
        if (a==1)
        {
            gameDataScrObj.totalCoin += 700;
        }
        else if (a==2)
        {
            gameDataScrObj.totalCoin += 500;
        }
        else if (a==3)
        {
            gameDataScrObj.totalCoin += 300;
        }
        else
        {
            gameDataScrObj.totalCoin += a;
        }
    }

    public void DoUpdateWhenFinishedRun(int coin)
    {
        UpdateLevel();
        UpdateCoin(coin);
    }

    public void SaveGameData()
    {
        PlayerPrefs.SetInt("level", gameDataScrObj.level);
        PlayerPrefs.SetInt("coin", gameDataScrObj.totalCoin);
    }
}
