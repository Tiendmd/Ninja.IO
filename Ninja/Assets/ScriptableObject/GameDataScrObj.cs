using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data")]
public class GameDataScrObj : ScriptableObject
{

    public int level;
    public int totalCoin;

    // chua lam skin !!!!
    public List<SkinnedMeshRenderer> listSkin1 = new List<SkinnedMeshRenderer>();
    public List<MeshRenderer> listSkin2_1 = new List<MeshRenderer>();
    public List<MeshFilter> listSkin2_2 = new List<MeshFilter>();

}
