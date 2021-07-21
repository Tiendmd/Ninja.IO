using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollNinja : MonoBehaviour
{
    private void Start()
    {
        transform.DORotate(new Vector3(0, -360, 0), 3f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        transform.DOMoveY(transform.position.y + 0.5f, 1.5f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}
