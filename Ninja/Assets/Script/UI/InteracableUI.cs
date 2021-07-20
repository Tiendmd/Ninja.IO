using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteracableUI : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MyScene.Instance.oneTime = true;
    }


}
