using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 offset2;
    public GameObject player;
    public float smoothSpeed;
    public float smoothZSpeed;
    public float smoothXSpeed;
    public Vector3 rotation1;
    public Vector3 rotation2;
    private void Start()
    {
        //originRotation = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }
        if (player != null)
        {
            float desiredZPosition = player.transform.position.z + offset.z;
            float smoothZPosition = Mathf.Lerp(transform.position.z, desiredZPosition, smoothZSpeed * Time.deltaTime);

            float desiredXPosition = player.transform.position.x + offset.x;
            float smoothXPosition = Mathf.Lerp(transform.position.x, desiredXPosition, smoothXSpeed * Time.deltaTime);
            transform.position = new Vector3(smoothXPosition, player.transform.position.y + offset.y, player.transform.position.z + offset.z);

        }
    }

}
