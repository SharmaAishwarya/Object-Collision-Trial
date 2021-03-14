//CREATE OBJECT AT THE LOCATION OF THE CAMERA
//ADD SCRIPT TO THIS OBJECT ^^
//ADD MAIN CAMERA (XR RIG >> CAMERA OFFSET >> MAIN CAMERA) AS THE objectPosition

//this basically tracks the camera but doesn't get rendered so it acts as the body of the study participant

//ALSO THIS DESTROYS MOVING CLONE OBJECT WHEN IT COLLIDES WITH THIS TRACKING OBJECT
//(we might be able to add a write to file function in the on collision enter method)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCamera : MonoBehaviour
{
    public Transform objectPosition;
    float speed = 10.0f;
    public Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(objectPosition.position);
        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    }
	
	private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
    }
}
