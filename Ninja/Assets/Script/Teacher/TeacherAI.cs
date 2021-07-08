using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeacherAI : MonoBehaviour
{
    public float timeLerp;
    public int i = 0;
    public bool allowPatrol;
    [Range(0, 360)]
    public List<float> angles = new List<float>();
    public List<float> angles2 = new List<float>();
    private FieldOfView fieldOfView;
    private bool oneTime = true;
    private bool controlDelayToPatrol = true;
    float velocity;

    public float idleToPatrolTime = 5f;

    private void Start()
    {
        fieldOfView = GetComponentInChildren<FieldOfView>();
        allowPatrol = true;
        for (int i = 0; i < angles.Count; i++)
        {
            angles2.Add(angles[i]);
        }
        Invoke("StartPatrol", 2);
        fieldOfView.enabled = false;
        fieldOfView.viewRadius = 0;
        fieldOfView.viewAngle = 0;
    }

    private void Update()
    {
        CheckReachFirst();
        if (i < angles.Count && allowPatrol)
        {
            CheckReachPatrolPoint();
        }
        else if (i == angles.Count)
        {
            ReachEndOfList();
        }
        if (i < angles.Count && !allowPatrol && controlDelayToPatrol)
        {
            StartCoroutine(DelayToPatrol());
        }
    }

    public void StartPatrol()
    {
        transform.DORotate(new Vector3(0, angles[0], 0), 2).SetEase(Ease.Linear);
    }

    public void CheckReachFirst()
    {
        if (Vector3.Distance(transform.rotation.eulerAngles, new Vector3(0, angles[0], 0)) <= 1)
        {
            fieldOfView.enabled = true;
            fieldOfView.viewAngle = fieldOfView.defaultViewAngle;
            fieldOfView.viewRadius = fieldOfView.defaultViewRadius;
        }
    }

    public void Patrol()
    {
        transform.DORotate(new Vector3(0, angles[i], 0), timeLerp).SetEase(Ease.Linear);
    }


    public void CheckReachPatrolPoint()
    {
        if (Vector3.Distance(new Vector3(0, transform.rotation.eulerAngles.y, 0), new Vector3(0, angles[i], 0)) <= 0.1f)
        {
            i++;
            if (i < angles.Count)
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
            transform.DORotate(Vector3.zero, timeLerp / 1.25f).SetEase(Ease.Linear);
            oneTime = false;
        }
        if (fieldOfView.viewAngle <= 0.0001f)
        {
            i = 0;
            fieldOfView.viewAngle = 0;
            fieldOfView.enabled = false;
        }
    }

    IEnumerator DelayToPatrol()
    {
        oneTime = true;
        controlDelayToPatrol = false;
        yield return new WaitForSeconds(idleToPatrolTime);
        DOTween.Kill(transform, false);
        StartPatrol();
        allowPatrol = true;
        controlDelayToPatrol = true;
    }

    public void KillTeacher()
    {
        DOTween.Kill(transform, false);
        Destroy(gameObject);
    }
}
