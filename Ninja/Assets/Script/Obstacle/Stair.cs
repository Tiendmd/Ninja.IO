using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
