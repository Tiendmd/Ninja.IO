using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    private List<Transform> list = new List<Transform>();
    public GameObject flamePrefab;
    private List<GameObject> temp = new List<GameObject>();
    public GameObject trigger;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            list.Add(transform.GetChild(i));
        }
        InvokeRepeating("InstanceFlame", 1, 7);
    }

    public void InstanceFlame()
    {
        trigger.SetActive(true);
        for (int i = 0; i < list.Count; i++)
        {
            temp.Add(Instantiate(flamePrefab, list[i].transform.position, Quaternion.Euler(-90, 0, 0), list[i].transform));
        }
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        trigger.SetActive(false);
        for (int i = 0; i < temp.Count; i++)
        {
            Destroy(temp[i].gameObject);
        }
    }
}
