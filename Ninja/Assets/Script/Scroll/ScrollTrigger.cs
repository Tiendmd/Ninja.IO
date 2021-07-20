using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScrollTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        DOTween.Kill(transform.parent);
        Destroy(transform.parent.gameObject);
    }
}
