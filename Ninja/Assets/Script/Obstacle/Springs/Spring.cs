using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spring : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerMovement>().Jump();
        }
        else if (other.transform.CompareTag("Enemy"))
        {
            other.GetComponentInParent<EnemyMovement>().Jump();

        }
    }
}
