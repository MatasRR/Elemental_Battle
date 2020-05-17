using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UgnisU2 : Kulka
{
    [HideInInspector]
    public float Jega;
    [HideInInspector]
    public float Dydis;
    [HideInInspector]
    public float DidejimoLaikas;
    [HideInInspector]
    public float AtakosSumazinimoStipris;
    [HideInInspector]
    public float AtakosSumazinimoTrukme;

    private float Laikas;
    private Vector3 PradinisDydis;
    private Zaidejas AutoriausZaidejoKodas;
    private List<GameObject> PaveiktiObjektai = new List<GameObject>();

    public override void Start()
    {
        base.Start();
        AutoriausZaidejoKodas = Autorius.GetComponent<Zaidejas>();
        PradinisDydis = transform.localScale;
        PaveiktiObjektai.Add(Autorius);
    }

    public override void Update()
    {
        base.Update();

        Laikas += Time.deltaTime;
        transform.localScale = PradinisDydis + (new Vector3(Dydis, Dydis, Dydis) - PradinisDydis) * (Laikas / DidejimoLaikas);
    }

    public override void OnTriggerEnter(Collider c)
    {
        bool DarNepaveiktas = true;
        foreach (GameObject go in PaveiktiObjektai)
        {
            if (c.gameObject == go)
            {
                DarNepaveiktas = false;
            }
        }

        if (DarNepaveiktas)
        {
            PaveiktiObjektai.Add(c.gameObject);
            Kontaktas(c.gameObject);
        }
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
