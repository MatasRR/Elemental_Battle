using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UgnisU1 : Kulka
{
    [HideInInspector]
    public float SunaikinimoLaikasPoKontakto;
    
    private bool Atsitrenke = false;

    public override void OnCollisionEnter(Collision c)
    {
        base.OnCollisionEnter(c);

        if (!Atsitrenke)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            Destroy(gameObject, SunaikinimoLaikasPoKontakto);
            Atsitrenke = true;
        }
    }
}
