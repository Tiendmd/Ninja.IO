using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject panel;
    public GameObject settingBtn;
    public GameObject dragTxt;
    public GameObject shopButton;
    public GameObject coinGroup;
    public GameObject nextLevelBtn;

    public GameObject player;
    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        settingBtn.gameObject.SetActive(true);
        shopButton.gameObject.SetActive(true);
        panel.gameObject.SetActive(true);
        coinGroup.gameObject.SetActive(true);
        nextLevelBtn.SetActive(false);

    }
    public void ShopBtnClick()
    {
        shopButton.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
    }

    public void NextLevelBtnClick()
    {
        DOTween.KillAll();
        int a = player.GetComponent<PlayerManager>().CoinGain(player.GetComponent<PlayerManager>().place);
        GameData.Instance.UpdateAllStats(1, a);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void StartGame()
    {
        settingBtn.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        coinGroup.gameObject.SetActive(false);
        dragTxt.SetActive(false);
    }

    public void FinishRun()
    {
        nextLevelBtn.SetActive(true);

    }
}
