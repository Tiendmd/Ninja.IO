using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject panel;
    public GameObject settingBtn;
    public GameObject dragTxt;
    public GameObject shopButton;
    public GameObject coinGroup;
    public GameObject nextLevelBtn;
    public GameObject countDown;
    
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

    public void StartGame()
    {
        settingBtn.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        coinGroup.gameObject.SetActive(false);
        dragTxt.SetActive(false);
        StartCoroutine(CountDown());
    }

    public void FinishRun()
    {
        nextLevelBtn.SetActive(true);
    }

    public IEnumerator CountDown()
    {
        countDown.transform.GetChild(0).gameObject.SetActive(true);
        Tween a = countDown.transform.GetChild(0).GetComponent<TMP_Text>().DOFade(0, 1);
        yield return a.WaitForCompletion();

        countDown.transform.GetChild(0).gameObject.SetActive(false);
        countDown.transform.GetChild(1).gameObject.SetActive(true);
        Tween b = countDown.transform.GetChild(1).GetComponent<TMP_Text>().DOFade(0, 1);

        yield return b.WaitForCompletion();
        countDown.transform.GetChild(1).gameObject.SetActive(false);
        countDown.transform.GetChild(2).gameObject.SetActive(true);
        Tween c = countDown.transform.GetChild(2).GetComponent<TMP_Text>().DOFade(0, 1);

        yield return c.WaitForCompletion();
        countDown.transform.GetChild(2).gameObject.SetActive(false);
        countDown.transform.GetChild(3).gameObject.SetActive(true);
        Tween d = countDown.transform.GetChild(3).GetComponent<TMP_Text>().DOFade(0, 1);

        yield return d.WaitForCompletion();
        countDown.transform.GetChild(3).gameObject.SetActive(false);
    }
}
