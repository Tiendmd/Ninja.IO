using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public List<Vector4> listOfDesination = new List<Vector4>();
    private NavMeshAgent agent;
    public Rigidbody rb { get; set; }
    public float rbSpeed;
    public Animator animator { get; set; }
    private bool stopStartRace = false;

    private EnemyManager enemyManager;
    private PlayerManager playerManager;
    public bool overrideSetDestination;
    private bool oneTime = true;

    private bool demandCase4;

    public float jumpForce;
    public GameObject child;
    public LayerMask layer;
    private bool checkJump;
    public bool pressing;

    [Header("CheckForward")]
    public LayerMask obstacleLayer;
    public float Y1Rotation;
    private float Y2Rotation;
    private bool rayCastOn;
    public float raycastLength;
    //public float degreePerRaycast;
    public int numerOfRay;
    private void Start()
    {
        //NavMesh.RemoveAllNavMeshData();
        overrideSetDestination = false;
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        enemyManager = GetComponent<EnemyManager>();
        animator = GetComponentInChildren<Animator>();
        //agent.SetDestination(new Vector3(listOfDesination[0].x,
        //               listOfDesination[0].y,
        //               listOfDesination[0].z));
        Y2Rotation = -Y1Rotation;
    }

    private void Update()
    {
        //if (DataManager.Instance.gameIsStart)
        //{
        //    if (!stopStartRace)
        //    {
        //        animator.SetBool("run", true);
        //        agent.velocity = new Vector3(0, 0, rbSpeed);
        //        if (oneTime)
        //        {
        //            StartCoroutine(StartRace());
        //        }
        //    }
        //    else if (stopStartRace)
        //    {
        //        if (enemyManager.isStupid && agent.enabled == true)
        //        {
        //            EnemyReachDestination();
        //        }
        //        else if (enemyManager.isSmart && agent.enabled == true)
        //        {
        //            agent.SetDestination(new Vector3(listOfDesination[listOfDesination.Count - 1].x,
        //                listOfDesination[listOfDesination.Count - 1].y,
        //                listOfDesination[listOfDesination.Count - 1].z));
        //        }
        //    }
        //}
        CheckGround();
    }

    private void FixedUpdate()
    {
        if (DataManager.Instance.gameIsStart)
        {
            //EnemyMovefoward();
            rb.velocity = new Vector3(0, rb.velocity.y, 1 * rbSpeed);

        }
    }

    #region NavMeshAgent
    public IEnumerator StartRace()
    {
        oneTime = false;
        yield return new WaitForSeconds(1);
        stopStartRace = true;
    }

    public void EnemyReachDestination()
    {
        if (listOfDesination.Count > 0)
        {
            if (Vector3.Distance(transform.position, new Vector3(listOfDesination[0].x, listOfDesination[0].y, listOfDesination[0].z)) <= 0.5f
    || transform.position.z >= listOfDesination[0].z && agent.enabled == true)
            {
                EnemyDoDemand();
                if (listOfDesination.Count > 1)
                {
                    listOfDesination.RemoveAt(0);
                }
            }
            if (demandCase4)
            {
                return;
            }
            else if (!overrideSetDestination && agent.enabled == true)
            {
                agent.SetDestination(new Vector3(listOfDesination[0].x, listOfDesination[0].y, listOfDesination[0].z));
            }
            else if (overrideSetDestination && agent.enabled == true)
            {
                agent.velocity = new Vector3(0, 0, rbSpeed);
            }

        }
    }

    public void EnemyDoDemand()
    {
        if (listOfDesination[0].w == 1)
        {
            overrideSetDestination = false;
        }
        else if (listOfDesination[0].w == 2 || listOfDesination[0].w == 3)
        {
            agent.ResetPath();
            overrideSetDestination = true;
            StartCoroutine(DemandCase23());
        }
        else if(listOfDesination[0].w == 4)
        {
            demandCase4 = true;
            agent.velocity = Vector3.zero;
            agent.speed = 0;
            StartCoroutine(DemandCase4());
        }
        else if(listOfDesination[0].w == 0)
        {
            agent.velocity = new Vector3(0, 0, rbSpeed);
            transform.GetComponent<EnemyMovement>().enabled = false;
        }
    }

    public IEnumerator DemandCase23()
    {
        yield return new WaitForSeconds(2f);
        overrideSetDestination = false;
    }

    public IEnumerator DemandCase4()
    {
        StartCoroutine(enemyManager.EnemySkin1ToSkin2());
        StartCoroutine(enemyManager.StartParticleSystem());
        yield return new WaitForSeconds(3);
        StartCoroutine(enemyManager.EnemySkin2ToSkin1());
        agent.speed = rbSpeed;
        agent.velocity = new Vector3(0, 0, rbSpeed);
        demandCase4 = false;
    }

    //public IEnumerator Delay(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    agent.enabled = true;
    //}
    #endregion

    public void EnemyMovefoward()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 1 * rbSpeed);
    }

    public void RayCastCheck()
    {
        if (rayCastOn)
        {
            float tempRotation = Y1Rotation;
            float step = Mathf.Abs(Y1Rotation - Y2Rotation) / numerOfRay;
            for (int i = 0; i < numerOfRay; i++)
            {
                Vector3 dir = Quaternion.Euler(0, tempRotation, 0) * Vector3.forward;
                Physics.Raycast(child.transform.position, dir, 5, obstacleLayer);
                tempRotation += step;
            }
        }
    }
    
    public void Jump()
    {
        agent.enabled = false;
        rb.AddForce(new Vector3(rb.velocity.x, 1 * jumpForce, 3.55f), ForceMode.Impulse);
        animator.SetBool("jump", true);
        StartCoroutine(DelayJump());
    }

    public void CheckGround()
    {
        if (Physics.Raycast(child.transform.position, Vector3.down, 0.05f, layer))
        {
            if (checkJump)
            {
                animator.SetBool("jump", false);
                agent.enabled = true;
                checkJump = false;
            }
        }
    }

    public IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(1);
        checkJump = true;

    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < listOfDesination.Count; i++)
        {
            Gizmos.color = Color.black;

            Gizmos.DrawWireSphere(new Vector3(listOfDesination[i].x, listOfDesination[i].y, listOfDesination[i].z), 0.5f);
        }

        Gizmos.color = Color.red;
        float tempRotation = Y1Rotation;
        float step = Mathf.Abs(Y1Rotation - Y2Rotation) / (numerOfRay - 1);
        for (int i = 0; i < numerOfRay; i++)
        {
            Vector3 dir = Quaternion.Euler(0, tempRotation, 0) * Vector3.forward;
            Gizmos.DrawRay(child.transform.position, Quaternion.Euler(0, tempRotation, 0) * Vector3.forward * raycastLength);
            tempRotation += step;
        }
    }
}
