using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cradle_Item_Move : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform tr;
    private int speed = 3;


    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(-1, -1, 0).normalized * speed * Time.deltaTime;

        tr.position += move;

        if (tr.transform.position.y < 18f)
        {
            tr.position = new Vector3(55f, 35f, 0);
        }
    }
}
