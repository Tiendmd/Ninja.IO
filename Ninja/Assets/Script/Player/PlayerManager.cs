using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    public bool canMove { get; set; }
    public bool canSlide;
    public GameObject skin1;
    public GameObject skin2;
    public GameObject particle;
    public CameraFollow myCamera;
    public bool isSkin1;
    public bool isSkin2;
    private NavMeshAgent agent;

    public Vector3 checkPointPosition;

    private Rigidbody rb;
    public Animator animator { get; set; }
    public RigidbodyConstraints constraint1 = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    private void Start()
    {
        if (transform.tag == "Enemy")
        {
            agent = GetComponent<NavMeshAgent>();
        }
        canMove = true;
        isSkin1 = true;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        checkPointPosition = new Vector3(0, transform.position.y, 0);
    }

    private void Update()
    {
    }

    public void ResetPositionToCheckPoint()
    {
        StartCoroutine(Delay(1,"die"));
    }

    public void PlayerFall()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        StartCoroutine(Delay(1, "fall"));

    }

    public void PlayerKick(Vector3 kickDirection)
    {
        rb.constraints = RigidbodyConstraints.None;
        StartCoroutine(Delay(1, "die"));
        rb.AddForce(kickDirection, ForceMode.Impulse);
    }

    IEnumerator Delay(float delay, string a)
    {
        canMove = false;
        rb.velocity = Vector3.zero;
        transform.GetComponent<PlayerInput>().enabled = false;
        transform.GetComponent<PlayerMovement>().enabled = false;
        animator.SetBool("run", false);
        animator.SetTrigger(a);
        myCamera.player = null;
        Collider[] b = transform.GetComponentsInChildren<CapsuleCollider>();
        for (int i = 0; i < b.Length; i++)
        {
            b[i].enabled = false;
        }
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("idle");
        for (int i = 0; i < b.Length; i++)
        {
            b[i].enabled = true;
        }
        rb.constraints = constraint1;
        myCamera.player = transform;
        myCamera.transform.position = checkPointPosition + myCamera.offset;
        transform.position = checkPointPosition;
        transform.GetComponent<PlayerInput>().enabled = true;
        transform.GetComponent<PlayerMovement>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        canMove = true;

    }

    IEnumerator EnemyDelay(float delay, string a)
    {
        StopCoroutine(GetComponent<EnemyMovement>().Delay(0));
        agent.velocity = Vector3.zero;
        agent.speed = 0;
        agent.enabled = false;
        transform.GetComponent<EnemyMovement>().enabled = false;
        animator.SetBool("run", false);
        animator.SetTrigger(a);
        Collider[] b = transform.GetComponentsInChildren<CapsuleCollider>();
        for (int i = 0; i < b.Length; i++)
        {
            b[i].enabled = false;
        }
        // sau delay giay thi sinh ra cho moi
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < b.Length; i++)
        {
            b[i].enabled = true;
        }
        animator.SetTrigger("idle");
        rb.constraints = constraint1;
        transform.position = checkPointPosition;
        // sau 0.5s idle thi sang run
        yield return new WaitForSeconds(0.5f);
        agent.speed = GetComponent<EnemyMovement>().rbSpeed;
        agent.velocity = new Vector3(0, 0, GetComponent<EnemyMovement>().rbSpeed);
        animator.SetBool("run", true);
        transform.GetComponent<EnemyMovement>().enabled = true;
    }

    public void ResetEnemyToCheckPoint()
    {
        StartCoroutine(EnemyDelay(1, "die"));
    }

    public void EnemyFall()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        StartCoroutine(EnemyDelay(1, "fall"));
    }
}
