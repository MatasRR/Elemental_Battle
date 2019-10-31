using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;
using Photon.Pun;

public class Ugnis : Elementas
{
    [Header("B 1: ")]
    public float B1_CD;

    public float B1Zala;

    public float B1DaiktoGreitis;
    public GameObject B1Daiktas;

    [Header("B 2: ")]
    public float B2_CD;

    public float B2GaunamosZalosMod;
    public float B2Trukme;
    public GameObject B2Daiktas;

    [Header("B 3: ")]
    public float B3_CD;

    public float B3Zala;

    public GameObject B3Daiktas;

    [Header("B 4: ")]
    public float B4_CD;

    public float B4Trukme;
    public float SoklumoKompensacija;
    public float B4VFXAtsiradimoDaznis;
    private float LikesB4VFXLaikas;
    public Transform B4VFXAtsiradimoVieta;
    public GameObject B4VFX;

    [Header("U 1: ")]
    public float U1_CD;

    public float U1Zala;

    public float U1DaiktoGreitis;
    public float U1SpindulioIlgis;
    public GameObject U1Daiktas;
    

    public override void Update()
    {
        base.Update();

        B4Liepsnos();
    }

    public override void B1()
    {
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas))
        {
            photonView.RPC("RPCKurtiUgnisB1", RpcTarget.All, PataikytasObjektas.point);
        }
    }

    [PunRPC]
    void RPCKurtiUgnisB1(Vector3 ZiurimasTaskas)
    {
        GameObject UgnisB1 = Instantiate(B1Daiktas, KulkosAtsiradimoVieta.position, KulkosAtsiradimoVieta.rotation);
        Kulka KulkosKodas = UgnisB1.GetComponent<Kulka>();

        UgnisB1.transform.LookAt(ZiurimasTaskas);

        KulkosKodas.Greitis = B1DaiktoGreitis;
        KulkosKodas.Zala = B1Zala;
        KulkosKodas.Autorius = gameObject;

        Destroy(UgnisB1, 10);
    }

    public override void B2()
    {
        StartCoroutine(_B2());
    }

    IEnumerator _B2()
    {
        photonView.RPC("RPCKurtiUgnisB2", RpcTarget.All);

        ZaidejoKodas.ZalosMod *= B2GaunamosZalosMod;
        yield return new WaitForSeconds(B2Trukme);
        ZaidejoKodas.ZalosMod /= B2GaunamosZalosMod;
    }

    [PunRPC]
    void RPCKurtiUgnisB2()
    {
        GameObject UgnisB2 = Instantiate(B2Daiktas, transform.position, transform.rotation);
        UgnisB2.transform.SetParent(transform);

        Destroy(UgnisB2, B2Trukme);
    }

    public override void B3()
    {
        photonView.RPC("RPCKurtiUgnisB3", RpcTarget.All);
    }

    [PunRPC]
    void RPCKurtiUgnisB3()
    {
        Vector3 AtsiradimoVieta = transform.position + new Vector3(0, 0, 0); /// Reiketu priesais zaideja
        GameObject UgnisB3 = Instantiate(B3Daiktas, AtsiradimoVieta, Quaternion.Euler(-90, 0, 0));
        Kulka KulkosKodas = UgnisB3.GetComponent<Kulka>();

        KulkosKodas.Zala = B3Zala;
        KulkosKodas.Autorius = gameObject;

        Destroy(UgnisB3, 10);
    }

    public override void B4()
    {
        StartCoroutine(_B4());
    }

    IEnumerator _B4()
    {
        ZaidejoKodas.SkraidymoCCLaikas += B4Trukme;
        ZaidejoKodas.SoklumoMod *= SoklumoKompensacija;
        ZaidejoKodas.GreicioIrSoklumoPerskaiciavimas();
        yield return new WaitForSeconds(B4Trukme);
        ZaidejoKodas.SoklumoMod /= SoklumoKompensacija;
        ZaidejoKodas.GreicioIrSoklumoPerskaiciavimas();
    }

    void B4Liepsnos()
    {
        LikesB4VFXLaikas -= Time.deltaTime;
        if (!JudejimoKodas.AntZemes() && ZaidejoKodas.GaliSkraidyti && LikesB4VFXLaikas <= 0)
        {
            LikesB4VFXLaikas = B4VFXAtsiradimoDaznis;
            photonView.RPC("RPCKurtiUgnisB4", RpcTarget.All);
        }
    }

    [PunRPC]
    void RPCKurtiUgnisB4()
    {
        Vector3 AtsiradimoVieta = B4VFXAtsiradimoVieta.position;
        AtsiradimoVieta.x += Random.Range(-0.5f, 0.5f);
        AtsiradimoVieta.z += Random.Range(-0.5f, 0.5f);

        GameObject UgnisB4VFX = Instantiate(B4VFX, AtsiradimoVieta, Quaternion.identity);

        Destroy(UgnisB4VFX, 2);
    }

    public override void U1()
    {
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas, U1SpindulioIlgis))
        {
            photonView.RPC("RPCKurtiUgnisU1", RpcTarget.All, PataikytasObjektas.point);
        }
        else
        {
            DabUCD = 0;
        }
    }

    [PunRPC]
    void RPCKurtiUgnisU1(Vector3 ZiurimasTaskas)
    {
        Vector3 AtsiradimoVieta = ZiurimasTaskas + new Vector3(Random.Range(-100f, 100f), 100, Random.Range(-100f, 100f));
        Quaternion AtsiradimoPosukis = transform.rotation * Quaternion.identity;
        GameObject UgnisU1 = Instantiate(U1Daiktas, AtsiradimoVieta, AtsiradimoPosukis);
        Kulka KulkosKodas = UgnisU1.GetComponent<Kulka>();

        UgnisU1.transform.LookAt(ZiurimasTaskas);
        KulkosKodas.Greitis = U1DaiktoGreitis;
        KulkosKodas.Zala = U1Zala;
        KulkosKodas.Autorius = gameObject;

        Destroy(UgnisU1, 10);
    }


    public override void CDNustatymas()
    {
        switch (Duomenys.B1)
        {
            case 1: BCD = B1_CD; break;
            case 2: BCD = B2_CD; break;
            case 3: BCD = B3_CD; break;
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
            case 3: BBCD = B3_CD; break;
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
            case 3: BBBCD = B3_CD; break;
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