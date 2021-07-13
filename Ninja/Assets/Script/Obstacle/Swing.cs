using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Swing : MonoBehaviour
{
    public float swingTime;
    public float returnTime;
    public float timer;
    public float defaultTimer;
    private int c;
    private Vector3 originTransform;
    public CapsuleCollider child;
    public float destinationX;


    private void Start()
    {
        originTransform = transform.position;    
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (Vector3.Distance(transform.position, new Vector3(0,transform.position.y, transform.position.z)) >= originTransform.x && c==0 && timer <= 0)
        {
            child.enabled = true;
            transform.DOMoveX(destinationX, swingTime).SetEase(Ease.Linear);
            c = 1;
            timer = defaultTimer;
        }
        else if (Vector3.Distance(transform.position, new Vector3(destinationX, transform.position.y, transform.position.z)) <= 0.1f && c == 1)
        {
            child.enabled = false;
            transform.DOMove(originTransform, returnTime).SetEase(Ease.Linear);
            c = 0;
        }
    }
}
