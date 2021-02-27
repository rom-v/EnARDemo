using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    public float speed=1;

    void Update()
    {
        transform.eulerAngles += new Vector3(0,0, speed) * Time.deltaTime;
    }
}
