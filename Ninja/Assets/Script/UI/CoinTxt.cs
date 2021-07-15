using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinTxt : MonoBehaviour
{
    public TMP_Text coinTxt;
    private void Start()
    {
        coinTxt.SetText("" + GameDataManager.Instance.gameDataScrObj.totalCoin);
    }
}
