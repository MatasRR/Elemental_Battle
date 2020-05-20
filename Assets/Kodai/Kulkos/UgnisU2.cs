using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UgnisU2 : Kulka
{
    [HideInInspector]
    public float Jega;
    [HideInInspector]
    public float Spindulys;
    [HideInInspector]
    public float Aukstis;
    [HideInInspector]
    public float DidejimoLaikas;
    [HideInInspector]
    public float AtakosSumazinimoStipris;
    [HideInInspector]
    public float AtakosSumazinimoTrukme;

    private float Laikas;
    private Vector3 PradinisDydis;
    private Zaidejas AutoriausZaidejoKodas;

    public override void Start()
    {
        base.Start();
        AutoriausZaidejoKodas = Autorius.GetComponent<Zaidejas>();
        PradinisDydis = transform.localScale;
    }

    public override void Update()
    {
        base.Update();

        Laikas += Time.deltaTime;
        transform.localScale = PradinisDydis + (new Vector3(Spindulys, /*Aukstis*/Spindulys, /*Spindulys*/Aukstis) - PradinisDydis) * (Laikas / DidejimoLaikas);
    }

    public override void Kontaktas(GameObject go)
    {
        base.Kontaktas(go);

        if (go.CompareTag("Player"))
        {
            go.GetComponent<Zaidejas>().KeistiAtakaIrGynyba(AtakosSumazinimoStipris, 0, AtakosSumazinimoTrukme);
        }
    }
}
