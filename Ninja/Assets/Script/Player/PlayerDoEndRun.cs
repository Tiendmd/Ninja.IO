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
        animator = GetComponent<Animator>();
    }
    public IEnumerator PlayerEndRun()
    {
        rb.velocity = Vector3.zero;
        DOTween.KillAll();
        Tween a = transform.DOMove(cylinder.position, 2);
        yield return a.WaitForCompletion();
        animator.SetTrigger("victory");
        UIManager.Instance.FinishRun();
        MyScene.Instance.gameIsFinish = true;
    }
}
