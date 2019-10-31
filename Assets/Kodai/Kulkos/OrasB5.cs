using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrasB5 : Kulka
{
    [HideInInspector]
    public float Jega;

    public override void Kontaktas(GameObject go)
    {
        Rigidbody RB = go.GetComponent<Rigidbody>();
        if (RB != null && go != Autorius)
        {
            Vector3 SulygintaPagalYJegosVieta = Autorius.transform.position;
            SulygintaPagalYJegosVieta.y = go.transform.position.y;
            Vector3 JegosVektorius = (go.transform.position - SulygintaPagalYJegosVieta).normalized * Jega;
            RB.AddForce(JegosVektorius, ForceMode.Impulse);
        }

        base.Kontaktas(go);
    }
}
