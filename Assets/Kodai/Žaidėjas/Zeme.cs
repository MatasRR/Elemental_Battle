using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Photon;
using Photon.Pun;

public class Zeme : Elementas
{
    [Header("B 1: ")]
    public float B1_CD;

    public float B1Zala;

    public float B1DaiktoGreitis;
    public GameObject B1Daiktas;

    [Header("B 2: ")]
    public float B2_CD;

    public float B2Gyvybes;
    public float B2MaxSugeriamaZala;
    public float B2Nuotolis;
    public GameObject B2Daiktas;

    [Header("B 3: ")]
    public float B3_CD;

    public float B3Zala;

    public float B3DaiktoGreitis;
    public float B3SukimosiGreitis;
    public GameObject B3Daiktas;

    [Header("U 1: ")]
    public float U1_CD;

    public float U1Zala;

    public float U1Dydis;
    public float U1Delsimas;
    public float U1JudejimoCCLaikas;


    public override void B1()
    {
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas))
        {
            photonView.RPC("RPCKurtiZemeB1", RpcTarget.All, PataikytasObjektas.point);
        }
    }

    [PunRPC]
    void RPCKurtiZemeB1(Vector3 ZiurimasTaskas)
    {
        GameObject ZemeB1 = Instantiate(B1Daiktas, KulkosAtsiradimoVieta.position, KulkosAtsiradimoVieta.rotation);
        Kulka KulkosKodas = ZemeB1.GetComponent<Kulka>();

        ZemeB1.transform.LookAt(ZiurimasTaskas);

        KulkosKodas.Greitis = B1DaiktoGreitis;
        KulkosKodas.Zala = B1Zala;
        KulkosKodas.Autorius = gameObject;

        Destroy(ZemeB1, 10);
    }

    public override void B2()
    {
        photonView.RPC("RPCKurtiZemeB2", RpcTarget.All);
    }

    [PunRPC]
    void RPCKurtiZemeB2()
    {
        Vector3 AtsiradimoVieta = transform.position + transform.forward * B2Nuotolis;
        GameObject ZemeB2 = Instantiate(B2Daiktas, AtsiradimoVieta, KulkosAtsiradimoVieta.rotation);
        Skydas SkydoKodas = ZemeB2.GetComponent<Skydas>();

        SkydoKodas.Gyvybes = B2Gyvybes;
        SkydoKodas.MaxSugeriamaZala = B2MaxSugeriamaZala;
    }

    public override void B3()
    {
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas))
        {
            photonView.RPC("RPCKurtiZemeB3", RpcTarget.All, PataikytasObjektas.point);
        }
    }

    [PunRPC]
    void RPCKurtiZemeB3(Vector3 ZiurimasTaskas)
    {
        GameObject ZemeB3 = Instantiate(B3Daiktas, KulkosAtsiradimoVieta.position, KulkosAtsiradimoVieta.rotation);
        Kulka KulkosKodas = ZemeB3.GetComponent<Kulka>();

        ZemeB3.transform.LookAt(ZiurimasTaskas);

        KulkosKodas.Greitis = B3DaiktoGreitis;
        KulkosKodas.Zala = B3Zala;
        KulkosKodas.Autorius = gameObject;

        Destroy(ZemeB3, 10);
    }

    public override void U1()
    {
        StartCoroutine(_U1());
    }

    IEnumerator _U1()
    {
        Rigidbody RB = GetComponent<Rigidbody>();
        RB.isKinematic = true;
        ZaidejoKodas.JudejimoLaikoIgnoravimas++;
        ZaidejoKodas.PuolimoLaikoIgnoravimas++;

        // Reikia garso/animacijos
        yield return new WaitForSeconds(U1Delsimas);

        photonView.RPC("RPCKurtiZemeU1", RpcTarget.All);

        ZaidejoKodas.JudejimoLaikoIgnoravimas--;
        ZaidejoKodas.PuolimoLaikoIgnoravimas--;
        RB.isKinematic = false;
    }

    [PunRPC]
    void RPCKurtiZemeU1()
    {
        Collider[] Kiti = Physics.OverlapSphere(transform.position, U1Dydis);
        foreach (Collider PataikeKitam in Kiti)
        {
            if (PataikeKitam.gameObject == gameObject)
            {
                return;
            }
            
            if (PataikeKitam.CompareTag("Player") && PataikeKitam.GetComponent<Judejimas>().AntZemes())
            {
                Zaidejas KitoZaidejoKodas = PataikeKitam.GetComponent<Zaidejas>();
                KitoZaidejoKodas.GautiZalos(U1Zala);
                KitoZaidejoKodas.JudejimoCCLaikas += U1JudejimoCCLaikas;
            }
            else if (PataikeKitam.CompareTag("Skydas"))
            {
                PataikeKitam.GetComponent<Skydas>().GautiZalos(U1Zala);
            }
        }
    }


    public override void CDNustatymas()
    {
        switch (Duomenys.B1)
        {
            case 1: BCD = B1_CD; break;
            case 2: BCD = B2_CD; break;
            case 3: BCD = B3_CD; break;/*
            case 4: BCD = B4_CD; break;/*
            case 5: BCD = B5_CD; break;/*
            case 6: BCD = B6_CD; break;/*
            case 7: BCD = B7_CD; break;/*
            case 8: BCD = B8_CD; break;/*
            case 9: BCD = B9_CD; break;*/
        }
        switch (Duomenys.B2)
        {
            case 1: BBCD = B1_CD; break;
            case 2: BBCD = B2_CD; break;
            case 3: BBCD = B3_CD; break;/*
            case 4: BBCD = B4_CD; break;/*
            case 5: BBCD = B5_CD; break;/*
            case 6: BBCD = B6_CD; break;/*
            case 7: BBCD = B7_CD; break;/*
            case 8: BBCD = B8_CD; break;/*
            case 9: BBCD = B9_CD; break;*/
        }
        switch (Duomenys.B3)
        {
            case 1: BBBCD = B1_CD; break;
            case 2: BBBCD = B2_CD; break;
            case 3: BBBCD = B3_CD; break;/*
            case 4: BBBCD = B4_CD; break;/*
            case 5: BBBCD = B5_CD; break;/*
            case 6: BBBCD = B6_CD; break;/*
            case 7: BBBCD = B7_CD; break;/*
            case 8: BBBCD = B8_CD; break;/*
            case 9: BBBCD = B9_CD; break;*/
        }
        switch (Duomenys.U)
        {
            case 1: UCD = U1_CD; break;/*
            case 2: UCD = U2_CD; break;/*
            case 3: UCD = U3_CD; break;/*
            case 4: UCD = U4_CD; break;/*
            case 5: UCD = U5_CD; break;/*
            case 6: UCD = U6_CD; break;/*
            case 7: UCD = U7_CD; break;/*
            case 8: UCD = U8_CD; break;/*
            case 9: UCD = U9_CD; break;*/
        }
    }
}