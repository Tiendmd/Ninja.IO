using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponentInParent<PlayerManager>().animator.SetTrigger("victory");
            other.GetComponentInParent<PlayerMovement>().rb.velocity = Vector3.zero;
            other.GetComponentInParent<PlayerMovement>().rb.isKinematic = true;
            other.GetComponentInParent<PlayerMovement>().enabled = false;
            other.GetComponentInParent<PlayerInput>().enabled = false;
            Collider[] list = other.GetComponentsInChildren<CapsuleCollider>();
            for (int i = 0; i < list.Length; i++)
            {
                Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), list[i]);
            }
        }
        else if (other.transform.tag == "Enemy")
        {
            //other.GetComponentInParent<EnemyMovement>().rb.velocity = Vector3.zero;
            //other.GetComponentInParent<EnemyMovement>().rb.isKinematic = true;
            //other.GetComponentInParent<EnemyMovement>().animator.SetTrigger("victory");
            //other.GetComponentInParent<EnemyMovement>().EnemyDoDemand();
            //other.GetComponentInParent<EnemyMovement>().enabled = false;
            //Collider[] list = other.GetComponentsInChildren<CapsuleCollider>();
            //for (int i = 0; i < list.Length; i++)
            //{
            //    Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), list[i]);
            //}


        }
    }
}
