using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanduoU3 : Kulka
{
    [HideInInspector]
    public float Jega;
    [HideInInspector]
    public float IlgejimoGreitis;

    public override void Start()
    {
        base.Start();
        
        InvokeRepeating("Ilgejimas", 0, 0.01f);
    }

    public override void Update()
    {
        base.Update();

        if (!ToliAukstyn() && !ToliZemyn())
        {
            Autorius.GetComponent<Zaidejas>().JudejimoLaikoIgnoravimas--;
            Autorius.GetComponent<Zaidejas>().PuolimoLaikoIgnoravimas--;
            Destroy(gameObject);
        }
    }

    void Ilgejimas()
    {
        transform.position += transform.forward * IlgejimoGreitis;
        transform.localScale += new Vector3(0, 0, IlgejimoGreitis);
    }

    public override void Kontaktas(GameObject go)
    {
        if (Autorius == go)
        {
            return;
        }

        Autorius.GetComponent<Zaidejas>().JudejimoLaikoIgnoravimas--;
        Autorius.GetComponent<Zaidejas>().PuolimoLaikoIgnoravimas--;

        base.Kontaktas(go);
    }

    public override void FizikosEfektai(Rigidbody rb)
    {
        Vector3 SulygintaPagalYJegosVieta = Autorius.transform.position;
        SulygintaPagalYJegosVieta.y = rb.position.y;
        Vector3 JegosVektorius = (rb.position - SulygintaPagalYJegosVieta).normalized * Jega;
        rb.AddForce(JegosVektorius, ForceMode.Impulse);
    }

    public bool ToliZemyn()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit Info))
        {
            return !Info.collider.CompareTag("Išversta sfera") ? true : false;
        }
        else
        {
            return false;
        }
    }

    public bool ToliAukstyn()
    {
        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit Info))
        {
            return !Info.collider.CompareTag("Išversta sfera") ? true : false;
        }
        else
        {
            return false;
        }
    }
}
