using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // ko phai do cai nay r =)) chiu =))
    // m nghi do cai gi da do cai update no update cai z lien tuc nen no bi cham la ??
    // m noi cai gi the
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * 3;
        if (Input.GetKey(KeyCode.C))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
