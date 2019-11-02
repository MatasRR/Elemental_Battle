using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kulka : MonoBehaviour
{
    [HideInInspector]
    public float Greitis;
    [HideInInspector]
    public float Zala;
    [HideInInspector]
    public GameObject Autorius;

    [HideInInspector]
    public float SustingdymoLaikas;
    [HideInInspector]
    public float NuginklavimoLaikas;
    [HideInInspector]
    public float SuletinimoLaikas;
    [HideInInspector]
    public float SuletinimoStipris;
    [HideInInspector]
    public float SoklumoSilpninimoStipris;

    public bool EinaKiaurai = false;

    public virtual void Start()
    {
        if (gameObject.GetComponent<Rigidbody>() != null && Greitis != 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity = transform.forward * Greitis;
        }
    }

    public virtual void Update()
    {

    }

    public virtual void OnTriggerEnter(Collider c)
    {
        Kontaktas(c.gameObject);
    }

    public virtual void OnTriggerStay(Collider c)
    {
        
    }

    public virtual void Kontaktas (GameObject go)
    {
        if (Autorius == go)
        {
            return;
        }

        if (go.CompareTag("Player"))
        {
            Zaidejas ZaidejoKodas = go.GetComponent<Zaidejas>();
            ZaidejoKodas.KeistiPaskutiniZalojusiZaideja(Autorius);
            ZaidejoKodas.GautiZalos(Zala);

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
                StartCoroutine(Suletinimas(ZaidejoKodas));
            }
        }
        else if (go.CompareTag("Skydas"))
        {
            Skydas SkydoKodas = go.GetComponent<Skydas>();
            if (SkydoKodas.Autorius != Autorius || !SkydoKodas.IgnoruojaSavoKulkas)
            {
                SkydoKodas.GautiZalos(Zala);
                if (Zala < SkydoKodas.MaxSugeriamaZala)
                {
                    Destroy(gameObject);
                }
            }
        }

        if (!EinaKiaurai)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Suletinimas(Zaidejas AukosZaidejoKodas)
    {
        AukosZaidejoKodas.GreicioMod *= SuletinimoStipris;
        AukosZaidejoKodas.SoklumoMod *= SoklumoSilpninimoStipris;
        AukosZaidejoKodas.GreicioIrSoklumoPerskaiciavimas();
        yield return new WaitForSeconds(SuletinimoLaikas);
        AukosZaidejoKodas.GreicioMod /= SuletinimoStipris;
        AukosZaidejoKodas.SoklumoMod /= SoklumoSilpninimoStipris;
        AukosZaidejoKodas.GreicioIrSoklumoPerskaiciavimas();
    }
}
