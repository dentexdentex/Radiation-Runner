using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPool : MonoBehaviour
{
    private CharacterMovement characterMovement;
    
    [FormerlySerializedAs("cubePrefab")] public GameObject bulletPrefab; // atılacak küpün prefab'ı
    [FormerlySerializedAs("cubeSpeed")] public float bulletSpeed = 5f; // küplerin hızı
    private Coroutine _bulletCoroutine;
    public float destroyDelay = 3f; // küplerin yok olma süresi
   // public float maxAngle = 30f; // Mermilerin maksimum açısı
    
    private List<GameObject> cubePool = new List<GameObject>(); // object pool'umuz
    void Start()
    {
        characterMovement = FindObjectOfType<CharacterMovement>();
        for (int i = 0; i < 10000; i++)
        {
            GameObject cube = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity );
            cube.SetActive(false);
            cubePool.Add(cube);
        }
    }
    void Update()
    {
            if (Input.GetMouseButtonDown(0))
            {
                if(_bulletCoroutine is not null) return;
                _bulletCoroutine = StartCoroutine(TryBulletSpawn());
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if(_bulletCoroutine is null) return;
                StopCoroutine(_bulletCoroutine);
                _bulletCoroutine = null;
            } 
    }


    private IEnumerator TryBulletSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject cube = GetPooledCube();
            if (cube != null)
            {
                cube.SetActive(true);
                cube.transform.position = transform.position + transform.forward * 2f; //önğnden ateşle
                Rigidbody rb = cube.GetComponent<Rigidbody>();
                rb.velocity = transform.forward * bulletSpeed;
                StartCoroutine(DestroyCube(cube));
            }
        }
    }
    GameObject GetPooledCube()
    {// object pool'dan kullanılabilir bir küp alıyoruz
        for (int i = 0; i < cubePool.Count; i++)
        {
            if (!cubePool[i].activeInHierarchy)
            {
                return cubePool[i];
            }
        }
        return null;
    }
    IEnumerator DestroyCube(GameObject cube)
    {
        yield return new WaitForSeconds(destroyDelay);
        cube.SetActive(false); //küpleri kapatıyoruz
    }
}


