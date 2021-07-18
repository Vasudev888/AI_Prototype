using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
    public float rotSpeed = 5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputY = Input.GetAxis("Vertical");
        float inputX = Input.GetAxis("Horizontal");
        transform.position += transform.forward * Time.deltaTime * speed * inputY;
        /*transform.rotation *= Quaternion.Euler(0, rotSpeed * Time.deltaTime * inputX, 0);*/
        transform.position += transform.right * Time.deltaTime * speed * inputX;
        
    }
}
