using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FasterSlower : MonoBehaviour
{
    public float force;

    private void OnTriggerStay(Collider other)
    {
        if (transform.CompareTag("Faster"))
        {
            if (other.transform.CompareTag("Player")|| other.transform.CompareTag("Enemy"))
            {
                Rigidbody rb = other.transform.GetComponentInParent<Rigidbody>();
                rb.AddForce(Vector3.forward * force, ForceMode.VelocityChange);
            }

        }
        else if (transform.CompareTag("Slower"))
        {
            if (other.transform.CompareTag("Player") || other.transform.CompareTag("Enemy"))
            {
                Rigidbody rb = other.transform.GetComponentInParent<Rigidbody>();
                rb.AddForce(-Vector3.forward * force, ForceMode.VelocityChange);
            }
        }
    }
}
