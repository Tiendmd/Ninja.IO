using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputEventsSystem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    public PlayerManager playerManager;
    public CameraFollow cameraFollow;
    Vector3 cameraToWord;
    public float halfRange;
    float x1;


    private void Update()
    {
        Debug.Log(cameraToWord);
    }
    private void SetDraggedPosition(PointerEventData data)
    {

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //cameraToWord = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        //x1 = cameraToWord.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
    //    cameraToWord = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));

    //    playerManager.gameObject.transform.position = new Vector3(Mathf.Clamp((playerManager.gameObject.transform.position.x + cameraToWord.x - x1), -halfRange, halfRange),
    //playerManager.gameObject.transform.position.y, playerManager.gameObject.transform.position.z) ;
    //    //Debug.Log(new Vector3(Mathf.Clamp(playerManager.gameObject.transform.position.x + cameraToWord.x - x1, -halfRange, halfRange),
    //    //playerManager.gameObject.transform.position.y, playerManager.gameObject.transform.position.z));
    //    x1 = cameraToWord.x;
        //cameraFollow.transform.position = playerManager.transform.position + cameraFollow.offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //cameraToWord = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        //x1 = cameraToWord.x;
        Debug.Log(x1);
    }
}
