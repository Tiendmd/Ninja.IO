using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CheckPoint : MonoBehaviour
{
    public GameObject teacher;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponentInParent<PlayerManager>().checkPointPosition = new Vector3(0, 0.5f, transform.position.z);

            //for (int i = 0; i < other.GetComponentsInChildren<CapsuleCollider>().Length; i++)
            //{
            //    Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), other.GetComponentsInChildren<CapsuleCollider>()[i]);
            //}
        }
        if (other.transform.tag == "Enemy")
        {
            other.GetComponentInParent<PlayerManager>().checkPointPosition = new Vector3(0, 0.5f, transform.position.z);

        }
        if (teacher != null)
        {
            DataManager.Instance.listOfTeacher.RemoveAt(0);
            teacher.GetComponent<TeacherAI>().KillTeacher();
        }
    }

}
