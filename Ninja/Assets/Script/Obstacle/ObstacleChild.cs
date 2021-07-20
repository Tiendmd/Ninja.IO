using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChild : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (transform.tag != "Swing")
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
        else if (transform.tag == "Swing")
        {
            float kickDirectionX = other.transform.position.x - transform.position.x;
            Vector3 kickDirection = new Vector3(kickDirectionX*10, 1f, 0);
            if (other.transform.tag == "Player")
            {
                other.transform.GetComponentInParent<PlayerManager>().PlayerKick(kickDirection);
                for (int i = 0; i < transform.parent.childCount; i++)
                {
                    Physics.IgnoreCollision(transform.parent.GetChild(i).GetComponent<CapsuleCollider>(), other);
                }
            }
            else if (other.transform.CompareTag("Enemy"))
            {
                other.transform.GetComponentInParent<PlayerManager>().EnemyKick(kickDirection );
                for (int i = 0; i < transform.parent.childCount; i++)
                {
                    Physics.IgnoreCollision(transform.parent.GetChild(i).GetComponent<CapsuleCollider>(), other);
                }
            }
        }
    }
}
