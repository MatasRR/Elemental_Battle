using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanduoB5 : Kulka
{
    [HideInInspector]
    public float JudejimoCCLaikas;

    public override void Kontaktas(GameObject go)
    {
        if (go.CompareTag("Player") && go != Autorius)
        {
            Zaidejas ZaidejoKodas = go.GetComponent<Zaidejas>();
            ZaidejoKodas.JudejimoCCLaikas += JudejimoCCLaikas;
            ZaidejoKodas.GaliJudeti = false;
        }

        base.Kontaktas(go);
    }
}
