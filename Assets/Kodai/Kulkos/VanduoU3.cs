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

        Rigidbody RB = go.GetComponent<Rigidbody>();
        if (RB != null)
        {
            Vector3 SulygintaPagalYJegosVieta = Autorius.transform.position;
            SulygintaPagalYJegosVieta.y = go.transform.position.y;
            Vector3 JegosVektorius = (go.transform.position - SulygintaPagalYJegosVieta).normalized * Jega;
            RB.AddForce(JegosVektorius, ForceMode.Impulse);
        }

        base.Kontaktas(go);
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
