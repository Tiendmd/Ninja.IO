using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDoEndRun : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    public Transform cylinder;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    public IEnumerator PlayerEndRun()
    {
        rb.velocity = Vector3.zero;
        Tween a = transform.DOMove(new Vector3(cylinder.position.x, transform.position.y, cylinder.position.z), 2);
        yield return a.WaitForCompletion();
        animator.SetTrigger("victory");
        MyScene.Instance.gameIsFinish = true;
    }


}
