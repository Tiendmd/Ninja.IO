using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MySlider : MonoBehaviour
{
    private SliderParent sliderParent;
    private Slider slider;
    public Transform player;
    private Vector3 startPosition;

    private void Start()
    {
        sliderParent = GetComponentInParent<SliderParent>();
        slider = GetComponent<Slider>();
        startPosition = player.transform.position;
    }

    private void Update()
    {
        slider.value = (player.position.z - startPosition.z) / (sliderParent.finishLine.position.z - player.position.z);
    }

}
