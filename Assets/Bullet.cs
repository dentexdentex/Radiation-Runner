using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null) 
        {
            rb.velocity = Vector3.forward * 10;
        }

        if (collision.gameObject.TryGetComponent(out CubesHealt cubesHealt))
        {
            cubesHealt.CanDusur(1);
        }
    }
}
