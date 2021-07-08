using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("MoveSpeed")]
    public float moveSpeed;
    public float slowSpeed;
    public float originMoveSpeed;

    [Header("Component")]
    public Rigidbody rb;
    private PlayerManager playerManager;
    private Animator animator;
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
    public bool isOn = false;

    protected Vector2 lastCursorPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastCursorPosition = WorldMousePos();
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 delta = WorldMousePos() - lastCursorPosition;

            if (isOn)
                MoveHorizontal(delta.x / Screen.width * sensitive * halfRange);

            lastCursorPosition = Input.mousePosition;
        }
    }
    private void FixedUpdate()
    {
        MoveForward();

    }
    public void MoveForward()
    {
        if (playerManager.canMove && Input.GetMouseButton(0))
        {
            rb.velocity = transform.forward * moveSpeed;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }

    //protected void MoveForward()
    //{
    //    if (playerManager.canMove && Input.GetMouseButton(0))
    //        //    {
    //        //        rb.velocity = transform.forward * moveSpeed;
    //        //    }
    //        //    else
    //        //    {
    //        //        rb.velocity = Vector3.zero;
    //        //    }
    //        rb.position += Vector3.forward * Time.deltaTime * speedForward;
    //    else rb.velocity = Vector3.zero;
    //}

    public Vector2 WorldMousePos() => Input.mousePosition;


    protected void MoveHorizontal(float move)
    {
        float deltaX = Mathf.Clamp(transform.position.x + move, -halfRange, halfRange) - transform.position.x;
        //rb.position += Vector3.right * deltaX;
        rb.position = new Vector3(Mathf.Clamp(transform.position.x + move, -halfRange, halfRange), rb.position.y, rb.position.z);
    }
}
