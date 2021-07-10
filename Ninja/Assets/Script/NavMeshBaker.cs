using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    public NavMeshSurface[] list;
    // Update is called once per frame
    private void Start()
    {
        for (int i = 0; i < list.Length; i++)
        {
            list[i].BuildNavMesh();
        }
    }
}
