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

    void Aktyvavimas()
    {
        Collider[] Kiti = Physics.OverlapSphere(transform.position, Dydis);
        foreach (Collider PataikeKitam in Kiti)
        {
            if (Korpusas.bounds.Intersects(PataikeKitam.bounds))
            {
                Kontaktas(PataikeKitam.gameObject);
            }
        }
    }

    public override void FizikosEfektai(Rigidbody rb)
    {
        Vector3 SulygintaPagalYJegosVieta = transform.position;
        SulygintaPagalYJegosVieta.y = rb.position.y;
        Vector3 JegosVektorius = (SulygintaPagalYJegosVieta - rb.position).normalized * Jega;
        rb.AddForce(JegosVektorius, ForceMode.Impulse);
    }
}
