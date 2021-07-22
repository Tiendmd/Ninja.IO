using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyDodge : MonoBehaviour
{
    private PlayerManager playerManager;
    private EnemyManager enemyManager;
    private EnemyMovement enemyMovement;
    private Rigidbody rb;
    public MyScene dataManager;
    public GameObject teacher;
    private FieldOfView fov;
    [Range(0, 100)]
    public float dodgePercent;

    private bool oneTime = true;
    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();
        enemyManager = GetComponent<EnemyManager>();
        teacher = dataManager.listOfTeacher[0];
        fov = teacher.GetComponentInChildren<FieldOfView>();
        enemyMovement = GetComponent<EnemyMovement>();

    }

    private void Update()
    {
        EnemyDodgeControl();

    }

    public void EnemyDodgeControl()
    {
        if (MyScene.Instance.listOfTeacher.Count == 0)
        {
            return;
        }
        if (Vector3.Angle((transform.position - teacher.transform.position), fov.transform.forward) < fov.viewAngle / 2 + 5
            && Vector3.Distance(transform.position, teacher.transform.position) <= fov.viewRadius && oneTime)
        {
            if (enemyMovement.intelligent <=5)
            {
                int a = Random.Range(0, 100);
                if (a < dodgePercent)
                {
                    rb.velocity = Vector3.zero;
                    DOTween.Kill(transform);

                    transform.GetComponent<EnemyMovement>().enabled = false;
                    //transform.GetComponent<NavMeshAgent>().enabled = false;
                    StartCoroutine(enemyManager.EnemySkin1ToSkin2());
                    enemyManager.StartParticleSystem();
                }
            }
            else if (enemyMovement.intelligent > 5)
            {
                rb.velocity = Vector3.zero;
                DOTween.Kill(transform);
                transform.GetComponent<EnemyMovement>().enabled = false;
                //transform.GetComponent<NavMeshAgent>().enabled = false;
                StartCoroutine(enemyManager.EnemySkin1ToSkin2());
                enemyManager.StartParticleSystem();
            }
            oneTime = false;
        }
        else if (Vector3.Angle((transform.position - teacher.transform.position), fov.transform.forward) > fov.viewAngle / 2 + 5
            && Vector3.Distance(transform.position, teacher.transform.position) <= fov.viewRadius && !oneTime)
        {

            if (enemyMovement.intelligent <= 5)
            {
                int a = Random.Range(0, 3);
                StartCoroutine(Delay(a));
            }
            else if (enemyMovement.intelligent > 5)
            {
                StartCoroutine(enemyManager.EnemySkin2ToSkin1());
                transform.GetComponent<EnemyMovement>().enabled = true;
                //transform.GetComponent<NavMeshAgent>().enabled = true;
            }
            oneTime = true;
        }
    }

    IEnumerator Delay(int a)
    {
        yield return new WaitForSeconds(a);
        StartCoroutine(enemyManager.EnemySkin2ToSkin1());
        transform.GetComponent<EnemyMovement>().enabled = true;
        //transform.GetComponent<NavMeshAgent>().enabled = true;
        //transform.GetComponent<EnemyManager>().agent.velocity = new Vector3(0, 0, GetComponent<EnemyMovement>().rbSpeed);
    }
}
