using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZemeB4 : Kulka
{
    [HideInInspector]
    public float Aukstis;
    [HideInInspector]
    public float DidejimoLaikas;
    [HideInInspector]
    public float Trukme;

    public float AukscioPriedas;

    private float Laikas;
    private Vector3 PradinisDydis;

    public override void Start()
    {
        base.Start();
        PradinisDydis = transform.localScale;
    }

    public override void Update()
    {
        base.Update();

        Laikas += Time.deltaTime;

        if (Laikas <= DidejimoLaikas)
        {            
            transform.localScale = PradinisDydis + new Vector3(0, /*Aukstis*/0, /*0*/Aukstis) * (Laikas / DidejimoLaikas) + new Vector3(0, /*AukscioPriedas*/0, /*0*/AukscioPriedas);
        }
        else if (Laikas >= Trukme - DidejimoLaikas && Laikas <= Trukme)
        {
            transform.localScale = PradinisDydis + new Vector3(0, /*Aukstis*/0, /*0*/Aukstis) * ((Trukme - Laikas) / DidejimoLaikas) + new Vector3(0, /*AukscioPriedas*/0, /*0*/AukscioPriedas);
        }
        else if (Laikas > Trukme)
        {
            Destroy(gameObject);
        }
    }
}
