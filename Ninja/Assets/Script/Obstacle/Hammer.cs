using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hammer : MonoBehaviour
{
    public float timer;
    public float defaultTimer;
    public float downTime;
    public float upTime;
    public List<Vector3> list = new List<Vector3>();
    public int c = 0;
    private Vector3 originTransform;

    private void Start()
    {
        timer = 1;
        originTransform = Vector3.zero;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        HammerControl();
    }

    public void HammerControl()
    {
        if (transform.rotation.eulerAngles.z <= 270 && c == 1 && timer <= 0)
        {
            c = 0;
            transform.DORotate(list[c], downTime).SetEase(Ease.Linear);
            timer = defaultTimer;
        }
        else if (Vector3.Distance(transform.rotation.eulerAngles, list[c]) <= 0.1f && c == 0)
        {
            c++;
            transform.DORotate(list[c], upTime).SetEase(Ease.Linear);
        }
    }
}
