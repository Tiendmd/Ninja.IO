using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public void NextLevel()
    {
        StartCoroutine(UIManager.Instance.LevelComplete());
    }
}
