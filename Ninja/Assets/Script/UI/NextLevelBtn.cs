using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class NextLevelBtn : MonoBehaviour
{
     public void NextLevelBtnClick()
    {
        DOTween.KillAll();
        GameDataManager.Instance.SaveGameData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
