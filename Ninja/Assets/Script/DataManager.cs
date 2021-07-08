using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public bool gameIsStart;
    public float finishZ;
    public List<TeacherAI> listOfTeacher = new List<TeacherAI>();

    private void Awake()
    {
        Instance = this;
    }
    

}
