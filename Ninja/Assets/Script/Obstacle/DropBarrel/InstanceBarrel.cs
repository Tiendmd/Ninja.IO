using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceBarrel : MonoBehaviour
{
    public GameObject barrel;
    public Transform instancePosition;
    public float a;
    private void Start()
    {
        InvokeRepeating("InstancingBarrel", 0, a);
    }

    public void InstancingBarrel()
    {
        GameObject a = Instantiate(barrel, instancePosition.position, Quaternion.identity);
        StartCoroutine(Delay(a));
    }

    IEnumerator Delay(GameObject a)
    {
        yield return new WaitForSeconds(12);
        Destroy(a);
    }
}
