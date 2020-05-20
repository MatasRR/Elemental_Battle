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
        if (gameObject.GetComponent<Rigidbody>() != null && Greitis != 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity = transform.forward * Greitis;
        }

        ElementoNr = Autorius.GetComponent<Zaidejas>().ElementoNr;
        KomandosNr = Autorius.GetComponent<Zaidejas>().KomandosNr;
        AtakosMod = Autorius.GetComponent<Zaidejas>().AtakosMod;
        PaveiktiObjektai.Add(Autorius);
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
            if (!GaliVeiktiAukaKelisKartus)
            {
                PaveiktiObjektai.Add(gameObject);
            }
            
            return;
        }



        bool NaikintiKulka = true;
        Rigidbody AukosRB = null;
        
        if (TuriFizikosEfektu)
        {
            AukosRB = go.GetComponent<Rigidbody>();
        }

        if (go.CompareTag("Player"))
        {
            Zaidejas ZaidejoKodas = go.GetComponent<Zaidejas>();

            if (ZaidejoKodas.KomandosNr != KomandosNr || ZaidejoKodas.KomandosNr == 0)
            {
                ZaidejoKodas.KeistiPaskutiniZalojusiZaideja(Autorius);
                ZaidejoKodas.GautiZalos(Zala * AtakosMod, ElementoNr);
                FizikosEfektai(go.GetComponent<Rigidbody>());

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

    public virtual void FizikosEfektai(Rigidbody rb)
    {

    }
}
