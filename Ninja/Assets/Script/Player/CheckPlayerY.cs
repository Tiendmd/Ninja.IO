using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerY : MonoBehaviour
{
    PlayerManager playerManager;
    public bool oneTime = true;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    private void Update()
    {
        if (transform.position.y <= 0.1f && oneTime)
        {
            playerManager.PlayerFall();
            oneTime = false;
        }
    }

    IEnumerator DelaySetBool()
    {
        yield return new WaitForSeconds(playerManager.timeBetweenResurrect);
        oneTime = true;
    }

}