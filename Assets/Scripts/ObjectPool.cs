using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject cubePrefab; // atılacak küpün prefab'ı
    public float cubeSpeed = 5f; // küplerin hızı
    public float destroyDelay = 3f; // küplerin yok olma süresi

    private List<GameObject> cubePool = new List<GameObject>(); // object pool'umuz

    // object pool'umuzu başlatıyoruz
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject cube = Instantiate(cubePrefab, Vector3.zero, Quaternion.identity);
            cube.SetActive(false);
            cubePool.Add(cube);
        }
    }
    // her tıklamada küp atıyoruz
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject cube = GetPooledCube();
            if (cube != null)
            {
                cube.SetActive(true);
                cube.transform.position = transform.position;
                Rigidbody rb = cube.GetComponent<Rigidbody>();
                rb.velocity = Camera.main.transform.forward * cubeSpeed;
                StartCoroutine(DestroyCube(cube));
            }
        }
    }
    // object pool'dan kullanılabilir bir küp alıyoruz
    GameObject GetPooledCube()
    {
        for (int i = 0; i < cubePool.Count; i++)
        {
            if (!cubePool[i].activeInHierarchy)
            {
                return cubePool[i];
            }
        }
        return null;
    }

    // küplerin yok olma coroutine'i
    IEnumerator DestroyCube(GameObject cube)
    {
        yield return new WaitForSeconds(destroyDelay);
        cube.SetActive(false);
    }

    // küplerin çarpma olayları
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(collision.impulse.normalized * cubeSpeed, ForceMode.Impulse);
        }
    }
}