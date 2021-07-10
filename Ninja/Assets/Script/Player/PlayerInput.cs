using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PlayerInput : MonoBehaviour
{
    private PlayerManager playerManager;
    private Vector3 skin1OriginSize;
    private Vector3 skin2OriginSize;
    public float scaleTime;
    private bool oneTime = true;


    [Header("Component")]
    private Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerManager = GetComponent<PlayerManager>();
        skin1OriginSize = playerManager.skin1.transform.localScale;
        skin2OriginSize = playerManager.skin2.transform.localScale;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && oneTime)
        {
            DataManager.Instance.gameIsStart = true;
            oneTime = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("run", true);
        }
        InputReceive();

    }
    public void InputReceive()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerManager.isSkin2)
        {
            StartCoroutine(Skin2ToSkin1());
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && playerManager.isSkin1)
        {

            StartCoroutine(Skin1ToSkin2());
            StartCoroutine(StartParticleSystem());
        }
    }

    public IEnumerator StartParticleSystem()
    {
        GameObject temp = Instantiate(playerManager.particle, transform.position, Quaternion.Euler(-90, 0, 0));
        yield return new WaitForSeconds(playerManager.particle.GetComponent<ParticleSystem>().main.duration+1);
        Destroy(temp);
    }

    public IEnumerator Skin1ToSkin2()
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

    public IEnumerator Skin2ToSkin1()
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
