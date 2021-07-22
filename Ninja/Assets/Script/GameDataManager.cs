using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    public GameDataScrObj gameDataScrObj;

    private void Awake()
    {
        Instance = this;
        gameDataScrObj = Resources.Load("data") as GameDataScrObj;
        LoadGameData();
    }

    public bool CheckFirstTimePlay()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            return false;
        }
        return true;
    }

    public void SetKey()
    {
        gameDataScrObj.keys++;
    }

    public bool SetMusic()
    {
        return !gameDataScrObj.musicOn;
    }

    public bool SetVibrate()
    {
        return !gameDataScrObj.vibrateOn;
    }

    public void SetLevel()
    {
        gameDataScrObj.level++;
    }

    public void SetCoin(int a)
    {
        if (a==1)
        {
            gameDataScrObj.totalCoin += 700;
        }
        else if (a == 2)
        {
            gameDataScrObj.totalCoin += 500;
        }
        else if (a == 3)
        {
            gameDataScrObj.totalCoin += 300;
        }
        else
        {
            gameDataScrObj.totalCoin += a;
        }
    }

    public void SaveGameData()
    {
        SetCoin(PlayerData.Instance.coinEarnThisRun);
        SetLevel();

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "player.data";
        FileStream stream = new FileStream(path, FileMode.Create);
        var json = JsonUtility.ToJson(gameDataScrObj);
        formatter.Serialize(stream, json);
        stream.Close();
    }

    public void LoadGameData()
    {
        string path = Application.persistentDataPath + "player.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            //GameDataScrObj gameDataScrObj = formatter.Deserialize(stream) as GameDataScrObj;
            JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(stream), gameDataScrObj);
            stream.Close();
            //return gameDataScrObj;
        }
        else
        {
            Debug.Log("Load Game Error");
            //return Resources.Load("Data") as GameDataScrObj;
        }
    }
}

public static class GameData
{
    public static int level;
    public static int totalCoin;
    public static GameObject skin1;
    public static GameObject skin2;
}