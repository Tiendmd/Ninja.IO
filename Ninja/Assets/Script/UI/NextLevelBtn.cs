using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class NextLevelBtn : MonoBehaviour
{
    private TMP_Text nextLevelTMP;

    private void Start()
    {
        nextLevelTMP = GetComponentInChildren<TMP_Text>();
    }

    public void NextLevelBtnClick()
    {
        DOTween.KillAll();
        GameDataManager.Instance.SetLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void UpdateTMPText()
    {
        //nextLevelTMP.SetText()
    }
}
