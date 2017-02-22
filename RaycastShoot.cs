using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShoot : MonoBehaviour
{
    public int gunDamage = 1;
    public int gunDamage2 = 2;// Set the number of hitpoints that this gun will take away from shot objects with a health script
    public float fireRate = 0.25f;                                      // Number in seconds which controls how often the player can fire
    public float weaponRange = 50f;                                     // Distance in Unity units over which the player can fire
    public float hitForce = 200f;                                       // Amount of force which will be added to objects with a rigidbody shot by the player
    public Transform gunEnd;
    public float recoilRotate = 30f;
    private AudioSource gunAudio;
    public Rigidbody bulletPrefab; // 射出子弹
    public float bulletSpeed = 3000f;
    float timer = 0;
    float timer2 = 0;
    bool Top = false;

    private Camera fpsCam;                                              // Holds a reference to the first person camera
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
    private Transform originalTransform;
    private LineRenderer laserLine;                                     // Reference to the LineRenderer component which will display our laserline
    protected float nextFire;
    private int boxNum = 0;// Float to store the time the player will be allowed to fire again, after firing
    //private int numPro = 0;

    // Use this for initialization
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Rigidbody bulletInstance = Instantiate(bulletPrefab, gunEnd.position, gunEnd.rotation) as Rigidbody; //实例化子弹
            bulletInstance.AddForce(gunEnd.forward * bulletSpeed); // 设置子弹射击方向

            StartCoroutine(ShotEffect());

            //originalTransform = GetComponentInChildren<Transform>();

            // recoilRotate = Random.Range(2.8f, 3.2f); // Randomdize the recoil angle


            //  transform.position = originalTransform.position;

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);

                ShootableBox health = hit.collider.GetComponent<ShootableBox>();

                if (health != null)
                {
                    health.Damage(gunDamage);                    

                }

                    if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
        }
        if (Input.GetButton("Fire2")&& Time.time > nextFire)
        {

            nextFire = Time.time + 1f;
            Rigidbody bulletInstance = Instantiate(bulletPrefab, gunEnd.position, gunEnd.rotation) as Rigidbody; //实例化子弹
            bulletInstance.AddForce(gunEnd.forward * bulletSpeed); // 设置子弹射击方向
            StartCoroutine(ShotEffect2());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);

                ShootableBox health = hit.collider.GetComponent<ShootableBox>();

                if (health != null)
                {
                    health.Damage(gunDamage2);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }

            //    if (timer < 0.2)
            //    {
            //        transform.Rotate(Vector3.left * 30f * Time.deltaTime);
            //    }
            //    timer += Time.deltaTime;
            //    if (timer >= 0.2) 
            //    {
            //        Top = true;
            //    }
            //}
            //if (Top)
            //{
            //    if (timer2 < 0.2)
            //    {
            //        transform.Rotate(Vector3.right * 30f * Time.deltaTime);
            //    }
            //    timer2 += Time.deltaTime;
            //    if (timer2 >= 0.2)
            //    {
            //        Top = false;
            //        timer = 0;
            //        timer2 = 0;
            //    }



        }
        //if (timer >= 1)
        //{
        //    transform.Rotate(Vector3.right * 10f * Time.deltaTime);
        //    timer = 0;
        //}


    }

    private IEnumerator ShotEffect2()
    {
        gunAudio.Play();

        for (float timer = 0; timer < 0.1f; timer += Time.deltaTime)
        {
            transform.Rotate(Vector3.left * 5.5f);
            yield return 0;
        }

        for (float timer = 0; timer < 0.03f; timer += Time.deltaTime)
        {
            transform.Translate(new Vector3(0, 2f, 0) * 0.002f);
            yield return 0;
        }

        for (float timer = 0; timer < 0.01f; timer += Time.deltaTime)
        {
            yield return 0;
        }

        for (float timer = 0; timer < 0.03f; timer += Time.deltaTime)
        {
            transform.Translate(new Vector3(0, -2f, 0) * 0.002f);
            yield return 0;
        }

        for (float timer = 0; timer < 0.1f; timer += Time.deltaTime)
        {

            transform.Rotate(Vector3.right * 5.5f);
            //transform.position = originalTransform.position;
            yield return 0;
        }


        yield return shotDuration;
        laserLine.enabled = false;
        yield return 0;
    }

    private IEnumerator ShotEffect()
    {
        laserLine.enabled = true;
        gunAudio.Play();
        //for (float timer = 0; timer < 0.1f; timer += Time.deltaTime)
        //{
        //    int audioNum = Random.Range(1, 4);
        //    if (audioNum > 0 && audioNum <= 1)
        //    {

        //    }
        //    yield return 0;
        //}
        //originalTransform = GetComponent<Transform>();
        for (float timer = 0; timer < 0.1f; timer += Time.deltaTime)
        {
            transform.Rotate(Vector3.left * 3f);
            yield return 0;
        }

        for (float timer = 0; timer < 0.03f; timer += Time.deltaTime)
        {
            transform.Translate(new Vector3(0, 1, 0) * 0.002f);
            yield return 0;
        }

        for (float timer = 0; timer < 0.01f; timer += Time.deltaTime)
        {
            yield return 0;
        }

        for (float timer = 0; timer < 0.03f; timer += Time.deltaTime)
        {
            transform.Translate(new Vector3(0, -1, 0) * 0.002f);
            yield return 0;
        }

        for (float timer = 0; timer < 0.1f; timer += Time.deltaTime)
        {

            transform.Rotate(Vector3.right * 3f);
            //transform.position = originalTransform.position;
            yield return 0;
        }


        yield return shotDuration;
        laserLine.enabled = false;
    }

}
