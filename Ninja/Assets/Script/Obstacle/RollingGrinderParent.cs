using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingGrinderParent : MonoBehaviour
{
    private Rigidbody rb;
    public float force;
    private RollingGrinderChild rollingGrinderChild;
    public bool check { get; set; }
    private bool oneTime;
    private void Start()
    {
        oneTime = true; 
        rb = GetComponent<Rigidbody>();
        rollingGrinderChild = transform.parent.GetComponentInChildren<RollingGrinderChild>();
    }

    private void Update()
    {
        if (check)
        {
            rb.velocity = new Vector3(0, 0, Mathf.Clamp(rb.velocity.z, 0,5));
            rb.AddForce(-Vector3.forward * force, ForceMode.VelocityChange);
            if (oneTime)
            {
                StartCoroutine(Delay());
                oneTime = false;
            }
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        Destroy(transform.parent.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.GetComponentInParent<PlayerManager>().ResetPositionToCheckPoint();
            Collider[] list = collision.transform.GetComponentsInChildren<CapsuleCollider>();
            for (int i = 0; i < list.Length; i++)
            {
                Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), list[i]);
            }
        }
        else if (collision.transform.tag == "Enemy")
        {
            collision.transform.GetComponentInParent<PlayerManager>().ResetEnemyToCheckPoint();
            Collider[] list = collision.transform.GetComponentsInChildren<CapsuleCollider>();
            for (int i = 0; i < list.Length; i++)
            {
                Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), list[i]);
            }
        }

    }

}
