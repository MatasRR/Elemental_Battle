using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Photon;
using Photon.Pun;

public class Zeme : Elementas
{
    [Header("B 1: ")]
    public Sprite[] B1Paveiksleliai;
    public float B1_CD;

    public float B1Zala;

    public float B1DaiktoGreitis;
    public GameObject B1Daiktas;

    [Header("B 2: ")]
    public Sprite[] B2Paveiksleliai;
    public float B2_CD;

    public float B2Gyvybes;
    public float B2MaxSugeriamaZala;
    public float B2Nuotolis;
    public float B2Trukme;
    public GameObject B2Daiktas;

    [Header("B 3: ")]
    public Sprite[] B3Paveiksleliai;
    public float B3_CD;

    public float B3Zala;

    public float B3DaiktoGreitis;
    public float B3AtsiradimoNuotolis;
    public float B3SukimosiGreitis;
    public GameObject B3Daiktas;

    [Header("B 4: ")]
    public Sprite[] B4Paveiksleliai;
    public float B4_CD;

    public float B4Zala;

    public int B4SpygliuSkaicius;
    public float B4SpygliuTrukme;
    public float B4AtstumasTarpSpygliu;
    public float B4AtstumasIkiPirmoSpyglio;
    public float B4LaikasTarpSpygliuSukurimo;
    public float B4TikrinimoAukstis;
    public GameObject B4Daiktas;

    [Header("U 1: ")]
    public Sprite[] U1Paveiksleliai;
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
        SkydoKodas.Autorius = gameObject;

        Destroy(ZemeB2, B2Trukme);
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
        Vector3 AtsiradimoVieta = KulkosAtsiradimoVieta.position + (transform.forward * B3AtsiradimoNuotolis);
        GameObject ZemeB3 = Instantiate(B3Daiktas, AtsiradimoVieta, KulkosAtsiradimoVieta.rotation);
        Kulka KulkosKodas = ZemeB3.GetComponent<Kulka>();

        ZemeB3.transform.LookAt(ZiurimasTaskas);

        KulkosKodas.Greitis = B3DaiktoGreitis;
        KulkosKodas.Zala = B3Zala;
        KulkosKodas.Autorius = gameObject;
        ZemeB3.transform.GetChild(0).GetComponent<Sukimasis>().SukimosiVektorius = new Vector3(0, 0, B3SukimosiGreitis);

        Destroy(ZemeB3, 10);
    }

    public override void B4()
    {
        StartCoroutine(_B4());
    }

    IEnumerator _B4()
    {
        Vector3 Kryptis = Kamera.ScreenPointToRay(Input.mousePosition).direction;
        Kryptis.y = 0;
        Vector3.Normalize(Kryptis);
        Vector3 SpindulioSaltinis = transform.position + new Vector3(0, B4TikrinimoAukstis, 0) + Kryptis * B4AtstumasIkiPirmoSpyglio;

        int AplinkosSluoksnis = LayerMask.GetMask("Aplinka");

        ZaidejoKodas.JudejimoLaikoIgnoravimas++;
        ZaidejoKodas.PuolimoLaikoIgnoravimas++;

        for (int i = 0; i < B4SpygliuSkaicius; i++)
        {
            if (Physics.Raycast(SpindulioSaltinis + i * B4AtstumasTarpSpygliu * Kryptis, new Vector3(0, -1, 0), out RaycastHit PataikytasObjektas, AplinkosSluoksnis))
            {
                photonView.RPC("RPCKurtiZemeB4", RpcTarget.All, PataikytasObjektas.point);
            }
            yield return new WaitForSeconds(B4LaikasTarpSpygliuSukurimo);
        }

        ZaidejoKodas.JudejimoLaikoIgnoravimas--;
        ZaidejoKodas.PuolimoLaikoIgnoravimas--;
    }

    [PunRPC]
    void RPCKurtiZemeB4(Vector3 ZiurimasTaskas)
    {     
        GameObject ZemeB4 = Instantiate(B4Daiktas, ZiurimasTaskas, Quaternion.Euler(-90, 0, 0));
        Kulka KulkosKodas = ZemeB4.GetComponent<Kulka>();
        
        KulkosKodas.Zala = B3Zala;
        KulkosKodas.Autorius = gameObject;

        Destroy(ZemeB4, B4SpygliuTrukme);
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
                Zaidejas AukosZaidejoKodas = PataikeKitam.GetComponent<Zaidejas>();
                if (AukosZaidejoKodas.KomandosNr != ZaidejoKodas.KomandosNr || AukosZaidejoKodas.KomandosNr == 0)
                {
                    AukosZaidejoKodas.GautiZalos(U1Zala, 3);
                    AukosZaidejoKodas.JudejimoCCLaikas += U1JudejimoCCLaikas;
                }                
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
            case 1: BCD = B1_CD; ZaidejoKodas.BPaveikslelis.sprite = B1Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 2: BCD = B2_CD; ZaidejoKodas.BPaveikslelis.sprite = B2Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 3: BCD = B3_CD; ZaidejoKodas.BPaveikslelis.sprite = B3Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 4: BCD = B4_CD; ZaidejoKodas.BPaveikslelis.sprite = B4Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 5: BCD = B5_CD; ZaidejoKodas.BPaveikslelis.sprite = B5Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 6: BCD = B6_CD; ZaidejoKodas.BPaveikslelis.sprite = B6Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 7: BCD = B7_CD; ZaidejoKodas.BPaveikslelis.sprite = B7Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 8: BCD = B8_CD; ZaidejoKodas.BPaveikslelis.sprite = B8Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 9: BCD = B9_CD; ZaidejoKodas.BPaveikslelis.sprite = B9Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;*/
        }
        switch (Duomenys.B2)
        {
            case 1: BBCD = B1_CD; ZaidejoKodas.BBPaveikslelis.sprite = B1Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 2: BBCD = B2_CD; ZaidejoKodas.BBPaveikslelis.sprite = B2Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 3: BBCD = B3_CD; ZaidejoKodas.BBPaveikslelis.sprite = B3Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 4: BBCD = B4_CD; ZaidejoKodas.BBPaveikslelis.sprite = B4Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 5: BBCD = B5_CD; ZaidejoKodas.BBPaveikslelis.sprite = B5Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 6: BBCD = B6_CD; ZaidejoKodas.BBPaveikslelis.sprite = B6Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 7: BBCD = B7_CD; ZaidejoKodas.BBPaveikslelis.sprite = B7Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 8: BBCD = B8_CD; ZaidejoKodas.BBPaveikslelis.sprite = B8Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 9: BBCD = B9_CD; ZaidejoKodas.BBPaveikslelis.sprite = B9Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;*/
        }
        switch (Duomenys.B3)
        {
            case 1: BBBCD = B1_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B1Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 2: BBBCD = B2_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B2Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 3: BBBCD = B3_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B3Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 4: BBBCD = B4_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B4Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 5: BBBCD = B5_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B5Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 6: BBBCD = B6_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B6Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 7: BBBCD = B7_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B7Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 8: BBBCD = B8_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B8Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 9: BBBCD = B9_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B9Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;*/
        }
        switch (Duomenys.U)
        {
            case 1: UCD = U1_CD; ZaidejoKodas.UPaveikslelis.sprite = U1Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 2: UCD = U2_CD; ZaidejoKodas.UPaveikslelis.sprite = U2Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 3: UCD = U3_CD; ZaidejoKodas.UPaveikslelis.sprite = U3Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 4: UCD = U4_CD; ZaidejoKodas.UPaveikslelis.sprite = U4Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 5: UCD = U5_CD; ZaidejoKodas.UPaveikslelis.sprite = U5Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 6: UCD = U6_CD; ZaidejoKodas.UPaveikslelis.sprite = U6Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 7: UCD = U7_CD; ZaidejoKodas.UPaveikslelis.sprite = U7Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 8: UCD = U8_CD; ZaidejoKodas.UPaveikslelis.sprite = U8Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 9: UCD = U9_CD; ZaidejoKodas.UPaveikslelis.sprite = U9Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;*/
        }
    }
}