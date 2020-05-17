using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrasB5 : Kulka
{
    [HideInInspector]
    public float Jega;

    public override void FizikosEfektai(Rigidbody rb)
    {
        Vector3 SulygintaPagalYJegosVieta = Autorius.transform.position;
        SulygintaPagalYJegosVieta.y = rb.position.y;
        Vector3 JegosVektorius = (rb.position - SulygintaPagalYJegosVieta).normalized * Jega;
        rb.AddForce(JegosVektorius, ForceMode.Impulse);
    }
}
