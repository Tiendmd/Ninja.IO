using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Swing2 : MonoBehaviour
{
    public Vector3 swingDestination1;
    public Vector3 swingDestination2;
    public float swingTime;
    int c = 0;

    private void Start()
    {
        swingDestination1 = transform.rotation.eulerAngles;
    }
    private void Update()
    {
        if (Vector3.Distance(transform.rotation.eulerAngles, swingDestination1) <= 0.1f && c == 0)
        {
            transform.DORotate(swingDestination2, swingTime).SetEase(Ease.InOutCubic);
            c = 1;
        }
        else if (Vector3.Distance(transform.rotation.eulerAngles, swingDestination2) <= 0.1f && c==1)
        {
            transform.DORotate(swingDestination1, swingTime).SetEase(Ease.InOutCubic);
            c = 0;
        }
    }
}
