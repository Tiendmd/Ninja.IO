using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stair : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (transform.CompareTag("StairUp"))
        {
            if (other.transform.tag == "Player")
            {
                other.GetComponentInParent<PlayerMovement>().rb.velocity = new Vector3(other.GetComponentInParent<PlayerMovement>().rb.velocity.x, 0, other.GetComponentInParent<PlayerMovement>().rb.velocity.z);
            }
            //if (other.transform.CompareTag("Enemy"))
            //{
            //    //other.transform.position = new Vector3(other.transform.position.x, 0.5f, other.transform.position.z);
            //    other.GetComponentInParent<EnemyMovement>().rb.velocity = new Vector3(other.GetComponentInParent<EnemyMovement>().rb.velocity.x, 0, other.GetComponentInParent<EnemyMovement>().rb.velocity.z);
            //}
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.CompareTag("StairDown"))
        {
            if (other.transform.tag == "Player")
            {
                other.GetComponentInParent<PlayerMovement>().rb.AddForce(new Vector3(0, -5, 0), ForceMode.Impulse);
            }
        }
        //else if (transform.CompareTag("StairDown2"))
        //{
        //    if (other.transform.CompareTag("Enemy"))
        //    {
        //        Debug.Log(1);
        //        other.transform.position = new Vector3(other.transform.position.x, 0.5f, other.transform.position.z);
        //        other.GetComponentInParent<NavMeshAgent>().velocity = new Vector3(other.GetComponentInParent<NavMeshAgent>().velocity.x, 0, other.GetComponentInParent<NavMeshAgent>().velocity.z);
        //    }
        //}
    }
}
