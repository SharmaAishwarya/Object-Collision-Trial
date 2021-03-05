using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;
using System.IO;

public class ClonePrefab : MonoBehaviour
{
    private InputDevice targetDevice;
    public Rigidbody prefabObject;
    public Transform transform;
    public float spawnRate = 0.5f;
    private float nextSpawn = 1.0f;
    private bool hasSpawned = false;
    private string myFilePath;


    // Start is called before the first frame update
    void Start()
    {
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

        myFilePath = "./testFile.txt";

        if(File.Exists(myFilePath))
        {
            try
            {
                File.Delete(myFilePath);
                Debug.Log("File deleted");
            }
            catch(System.Exception e) 
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
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            hasSpawned = false;
        }

        if(hasSpawned == false)
        {
            targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue);

            if (primaryButtonValue)
            {
                Debug.Log("Pressing primary button on left controller X/Y");
                Rigidbody prefabInstance;
                prefabInstance = Instantiate(prefabObject, transform.position, transform.rotation) as Rigidbody;
                hasSpawned = true;
                WriteToFile("Button pressed at " + Time.time + " - " + System.DateTime.Now + "\n");
            }
        }
        
    }
}
