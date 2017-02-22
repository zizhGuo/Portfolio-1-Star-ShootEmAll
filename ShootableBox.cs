using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableBox : MonoBehaviour
{
    public int currentHealth = 4;

    // Use this for initialization
    public void Damage(int damageAmount)
    {
        //subtract damage amount when Damage function is called
        currentHealth -= damageAmount;
        if (currentHealth > 0 && currentHealth<= 4)
        {
            GetComponent<Renderer>().material.color = new Color(100f, 0, 0);
        }


        //Check if health has fallen below zero
        if (currentHealth <= 0)
        {
            //if health has fallen below zero, deactivate it 

            gameObject.SetActive(false);

        }
    }

    void Start()
    {

    }
    private void OnMouseOver()
    {
        GetComponent<Renderer>().material.color -= new Color(2f, 0, 0) * Time.deltaTime;
    }
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
