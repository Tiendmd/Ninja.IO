using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningFan : MonoBehaviour
{
    private float a;
    public float speed;
    // Update is called once per frame
    void FixedUpdate()
    {
        a -= Time.deltaTime * speed ;
        transform.rotation = Quaternion.Euler(transform.rotation.x, a, transform.rotation.z);
    }
}
