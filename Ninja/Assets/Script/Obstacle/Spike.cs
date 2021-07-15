using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponentInParent<PlayerMovement>().moveSpeed = other.GetComponentInParent<PlayerMovement>().slowSpeed;
        }
        else if (other.transform.tag == "Enemy")
        {
            other.GetComponentInParent<EnemyMovement>().rbSpeed = 1;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponentInParent<PlayerMovement>().moveSpeed = other.GetComponentInParent<PlayerMovement>().originMoveSpeed;
        }
        else if (other.transform.tag == "Enemy")
        {
            other.GetComponentInParent<EnemyMovement>().rbSpeed = other.GetComponentInParent<EnemyMovement>().rbSpeedOrigin;
        }
    }
}
