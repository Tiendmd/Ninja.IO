using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicBtn : MonoBehaviour
{
    public Sprite defaultSprite1;
    public Sprite spriteMute;
    private bool check = true;

    public void Click()
    {
        GameDataManager.Instance.SetMusic();
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
