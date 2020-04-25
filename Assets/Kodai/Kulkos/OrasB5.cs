using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrasB5 : Kulka
{
    [HideInInspector]
    public float Jega;

    public override void Kontaktas(GameObject go)
    {
        Rigidbody RB = go.GetComponent<Rigidbody>();
        Zaidejas AukosZaidejoKodas = go.GetComponent<Zaidejas>();
        if (RB != null && go != Autorius)
        {
            if (AukosZaidejoKodas != null)
            {
                if (AukosZaidejoKodas.KomandosNr == KomandosNr && AukosZaidejoKodas.KomandosNr != 0)
                {
                    return;
                }
            }

            Vector3 SulygintaPagalYJegosVieta = Autorius.transform.position;
            SulygintaPagalYJegosVieta.y = go.transform.position.y;
            Vector3 JegosVektorius = (go.transform.position - SulygintaPagalYJegosVieta).normalized * Jega;
            RB.AddForce(JegosVektorius, ForceMode.Impulse);
        }

        base.Kontaktas(go);
    }
}
