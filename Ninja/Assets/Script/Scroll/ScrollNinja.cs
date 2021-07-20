using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollNinja : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(RotateRepeat());
        StartCoroutine(RotateRepeat2());
    }

    public IEnumerator RotateRepeat()
    {
        Tween a = transform.DORotate(new Vector3(0, -180, 0), 1.5f).SetEase(Ease.Linear);
        yield return a.WaitForCompletion();
        Tween b = transform.DORotate(new Vector3(0, -360, 0), 1.5f).SetEase(Ease.Linear);
        yield return b.WaitForCompletion();
        StartCoroutine(RotateRepeat());
    }

    public IEnumerator RotateRepeat2()
    {
        Tween a = transform.DOMoveY(transform.position.y + 0.5f, 1.5f).SetEase(Ease.Linear);
        yield return a.WaitForCompletion();
        Tween b = transform.DOMoveY(transform.position.y - 0.5f, 1.5f).SetEase(Ease.Linear);
        yield return b.WaitForCompletion();
        StartCoroutine(RotateRepeat2());
    }
}
