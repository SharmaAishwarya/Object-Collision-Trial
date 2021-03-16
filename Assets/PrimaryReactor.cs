//APPLY TO OBJECT IN SCENE -- IT WILL NOT BE RENDERED

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.PhysicsModule;
using System.IO;
using System;



public class PrimaryReactor : MonoBehaviour
{
    public static ArrayList speed = new ArrayList();
       

    public PrimaryButtonWatcher watcher;	//empty game object with PrimaryButtonWatcher.cs script
    public bool IsPressed = false;

    public GameObject capsuleClone;	//add an object that will be cloned (don't use the object this script is applied to)
	//Movement.cs will be applied to ^^ this object
	public Transform startPosition;	//empty game object
    public Renderer rend;


    void Start()
    {
        speed.Add(2f);
        speed.Add(2f);
        speed.Add(2f);
        speed.Add(6f);
        speed.Add(6f);
        speed.Add(6f);
        speed.Add(6f);
        speed.Add(10f);
        speed.Add(10f);
        speed.Add(10f);
        watcher.primaryButtonPress.AddListener(onPrimaryButtonEvent);
        rend = GetComponent<Renderer>();
        rend.enabled = false;   //don't show the main capsule object

    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        GameObject capsuleInstance;
        IsPressed = pressed;
        if (!pressed)
        {
            Debug.Log(" button pressed in PrimaryReactor ");
            if (PrimaryReactor.speed.Count > 0)
                PrimaryReactor.speed.RemoveAt(0);
            Debug.Log( " count " + speed.Count);
            Debug.Log(" position of gameObject " + startPosition.position );
            capsuleInstance = Instantiate(capsuleClone, startPosition.position, startPosition.rotation) as GameObject;
        }
            
    }

    

}