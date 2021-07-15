using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject confettiParticle;
    private void OnTriggerEnter(Collider other)
    {
        MyScene.Instance.placeCount++;
        if (other.transform.tag == "Player")
        {
            other.GetComponentInParent<PlayerManager>().animator.SetTrigger("victory");
            other.GetComponentInParent<PlayerMovement>().rb.velocity = Vector3.zero;
            //other.GetComponentInParent<PlayerMovement>().rb.isKinematic = true;
            other.GetComponentInParent<PlayerMovement>().enabled = false;
            other.GetComponentInParent<PlayerInput>().enabled = false;
            Collider[] list = other.GetComponentsInChildren<CapsuleCollider>();
            for (int i = 0; i < list.Length; i++)
            {
                Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), list[i]);
            }
            StartCoroutine(Delay(transform.position - Vector3.right*2.5f, transform.position + Vector3.right * 2.5f));
            int a =  MyScene.Instance.placeCount;
            other.GetComponentInParent<PlayerManager>().place = a;
            GameDataManager.Instance.DoUpdateWhenFinishedRun(a);
            UIManager.Instance.FinishRun();
        }
        else if (other.transform.tag == "Enemy")
        {
            other.GetComponentInParent<EnemyMovement>().rb.velocity = Vector3.zero;
            other.GetComponentInParent<EnemyMovement>().rb.isKinematic = true;
            other.GetComponentInParent<EnemyMovement>().animator.SetTrigger("victory");
            //other.GetComponentInParent<EnemyMovement>().EnemyDoDemand();
            other.GetComponentInParent<EnemyMovement>().enabled = false;
            Collider[] list = other.GetComponentsInChildren<CapsuleCollider>();
            for (int i = 0; i < list.Length; i++)
            {
                Physics.IgnoreCollision(transform.GetComponent<BoxCollider>(), list[i]);
            }
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
