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
    public float delayTime = 3;
    public Animator animator { get; set; }
    private bool stopStartRace = false;

    private EnemyManager enemyManager;
    private PlayerManager playerManager;
    public bool overrideSetDestination;
    private bool oneTime = true;

    private bool demandCase4;
    private bool demandCase23;
    private bool demandCase1;
    private bool demandCase0;
    private void Start()
    {
        overrideSetDestination = false;
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        enemyManager = GetComponent<EnemyManager>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (DataManager.Instance.gameIsStart)
        {
            if (!stopStartRace)
            {
                animator.SetBool("run", true);
                agent.velocity = new Vector3(0, 0, rbSpeed);
                if (oneTime)
                {
                    StartCoroutine(StartRace());
                }
            }
            else if (stopStartRace)
            {
                if (enemyManager.isStupid)
                {
                    EnemyReachDestination();
                }
                else if (enemyManager.isSmart)
                {
                    //agent.enabled = true;
                    agent.SetDestination(new Vector3(listOfDesination[listOfDesination.Count - 1].x,
                        listOfDesination[listOfDesination.Count - 1].y,
                        listOfDesination[listOfDesination.Count - 1].z));
                }
            }
        }
    }

    IEnumerator StartRace()
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
    || transform.position.z >= listOfDesination[0].z)
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
            else if (!overrideSetDestination)
            {
                agent.SetDestination(new Vector3(listOfDesination[0].x, listOfDesination[0].y, listOfDesination[0].z));
            }
            else if (overrideSetDestination)
            {
                agent.velocity = transform.forward * rbSpeed;
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

    IEnumerator DemandCase23()
    {
        yield return new WaitForSeconds(2f);
        overrideSetDestination = false;
    }

    IEnumerator DemandCase4()
    {
        StartCoroutine(enemyManager.EnemySkin1ToSkin2());
        StartCoroutine(enemyManager.StartParticleSystem());
        yield return new WaitForSeconds(3);
        StartCoroutine(enemyManager.EnemySkin2ToSkin1());
        agent.speed = rbSpeed;
        agent.velocity = new Vector3(0, 0, rbSpeed);
        demandCase4 = false;
    }

    public IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        agent.enabled = true;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < listOfDesination.Count; i++)
        {
            Gizmos.color = Color.white;

            Gizmos.DrawWireSphere(new Vector3(listOfDesination[i].x, listOfDesination[i].y, listOfDesination[i].z), 0.5f);
        }
    }
}
