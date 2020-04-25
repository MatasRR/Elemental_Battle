using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UgnisB3 : Kulka
{
    [HideInInspector]
    public float Daznis;
    [HideInInspector]
    public float TikrinimoSpindulys;
    private float LikesLaikas;
    private Collider Korpusas;

    public override void Start()
    {
        base.Start();

        TikrinimoSpindulys = Mathf.Max(Mathf.Max(transform.localScale.x, transform.localScale.y), transform.localScale.z) / 2;
        Korpusas = gameObject.GetComponent<Collider>();
        LikesLaikas = Daznis;
    }

    public override void Update()
    {
        base.Update();

        if (LikesLaikas < 0)
        {
            LikesLaikas = Daznis;
            Aktyvavimas();
        }
        else
        {
            LikesLaikas -= Time.deltaTime;
        }
    }

    public override void OnTriggerEnter(Collider c)
    {
        /// Nieko nereikia daryti
    }

    public override void Kontaktas(GameObject go)
    {
        /// Nieko nereikia daryti
    }

    void Aktyvavimas()
    {
        Collider[] Kiti = Physics.OverlapSphere(transform.position, TikrinimoSpindulys);
        foreach (Collider PataikeKitam in Kiti)
        {
            Debug.Log(PataikeKitam.name);
            if (PataikeKitam.gameObject == Autorius || !Korpusas.bounds.Intersects(PataikeKitam.bounds))
            {
                return;
            }

            if (PataikeKitam.CompareTag("Player"))
            {
                Zaidejas AukosZaidejoKodas = PataikeKitam.GetComponent<Zaidejas>();
                if (AukosZaidejoKodas.KomandosNr != KomandosNr || AukosZaidejoKodas.KomandosNr == 0)
                {
                    AukosZaidejoKodas.GautiZalos(Zala, 4);
                }
            }
            else if (PataikeKitam.CompareTag("Skydas"))
            {
                PataikeKitam.GetComponent<Skydas>().GautiZalos(Zala);
            }
        }
    }
}
