using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AllPatrol : MonoBehaviour
{
    public float X1;
    public float X2;
    public float patrolTime;

    private void Start()
    {
        Tween a = transform.DOMoveX(X1, patrolTime).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
    }
}
