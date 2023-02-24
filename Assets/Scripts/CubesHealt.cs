using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesHealt : MonoBehaviour
{
    public int can = 20;

    public void CanDusur(int dusulecekMiktar)
    {
        can -= dusulecekMiktar;
        if (can <= 0)
        {
            Destroy(gameObject);
            return;
        }
     
    }
}
