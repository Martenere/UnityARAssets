using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Bounce : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    int force = 25;
 
    void OnCollisionEnter(Collision col) {            
        Debug.Log(gameObject.GetComponent<Rigidbody>().velocity.y);
        if (gameObject.GetComponent<Rigidbody>()) {
            gameObject.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3 (gameObject.GetComponent<Rigidbody>().velocity.x, Math.Abs(gameObject.GetComponent<Rigidbody>().velocity.y) + force, gameObject.GetComponent<Rigidbody>().velocity.z));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
