using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteracableUI : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    public PlayerInput playerInput;
    public EnemyManager enemyManager;
    private bool oneTime = true;
    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MyScene.Instance.oneTime = true;
    }


}
