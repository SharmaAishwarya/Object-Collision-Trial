//APPLY TO CLONED OBJECT 
//MOVES OBJECT AND AFTER 3 SECONDS IT STOPS THE RENDERING BUT DOES NOT DESTROY THE OBJECT

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Renderer rend;
    
    //timers
    private float moveTime = 3.0f;
    private float timer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Renderer>().material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > moveTime)
        {
            rend = GetComponent<Renderer>();
            rend.enabled = false;
        }
        transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
    }

}
