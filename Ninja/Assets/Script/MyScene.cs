using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScene : MonoBehaviour
{
    public static MyScene Instance;
    public bool gameIsStart;
    public bool gameIsFinish;
    public float finishZ;
    public List<GameObject> listOfTeacher = new List<GameObject>();
    public int placeCount = 0;

    public PlayerInput playerInput;
    public List<EnemyManager> enemysManager = new List<EnemyManager>();
    public bool oneTime = false;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (oneTime)
        {
            StartCoroutine(Delay());
        }
    }

    public IEnumerator Delay()
    {
        oneTime = false;
        UIManager.Instance.StartGame();
        for (int i = 0; i < enemysManager.Count; i++)
        {
            enemysManager[i].animator.SetTrigger("prepare_run");
        }
        playerInput.animator.SetTrigger("prepare_run");
        yield return new WaitForSeconds(3);
        gameIsStart = true;
    }
}
