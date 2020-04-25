using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanduoU2 : Kulka
{
    [HideInInspector]
    public float Jega;
    [HideInInspector]
    public float Daznis;
    [HideInInspector]
    public float Dydis;
    private float LikesLaikas;
    private Collider Korpusas;

    public override void Start()
    {
        base.Start();

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
        Collider[] Kiti = Physics.OverlapSphere(transform.position, Dydis);
        foreach (Collider PataikeKitam in Kiti)
        {
            if (PataikeKitam.gameObject == Autorius || !Korpusas.bounds.Intersects(PataikeKitam.bounds))
            {
                return;
            }

            Rigidbody KitoRB = PataikeKitam.GetComponent<Rigidbody>();
            Zaidejas AukosZaidejoKodas = PataikeKitam.GetComponent<Zaidejas>();
            if (KitoRB != null)
            {
                if (PataikeKitam.CompareTag("Player"))
                {
                    if (AukosZaidejoKodas.KomandosNr == KomandosNr && AukosZaidejoKodas.KomandosNr != 0)
                    {
                        return;
                    }
                }

                AukosZaidejoKodas.GautiZalos(Zala, 2);

                Vector3 SulygintaPagalYJegosVieta = transform.position;
                SulygintaPagalYJegosVieta.y = KitoRB.position.y;
                Vector3 JegosVektorius = (SulygintaPagalYJegosVieta - KitoRB.position).normalized * Jega;
                KitoRB.AddForce(JegosVektorius, ForceMode.Impulse);
            }
            
            
            else if (PataikeKitam.CompareTag("Skydas"))
            {
                PataikeKitam.GetComponent<Skydas>().GautiZalos(Zala);
            }
        }
    }
}
