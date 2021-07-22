using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyMovement : MonoBehaviour
{
    private bool isPushed;
    public Rigidbody rb { get; set; }
    public float rbSpeed;
    public float rbSpeedOrigin;
    public Animator animator { get; set; }
    private bool oneTime = true;

    public float jumpForce;
    public GameObject child;
    public LayerMask layer;
    private bool checkJump;

    [Header("Slide")]
    public float slideForce;
    [Header("CheckForward")]
    public LayerMask obstacleLayer;
    public LayerMask wallLayer;
    public float Y1Rotation;
    private float Y2Rotation;
    private bool rayCastOn;
    public float raycastLength;
    public int numerOfRay;
    public int scanFrequence;
    [Range(1,9)]
    public int intelligent;
    private float desiredX;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        Y2Rotation = -Y1Rotation;
        rbSpeedOrigin = rbSpeed;
        rayCastOn = true;
    }   

    private void Update()
    {
        if (MyScene.Instance.gameIsStart && !isPushed)
        {
            RayCastCheck();
            RayCastOnOff();
            CheckGround();
        }
    }

    private void FixedUpdate()
    {
        if (MyScene.Instance.gameIsStart && !isPushed)
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
                    transform.DOMoveX(listOfVectorNotZero[temp].x, 1);
                }
                else
                {
                    int temp3 = Random.Range(0, 10);
                    if (temp3 <= 4 && FrameCount(30))
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
                        transform.DOMoveX(listOfVectorNotZero[temp2].x, 1);
                    }
                    else
                    {
                        int temp3 = Random.Range(0, 10);
                        if (temp3 <= 4 && FrameCount(30))
                        {
                            if (RayCastLeft())
                            {
                                transform.DOMoveX(transform.position.x + 0.5f, 1);
                            }
                            else if (RayCastRight())
                            {
                                transform.DOMoveX(transform.position.x - 0.5f, 1);
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
                        if (temp3 <= 7 && FrameCount(30))
                        {
                            if (RayCastLeft())
                            {
                                transform.DOMoveX(transform.position.x + 1f, 1);

                            }
                            else if (RayCastRight())
                            {
                                transform.DOMoveX(transform.position.x - 1f, 1);
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
                            transform.DOMoveX(transform.position.x + 1.5f, 2);
                        }
                        else if (RayCastRight())
                        {
                            transform.DOMoveX(transform.position.x - 1.5f, 2);
                        }
                    
                }

            }
        }
    }

    public bool FrameCount(int a)
    {
        if ((int)(Time.frameCount % a )== 0)
        {
            return true;
        }
        return false;
    }

    public bool RayCastLeft()
    {
        if (Physics.Raycast(child.transform.position, -Vector3.right, 3, wallLayer))
        {
            return true;
        }
        return false;
    }


    public bool RayCastRight()
    {
        if (Physics.Raycast(child.transform.position, Vector3.right, 3, obstacleLayer))
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
        transform.DOMoveX(desiredX, 1);

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
        rb.AddForce(new Vector3(rb.velocity.x, 1 * jumpForce, 3.55f), ForceMode.Impulse);
        animator.SetBool("jump", true);
        StartCoroutine(DelayJump());
    }

    public void SuperJump()
    {
        rb.AddForce(new Vector3(rb.velocity.x, 2.5f * jumpForce, 3.55f), ForceMode.Impulse);
        animator.SetBool("jump", true);
        StartCoroutine(DelayJump());
    }

    public IEnumerator PushBack(Vector3 dir)
    {
        rb.velocity = Vector3.zero;
        animator.SetBool("stun", true);
        isPushed = true;
        rb.AddForce(dir, ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("stun", false);
        isPushed = false;
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

        Gizmos.color = Color.red;
        float tempRotation = Y1Rotation;
        float step = Mathf.Abs(Y1Rotation - Y2Rotation) / (numerOfRay - 1);
        for (int i = 0; i < numerOfRay; i++)
        {
            Vector3 dir = Quaternion.Euler(0, tempRotation, 0) * Vector3.forward;
            Gizmos.DrawRay(child.transform.position, Quaternion.Euler(0, tempRotation, 0) * Vector3.forward * raycastLength);
            tempRotation += step;
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(child.transform.position, Vector3.right*2);
        Gizmos.DrawRay(child.transform.position, -Vector3.right*2);
    }
}
