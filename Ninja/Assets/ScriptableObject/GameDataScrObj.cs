using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data")]
public class GameDataScrObj : ScriptableObject
{
    public int keys;
    public int level;
    public int totalCoin;
    public GameObject skin1;
    public GameObject skin2;

    public List<GameObject> listSkin1 = new List<GameObject>();
    public List<GameObject> listSkin2 = new List<GameObject>();


    public bool vibrateOn;
    public bool musicOn;
    //public List<MeshRenderer> listSkin2_1 = new List<MeshRenderer>();
    //public List<MeshFilter> listSkin2_2 = new List<MeshFilter>();
}
