using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMove : MonoBehaviour
{
    private SwerveInput _swerveInputSystem;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;

    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerveInput>();
    }

    private void Update()
    {
        float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX ;
        //swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        //transform.Translate(Mathf.Clamp(swerveAmount, -2.5f, 2.5f), 0, 0);
        transform.Translate(swerveAmount, 0, 0);
    }
}
