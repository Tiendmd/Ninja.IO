using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public void NextLevel()
    {
        UIManager.Instance.FinishRun();
    }
}
