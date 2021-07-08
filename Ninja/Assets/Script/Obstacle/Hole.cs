using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{

    private void Update()
    {
        //CastHole();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, radius);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponentInParent<PlayerManager>().PlayerFall();
        }
        else if (other.transform.tag == "Enemy")
        {
            other.GetComponentInParent<PlayerManager>().EnemyFall();
        }
    }
}
