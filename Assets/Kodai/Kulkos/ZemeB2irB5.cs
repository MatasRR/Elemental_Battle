using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZemeB2irB5 : Skydas
{
    [HideInInspector]
    public float Aukstis;
    [HideInInspector]
    public float DidejimoLaikas;

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

        if (Laikas <= DidejimoLaikas)
        {
            Laikas += Time.deltaTime;
            transform.localScale = PradinisDydis + new Vector3(0, Aukstis, 0) * (Laikas / DidejimoLaikas);
        }
    }
}
