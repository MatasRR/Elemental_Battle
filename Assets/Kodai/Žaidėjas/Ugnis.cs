using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;
using Photon.Pun;

public class Ugnis : Elementas
{
    [Header("B 1: ")]
    public Sprite[] B1Paveiksleliai;
    public float B1_CD;

    public float B1Zala;
    public float B1_DOTZala;
    public float B1_DOTDaznis;
    public float B1_DOTTrukme;

    public float B1DaiktoGreitis;
    public GameObject B1Daiktas;

    [Header("B 2: ")]
    public Sprite[] B2Paveiksleliai;
    public float B2_CD;

    public float B2GaunamosZalosMod;
    public float B2Trukme;
    public GameObject B2Daiktas;

    [Header("B 3: ")]
    public Sprite[] B3Paveiksleliai;
    public float B3_CD;

    public float B3Zala;
    public float B3ZalojimoDaznis;
    public float B3Nuotolis;
    public float B3Trukme;
    
    public GameObject B3Daiktas;

    [Header("B 4: ")]
    public Sprite[] B4Paveiksleliai;
    public float B4_CD;

    public float B4Trukme;
    public float SoklumoKompensacija;
    public float B4VFXAtsiradimoDaznis;
    private float LikesB4VFXLaikas;
    public GameObject B4VFX;

    [Header("B 5: ")]
    public Sprite[] B5Paveiksleliai;
    public float B5_CD;

    public float B5Zala;
    public float B5StingdymoLaikas;
    public float B5NuginklavimoLaikas;

    public float B5DaiktoGreitis;
    public GameObject B5Daiktas;

    [Header("U 1: ")]
    public Sprite[] U1Paveiksleliai;
    public float U1_CD;

    public float U1Zala;

    public float U1DaiktoGreitis;
    public float U1SunaikinimoLaikasPoKontakto;
    public float U1SpindulioIlgis;
    public GameObject U1Daiktas;

    [Header("U 2: ")]
    public Sprite[] U2Paveiksleliai;
    public float U2_CD;

    public float U2_DOTZala;
    public float U2_DOTDaznis;
    public float U2_DOTTrukme;
    public float U2AtakosSumazinimoStipris;
    public float U2AtakosSumazinimoTrukme;
    public float U2Spindulys;
    public float U2Aukstis;
    public float U2DidejimoLaikas;
    public GameObject U2Daiktas;

    [Header("U 3: ")]
    public Sprite[] U3Paveiksleliai;
    public float U3_CD;

    public float U3Zala;
    public float U3SustingdymoLaikas;
    public float U3NuginklavimoLaikas;

    public float U3SpindulioIlgis;
    public float U3ZaibuAtsiradimoSpindulys;
    public float U3Pakelimas;
    private Vector3 U3ZaibuAtsiradimoVieta;

    public float U3Trukme;
    private float LikusiU3Trukme;
    
    public float U3BangosBeTaikinioDaznis;
    private float LikesU3BangosBeTaikinioLaikas;
    public float U3BangosBeTaikinioLaseliuKiekis;
    public float U3SuTaikiniuDaznis;
    private float LikesU3SuTaikiniuLaikas;

    public float U3Greitis;
    public GameObject U3Daiktas;
    public GameObject U3Zaibas;


    public override void Update()
    {
        base.Update();

        B4Liepsnos();
        U3ZaibuLaikmatis();
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
        GameObject UgnisB1 = Instantiate(B1Daiktas, KulkuAtsiradimoVieta.position, KulkuAtsiradimoVieta.rotation);
        Kulka KulkosKodas = UgnisB1.GetComponent<Kulka>();

        UgnisB1.transform.LookAt(ZiurimasTaskas);

        KulkosKodas.Greitis = B1DaiktoGreitis;
        KulkosKodas.Zala = B1Zala;
        KulkosKodas.DOTZala = B1_DOTZala;
        KulkosKodas.DOTDaznis = B1_DOTDaznis;
        KulkosKodas.DOTTrukme = B1_DOTTrukme;
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

        ZaidejoKodas.GynybosMod *= B2GaunamosZalosMod;
        yield return new WaitForSeconds(B2Trukme);
        ZaidejoKodas.GynybosMod /= B2GaunamosZalosMod;
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
        Vector3 AtsiradimoVieta = transform.position + transform.forward * B3Nuotolis;
        GameObject UgnisB3 = Instantiate(B3Daiktas, AtsiradimoVieta, KulkuAtsiradimoVieta.rotation);
        UgnisB3 KulkosKodas = UgnisB3.GetComponent<UgnisB3>();

        KulkosKodas.Daznis = B3ZalojimoDaznis;
        KulkosKodas.Zala = B3Zala;
        KulkosKodas.Autorius = gameObject;

        Destroy(UgnisB3, B3Trukme);
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
        Vector3 AtsiradimoVieta = KojuVieta.position;
        AtsiradimoVieta.x += Random.Range(-0.5f, 0.5f);
        AtsiradimoVieta.z += Random.Range(-0.5f, 0.5f);

        GameObject UgnisB4VFX = Instantiate(B4VFX, AtsiradimoVieta, Quaternion.identity);

        Destroy(UgnisB4VFX, 2);
    }

    public override void B5()
    {
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas))
        {
            photonView.RPC("RPCKurtiUgnisB5", RpcTarget.All, PataikytasObjektas.point);
        }
    }

    [PunRPC]
    void RPCKurtiUgnisB5(Vector3 ZiurimasTaskas)
    {
        GameObject UgnisB5 = Instantiate(B5Daiktas, KulkuAtsiradimoVieta.position, KulkuAtsiradimoVieta.rotation);
        Kulka KulkosKodas = UgnisB5.GetComponent<Kulka>();

        UgnisB5.transform.LookAt(ZiurimasTaskas);

        KulkosKodas.Greitis = B5DaiktoGreitis;
        KulkosKodas.Zala = B5Zala;
        KulkosKodas.SustingdymoLaikas = B5StingdymoLaikas;
        KulkosKodas.NuginklavimoLaikas = B5NuginklavimoLaikas;
        KulkosKodas.Autorius = gameObject;

        Destroy(UgnisB5, 10);
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
        UgnisU1 KulkosKodas = UgnisU1.GetComponent<UgnisU1>();

        UgnisU1.transform.LookAt(ZiurimasTaskas);
        KulkosKodas.Greitis = U1DaiktoGreitis;
        KulkosKodas.Zala = U1Zala;
        KulkosKodas.Autorius = gameObject;
        KulkosKodas.SunaikinimoLaikasPoKontakto = U1SunaikinimoLaikasPoKontakto;

        Destroy(UgnisU1, 10);
    }

    public override void U2()
    {
        StartCoroutine(_U2());
    }

    IEnumerator _U2()
    {
        ZaidejoKodas.JudejimoLaikoIgnoravimas++;
        ZaidejoKodas.PuolimoLaikoIgnoravimas++;

        photonView.RPC("RPCKurtiUgnisU2", RpcTarget.All);

        yield return new WaitForSeconds(U2DidejimoLaikas);

        ZaidejoKodas.JudejimoLaikoIgnoravimas--;
        ZaidejoKodas.PuolimoLaikoIgnoravimas--;
    }

    [PunRPC]
    void RPCKurtiUgnisU2()
    {
        GameObject UgnisU2 = Instantiate(U2Daiktas, transform.position, /*transform.rotation*/Quaternion.Euler(-90, 0, 0));
        UgnisU2 KulkosKodas = UgnisU2.GetComponent<UgnisU2>();
        UgnisU2.transform.SetParent(transform);

        KulkosKodas.DOTZala = U2_DOTZala;
        KulkosKodas.DOTDaznis = U2_DOTDaznis;
        KulkosKodas.DOTTrukme = U2_DOTTrukme;
        KulkosKodas.Autorius = gameObject;
        KulkosKodas.AtakosSumazinimoStipris = U2AtakosSumazinimoStipris;
        KulkosKodas.AtakosSumazinimoTrukme = U2AtakosSumazinimoTrukme;
        KulkosKodas.Spindulys = U2Spindulys;
        KulkosKodas.Aukstis = U2Aukstis;
        KulkosKodas.DidejimoLaikas = U2DidejimoLaikas;

        Destroy(UgnisU2, U2DidejimoLaikas);
    }

    public override void U3()
    {
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas, U3SpindulioIlgis))
        {
            photonView.RPC("RPCKurtiUgnisU3", RpcTarget.All, PataikytasObjektas.point);
        }
        else
        {
            photonView.RPC("RPCKurtiUgnisU3", RpcTarget.All, transform.position);
        }
    }

    [PunRPC]
    void RPCKurtiUgnisU3(Vector3 AtsiradimoVieta)
    {
        AtsiradimoVieta += new Vector3(0, U3Pakelimas, 0);
        GameObject VanduoU3 = Instantiate(U3Daiktas, AtsiradimoVieta, Quaternion.identity);

        VanduoU3.transform.rotation *= Quaternion.Euler(-90, 0, 0);

        U3ZaibuAtsiradimoVieta = AtsiradimoVieta;
        LikusiU3Trukme = U3Trukme;

        Destroy(VanduoU3, U3Trukme);
    }

    void U3ZaibuLaikmatis()
    {
        if (LikusiU3Trukme > 0f)
        {
            LikusiU3Trukme -= Time.deltaTime;

            /// Be taikinio
            if (LikesU3BangosBeTaikinioLaikas > 0)
            {
                LikesU3BangosBeTaikinioLaikas -= Time.deltaTime;
            }
            else
            {
                photonView.RPC("RPCKurtiUgnisU3ZaibuBangaBeTaikinio", RpcTarget.All);
                LikesU3BangosBeTaikinioLaikas = U3BangosBeTaikinioDaznis;
            }
            
            /// Su taikiniu
            if (LikesU3SuTaikiniuLaikas > 0)
            {
                LikesU3SuTaikiniuLaikas -= Time.deltaTime;
            }
            else
            {
                photonView.RPC("RPCKurtiUgnisU3ZaibaSuTaikiniu", RpcTarget.All);
                LikesU3SuTaikiniuLaikas = U3SuTaikiniuDaznis;
            }
        }
    }

    [PunRPC]
    void RPCKurtiUgnisU3ZaibuBangaBeTaikinio()
    {
        for (int i = 0; i < U3BangosBeTaikinioLaseliuKiekis; i++)
        {
            Vector3 U3ZaiboAtsiradimoVieta = U3ZaibuAtsiradimoVieta;
            U3ZaiboAtsiradimoVieta.x += Random.Range(-U3ZaibuAtsiradimoSpindulys, U3ZaibuAtsiradimoSpindulys);
            U3ZaiboAtsiradimoVieta.z += Random.Range(-U3ZaibuAtsiradimoSpindulys, U3ZaibuAtsiradimoSpindulys);
            GameObject Zaibas = Instantiate(U3Zaibas, U3ZaiboAtsiradimoVieta, Quaternion.Euler(90, 0, 0));
            Kulka KulkosKodas = Zaibas.GetComponent<Kulka>();

            KulkosKodas.Zala = U3Zala;
            KulkosKodas.Greitis = U3Greitis;
            KulkosKodas.SustingdymoLaikas = U3SustingdymoLaikas;
            KulkosKodas.NuginklavimoLaikas = U3NuginklavimoLaikas;
            KulkosKodas.Autorius = gameObject;
            KulkosKodas.ElementoNr = 4;

            Destroy(Zaibas, 10);
        }            
    }

    [PunRPC]
    void RPCKurtiUgnisU3ZaibaSuTaikiniu()
    {
        RaycastHit Pataike;
        if (Physics.SphereCast(U3ZaibuAtsiradimoVieta, U3ZaibuAtsiradimoSpindulys, Vector3.down, out Pataike, U3Pakelimas * 5))
        {
            if (Pataike.transform.GetComponent<Zaidejas>() != null)
            {
                Vector3 U3ZaiboAtsiradimoVieta = new Vector3(Pataike.transform.position.x, U3ZaibuAtsiradimoVieta.y, Pataike.transform.position.z);

                GameObject Zaibas = Instantiate(U3Zaibas, U3ZaiboAtsiradimoVieta, Quaternion.Euler(90, 0, 0));
                Kulka KulkosKodas = Zaibas.GetComponent<Kulka>();

                KulkosKodas.Zala = U3Zala;
                KulkosKodas.Greitis = U3Greitis;
                KulkosKodas.SustingdymoLaikas = U3SustingdymoLaikas;
                KulkosKodas.NuginklavimoLaikas = U3NuginklavimoLaikas;
                KulkosKodas.Autorius = gameObject;

                Destroy(Zaibas, 10);
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
            case 2: UCD = U2_CD; ZaidejoKodas.UPaveikslelis.sprite = U2Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
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