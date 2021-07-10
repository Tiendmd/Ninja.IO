using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour
{
    public float x1;
    public float x2;
    public float timeBetweenX;
    private int c = 0;

    private void Start()
    {

    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, new Vector3(x1, transform.position.y, transform.position.z)) <= 0.1f && c == 0)
        {
            transform.DOMoveX(x2, timeBetweenX).SetEase(Ease.Linear);
            c = 1;
        }
        else if (Vector3.Distance(transform.position, new Vector3(x2, transform.position.y, transform.position.z)) <= 0.1f && c == 1)
        {
            transform.DOMoveX(x1, timeBetweenX).SetEase(Ease.Linear);
            c = 0;
        }
    }
}
