using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmosRed : MonoBehaviour
{
    private EnemyMovement enemyMovement;
    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }


}
