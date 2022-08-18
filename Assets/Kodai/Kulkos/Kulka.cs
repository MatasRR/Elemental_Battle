using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kulka : MonoBehaviour
{
    [HideInInspector] public float Zala;
    [HideInInspector] public float DOTZala;
    [HideInInspector] public float DOTDaznis;
    [HideInInspector] public float DOTTrukme = 0f;

    [HideInInspector] public float Greitis;
    [HideInInspector] public GameObject Autorius;
    [HideInInspector] public Zaidejas AutoriausZaidejoKodas;
    [HideInInspector] public int KomandosNr;
    [HideInInspector] public int ElementoNr;
    [HideInInspector] public float AtakosMod;

    [HideInInspector] public float SustingdymoLaikas = 0f;
    [HideInInspector] public float NuginklavimoLaikas = 0f;
    [HideInInspector] public float SuletinimoLaikas = 0f;
    [HideInInspector] public float SuletinimoStipris = 0f;
    [HideInInspector] public float SoklumoSilpninimoStipris = 0f;

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

                AutoriausZaidejoKodas.Patirtis += Zala * AtakosMod / ZaidejoKodas.GynybosMod;

                ZaidejoKodas.GautiDOTZalos(DOTZala * AtakosMod, DOTDaznis, DOTTrukme, ElementoNr);
                ZaidejoKodas.JudejimoCCLaikas += SustingdymoLaikas;
                ZaidejoKodas.PuolimoCCLaikas += NuginklavimoLaikas;
                ZaidejoKodas.Suletinti(SuletinimoStipris, SoklumoSilpninimoStipris, SuletinimoLaikas);

                if (TuriFizikosEfektu)
                {
                    FizikosEfektai(go.GetComponent<Rigidbody>());
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
                    StartCoroutine(SkydoKodas.GautiDOTZalos(DOTZala * AtakosMod, DOTDaznis, DOTTrukme));

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
