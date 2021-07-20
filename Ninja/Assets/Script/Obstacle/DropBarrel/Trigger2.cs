using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Trigger2 : MonoBehaviour
{
    public AllPatrol allPatrol;
    public InstanceBarrel instanceBarrel;

    private void OnTriggerEnter(Collider other)
    {
        DOTween.Kill(allPatrol.transform.gameObject);
        allPatrol.enabled = false;
        instanceBarrel.enabled = false;
        instanceBarrel.a = 0;
    }
}
