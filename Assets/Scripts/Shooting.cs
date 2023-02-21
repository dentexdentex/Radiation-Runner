using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab; // mermi objesi
    public float bulletSpeed = 10f; // mermi hızı

    private float bulletLifetime = 3f; // mermi ömrü

    private Transform characterTransform; // karakterin Transform bileşeni

    void Start()
    {
        characterTransform = GetComponent<Transform>();
    }

    void Update()
    {
        // Mermi atma işlemi
        if (Input.GetButtonDown("Fire1"))
        {
            // Mermi objesini havuzdan al
            GameObject bullet = ObjectPool.SharedInstance.GetPooledObject("Bullet");

            if (bullet != null)
            {
                // Mermiyi karakterin pozisyonunda oluştur
                bullet.transform.position = transform.position;

                // Mermiye doğru yönü ver
                bullet.transform.rotation = characterTransform.rotation;

                // Mermi objesini etkinleştir
                bullet.SetActive(true);

                // Mermiye kuvvet uygula
                Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
                bulletRb.AddForce(characterTransform.rotation * Vector3.right * bulletSpeed, ForceMode.Impulse);

                // Mermiyi belirli bir süre sonra yok et
                StartCoroutine(DeactivateBulletAfterTime(bullet));
            }
        }
    }

    IEnumerator DeactivateBulletAfterTime(GameObject bullet)
    {
        yield return new WaitForSeconds(bulletLifetime);
        bullet.SetActive(false);
    }
}