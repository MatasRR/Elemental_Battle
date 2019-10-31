using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sukimasis : MonoBehaviour
{
    [HideInInspector]
    public Vector3 SukimosiVektorius;
    
    void Update()
    {
        transform.Rotate(SukimosiVektorius * Time.deltaTime);
    }
}
