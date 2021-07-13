using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    public GameObject player;
    public float smoothSpeed;
    public float smoothZSpeed;
    public float smoothXSpeed;
    public PlayerManager playerManager;
    public LayerMask groundLayer;

    //public Vector3 originRotation;


    private float xTemp;
    private Vector3 tempPos;
    public Camera subCam;
    private float tempVal;

    public Transform subObj;

    private void Start()
    {
        //originRotation = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    //private void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 50, groundLayer))
    //        {
    //            xTemp = hit.point.x;
    //        }

    //        tempVal = Input.mousePosition.x;
    //    }

    //    if (Input.GetMouseButton(0))
    //    {

    //        player.position = Vector3.Lerp(player.position, new Vector3(tempPos.x, player.position.y, player.position.z), 0.25f);

    //        if (Mathf.Abs(Input.mousePosition.x - tempVal) < 2)
    //            return;


    //        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 50, groundLayer))
    //        {

    //            tempPos = hit.point;

    //            subObj.position = tempPos;
    //            //player.position = new Vector3(tempPos.x, player.position.y, player.position.z);

    //            tempVal = Input.mousePosition.x;

    //            //if (Mathf.Abs(xTemp - hit.point.x) >= 0.1f)
    //            //{
    //            //    tempPos = hit.point;
    //            //}

    //        }
    //    }
    //}

    private void Update()
    {
        if (player != null)
        {
            float desiredZPosition = player.transform.position.z + offset.z;
            float smoothZPosition = Mathf.Lerp(transform.position.z, desiredZPosition, smoothZSpeed * Time.deltaTime);

            float desiredXPosition = player.transform.position.x + offset.x;
            float smoothXPosition = Mathf.Lerp(transform.position.x, desiredXPosition, smoothXSpeed * Time.deltaTime);

            //transform.position = new Vector3(smoothXPosition, offset.y, player.transform.position.z + offset.z);

            //if (player.position.x >= -2f && player.position.x <= 2f)
            //{
            //    transform.position = new Vector3(smoothXPosition, offset.y, player.transform.position.z + offset.z);
            //    //transform.position = player.position + offset;
            //}

            transform.position = new Vector3(smoothXPosition, offset.y, player.transform.position.z + offset.z);
            //transform.position = player.position + offset;

            //transform.position = new Vector3(smoothXPosition,
            //    transform.position.y, smoothZPosition);

            //transform.position = new Vector3(Mathf.Lerp(transform.position.x, player.transform.position.x + offset.x, smoothXSpeed),
            //    transform.position.y, Mathf.Lerp(transform.position.z, player.transform.position.z + offset.z, smoothZSpeed));

        }
    }

}
