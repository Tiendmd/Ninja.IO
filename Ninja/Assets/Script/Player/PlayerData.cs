using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Placement")]
    public static PlayerData Instance;
    public int place;
    public int coinEarn;

    private void Awake()
    {
            Instance = this;
    }

    public void CoinEarnProcess(int a)
    {
        if (a == 1)
        {
            coinEarn = 700;
        }
        else if (a == 2)
        {
            coinEarn = 500;
        }
        else if (a == 3)
        {
            coinEarn = 300;
        }
        else
        {
            coinEarn = a;
        }
    }
}
