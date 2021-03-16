using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;
using System.IO;

public class ClonePrefab : MonoBehaviour
{
    private InputDevice targetDevice;
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
            targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue);

            if (secondaryButtonValue)
            {
                Debug.Log("Pressing sedondary button on left controller X/Y" + System.DateTime.Now);
                WriteToFile("Button pressed at " + Time.time + " - " + System.DateTime.Now + "\n");
            }
    }

}
