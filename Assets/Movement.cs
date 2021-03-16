//APPLY TO CLONED OBJECT 
//MOVES OBJECT AND AFTER 3 SECONDS IT STOPS THE RENDERING BUT DOES NOT DESTROY THE OBJECT

using Random = System.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
   

   // public float moveSpeed = 2f;
    public Renderer rend;

    //timers
    private float moveTime = 3.0f;
    private float timer = 0.0f;
    float moveSpeed;

    // Start is called before the first frame update
    void Start() {

        randomOrder(PrimaryReactor.speed);
        moveSpeed = (float)PrimaryReactor.speed[0];
        
        
    }

// Update is called once per frame
void Update()
    {
        timer += Time.deltaTime;
        if (timer > moveTime)
        {
            rend = GetComponent<Renderer>();
            rend.enabled = false;
            Debug.Log(" The sphere disappeared at " +DateTime.Now);
        }
        // Debug.Log(" The speed is " + moveSpeed + " capacity " + PrimaryReactor.speed.Count);
        transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
    }
public void randomOrder(ArrayList arrList)
    {
        Random r = new Random();
        for (int cnt = 0; cnt < arrList.Count; cnt++)
        {
            object tmp = arrList[cnt];
            int idx = r.Next(arrList.Count - cnt) + cnt;
            arrList[cnt] = arrList[idx];
            arrList[idx] = tmp;
        }

    }

}
