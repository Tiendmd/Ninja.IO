using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGrinder : MonoBehaviour
{
    public float speed;
    private float a;

    private void FixedUpdate()
    {
        a -= Time.deltaTime* speed;
        transform.localRotation = Quaternion.Euler(transform.localRotation.x, a, transform.localRotation.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.GetComponentInParent<PlayerManager>().ResetPositionToCheckPoint();
        }
        else if (other.transform.tag == "Enemy")
        {
            other.transform.GetComponentInParent<PlayerManager>().ResetEnemyToCheckPoint();
        }
    }
}
