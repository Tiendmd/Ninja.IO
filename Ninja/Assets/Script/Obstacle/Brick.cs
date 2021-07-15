using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private BoxCollider boxCollider;
    private Rigidbody rb;
    private bool check = true;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" && check)
        {
            rb.AddForce((transform.position - collision.transform.position) * 20, ForceMode.Impulse);
            Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), collision.transform.GetComponentInChildren<CapsuleCollider>());
            check = false;
        }
        //else if (collision.transform.tag == "Player" && !check)
        //{
        //    rb.AddForce((transform.position - collision.transform.position) * 10, ForceMode.Impulse);
        //}

        else if (collision.transform.CompareTag("Enemy") && check)
        {
            rb.AddForce((transform.position - collision.transform.position) * 20, ForceMode.Impulse);
            Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), collision.transform.GetComponentInChildren<CapsuleCollider>());
            check = false;
        }
        //else if (collision.transform.CompareTag("Enemy") && !check)
        //{
        //    rb.AddForce((transform.position - collision.transform.position) * 10, ForceMode.Impulse);

        //}
    }

}
