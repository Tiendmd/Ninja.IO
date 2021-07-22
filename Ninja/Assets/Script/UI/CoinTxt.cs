using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinTxt : MonoBehaviour
{
    public Text coinTxt;
    private void Start()
    {
        coinTxt.text = "" + GameDataManager.Instance.gameDataScrObj.totalCoin;
    }
}
