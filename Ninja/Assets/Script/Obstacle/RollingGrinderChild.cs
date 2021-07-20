using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingGrinderChild : MonoBehaviour
{

    private RollingGrinderParent rollingGrinderParent;

    private void Start()
    {
        rollingGrinderParent = transform.parent.GetComponentInChildren<RollingGrinderParent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")|| other.transform.CompareTag("Enemy"))
        {
            rollingGrinderParent.check = true;
        }
    }
}
