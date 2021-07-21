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
        Sequence sequence = DOTween.Sequence();
        defaultY = transform.position.y;
        sequence.Append(transform.DOMoveY(0.5f, 0.25f).SetEase(Ease.Linear)).Append(transform.DOMoveY(defaultY, 2).SetEase(Ease.Linear))
            .Append(transform.DOMove(transform.position, 3)).SetLoops(-1, LoopType.Restart);
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
