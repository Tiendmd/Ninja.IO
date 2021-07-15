using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTxt : MonoBehaviour
{
    public TMP_Text levelTxt;

    private void Start()
    {
        levelTxt.SetText("Level " + GameDataManager.Instance.gameDataScrObj.level);
    }

}
