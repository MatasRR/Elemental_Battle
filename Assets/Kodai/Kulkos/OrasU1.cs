using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrasU1 : Kulka
{
    [HideInInspector]
    public float Jega;
    [HideInInspector]
    public float Skersmuo;
    [HideInInspector]
    public float LaukimoLaikas;
    [HideInInspector]
    public float DidejimoLaikas;

    //public float GalutineAlfa;

    private float Laikas;
    private Vector3 PradinisDydis;
    private Zaidejas AutoriausZaidejoKodas;
    //private Material Medziaga;

    public override void Start()
    {
        base.Start();
        AutoriausZaidejoKodas = Autorius.GetComponent<Zaidejas>();
        //Medziaga = gameObject.GetComponent<MeshRenderer>().material;
        //Medziaga.color = new Color(Medziaga.color.r, Medziaga.color.g, Medziaga.color.b, 0);
        PradinisDydis = transform.localScale;
        Laikas = -LaukimoLaikas;
    }

    public override void Update()
    {
        base.Update();

        Laikas += Time.deltaTime;

        if (Laikas < 0)
        {
            //Color Spalva = Medziaga.color;
            //Spalva.a = GalutineAlfa * (Laikas + LaukimoLaikas) / LaukimoLaikas;
            //Medziaga.color = Spalva;
        }
        else
        {
            transform.localScale = PradinisDydis + (new Vector3(Skersmuo, Skersmuo, Skersmuo) - PradinisDydis) * (Laikas / DidejimoLaikas);
        }
    }

    public override void OnTriggerEnter(Collider c)
    {
        if (Laikas >= 0)
        {
            Kontaktas(c.gameObject);
        }        
    }

    public override void FizikosEfektai(Rigidbody rb)
    {
        rb.AddExplosionForce(Jega, transform.position, Skersmuo, 0f, ForceMode.Impulse);
    }
}
