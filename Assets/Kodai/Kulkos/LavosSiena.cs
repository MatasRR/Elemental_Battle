using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavosSiena : Skydas
{
    [HideInInspector]
    public float SukietejimoLaikas;

    [HideInInspector]
    public Material SukietejusiMedziaga;

    [HideInInspector]
    public float Zala;

    [HideInInspector]
    public float ZalojimoDaznis;

    public float LikesZalojimoLaikas;

    private void Update()
    {
        if (SukietejimoLaikas > 0)
        {
            SukietejimoLaikas -= Time.deltaTime;
        }
        else
        {
            SukietejimoLaikas = 0;
            gameObject.GetComponent<MeshRenderer>().material = SukietejusiMedziaga;
        }
    }
}
