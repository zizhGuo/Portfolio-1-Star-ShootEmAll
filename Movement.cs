using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 0.5f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xDistance = Input.GetAxis("Vertical");
        float zDistance = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(xDistance, 0, -zDistance) * walkSpeed * Time.deltaTime);
    }
}
