using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class CheckPoint : MonoBehaviour
{
    public GameObject teacher;
    public List<Transform> flags = new List<Transform>();
    public GameObject confettiParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponentInParent<PlayerManager>().checkPointPosition = new Vector3(other.transform.position.x, 0.5f, transform.position.z);
            StartCoroutine(Delay(flags[0].position, flags[1].position));
            for (int i = 0; i < flags.Count; i++)
            {
                float tempRotationY = flags[i].transform.localRotation.eulerAngles.y;
                flags[i].transform.DORotate(new Vector3(0, tempRotationY, 0), 0.5f);
            }
            for (int i = 0; i < other.GetComponentsInChildren<CapsuleCollider>().Length; i++)
            {
                Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), other.GetComponentsInChildren<CapsuleCollider>()[i]);
            }
        }
        if (other.transform.tag == "Enemy")
        {
            other.GetComponentInParent<PlayerManager>().checkPointPosition = new Vector3(other.transform.position.x, 0.5f, transform.position.z);
            for (int i = 0; i < other.GetComponentsInChildren<CapsuleCollider>().Length; i++)
            {
                Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), other.GetComponentsInChildren<CapsuleCollider>()[i]);
            }
        }
        if (teacher != null)
        {
            DataManager.Instance.listOfTeacher.RemoveAt(0);
            teacher.GetComponent<TeacherAI>().KillTeacher();
        }
    }


    IEnumerator Delay(Vector3 position1, Vector3 position2)
    {
        GameObject a = Instantiate(confettiParticle, position1, Quaternion.Euler(-90, 0, 0));
        GameObject b = Instantiate(confettiParticle, position2, Quaternion.Euler(-90, 0, 0));
        GameObject c = Instantiate(confettiParticle, transform.position, Quaternion.Euler(-90, 0, 0));
        yield return new WaitForSeconds(1);
        Destroy(a);
        Destroy(b);
        Destroy(c);
    }
}
