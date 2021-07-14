using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScene : MonoBehaviour
{
    public static MyScene Instance;
    public bool gameIsStart { get; set; }
    public float finishZ;
    public List<TeacherAI> listOfTeacher = new List<TeacherAI>();
    public int placeCount = 0;
    private void Awake()
    {
        Instance = this;
    }


}
