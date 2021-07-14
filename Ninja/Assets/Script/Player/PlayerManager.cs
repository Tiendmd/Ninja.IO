using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    public bool canMove { get; set; }
    public bool canSlide;
    public bool jumping;
    public GameObject skin1;
    public GameObject skin2;
    public GameObject particle;
    public CameraFollow myCamera;
    public bool isSkin1;
    public bool isSkin2;
    //private NavMeshAgent agent;
    public float timeBetweenResurrect = 2;

    public Vector3 checkPointPosition;
    public bool playerIsDead = true;

    public bool enemyIsDead = true;
    private EnemyManager enemyManager;

    private Rigidbody rb;
    private PlayerInput playerInput;
    public Animator animator { get; set; }
    public RigidbodyConstraints constraint1 = RigidbodyConstraints.FreezeRotation;

    [Header("Placement")]
    public int place;

    private void Start()
    {
        //if (transform.tag == "Enemy")
        //{
        //    agent = GetComponent<NavMeshAgent>();
        //}
        playerInput = GetComponent<PlayerInput>();
        enemyManager = GetComponent<EnemyManager>();
        canMove = true;
        isSkin1 = true;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        //checkPointPosition = new Vector3(0, transform.position.y, 0);
    }

    public void ResetPositionToCheckPoint()
    {
        StartCoroutine(Delay(timeBetweenResurrect, "die"));
    }

    public void PlayerFall()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        Collider[] b = transform.GetComponentsInChildren<CapsuleCollider>();
        for (int i = 0; i < b.Length; i++)
        {
            b[i].enabled = false;
        }
        StartCoroutine(Delay(timeBetweenResurrect, "fall"));

    }

    public void PlayerKick(Vector3 kickDirection)
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(kickDirection, ForceMode.Impulse);
        StartCoroutine(Delay(timeBetweenResurrect, "die"));
    }

    IEnumerator Delay(float delay, string a)
    {
        if (playerIsDead)
        {
            playerIsDead = false;
            canMove = false;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            transform.GetComponent<PlayerInput>().enabled = false;
            transform.GetComponent<PlayerMovement>().enabled = false;
            animator.SetBool("run", false);
            animator.SetTrigger(a);
            myCamera.player = null;
            Collider[] b = transform.GetComponentsInChildren<CapsuleCollider>();
            yield return new WaitForSeconds(delay);
            animator.SetTrigger("idle");
            rb.velocity = new Vector3(0, 0, 0);
            transform.GetComponent<PlayerInput>().enabled = true;
            transform.GetComponent<PlayerMovement>().enabled = true;
            playerInput.checkAnimationRun = true;
            myCamera.player = transform.gameObject;
            myCamera.transform.position = checkPointPosition + myCamera.offset;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = checkPointPosition;
            StartCoroutine(playerInput.Skin2ToSkin1());
            for (int i = 0; i < b.Length; i++)
            {
                b[i].enabled = true;
            }
            rb.constraints = constraint1;

            yield return new WaitForSeconds(0.5f);
            canMove = true;
            playerIsDead = true;
        }

    }



    IEnumerator EnemyDelay(float delay, string a)
    {
        if (enemyIsDead)
        {
            enemyIsDead = false;
            StopCoroutine(GetComponent<EnemyMovement>().DelayJump());
            rb.velocity = Vector3.zero;
            transform.GetComponent<EnemyMovement>().enabled = false;
            animator.SetBool("run", false);
            animator.SetTrigger(a);
            Collider[] b = transform.GetComponentsInChildren<CapsuleCollider>();
            // sau delay giay thi sinh ra cho moi
            yield return new WaitForSeconds(delay);
            rb.velocity = new Vector3(0, 0, 0);

            for (int i = 0; i < b.Length; i++)
            {
                b[i].enabled = true;
            }
            animator.SetTrigger("idle");
            rb.constraints = constraint1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = checkPointPosition;
            StartCoroutine(enemyManager.EnemySkin2ToSkin1());

            // sau 0.5s idle thi sang run
            yield return new WaitForSeconds(0.5f);
            transform.GetComponent<EnemyMovement>().enabled = true;
            rb.velocity = new Vector3(0, 0, GetComponent<EnemyMovement>().rbSpeed);
            animator.SetBool("run", true);
            enemyIsDead = true;
        }

    }

    public void EnemyKick(Vector3 kickDirection)
    {
        rb.constraints = RigidbodyConstraints.None;
        //agent.enabled = false;
        rb.AddForce(kickDirection, ForceMode.Impulse);
        StartCoroutine(EnemyDelay(timeBetweenResurrect, "die"));
    }

    public void ResetEnemyToCheckPoint()
    {
        StartCoroutine(EnemyDelay(timeBetweenResurrect, "die"));
    }

    public void EnemyFall()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        Collider[] b = transform.GetComponentsInChildren<CapsuleCollider>();
        for (int i = 0; i < b.Length; i++)
        {
            b[i].enabled = false;
        }
        StartCoroutine(EnemyDelay(timeBetweenResurrect, "fall"));
    }

    public int CoinGain(int a)
    {
        if (a == 1)
        {
            return 700;
        }
        else if (a == 2)
        {
            return 500;
        }
        else
        {
            return 300;
        }
    }
}
