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
    public float B1Nuotolis;

    public float B1DaiktoGreitis;
    public GameObject B1Daiktas;

    [Header("B 2: ")]
    public Sprite[] B2Paveiksleliai;
    public float B2_CD;

    public float B2Nuotolis;

    public float B2Gyvybes;
    public float B2MaxSugeriamaZala;
    public float B2Aukstis;
    public float B2DidejimoLaikas;
    public float B2Spindulys;
    public float B2Trukme;
    public GameObject B2Daiktas;

    [Header("B 3: ")]
    public Sprite[] B3Paveiksleliai;
    public float B3_CD;

    public float B3Zala;
    public float B3Nuotolis;

    public float B3DaiktoGreitis;
    public float B3SukimosiGreitis;
    public GameObject B3Daiktas;

    [Header("B 4: ")]
    public Sprite[] B4Paveiksleliai;
    public float B4_CD;

    public float B4Zala;
    public float B4Nuotolis;

    public float B4Aukstis;
    public float B4DidejimoLaikas;

    public int B4SpygliuSkaicius;
    public float B4SpygliuTrukme;
    public float B4AtstumasTarpSpygliu;
    public float B4LaikasTarpSpygliuSukurimo;
    public float B4TikrinimoAukstis;
    public GameObject B4Daiktas;

    [Header("B 5: ")]
    public Sprite[] B5Paveiksleliai;
    public float B5_CD;

    public float B5Gyvybes;
    public float B5MaxSugeriamaZala;
    public float B5Aukstis;
    public float B5DidejimoLaikas;
    public float B5Trukme;
    public GameObject B5Daiktas;

    [Header("U 1: ")]
    public Sprite[] U1Paveiksleliai;
    public float U1_CD;

    public float U1Zala;
    
    public float U1Delsimas;
    public float U1JudejimoCCLaikas;
    public GameObject U1Daiktas;

    [Header("U 2: ")]
    public Sprite[] U2Paveiksleliai;
    public float U2_CD;

    public float U2Zala;
    public float U2ZalaPoKontakto;
    public float U2Nuotolis;

    public float U2SpindulioIlgis;
    public float U2SkrydzioAukstis;
    public float U2DaiktoGreitis;
    public float U2SunaikinimoLaikasPoKontakto;
    public GameObject U2Daiktas;


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
        GameObject ZemeB1 = Instantiate(B1Daiktas, KulkosVieta(B1Nuotolis), transform.rotation);
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
        Vector3 AtsiradimoVieta;
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas, B2Spindulys))
        {
            AtsiradimoVieta = PataikytasObjektas.point;
        }
        else
        {
            AtsiradimoVieta = KulkosVieta(B2Nuotolis);
            AtsiradimoVieta.y = transform.position.y - 1;
        }
        
        GameObject ZemeB2 = Instantiate(B2Daiktas, AtsiradimoVieta, transform.rotation);
        ZemeB2irB5 SkydoKodas = ZemeB2.GetComponent<ZemeB2irB5>();

        SkydoKodas.Gyvybes = B2Gyvybes;
        SkydoKodas.MaxSugeriamaZala = B2MaxSugeriamaZala;
        SkydoKodas.Autorius = gameObject;
        SkydoKodas.Aukstis = B2Aukstis;
        SkydoKodas.DidejimoLaikas = B2DidejimoLaikas;

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
        GameObject ZemeB3 = Instantiate(B3Daiktas, KulkosVieta(B3Nuotolis), transform.rotation);
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
        Vector3 Kryptis = transform.forward;
        Vector3 SpindulioSaltinis = transform.position + new Vector3(0, B4TikrinimoAukstis, 0) + Kryptis * B4Nuotolis;

        int AplinkosSluoksnis = LayerMask.GetMask("Aplinka");

        ZaidejoKodas.PradetiGebejimoAktyvavimoLaukima(B4LaikasTarpSpygliuSukurimo * B4SpygliuSkaicius);

        for (int i = 0; i < B4SpygliuSkaicius; i++)
        {
            if (Physics.Raycast(SpindulioSaltinis + i * B4AtstumasTarpSpygliu * Kryptis, new Vector3(0, -1, 0), out RaycastHit PataikytasObjektas, AplinkosSluoksnis))
            {
                photonView.RPC("RPCKurtiZemeB4", RpcTarget.All, PataikytasObjektas.point);
            }
            yield return new WaitForSeconds(B4LaikasTarpSpygliuSukurimo);
        }
    }

    [PunRPC]
    void RPCKurtiZemeB4(Vector3 ZiurimasTaskas)
    {     
        GameObject ZemeB4 = Instantiate(B4Daiktas, ZiurimasTaskas, Quaternion.Euler(-90, 0, 0));
        ZemeB4 KulkosKodas = ZemeB4.GetComponent<ZemeB4>();
        
        KulkosKodas.Zala = B3Zala;
        KulkosKodas.Autorius = gameObject;
        KulkosKodas.Aukstis = B4Aukstis;
        KulkosKodas.DidejimoLaikas = B4DidejimoLaikas;
        KulkosKodas.Trukme = B4SpygliuTrukme;
    }

    public override void B5()
    {
        photonView.RPC("RPCKurtiZemeB5", RpcTarget.All);
    }

    [PunRPC]
    void RPCKurtiZemeB5()
    {
        GameObject ZemeB5 = Instantiate(B5Daiktas, KulkosVieta(0, -1), transform.rotation);
        ZemeB2irB5 SkydoKodas = ZemeB5.GetComponent<ZemeB2irB5>();

        SkydoKodas.Gyvybes = B5Gyvybes;
        SkydoKodas.MaxSugeriamaZala = B5MaxSugeriamaZala;
        SkydoKodas.Autorius = gameObject;
        SkydoKodas.Aukstis = B5Aukstis;
        SkydoKodas.DidejimoLaikas = B5DidejimoLaikas;

        Destroy(ZemeB5, B2Trukme);
    }

    public override void U1()
    {
        StartCoroutine(_U1());
    }

    IEnumerator _U1()
    {
        ZaidejoKodas.JudejimoLaikoIgnoravimas++;
        ZaidejoKodas.PuolimoLaikoIgnoravimas++;
        
        yield return new WaitForSeconds(U1Delsimas);

        photonView.RPC("RPCKurtiZemeU1", RpcTarget.All);

        ZaidejoKodas.JudejimoLaikoIgnoravimas--;
        ZaidejoKodas.PuolimoLaikoIgnoravimas--;
    }

    [PunRPC]
    void RPCKurtiZemeU1()
    {
        GameObject ZemeU1 = Instantiate(U1Daiktas, transform.position, transform.rotation);
        ZemeU1 KulkosKodas = ZemeU1.GetComponent<ZemeU1>();
        
        KulkosKodas.Zala = U1Zala;
        KulkosKodas.Autorius = gameObject;
        KulkosKodas.SustingdymoLaikas = U1JudejimoCCLaikas;

        Destroy(ZemeU1, 0.5f);
    }

    public override void U2()
    {
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas, U2SpindulioIlgis))
        {
            photonView.RPC("RPCKurtiZemeU2", RpcTarget.All, PataikytasObjektas.point);
        }
        else
        {
            DabUCD = 0;
        }
    }

    [PunRPC]
    void RPCKurtiZemeU2(Vector3 ZiurimasTaskas)
    {
        GameObject ZemeU2 = Instantiate(U2Daiktas, KulkosVieta(U2Nuotolis), transform.rotation);
        ZemeU2 KulkosKodas = ZemeU2.GetComponent<ZemeU2>();
        
        KulkosKodas.Zala = U2Zala;
        KulkosKodas.ZalaPoKontakto = U2ZalaPoKontakto;
        KulkosKodas.Autorius = gameObject;
        KulkosKodas.Tikslas = ZiurimasTaskas;
        KulkosKodas.Aukstis = U2SkrydzioAukstis + Mathf.Max(transform.position.y, ZiurimasTaskas.y);
        KulkosKodas.SunaikinimoLaikasPoKontakto = U2SunaikinimoLaikasPoKontakto;

        Destroy(ZemeU2, 10);
    }

    public override void CDNustatymas()
    {
        switch (Duomenys.B1)
        {
            case 1: BCD = B1_CD; ZaidejoKodas.BPaveikslelis.sprite = B1Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 2: BCD = B2_CD; ZaidejoKodas.BPaveikslelis.sprite = B2Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 3: BCD = B3_CD; ZaidejoKodas.BPaveikslelis.sprite = B3Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 4: BCD = B4_CD; ZaidejoKodas.BPaveikslelis.sprite = B4Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
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
            case 4: BBCD = B4_CD; ZaidejoKodas.BBPaveikslelis.sprite = B4Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
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
            case 4: BBBCD = B4_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B4Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 5: BBBCD = B5_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B5Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 6: BBBCD = B6_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B6Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 7: BBBCD = B7_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B7Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 8: BBBCD = B8_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B8Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;/*
            case 9: BBBCD = B9_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B9Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;*/
        }
        switch (Duomenys.U)
        {
            case 1: UCD = U1_CD; ZaidejoKodas.UPaveikslelis.sprite = U1Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
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