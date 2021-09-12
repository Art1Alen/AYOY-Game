using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boom : MonoBehaviour
{
    public GameObject deathCube;
    Image timerBar;
    public float maTime = 10f;
    float timeLeft;
    public  Color colorG;



    private void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maTime;
    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maTime;

        }
        else
        {  
            gameObject.GetComponent<Renderer>().material.color = Color.red;                       
            Invoke("Exploce", 1f);
        }
    }

    public float Radius;
    public float Force;
    //public float timer, coolDown;
    //public bool Active;
    public GameObject ExplosionEffect;

    //private void Update()
    //{
    //    if (Active )
    //    {
    //        Exploce();
    //    }
    //}

    //void Update()
    //{
    //    if (timer > 0)
    //    {
    //        timer -= Time.deltaTime;
    //    }
    //    if (timer <= 10)
    //    {
    //        timer = coolDown;
    //        Exploce();
    //    }
    //}

    public void Exploce()
    {
        Collider[] overLappedColliders = Physics.OverlapSphere(transform.position, Radius);

        for (int i = 0; i < overLappedColliders.Length; i++)
        {
            Rigidbody rigidbody = overLappedColliders[i].attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.AddExplosionForce(Force, transform.position, Radius, 1f);
            }
        }

        Destroy(deathCube);
        Destroy(ExplosionEffect);
        Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
        
    }
}
