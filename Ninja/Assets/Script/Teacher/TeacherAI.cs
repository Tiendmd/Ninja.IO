using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeacherAI : MonoBehaviour
{
    public float timeLerp;
    public int i = 0;
    public bool allowPatrol;
    public FieldOfView fieldOfView;
    private bool oneTime = true;
    private bool controlDelayToPatrol = true;

    public float idleToPatrolTime = 5f;
    public Animator animator;
    public Transform target;
    private Vector3 defaultTargetPosition;
    public Transform child;
    [Range(-2.5f, 2.5f)]
    public List<float> targetsXs = new List<float>();
    public bool fieldOfViewOneTime = true;
    public bool startFieldOfView = false;
    public bool stopToCheckPlayer;
    public LayerMask layer;

    private void Start()
    {
        defaultTargetPosition = target.position;
        animator = GetComponentInChildren<Animator>();
        fieldOfView = GetComponentInChildren<FieldOfView>();
        allowPatrol = true;
        StartCoroutine(StartPatrol());
        fieldOfView.enabled = false;
        fieldOfView.viewRadius = 0;
        fieldOfView.viewAngle = 0;
        InvokeRepeating("ScanBool", 2, 2);
    }

    private void Update()
    {
        if (startFieldOfView)
        {
            fieldOfView.viewRadius = Mathf.SmoothStep(fieldOfView.viewRadius, fieldOfView.defaultViewRadius, 0.1f);
        }
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
        transform.DORotate(new Vector3(0, 180, 0), 2).SetEase(Ease.Linear);
        animator.SetBool("turn", true);
        yield return new WaitForSeconds(2);
        startFieldOfView = true;
        fieldOfView.enabled = true;
        animator.SetTrigger("lookup");
        fieldOfView.viewAngle = fieldOfView.defaultViewAngle;
        yield return new WaitForSeconds(2);
        startFieldOfView = false;
        Patrol();
    }

    public void Patrol()
    {
        int a = Mathf.FloorToInt(Random.Range(0, 5));
        if (a == 0)
        {
            target.transform.DOMoveX(targetsXs[i], timeLerp).SetEase(Ease.Linear);
        }
        else if (a == 1)
        {

            target.transform.DOMoveX(targetsXs[i], timeLerp).SetEase(Ease.OutSine);
        }
        else if (a == 2)
        { 
            target.transform.DOMoveX(targetsXs[i], timeLerp).SetEase(Ease.OutCirc);
        }
        else if (a == 3)
        {
            target.transform.DOMoveX(targetsXs[i], timeLerp).SetEase(Ease.InQuart);
        }
        else if (a ==4 )
        {
            target.transform.DOMoveX(targetsXs[i], timeLerp).SetEase(Ease.InBack);
        }
    }

    private bool scanBool;

    public void CheckReachPatrolPoint()
    {
        if (Vector3.Distance(new Vector3(target.transform.position.x, 0, 0), new Vector3(targetsXs[i], 0, 0)) <= 0.05f)
        {
            i++;
            if (i < targetsXs.Count)
            {
                Patrol();
            }
        }

        if (scanBool)
        {
            if (Physics.Raycast(child.position, new Vector3(target.position.x, child.position.y, target.position.z) - child.position, fieldOfView.viewRadius - 0.25f, layer) && stopToCheckPlayer && stopToCheckPlayer)
            {
                DOTween.Pause(target.transform);
                fieldOfView.viewAngle = Mathf.SmoothStep(5, fieldOfView.viewAngle, 0.8f);
                StartCoroutine(DelayFieldOfView());
            }
        }
        if (!stopToCheckPlayer)
        {
            fieldOfView.viewAngle = Mathf.SmoothStep(fieldOfView.viewAngle, fieldOfView.defaultViewAngle, 0.1f);
        }
    }

    public void ScanBool()
    {
        float a = Random.Range(0, 10);
        if (a <= 4)
        {
            scanBool = true;
        }
        else
        {
            scanBool = false;
        }
    }

    IEnumerator DelayFieldOfView()
    {
        if (fieldOfViewOneTime)
        {
            fieldOfViewOneTime = false;
            yield return new WaitForSeconds(1);
            DOTween.Play(target.transform);
            stopToCheckPlayer = false;
            yield return new WaitForSeconds(1);
            fieldOfViewOneTime = true;
            stopToCheckPlayer = true;
        }
    }


    public void ReachEndOfList()
    {
        //fieldOfView.viewAngle = Mathf.Lerp(0, fieldOfView.viewAngle, 0.9f);
        fieldOfView.viewAngle = Mathf.SmoothStep(0, fieldOfView.viewAngle, 0.93f);
        //LerpFieldOfView(0, 0.93f);
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
            fieldOfView.viewRadius = 0;
            fieldOfView.enabled = false;
        }
    }

    IEnumerator Delay(float a)
    {
        yield return new WaitForSeconds(a);
        target.transform.position = defaultTargetPosition;
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
        target.transform.position = defaultTargetPosition;

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
