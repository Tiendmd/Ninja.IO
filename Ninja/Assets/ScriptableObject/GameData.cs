using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data")]
public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public int level = 1;
    public SkinnedMeshRenderer playerSkin1;
    public MeshRenderer playerSkin2MeshRenderer;
    public MeshFilter playerSkin2MeshFilter;
    public int coin = 0;
    public int skin1 = 0;
    public int skin2_1 = 0;
    public int skin2_2 = 0;
    public List<SkinnedMeshRenderer> listSkin1 = new List<SkinnedMeshRenderer>();
    public List<MeshRenderer> listSkin2_1 = new List<MeshRenderer>();
    public List<MeshFilter> listSkin2_2 = new List<MeshFilter>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateAllStats(int increaseLevel, int coinGathered)
    {
        level += increaseLevel;
        coin += coinGathered;
    }

    public void ChangeSkin1(int a)
    {
        skin1 = a;
    }

    public void ChangeSkin2(int a)
    {
        skin2_1 = a;
        skin2_2 = a;
    }

    public void SetAllStats()
    {
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("coin", coin);
    }
}
