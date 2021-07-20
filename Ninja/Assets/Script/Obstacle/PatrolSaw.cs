using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PatrolSaw : MonoBehaviour
{
    public float X1;
    public float X2;
    public float patrolTime;
    private float a;
    public float speedSaw;

    private void Start()
    {
        X2 = -X1;
        StartCoroutine(Patrol());

    }
    public void FixedUpdate()
    {
        a -= Time.deltaTime*speedSaw;
        transform.localRotation = Quaternion.Euler(a, transform.localRotation.y, transform.localRotation.z);
    }

    IEnumerator Patrol()
    {
        Tween a = transform.DOMoveX(X1, patrolTime).SetEase(Ease.Linear);
        yield return a.WaitForCompletion();
        Tween b = transform.DOMoveX(X2, patrolTime).SetEase(Ease.Linear);
        yield return b.WaitForCompletion();
        StartCoroutine(Patrol());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.GetComponentInParent<PlayerManager>().ResetPositionToCheckPoint();
        }
        else if (other.transform.tag == "Enemy")
        {
            other.transform.GetComponentInParent<PlayerManager>().ResetEnemyToCheckPoint();
        }
    }
}
