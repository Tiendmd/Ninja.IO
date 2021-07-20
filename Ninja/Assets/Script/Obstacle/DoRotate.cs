using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoRotate : MonoBehaviour
{
    public float X1;
    public float X2;
    public float time;
    public float delayTime;

    private void Start()
    {
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        Tween a = transform.DOLocalRotate(Vector3.right * X1, time, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
        yield return a.WaitForCompletion();
        yield return new WaitForSeconds(delayTime);
        Tween c = transform.DOLocalRotate(Vector3.right * X2, time, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
        yield return c.WaitForCompletion();
        yield return new WaitForSeconds(delayTime);
        StartCoroutine(Rotate());
    }
}
