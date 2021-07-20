using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpring : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerMovement>().SuperJump();
        }
        else if (other.transform.CompareTag("Enemy"))
        {
            other.GetComponentInParent<EnemyMovement>().SuperJump();

        }
    }
}
