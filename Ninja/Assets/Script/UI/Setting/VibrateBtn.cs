using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VibrateBtn : MonoBehaviour
{
    public Sprite defaultSprite1;
    public Sprite spriteMute;
    private bool check = true;

    public void Click()
    {
        GameDataManager.Instance.SetVibrate();
        if (check)
        {
            transform.GetComponent<Image>().sprite = spriteMute;
            check = false;
        }
        else if (!check)
        {
            transform.GetComponent<Image>().sprite = defaultSprite1;
            check = true;
        }
    }
}
