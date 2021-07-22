using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Placement")]
    public static PlayerData Instance;
    public int place;
    public int coinEarnThisRun;

    private void Awake()
    {
            Instance = this;
    }
    
    public void CoinEarnThisRun(int a)
    {
        if (a==1)
        {
            coinEarnThisRun = 700;
        }
        else if (a==2)
        {
            coinEarnThisRun = 500;

        }
        else if (a==3)
        {
            coinEarnThisRun = 300;

        }
    }
}
