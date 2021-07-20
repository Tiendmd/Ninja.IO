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
        StartCoroutine(Patrol());
    }
    public IEnumerator Patrol()
    {
        Tween a = transform.DOMoveX(X1, patrolTime).SetEase(Ease.Linear);
        yield return a.WaitForCompletion();
        Tween b = transform.DOMoveX(X2, patrolTime).SetEase(Ease.Linear);
        yield return b.WaitForCompletion();
        StartCoroutine(Patrol());
    }
}
