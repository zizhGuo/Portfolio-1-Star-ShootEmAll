using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    //public float fireRate = 0.25f;
    //protected float nextFire;
    //private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);
    //bool cancelShake = false;
    //private Camera abc;

    //// Use this for initialization
    //void Start()
    //{
    //    abc = GetComponent<Camera>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    nextFire = Time.time + fireRate;
    //    if (Input.GetKeyDown(KeyCode.K) && Time.time > nextFire)
    //    {
    //        //StartCoroutine(Shake());
    //        Shakeit();
    //    }
    //}
    //private void Shakeit()
    //{
        
    //    float t = 0;
    //    Vector3 orgPosition = abc.transform.localPosition;
    //    while (t < 1 && !cancelShake)
    //    {
    //        t += Time.deltaTime * 2.5f;
    //        GetComponent<Camera>().transform.position = orgPosition + new Vector3(Mathf.Sin(14 * t), Mathf.Cos(14 * t), 0) * Mathf.Lerp(0.2f, 0, t);
    //       // yield return 0;
    //    }
    //    cancelShake = false;
    //    //yield return shotDuration;
    //}

    ////private IEnumerator Shake()
    ////{
    ////    bool cancelShake = false;
    ////    float t = 0;
    ////    Camera abc = GetComponent<Camera>();
    ////    Vector3 orgPosition = abc.transform.localPosition;
    ////    while (t < 1 && !cancelShake)
    ////    {
    ////        t += Time.deltaTime * 2.5f;
    ////        GetComponent<Camera>().transform.position = orgPosition + new Vector3(Mathf.Sin(14 * t), Mathf.Cos(14 * t), 0) * Mathf.Lerp(0.2f, 0, t);
    ////        yield return 0;
    ////    }
    ////    cancelShake = false;
    ////    yield return shotDuration;
    ////}



    // 震动标志位
    private bool isshakeCamera = false;
    // 震动幅度
    public float shakeLevel = 3f;
    // 震动时间
    public float setShakeTime = 0.2f;
    // 震动的FPS
    public float shakeFps = 45f;
    private float fps;
    private float shakeTime = 0.0f;
    private float frameTime = 0.0f;
    private float shakeDelta = 0.005f;
    private Camera selfCamera;
    void Awake()
    {
        selfCamera = GetComponent<Camera>();
    }
    // Use this for initialization
    void Start()
    {
        shakeTime = setShakeTime;
        fps = shakeFps;
        frameTime = 0.03f;
        shakeDelta = 0.005f;
    }
    // Update is called once per frame
    void Update()
    {
        if (isshakeCamera)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                if (shakeTime <= 0)
                {
                    selfCamera.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                    isshakeCamera = false;
                    shakeTime = setShakeTime;
                    fps = shakeFps;
                    frameTime = 0.03f;
                    shakeDelta = 0.005f;
                }
                else
                {
                    frameTime += Time.deltaTime;
                    if (frameTime > 1.0 / fps)
                    {
                        frameTime = 0;
                        selfCamera.rect = new Rect(shakeDelta * (-1.0f + shakeLevel * Random.value), shakeDelta * (-1.0f + shakeLevel * Random.value), 1.0f, 1.0f);
                    }
                }
            }
        }
    }
    public void shake()
    {
        isshakeCamera = true;
    }
}