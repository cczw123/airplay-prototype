using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataserver : MonoBehaviour
{
    private void Start()
    {
        datasir data1 = new datasir();
        string json = JsonUtility.ToJson(data1);
        Debug.Log(json);
    }

}

public class datasir
{

    
    public float targetdiameter = 0.36f;
    public float mintargetspeed = 0.08f;
    public float maxtargetspeed = 5.86f;
    public float goalsize = 2.1f;
    public float friction = 0.33f;
    public float boudaryelacity = 90;
    public float maxdiameterofcircle = 2.52f;
    public float maxspeedofexpansion = 20;
    public float maxholdtimeofcircle = 1.5f;

}


