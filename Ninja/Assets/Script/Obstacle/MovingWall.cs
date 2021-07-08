using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingWall : MonoBehaviour
{
    public List<float> points = new List<float>();
    public float lerpTime;
    private int c = 0;

    private void Start()
    {
        MoveWallToPosition(points[c]);
    }

    private void Update()
    {
        CheckReachPoint();
    }

    public void MoveWallToPosition(float point)
    {
        transform.DOMoveX(point, lerpTime).SetEase(Ease.Linear);
    }

    public void CheckReachPoint()
    {

        if (Vector3.Distance(transform.position, new Vector3(points[c], transform.position.y, transform.position.z)) <= 0.1f && c == points.Count - 1)
        {
            c = 0;
            MoveWallToPosition(points[c]);
        }
        else if (Vector3.Distance(transform.position, new Vector3(points[c], transform.position.y, transform.position.z)) <= 0.1f)
        {
            c++;
            MoveWallToPosition(points[c]);
        }
    }
}
