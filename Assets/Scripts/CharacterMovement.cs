using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float movementSpeed = 5f; // karakterin hareket hızı
    public float horizontalInput;//karakter yatay hızı 
    
    private Rigidbody rb; // karakterin rigidbody bileşeni

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        float movement = horizontalInput * movementSpeed * Time.deltaTime;
        transform.position += new Vector3(movement, 0, 2f*Time.deltaTime);
        if (movement != 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(movement, 0, 0));
            transform.rotation =  Quaternion.Slerp(transform.rotation,targetRotation, Time.deltaTime * 1f);
        }
        else
        {
            transform.localEulerAngles = Vector3.zero;
        }
    }
}