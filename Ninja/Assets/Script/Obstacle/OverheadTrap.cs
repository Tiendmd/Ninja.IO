using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OverheadTrap : MonoBehaviour
{
    private float defaultY;
    public float delay;
    private void Start()
    {
        defaultY = transform.position.y;
        StartCoroutine(Repeat());
    }

    IEnumerator Repeat()
    {
        Tween a = transform.DOMoveY(0.5f, 0.25f).SetEase(Ease.Linear);
        yield return a.WaitForCompletion();
        Tween b = transform.DOMoveY(defaultY, 2).SetEase(Ease.Linear);
        yield return b.WaitForCompletion();
        yield return new WaitForSeconds(delay);
        StartCoroutine(Repeat());
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
