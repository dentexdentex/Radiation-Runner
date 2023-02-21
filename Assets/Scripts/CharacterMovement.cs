using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float movementSpeed = 5f; // karakterin hareket hızı

    private Rigidbody rb; // karakterin rigidbody bileşeni

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float movement = horizontalInput * movementSpeed * Time.deltaTime;
        transform.position += new Vector3(movement, 0, 0);
    }
}