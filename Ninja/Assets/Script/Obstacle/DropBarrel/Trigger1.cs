using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger1 : MonoBehaviour
{
    public AllPatrol scriptMove;
    public InstanceBarrel scriptInstance;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player")|| other.transform.CompareTag("Enemy"))
        {
            scriptMove.enabled = true;
            scriptInstance.enabled = true;
        }

    }
}
