using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyManager : MonoBehaviour
{
    public float scaleTime = 0.15f;
    public float percentToSmart;
    public float percentToStupid;
    public float timeBetweenInvoke;
    public Transform player;
    private PlayerManager playerManager;
    private Vector3 skin1OriginSize;
    private Vector3 skin2OriginSize;
    public Animator animator { get; set; }
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerManager = GetComponent<PlayerManager>();
        skin1OriginSize = playerManager.skin1.transform.localScale;
        skin2OriginSize = playerManager.skin2.transform.localScale;
        InvokeRepeating("SmartStupidControl", 3, timeBetweenInvoke);
    }

    private void Update()
    {
    }

    public void SmartStupidControl()
    {
        if (Mathf.Abs(MyScene.Instance.finishZ - transform.position.z) >= Mathf.Abs(MyScene.Instance.finishZ - player.position.z))
        {
            int a = Random.Range(0, 100);
            if (a < percentToSmart)
            {
            }
        }
        else if (Mathf.Abs(MyScene.Instance.finishZ - transform.position.z) < Mathf.Abs(MyScene.Instance.finishZ - player.position.z))
        {
            int a = Random.Range(0, 100);
            if (a > percentToSmart && a < percentToStupid)
            {
            }
        }
    }

    public void StartParticleSystem()
    {
        Instantiate(playerManager.particle, transform.position, Quaternion.Euler(-90, 0, 0));
    }

    public IEnumerator EnemySkin1ToSkin2()
    {
        playerManager.canMove = false;
        playerManager.isSkin1 = false;
        playerManager.skin1.transform.DOScale(Vector3.zero, scaleTime);
        playerManager.skin2.GetComponent<CapsuleCollider>().enabled = true;
        playerManager.skin2.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(scaleTime);
        playerManager.isSkin2 = true;
        playerManager.skin1.GetComponent<CapsuleCollider>().enabled = false;
        playerManager.skin1.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        playerManager.skin2.transform.localScale = skin2OriginSize;

    }

    public IEnumerator EnemySkin2ToSkin1()
    {
        playerManager.canMove = true;
        playerManager.isSkin2 = false;
        playerManager.skin2.transform.DOScale(Vector3.zero, scaleTime);
        playerManager.skin1.GetComponent<CapsuleCollider>().enabled = true;
        playerManager.skin1.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        yield return new WaitForSeconds(scaleTime);
        playerManager.isSkin1 = true;
        playerManager.skin2.GetComponent<CapsuleCollider>().enabled = false;
        playerManager.skin2.GetComponent<MeshRenderer>().enabled = false;
        playerManager.skin1.transform.localScale = skin1OriginSize;

    }

}
