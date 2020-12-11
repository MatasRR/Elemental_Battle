using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kulka : MonoBehaviour
{
    [HideInInspector]
    public float Zala;
    [HideInInspector]
    public float DOTZala;
    [HideInInspector]
    public float DOTDaznis;
    [HideInInspector]
    public float DOTTrukme;

    [HideInInspector]
    public float Greitis;
    [HideInInspector]
    public GameObject Autorius;
    [HideInInspector]
    public Zaidejas AutoriausZaidejoKodas;
    [HideInInspector]
    public Elementas AutoriausElementoKodas;
    [HideInInspector]
    public int KomandosNr;
    [HideInInspector]
    public int ElementoNr;
    [HideInInspector]
    public float AtakosMod;

    [HideInInspector]
    public float SustingdymoLaikas;
    [HideInInspector]
    public float NuginklavimoLaikas;
    [HideInInspector]
    public float SuletinimoLaikas;
    [HideInInspector]
    public float SuletinimoStipris = 1f;
    [HideInInspector]
    public float SoklumoSilpninimoStipris = 1f;

    public bool EinaKiaurai = false;
    public bool TuriFizikosEfektu = false;
    public bool GaliVeiktiAukaKelisKartus = false;

    private List<GameObject> PaveiktiObjektai = new List<GameObject>();

    public virtual void Start()
    {
        PaveiktiObjektai.Add(Autorius);

        if (gameObject.GetComponent<Rigidbody>() != null && Greitis != 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity = transform.forward * Greitis;
        }

        AutoriausZaidejoKodas = Autorius.GetComponent<Zaidejas>();

        ElementoNr = AutoriausZaidejoKodas.ElementoNr;
        KomandosNr = AutoriausZaidejoKodas.KomandosNr;
        AtakosMod = AutoriausZaidejoKodas.AtakosMod;

        switch(ElementoNr)
        {
            case 1:
                AutoriausElementoKodas = Autorius.GetComponent<Oras>();
                break;
            case 2:
                AutoriausElementoKodas = Autorius.GetComponent<Vanduo>();
                break;
            case 3:
                AutoriausElementoKodas = Autorius.GetComponent<Zeme>();
                break;
            case 4:
                AutoriausElementoKodas = Autorius.GetComponent<Ugnis>();
                break;
        }
    }

    public virtual void Update()
    {

    }

    public virtual void OnTriggerEnter(Collider c)
    {
        Kontaktas(c.gameObject);
    }

    public virtual void OnCollisionEnter(Collision c)
    {
        Kontaktas(c.collider.gameObject);
    }

    public virtual void Kontaktas (GameObject go)
    {
        bool DarNepaveiktas = true;

        foreach (GameObject Paveiktas in PaveiktiObjektai)
        {
            if (go == Paveiktas)
            {
                DarNepaveiktas = false;
            }
        }

        if (!DarNepaveiktas)
        {                     
            return;
        }



        if (!GaliVeiktiAukaKelisKartus)
        {
            PaveiktiObjektai.Add(go);
        }

        bool NaikintiKulka = true;
        Rigidbody AukosRB = null;
        
        if (TuriFizikosEfektu)
        {
            AukosRB = go.GetComponent<Rigidbody>();
            Debug.Log(go.name);
        }

        if (go.CompareTag("Player"))
        {
            Zaidejas ZaidejoKodas = go.GetComponent<Zaidejas>();

            if (ZaidejoKodas.KomandosNr != KomandosNr || ZaidejoKodas.KomandosNr == 0)
            {
                ZaidejoKodas.KeistiPaskutiniZalojusiZaideja(AutoriausZaidejoKodas);
                ZaidejoKodas.GautiZalos(Zala * AtakosMod, ElementoNr);

                AutoriausElementoKodas.Patirtis += Zala;

                if (TuriFizikosEfektu)
                {
                    FizikosEfektai(go.GetComponent<Rigidbody>());
                }

                if (DOTZala != 0)
                {
                    ZaidejoKodas.GautiDOTZalos(DOTZala * AtakosMod, DOTDaznis, DOTTrukme, ElementoNr);
                }
                if (SustingdymoLaikas != 0)
                {
                    ZaidejoKodas.JudejimoCCLaikas += SustingdymoLaikas;
                }
                if (NuginklavimoLaikas != 0)
                {
                    ZaidejoKodas.PuolimoCCLaikas += NuginklavimoLaikas;
                }
                if (SuletinimoLaikas != 0)
                {
                    ZaidejoKodas.Suletinti(SuletinimoStipris, SoklumoSilpninimoStipris, SuletinimoLaikas);
                }
            }
        }
        else if (go.CompareTag("Skydas"))
        {
            Skydas SkydoKodas = go.GetComponent<Skydas>();
            if (SkydoKodas.IgnoruojaSavoKulkas)
            {
                if ( (Autorius == SkydoKodas.Autorius) || (SkydoKodas.KomandosNr == KomandosNr && SkydoKodas.KomandosNr != 0) )
                {
                    NaikintiKulka = false;
                }
                else
                {
                    SkydoKodas.GautiZalos(Zala);

                    if (DOTZala != 0)
                    {
                        StartCoroutine(SkydoKodas.GautiDOTZalos(DOTZala * AtakosMod, DOTDaznis, DOTTrukme));
                    }

                    if (Zala < SkydoKodas.MaxSugeriamaZala)
                    {
                        Destroy(gameObject);
                    }
                    
                    if (AukosRB != null)
                    {
                        FizikosEfektai(go.GetComponent<Rigidbody>());
                    }
                }
            }
            else
            {
                SkydoKodas.GautiZalos(Zala);
                if (Zala < SkydoKodas.MaxSugeriamaZala)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            if (AukosRB != null)
            {
                FizikosEfektai(go.GetComponent<Rigidbody>());
            }
        }

        if (!EinaKiaurai && NaikintiKulka)
        {
            Destroy(gameObject);
        }
    }

    public virtual void FizikosEfektai(Rigidbody rb) { }
}
