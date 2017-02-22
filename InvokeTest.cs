using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeTest : MonoBehaviour
{
    public GameObject sphere;
    private int sphereNum = 0;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Produce", 2, 1.5f);
       // Destroy(sphere, 2f); assign to prefab, this is not gonna work.
    }
    void Produce()
    {
        float x = Random.Range(3f, 22f);
        float z = Random.Range(3f, 22f);
        Instantiate(sphere, new Vector3(x, 8, z), Quaternion.identity);
        sphereNum++;


    }
    // Update is called once per frame
    void Update()
    {
        if (sphereNum == 10)
        {
            Application.Quit();
        }
    }
}
