using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChild : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (transform.tag != "Swing")
        {
            if (collision.transform.tag == "Player")
            {
                collision.transform.GetComponentInParent<PlayerManager>().ResetPositionToCheckPoint();
            }
            else if (collision.transform.tag == "Enemy")
            {
                collision.transform.GetComponentInParent<PlayerManager>().ResetEnemyToCheckPoint();
            }
        }
        else if (transform.tag == "Swing")
        {
            float kickDirectionX = collision.transform.position.x - transform.position.x;
            Vector3 kickDirection = new Vector3(kickDirectionX, 0, 0).normalized;
            if (collision.transform.tag == "Player" )
            {
                collision.transform.GetComponentInParent<PlayerManager>().PlayerKick(kickDirection * 10);
            }
            else if (collision.transform.tag == "Enemy" )
            {
                collision.transform.GetComponentInParent<PlayerManager>().PlayerKick(kickDirection * 10);
            }
        }
    }
}
