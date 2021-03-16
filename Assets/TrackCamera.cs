//CREATE OBJECT AT THE LOCATION OF THE CAMERA
//ADD SCRIPT TO THIS OBJECT ^^
//ADD MAIN CAMERA (XR RIG >> CAMERA OFFSET >> MAIN CAMERA) AS THE objectPosition

//this basically tracks the camera but doesn't get rendered so it acts as the body of the study participant

//ALSO THIS DESTROYS MOVING CLONE OBJECT WHEN IT COLLIDES WITH THIS TRACKING OBJECT
//(we might be able to add a write to file function in the on collision enter method)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;
using System.IO;

public class TrackCamera : MonoBehaviour
{
    public Transform objectPosition;
    float speed = 10.0f;
    public Renderer rend;
    private InputDevice targetDevice;
    private string myFilePath;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics desiredCharacteristics = InputDeviceCharacteristics.Left;
        InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, devices);
        foreach (var device in devices)
        {
            Debug.Log(string.Format("Device name '{0}' has characteristics '{1}'", device.name, device.characteristics.ToString()));
        }
        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            Debug.Log(targetDevice.name + " initialized at start ");
        }

        myFilePath = "./collision.txt";

        if (File.Exists(myFilePath))
        {
            try
            {
                File.Delete(myFilePath);
                Debug.Log("File deleted");
            }
            catch (System.Exception e)
            {
                Debug.LogError(" Cannot delete file because it does not exist");
            }
        }
    }

    public void WriteToFile(string message)
    {
        try
        {
            StreamWriter fileWriter = new StreamWriter(myFilePath, true);
            fileWriter.Write(message);
            fileWriter.Close();
        }
        catch (System.Exception e)
        {
            Debug.LogError(" Cannot write in the file.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(objectPosition.position);
        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue);

        if (secondaryButtonValue)
        {
            Debug.Log("Predicted Collision occured at" + Time.time + " date " +  System.DateTime.Now);
            WriteToFile("Predicted Collision occured at " + Time.time + " - " + System.DateTime.Now + "\n");

        }
    }
	
	private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Actual collision happened at" + Time.time + " date " + DateTime.Now);
        WriteToFile("Actual Collision occured at --- " + Time.time + " - " + System.DateTime.Now + "\n");
        Destroy(collision.gameObject);
    }
}
