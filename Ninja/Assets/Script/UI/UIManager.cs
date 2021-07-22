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
    public GameObject exitBtn;
    public GameObject settingBtn;
    public GameObject dragTxt;
    public GameObject shopButton;
    public GameObject coinGroup;
    public GameObject countDown;
    public GameObject sliders;

    [Header("Finished")]
    public RectTransform nextLevelBtn;
    public GameObject backGrounds;
    public Transform adsBtn;
    public RectTransform levelCompleteTxt;
    public RectTransform levelCompleteImg;
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
        nextLevelBtn.gameObject.SetActive(false);
        sliders.SetActive(false);
        StartMenu();
    }

    public void StartMenu()
    {
        shopButton.GetComponent<RectTransform>().DOAnchorPosX(-90, 0.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        Sequence a = DOTween.Sequence();
        a.Append(dragTxt.GetComponentInChildren<TMP_Text>().DOFade(1, 1))
            .Append(dragTxt.GetComponentInChildren<TMP_Text>().DOFade(1, 0.5f))
            .Append(dragTxt.GetComponentInChildren<TMP_Text>().DOFade(0, 1)).SetLoops(-1, LoopType.Yoyo);        
        Sequence b = DOTween.Sequence();
        b.Append(dragTxt.GetComponentInChildren<Image>().DOFade(1, 1))
            .Append(dragTxt.GetComponentInChildren<Image>().DOFade(1, 0.5f))
            .Append(dragTxt.GetComponentInChildren<Image>().DOFade(0, 1)).SetLoops(-1, LoopType.Yoyo);
    }
    public void ShopBtnClick()
    {
        //shopButton.gameObject.SetActive(false);
        //panel.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        settingBtn.gameObject.SetActive(false);
        exitBtn.gameObject.SetActive(false);
        shopButton.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        coinGroup.gameObject.SetActive(false);
        dragTxt.SetActive(false);
        StartCoroutine(CountDown());
    }

    public IEnumerator LevelComplete()
    {
        backGrounds.SetActive(true);
        Tween a = levelCompleteImg.DOAnchorPosX(0, 0.5f, true).SetEase(Ease.Linear);
        yield return a.WaitForCompletion();
        Tween b = levelCompleteTxt.DOAnchorPosY(15, 1, true).SetEase(Ease.OutBounce);
        yield return b.WaitForCompletion();
        yield return new WaitForSeconds(0.25f);
        SetNextLevelTxt();
        adsBtn.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        adsBtn.DOScale(new Vector3(8, 12, 0), 0.75f).SetEase(Ease.OutCubic).SetLoops(-1, LoopType.Yoyo);
        yield return new WaitForSeconds(3);
        nextLevelBtn.gameObject.SetActive(true);
        Tween d = nextLevelBtn.GetComponentInChildren<Text>().DOFade(1, 1).SetEase(Ease.Linear);
    }

    public void SetNextLevelTxt()
    {
        adsBtn.GetComponentInChildren<Text>().text = PlayerData.Instance.coinEarnThisRun * 5 + "";
        nextLevelBtn.GetComponentInChildren<Text>().text = PlayerData.Instance.coinEarnThisRun + " is enough !";
    }

    public IEnumerator CountDown()
    {
        sliders.SetActive(true);

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
