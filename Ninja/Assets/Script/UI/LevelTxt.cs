using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTxt : MonoBehaviour
{
    public TMP_Text levelTxt;

    private void Start()
    {
        SetLevelText(GameData.Instance.level);
    }

    public void SetLevelText(int levelNumber)
    {
        levelTxt.SetText("Level " + levelNumber);
    }
}
