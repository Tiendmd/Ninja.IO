using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeacherAI : MonoBehaviour
{
    public float timeLerp;
    public int i = 0;
    public bool allowPatrol;
    //[Range(0, 360)]
    //public List<float> angles = new List<float>();
    //public List<float> angles2 = new List<float>();
    public FieldOfView fieldOfView;
    private bool oneTime = true;
    private bool controlDelayToPatrol = true;

    public float idleToPatrolTime = 5f;
    public Animator animator;
    public Transform target;
    [Range(-2.5f, 2.5f)]
    public List<float> targetsXs = new List<float>();

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        fieldOfView = GetComponentInChildren<FieldOfView>();
        allowPatrol = true;
        //for (int i = 0; i < angles.Count; i++)
        //{
        //    angles2.Add(angles[i]);
        //}
        StartCoroutine(StartPatrol());
        fieldOfView.enabled = false;
        fieldOfView.viewRadius = 0;
        fieldOfView.viewAngle = 0;
    }

    private void Update()
    {
        //CheckReachFirstPoint();
        if (i < targetsXs.Count && allowPatrol)
        {
            CheckReachPatrolPoint();
        }
        else if (i == targetsXs.Count)
        {
            ReachEndOfList();
        }
        if (i < targetsXs.Count && !allowPatrol && controlDelayToPatrol)
        {
            StartCoroutine(DelayToPatrol());
        }
    }

    IEnumerator StartPatrol()
    {
        //target.transform.position = new Vector3(0, target.transform.position.y, target.transform.position.z);
        transform.DORotate(new Vector3(0, 180, 0), 2).SetEase(Ease.Linear);
        animator.SetBool("turn", true);
        yield return new WaitForSeconds(2);
        fieldOfView.enabled = true;
        animator.SetTrigger("lookup");
        fieldOfView.viewAngle = fieldOfView.defaultViewAngle;
        fieldOfView.viewRadius = fieldOfView.defaultViewRadius;
        Patrol();
    }

    //public void CheckReachFirstPoint()
    //{
    //    if (Vector3.Distance(new Vector3(target.transform.position.x, 0, 0), new Vector3(targetsXs[0], 0, 0)) <= 0.05f)
    //    {
    //        fieldOfView.enabled = true; 
    //        fieldOfView.viewAngle = fieldOfView.defaultViewAngle;
    //        fieldOfView.viewRadius = fieldOfView.defaultViewRadius;
    //    }
    //}

    public void Patrol()
    {
        target.transform.DOMoveX(targetsXs[i], timeLerp).SetEase(Ease.Linear);
    }


    public void CheckReachPatrolPoint()
    {
        //int a = Random.Range(0, 10);
        //Collider[] list = new Collider[0];
        //if ((int)Time.frameCount % 30 ==0)
        //{
        //    list = Physics.OverlapSphere(transform.position, fieldOfView.viewRadius, fieldOfView.targetMask);
        //}
        //if (a<=4)
        //{
        //    if (Vector3.Distance(new Vector3(target.transform.position.x, 0, 0), new Vector3(targetsXs[i], 0, 0)) <= 0.05f)
        //    {
        //        i++;
        //        if (i < targetsXs.Count)
        //        {
        //            Patrol();
        //        }
        //    }
        //}
        //else if (list.Length > 0 && a > 5)
        //{
        //    DOTween.Kill(transform);
        //    target.transform.DOMoveX(list[Mathf.FloorToInt(Random.Range(0, list.Length))].transform.position.x, 1);
        //}
        if (Vector3.Distance(new Vector3(target.transform.position.x, 0, 0), new Vector3(targetsXs[i], 0, 0)) <= 0.05f)
        {
            i++;
            if (i < targetsXs.Count)
            {
                Patrol();
            }
        }
    }

    public void ReachEndOfList()
    {
        fieldOfView.viewAngle = Mathf.Lerp(0, fieldOfView.viewAngle, 0.9f);
        allowPatrol = false;
        if (oneTime)
        {
            animator.SetTrigger("looktoturn");
            transform.DORotate(Vector3.zero, 2).SetEase(Ease.Linear);
            StartCoroutine(Delay(2));
            oneTime = false;
        }
        if (fieldOfView.viewAngle <= 0.0001f)
        {
            i = 0;
            fieldOfView.viewAngle = 0;
            fieldOfView.enabled = false;
        }
    }

    IEnumerator Delay(float a)
    {
        yield return new WaitForSeconds(a);
        animator.SetBool("turn", false);
    }

    IEnumerator DelayToPatrol()
    {
        oneTime = true;
        controlDelayToPatrol = false;
        yield return new WaitForSeconds(idleToPatrolTime);
        DOTween.Kill(transform, false);
        DOTween.Kill(target.transform, false);
        StartCoroutine(StartPatrol());
        target.transform.position = new Vector3(0, target.transform.position.y, target.transform.position.z);

        allowPatrol = true;
        controlDelayToPatrol = true;
    }

    public void KillTeacher()
    {
        DOTween.Kill(transform, false);
        DOTween.Kill(target.transform, false);
        Destroy(gameObject);
    }
}
