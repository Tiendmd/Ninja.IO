using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool moveForward;
    [Header("MoveSpeed")]
    public float moveSpeed;
    public float slowSpeed;
    public float originMoveSpeed;

    [Header("Jump")]
    public float jumpForce;
    public GameObject child;
    public LayerMask layer;
    private bool checkJump;
    [Header("Component")]
    public Rigidbody rb;
    private PlayerManager playerManager;
    private Animator animator;
    [Header("Slope")]
    public float raycastLength;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerManager = GetComponent<PlayerManager>();
        animator = GetComponentInChildren<Animator>();
    }
    public float defaultSpeedForward = 5;
    public float speedForward = 5;
    public float halfRange;

    public float sensitive = 1;

    protected Vector2 lastCursorPosition;

    private void Update()
    {
        //MoveForward();
        if (playerManager.canMove && Input.GetMouseButtonDown(0))
        {
            lastCursorPosition = WorldMousePos();
        }
        else if (playerManager.canMove && Input.GetMouseButton(0))
        {
            Vector2 delta = WorldMousePos() - lastCursorPosition;

            MoveHorizontal(delta.x / Screen.width * sensitive * halfRange);

            lastCursorPosition = Input.mousePosition;
        }
        CheckGround();
    }
    private void FixedUpdate()
    {
        if (MoveForward())
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 1 * moveSpeed);

        }
        if (!MoveForward())
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

    }
    public bool MoveForward()
    {
        if (Input.GetMouseButtonUp(0))
        {
            //moveForward = false;
            return false;
        }
        if (playerManager.canMove && Input.GetMouseButton(0))
        {
            //moveForward = true;
            return true;
        }
        return false;
    }

    public Vector2 WorldMousePos() => Input.mousePosition;


    protected void MoveHorizontal(float move)
    {
        //float deltaX = Mathf.Clamp(transform.position.x + move, -halfRange, halfRange) - transform.position.x;
        rb.position = new Vector3(Mathf.Clamp(transform.position.x + move, -halfRange, halfRange), rb.position.y, rb.position.z);
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        checkJump = true;

    }

    public void Jump()
    {
        playerManager.jumping = true;
        rb.AddForce(new Vector3(rb.velocity.x, 1 * jumpForce, rb.velocity.z), ForceMode.Impulse);
        animator.SetBool("jump", true);
        StartCoroutine(Delay());
    }

    public void CheckGround()
    {
        if (Physics.Raycast(child.transform.position, Vector3.down, 0.05f, layer))
        {
            if (checkJump)
            {
                animator.SetBool("jump", false);
                checkJump = false;
                playerManager.jumping = false;
            }
        }
    }

}
