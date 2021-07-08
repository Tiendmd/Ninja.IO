using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickWall : MonoBehaviour
{
    public GameObject brick;
    private float brickX;
    private float brickY;
    private float brickZ;
    private int c = 0;
    private Vector3 d1;


    private void Start()
    {
        brickX = brick.transform.localScale.x;
        brickY = brick.transform.localScale.y;
        brickZ = brick.transform.localScale.z;
        InstanceBrick();
    }

    public void InstanceBrick()
    {
        for (int i = 0; i < 4; i++)
        {
            if (c % 2 == 0)
            {
                d1 = transform.position + new Vector3(-brickX - (brickX / 2), brickY / 2 + brickY * c, 0);
            }
            else if (c % 2 != 0)
            {
                d1 = transform.position + new Vector3(-brickX, brickY / 2 + brickY * c, 0);
            }
            Vector3 temp = d1;
            if (c % 2 == 0)
            {
                for (int j = 0; j < 4; j++)
                {
                    Instantiate(brick, temp, Quaternion.identity, transform);
                    temp += new Vector3(brickX, 0, 0);
                }
            }
            else if (c % 2 != 0)
            {
                for (int j = 0; j < 3; j++)
                {
                    Instantiate(brick, temp, Quaternion.identity, transform);
                    temp += new Vector3(brickX, 0, 0);
                }
            }
            c++;
        }
    }
}
