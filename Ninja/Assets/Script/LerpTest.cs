using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;

public class LerpTest : MonoBehaviour
{
    [Range(0f, 5f)]
    public float speed;
    public Transform player;
    public Transform childOfPlayer;
    private Camera cam;
    public LayerMask ground;
    public Camera raycastCam;
    public bool isMove;
    public Vector3 offset, lastHit, offset2;
    Vector3 newHit;
    Vector3 lastScreenPos;
    Ray ray;

    public Transform subCam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        offset = transform.position - childOfPlayer.position;
        offset2 = subCam.position - childOfPlayer.position;
    }

    void Update()
    {
        if (childOfPlayer == null)
        {
            return;
        }
        if (player.GetComponent<PlayerManager>().canSlide)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var inputPos = Input.mousePosition;
                ray = raycastCam.ScreenPointToRay(inputPos);
                if (Physics.Raycast(ray, out RaycastHit hit, 200, ground))
                {
                    lastHit = hit.point;
                }
            }
            if (Input.GetMouseButton(0))
            {
                var inputPos = Input.mousePosition;
                ray = raycastCam.ScreenPointToRay(inputPos);
                if (Physics.Raycast(ray, out RaycastHit hit, 200, ground))
                {
                    newHit = hit.point;
                }
                float delta = (newHit.x - lastHit.x);
                if (delta != 0)
                {
                    //player.GetComponent<PlayerMovement>().rb.position = new Vector3(Mathf.Clamp(player.GetComponent<PlayerMovement>().rb.position.x + delta * speed, -2.75f, 2.75f), player.GetComponent<PlayerMovement>().rb.position.y, player.GetComponent<PlayerMovement>().rb.position.z);
                    player.position = new Vector3(Mathf.Clamp(player.position.x + delta * speed, -2.75f, 2.75f), player.position.y, player.position.z);
                    lastHit = newHit;
                }
            }
        }


        //if (Input.GetMouseButton(0))
        //{
        //    player.position = Vector3.Lerp(player.position, new Vector3(lastHit.x * speed, player.position.y, player.position.z), 15 * Time.deltaTime);
        //    var inputPos = Input.mousePosition;

        //    ray = raycastCam.ScreenPointToRay(inputPos);

        //    if (Physics.Raycast(ray, out RaycastHit hit, 200, ground))
        //    {

        //        lastHit = hit.point;
        //        lastScreenPos = inputPos;
        //    }
        //}
    }

    private void LateUpdate()
    {
        if (childOfPlayer == null)
        {
            return;
        }
        if (isMove)
        {
            var dir = (subCam.position - childOfPlayer.position).normalized;
            //subCam.position= new Vector3(subCam.position.x, subCam.position.y, childOfPlayer.position.z + offset2.z);
            //transform.position = Vector3.Lerp(transform.position, childOfPlayer.position + dir * offset.magnitude, 0.1f);
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, childOfPlayer.position.x + dir.x * offset.x, 0.1f),
                transform.position.y, childOfPlayer.position.z + offset.z);

        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(ray.origin, ray.direction * 500);
    }
}
