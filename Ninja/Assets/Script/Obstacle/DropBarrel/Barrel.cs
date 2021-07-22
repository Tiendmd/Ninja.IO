using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private Rigidbody rb;
    public float force;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(-Vector3.forward * force, ForceMode.VelocityChange);
    }


    private void OnCollisionEnter(Collision collision)
    {

        Vector3 target = collision.transform.position;
        Vector3 dir = target - new Vector3(target.x, transform.position.y - transform.localScale.y / 2, transform.position.y);
        dir.Normalize();
        if (collision.transform.CompareTag("Player"))
        {
            int a = Random.Range(0, 10);
            if (a < 5)
            {
                transform.GetComponent<Rigidbody>().AddForce((Vector3.right + Vector3.forward) * 7, ForceMode.VelocityChange);
            }
            else
            {
                transform.GetComponent<Rigidbody>().AddForce((Vector3.left + Vector3.forward) * 7, ForceMode.VelocityChange);
            }
            StartCoroutine(collision.transform.GetComponentInParent<PlayerMovement>().PushBack(dir * 5));
        }
        else if (collision.transform.CompareTag("Enemy"))
        {
            int a = Random.Range(0, 10);
            if (a < 5)
            {
                transform.GetComponent<Rigidbody>().AddForce((Vector3.right + Vector3.forward) * 7, ForceMode.VelocityChange);
            }
            else
            {
                transform.GetComponent<Rigidbody>().AddForce((Vector3.left + Vector3.forward) * 7, ForceMode.VelocityChange);
            }
            StartCoroutine(collision.transform.GetComponentInParent<EnemyMovement>().PushBack(dir * 5));
        }
    }
}
