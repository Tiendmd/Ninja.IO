using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    public List<Vector4> listOfDesination = new List<Vector4>();
    //private NavMeshAgent agent;
    public Rigidbody rb { get; set; }
    public float rbSpeed;
    public Animator animator { get; set; }
    private bool stopStartRace = false;

    private EnemyManager enemyManager;
    private PlayerManager playerManager;
    public bool overrideSetDestination;
    private bool oneTime = true;

    private bool demandCase4;

    public float jumpForce;
    public GameObject child;
    public LayerMask layer;
    private bool checkJump;
    public bool pressing;

    [Header("Slide")]
    public float slideForce;
    public bool slideRight;
    public bool slideLeft;
    [Header("CheckForward")]
    public LayerMask obstacleLayer;
    public LayerMask wallLayer;
    public float Y1Rotation;
    private float Y2Rotation;
    private bool rayCastOn;
    public float raycastLength;
    //public float degreePerRaycast;
    public int numerOfRay;
    public int scanFrequence;
    [Range(1,9)]
    public int intelligent;
    private float desiredX;
    private void Start()
    {
        //NavMesh.RemoveAllNavMeshData();
        overrideSetDestination = false;
        playerManager = GetComponent<PlayerManager>();
        rb = GetComponent<Rigidbody>();
        //agent = GetComponent<NavMeshAgent>();
        enemyManager = GetComponent<EnemyManager>();
        animator = GetComponentInChildren<Animator>();
        //agent.SetDestination(new Vector3(listOfDesination[0].x,
        //               listOfDesination[0].y,
        //               listOfDesination[0].z));
        Y2Rotation = -Y1Rotation;

        rayCastOn = true;
        slideLeft = false;
        slideRight = false;
    }   

    private void Update()
    {
        if (MyScene.Instance.gameIsStart)
        {
            RayCastCheck();
            RayCastOnOff();
            CheckGround();

        }

        //if (DataManager.Instance.gameIsStart)
        //{
        //    if (!stopStartRace)
        //    {
        //        animator.SetBool("run", true);
        //        agent.velocity = new Vector3(0, 0, rbSpeed);
        //        if (oneTime)
        //        {
        //            StartCoroutine(StartRace());
        //        }
        //    }
        //    else if (stopStartRace)
        //    {
        //        if (enemyManager.isStupid && agent.enabled == true)
        //        {
        //            EnemyReachDestination();
        //        }
        //        else if (enemyManager.isSmart && agent.enabled == true)
        //        {
        //            agent.SetDestination(new Vector3(listOfDesination[listOfDesination.Count - 1].x,
        //                listOfDesination[listOfDesination.Count - 1].y,
        //                listOfDesination[listOfDesination.Count - 1].z));
        //        }
        //    }
        //}
    }

    private void FixedUpdate()
    {
        if (MyScene.Instance.gameIsStart)
        {
            if (oneTime)
            {
                animator.SetBool("run", true);
                oneTime = false;
            }
            EnemyMovefoward();
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.5f, 2.5f), transform.position.y, transform.position.z);
    }

    #region NavMeshAgent
    //public IEnumerator StartRace()
    //{
    //    oneTime = false;
    //    yield return new WaitForSeconds(1);
    //    stopStartRace = true;
    //}

    //public void EnemyReachDestination()
    //{
    //    if (listOfDesination.Count > 0)
    //    {
    //        if (Vector3.Distance(transform.position, new Vector3(listOfDesination[0].x, listOfDesination[0].y, listOfDesination[0].z)) <= 0.5f
    //|| transform.position.z >= listOfDesination[0].z && agent.enabled == true)
    //        {
    //            EnemyDoDemand();
    //            if (listOfDesination.Count > 1)
    //            {
    //                listOfDesination.RemoveAt(0);
    //            }
    //        }
    //        if (demandCase4)
    //        {
    //            return;
    //        }
    //        else if (!overrideSetDestination && agent.enabled == true)
    //        {
    //            agent.SetDestination(new Vector3(listOfDesination[0].x, listOfDesination[0].y, listOfDesination[0].z));
    //        }
    //        else if (overrideSetDestination && agent.enabled == true)
    //        {
    //            agent.velocity = new Vector3(0, 0, rbSpeed);
    //        }

    //    }
    //}

    //public void EnemyDoDemand()
    //{
    //    if (listOfDesination[0].w == 1)
    //    {
    //        overrideSetDestination = false;
    //    }
    //    else if (listOfDesination[0].w == 2 || listOfDesination[0].w == 3)
    //    {
    //        agent.ResetPath();
    //        overrideSetDestination = true;
    //        StartCoroutine(DemandCase23());
    //    }
    //    else if(listOfDesination[0].w == 4)
    //    {
    //        demandCase4 = true;
    //        agent.velocity = Vector3.zero;
    //        agent.speed = 0;
    //        StartCoroutine(DemandCase4());
    //    }
    //    else if(listOfDesination[0].w == 0)
    //    {
    //        agent.velocity = new Vector3(0, 0, rbSpeed);
    //        transform.GetComponent<EnemyMovement>().enabled = false;
    //    }
    //}

    //public IEnumerator DemandCase23()
    //{
    //    yield return new WaitForSeconds(2f);
    //    overrideSetDestination = false;
    //}

    //public IEnumerator DemandCase4()
    //{
    //    StartCoroutine(enemyManager.EnemySkin1ToSkin2());
    //    StartCoroutine(enemyManager.StartParticleSystem());
    //    yield return new WaitForSeconds(3);
    //    StartCoroutine(enemyManager.EnemySkin2ToSkin1());
    //    agent.speed = rbSpeed;
    //    agent.velocity = new Vector3(0, 0, rbSpeed);
    //    demandCase4 = false;
    //}

    ////public IEnumerator Delay(float time)
    ////{
    ////    yield return new WaitForSeconds(time);
    ////    agent.enabled = true;
    ////}
    #endregion

    public void EnemyMovefoward()
    {
        rb.velocity = new Vector3(0, rb.velocity.y, 1 * rbSpeed);
    }

    public void RayCastCheck()
    {
        if (rayCastOn)
        {
            DOTween.Kill(transform, false);
            Vector3[] a = new Vector3[numerOfRay];
            float tempRotation = Y1Rotation;
            float step = Mathf.Abs(Y1Rotation - Y2Rotation) / (numerOfRay - 1);
            for (int i = 0; i < numerOfRay; i++)
            {
                Vector3 dir = Quaternion.Euler(0, tempRotation, 0) * Vector3.forward;
                RaycastHit hit;
                if (Physics.Raycast(child.transform.position, dir, out hit, raycastLength, obstacleLayer))
                {
                    if (hit.transform.CompareTag("Obstacle"))
                    {
                        a[i] = hit.point;
                    }

                }
                else
                {
                    a[i] = dir;
                }
                tempRotation += step;
            }

            List<Vector3> listOfVector0 = new List<Vector3>();
            List<Vector3> listOfVectorNotZero = new List<Vector3>();

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i].y == 0 && (a[i].x !=0||a[i].z !=0))
                {
                    listOfVector0.Add(a[i]);
                }
                else if (a[i].y != 0)
                {
                    listOfVectorNotZero.Add(a[i]);
                }
            }
            if (intelligent <= 3)
            {
                if (listOfVectorNotZero.Count !=0)
                {
                    int temp = Mathf.FloorToInt(MyRandom(listOfVectorNotZero.Count));
                    transform.DOMoveX(listOfVectorNotZero[temp].x, scanFrequence / 60);
                }
                else
                {
                    int temp3 = Random.Range(0, 10);
                    if (temp3 <= 4)
                    {
                        if (RayCastLeft())
                        {
                            transform.DOMoveX(transform.position.x - 0.5f, 1);

                        }
                        else if (RayCastRight())
                        {
                            transform.DOMoveX(transform.position.x + 0.5f, 1);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            else if (intelligent >= 4 && intelligent <= 6)
            {
                int temp = Random.Range(0, 10);
                if (temp <= 4)
                {
                    if (listOfVectorNotZero.Count != 0)
                    {
                        int temp2 = Mathf.FloorToInt(MyRandom(listOfVectorNotZero.Count));
                        transform.DOMoveX(listOfVectorNotZero[temp2].x, scanFrequence / 60);
                    }
                    else
                    {
                        int temp3 = Random.Range(0, 10);
                        if (temp3 <= 4)
                        {
                            if (RayCastLeft())
                            {
                                transform.DOMoveX(transform.position.x - 0.5f, 1);

                            }
                            else if (RayCastRight())
                            {
                                transform.DOMoveX(transform.position.x + 0.5f, 1);
                            }
                        }
                    }
                }
                else
                {
                    if (listOfVector0.Count != 0)
                    {
                        int temp2 = Mathf.FloorToInt(MyRandom(listOfVector0.Count));
                        CaculateX(listOfVector0[temp2]);
                    }
                    else
                    {
                        int temp3 = Random.Range(0, 10);
                        if (temp3 <= 7)
                        {
                            if (RayCastLeft())
                            {
                                transform.DOMoveX(transform.position.x - 0.5f, 1);

                            }
                            else if (RayCastRight())
                            {
                                transform.DOMoveX(transform.position.x + 0.5f, 1);
                            }
                        }
                    }
                }
            }

            else if (intelligent >=7 && intelligent <=9)
            {
                if (listOfVector0.Count!=0)
                {
                    int temp2 = Mathf.FloorToInt(MyRandom(listOfVector0.Count));
                    CaculateX(listOfVector0[temp2]);
                }
                else
                {
                        if (RayCastLeft())
                        {
                            transform.DOMoveX(transform.position.x - 0.5f, 1);

                        }
                        else if (RayCastRight())
                        {
                            transform.DOMoveX(transform.position.x + 0.5f, 1);
                        }
                }

            }
        }
    }

    public bool RayCastLeft()
    {
        if (Physics.Raycast(child.transform.position, -Vector3.right, 1, wallLayer))
        {
            return true;
        }
        return false;
    }

    public bool RayCastRight()
    {
        if (Physics.Raycast(child.transform.position, Vector3.right, 1, obstacleLayer))
        {
            return true;
        }
        return false;
    }

    public int MyRandom(int a)
    {
        return Random.Range(0, a);
    }

    public void CaculateX(Vector3 dir)
    {
        float referenceX = 0;
        referenceX = Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(Vector3.right, dir)) * raycastLength;
        desiredX = transform.position.x + referenceX;
        transform.DOMoveX(desiredX, scanFrequence / 60);

    }

    public void RayCastOnOff()
    {
        if ((int)(Time.frameCount % scanFrequence) == 0)
        {
            rayCastOn = true;
        }
        else if ((int)(Time.frameCount % scanFrequence) != 0)
        {
            rayCastOn = false;
        }
    }

    public void Jump()
    {
        //agent.enabled = false;
        rb.AddForce(new Vector3(rb.velocity.x, 1 * jumpForce, 3.55f), ForceMode.Impulse);
        animator.SetBool("jump", true);
        StartCoroutine(DelayJump());
    }

    public void CheckGround()
    {
        if (Physics.Raycast(child.transform.position, Vector3.down, 0.05f, layer))
        {
            if (checkJump)
            {
                animator.SetBool("jump", false);
                //agent.enabled = true;
                checkJump = false;
            }
        }
    }

    public IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(1);
        checkJump = true;

    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < listOfDesination.Count; i++)
        {
            Gizmos.color = Color.black;

            Gizmos.DrawWireSphere(new Vector3(listOfDesination[i].x, listOfDesination[i].y, listOfDesination[i].z), 0.5f);
        }

        Gizmos.color = Color.red;
        float tempRotation = Y1Rotation;
        float step = Mathf.Abs(Y1Rotation - Y2Rotation) / (numerOfRay - 1);
        for (int i = 0; i < numerOfRay; i++)
        {
            Vector3 dir = Quaternion.Euler(0, tempRotation, 0) * Vector3.forward;
            Gizmos.DrawRay(child.transform.position, Quaternion.Euler(0, tempRotation, 0) * Vector3.forward * raycastLength);
            tempRotation += step;
        }
    }
}
