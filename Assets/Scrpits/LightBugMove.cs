using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBugMove : MonoBehaviour
{
    public int check;
    public GameObject rotateAround;
    public float speed;
    void FixedUpdate()
    {
        if(check == 0)
        {
            transform.RotateAround(rotateAround.transform.position, Vector3.forward, speed);
        }
        else
        {
            transform.RotateAround(rotateAround.transform.position, Vector3.forward * -1, speed);
        }
    }
}
