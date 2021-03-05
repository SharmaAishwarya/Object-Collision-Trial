using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneDestruction : MonoBehaviour
{
    public float moveSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }
}
