using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;
using Photon.Pun;

public class Vanduo : Elementas
{
	[Header("B 1: ")]
    public Sprite[] B1Paveiksleliai;
	public float B1_CD;

    public float B1Zala;
    public float B1SuletinimoStipris;
    public float B1SuletinimoLaikas;

    public float B1DaiktoGreitis;
	public GameObject B1Daiktas;

	[Header("B 2: ")]
    public Sprite[] B2Paveiksleliai;
	public float B2_CD;

    public float B2Gydymas;
    public float B2GydymoLaikas;

	[Header("B 3: ")]
    public Sprite[] B3Paveiksleliai;
	public float B3_CD;

    public float B3Zala;

    public int B3BangosLaseliuKiekis;
    public float B3Trukme;
    private float LikusiB3Trukme;
    public float B3BanguDaznis;
    private float LikesB3BangosLaikas;

    public float B3LaseliuAtsiradimoSpindulys;
    public float B3Pakelimas;
    private Vector3 B3LaseliuAtsiradimoVieta;
    public float B3SpindulioIlgis;

	public GameObject B3Daiktas;
    public GameObject B3Laselis;

    [Header("B 4: ")]
    public Sprite[] B4Paveiksleliai;
    public float B4_CD;

    public float B4Zala;

    public float B4AtsiradimoNuotolis;
    public float B4DaiktoGreitis;
    public GameObject B4Daiktas;

    [Header("B 5: ")]
    public Sprite[] B5Paveiksleliai;
    public float B5_CD;

    public float B5Zala;

    public float B5JudejimoCCLaikas;
    public float B5DaiktoGreitis;
    public GameObject B5Daiktas;

    [Header("U 1: ")]
    public Sprite[] U1Paveiksleliai;
	public float U1_CD;

    public float U1Zala;

    public float U1AtsiradimoNuotolis;
    public float U1DaiktoGreitis;
	public GameObject U1Daiktas;

    [Header("U 2: ")]
    public Sprite[] U2Paveiksleliai;
    public float U2_CD;

    public float U2Zala;

    public float U2Jega;
    public float U2Daznis;
    public float U2SferosDydis;
    public float U2SpindulioIlgis;
    public float U2Trukme;
    public GameObject U2Daiktas;

    [Header("U 3: ")]
    public Sprite[] U3Paveiksleliai;
    public float U3_CD;

    public float U3Zala;

    public float U3Jega;
    public float U3IlgejimoGreitis;
    public float U3AtsiradimoNuotolis;
    public float U3Delsimas;
    public GameObject U3SaltinioDaiktas;
    public GameObject U3Daiktas;


    public override void Update()
    {
        base.Update();
        B3LaseliuLaikmatis();
    }

    public override void B1()
    {
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas))
        {
            photonView.RPC("RPCKurtiVanduoB1", RpcTarget.All, PataikytasObjektas.point);
        }
    }

    [PunRPC]
    void RPCKurtiVanduoB1(Vector3 ZiurimasTaskas)
    {
        GameObject VanduoB1 = Instantiate(B1Daiktas, KulkuAtsiradimoVieta.position, KulkuAtsiradimoVieta.rotation);
        Kulka KulkosKodas = VanduoB1.GetComponent<Kulka>();

        VanduoB1.transform.LookAt(ZiurimasTaskas);

        KulkosKodas.Greitis = B1DaiktoGreitis;
        KulkosKodas.Zala = B1Zala;
        KulkosKodas.SuletinimoLaikas = B1SuletinimoLaikas;
        KulkosKodas.SuletinimoStipris = B1SuletinimoStipris;
        KulkosKodas.Autorius = gameObject;

        Destroy(VanduoB1, 10);
    }

    public override void B2()
	{
        StartCoroutine(_B2());
    }

    IEnumerator _B2()
    {
        ZaidejoKodas.GebejimuAktyvinimoLaikas = ZaidejoKodas.LikesGebejimuAktyvinimoLaikas = B2GydymoLaikas;
        yield return new WaitForSeconds(B2GydymoLaikas);
        ZaidejoKodas.Gyvybes += B2Gydymas;
    }

    public override void B3()
	{
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas, B3SpindulioIlgis))
        {
            photonView.RPC("RPCKurtiVanduoB3", RpcTarget.All, PataikytasObjektas.point);
        }
        else
        {
            photonView.RPC("RPCKurtiVanduoB3", RpcTarget.All, transform.position);
        }
    }

	[PunRPC]
	void RPCKurtiVanduoB3(Vector3 AtsiradimoVieta)
	{
        AtsiradimoVieta += new Vector3(0, B3Pakelimas, 0);
        GameObject VanduoB3 = Instantiate(B3Daiktas, AtsiradimoVieta, Quaternion.identity);

        VanduoB3.transform.rotation *= Quaternion.Euler(90, 0, 0);

        B3LaseliuAtsiradimoVieta = AtsiradimoVieta;
        LikusiB3Trukme = B3Trukme;

        Destroy(VanduoB3, B3Trukme);
    }

    [PunRPC]
    void RPCKurtiVanduoB3Laseli()
    {
        for (int i = 0; i < B3BangosLaseliuKiekis; i++)
        {
            Vector3 B3LaselioAtsiradimoVieta = B3LaseliuAtsiradimoVieta;
            B3LaselioAtsiradimoVieta.x += Random.Range(-B3LaseliuAtsiradimoSpindulys, B3LaseliuAtsiradimoSpindulys);
            B3LaselioAtsiradimoVieta.z += Random.Range(-B3LaseliuAtsiradimoSpindulys, B3LaseliuAtsiradimoSpindulys);
            GameObject Laselis = Instantiate(B3Laselis, B3LaselioAtsiradimoVieta, Quaternion.Euler(-90, 0, 0));
            Kulka KulkosKodas = Laselis.GetComponent<Kulka>();

            KulkosKodas.Zala = B3Zala;
            KulkosKodas.Autorius = gameObject;

            Destroy(Laselis, 10);
        }
    }

    void B3LaseliuLaikmatis()
    {
        if (LikusiB3Trukme > 0f)
        {
            LikusiB3Trukme -= Time.deltaTime;
            if (LikesB3BangosLaikas > 0)
            {
                LikesB3BangosLaikas -= Time.deltaTime;
            }
            else
            {
                photonView.RPC("RPCKurtiVanduoB3Laseli", RpcTarget.All);
                LikesB3BangosLaikas = B3BanguDaznis;
            }
        }
    }

    public override void B4()
    {
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas))
        {
            photonView.RPC("RPCKurtiVanduoB4", RpcTarget.All, PataikytasObjektas.point);
        }
    }

    [PunRPC]
    void RPCKurtiVanduoB4(Vector3 ZiurimasTaskas)
    {
        Vector3 AtsiradimoVieta = KulkuAtsiradimoVieta.position + (transform.forward * B4AtsiradimoNuotolis);
        GameObject VanduoB4 = Instantiate(B4Daiktas, AtsiradimoVieta, KulkuAtsiradimoVieta.rotation);
        Kulka KulkosKodas = VanduoB4.GetComponent<Kulka>();

        VanduoB4.transform.LookAt(ZiurimasTaskas);

        KulkosKodas.Greitis = B4DaiktoGreitis;
        KulkosKodas.Zala = B4Zala;
        KulkosKodas.Autorius = gameObject;

        Destroy(VanduoB4, 10);
    }

    public override void B5()
    {
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas))
        {
            photonView.RPC("RPCKurtiVanduoB5", RpcTarget.All, PataikytasObjektas.point);
        }
    }

    [PunRPC]
    void RPCKurtiVanduoB5(Vector3 ZiurimasTaskas)
    {
        GameObject VanduoB5 = Instantiate(B5Daiktas, KulkuAtsiradimoVieta.position, KulkuAtsiradimoVieta.rotation);
        Kulka KulkosKodas = VanduoB5.GetComponent<Kulka>();

        VanduoB5.transform.LookAt(ZiurimasTaskas);

        KulkosKodas.Greitis = B5DaiktoGreitis;
        KulkosKodas.Zala = B5Zala;
        KulkosKodas.SustingdymoLaikas = B5JudejimoCCLaikas;
        KulkosKodas.Autorius = gameObject;

        Destroy(VanduoB5, 10);
    }

    public override void U1 ()
	{
        photonView.RPC("RPCKurtiVanduoU1", RpcTarget.All);
    }

	[PunRPC]
	void RPCKurtiVanduoU1()
    {
        Vector3 AtsiradimoVieta = KulkuAtsiradimoVieta.position + (transform.forward * U1AtsiradimoNuotolis) - (transform.up * transform.localScale.y / 2);
        Quaternion AtsiradimoPosukis = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        GameObject VanduoU1 = Instantiate (U1Daiktas, AtsiradimoVieta, AtsiradimoPosukis);
		VanduoU1 KulkosKodas = VanduoU1.GetComponent<VanduoU1>();

        KulkosKodas.Greitis = U1DaiktoGreitis;
		KulkosKodas.Zala = U1Zala;
        KulkosKodas.Autorius = gameObject;

        Destroy (VanduoU1, 10);
	}

    public override void U2()
    {
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas, U2SpindulioIlgis))
        {
            photonView.RPC("RPCKurtiVanduoU2", RpcTarget.All, PataikytasObjektas.point);
        }
        else
        {
            DabUCD = 0;
        }
    }

    [PunRPC]
    void RPCKurtiVanduoU2(Vector3 ZiurimasTaskas)
    {
        GameObject VanduoU2 = Instantiate(U2Daiktas, ZiurimasTaskas, KulkuAtsiradimoVieta.rotation);
        VanduoU2 KulkosKodas = VanduoU2.GetComponent<VanduoU2>();

        KulkosKodas.Zala = U2Zala;
        KulkosKodas.Autorius = gameObject;
        KulkosKodas.Jega = U2Jega;
        KulkosKodas.Daznis = U2Daznis;
        KulkosKodas.Dydis = U2SferosDydis;

        Destroy(VanduoU2, U2Trukme);
    }

    public override void U3()
    {
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas))
        {
            StartCoroutine(U3Aktyvavimas(PataikytasObjektas.point));
        }
    }

    IEnumerator U3Aktyvavimas(Vector3 ZiurimasTaskas)
    {
        ZaidejoKodas.JudejimoLaikoIgnoravimas++;
        ZaidejoKodas.PuolimoLaikoIgnoravimas++;

        photonView.RPC("RPCKurtiVanduoU3Saltini", RpcTarget.All, ZiurimasTaskas);
        yield return new WaitForSeconds(U3Delsimas);
        photonView.RPC("RPCKurtiVanduoU3", RpcTarget.All, ZiurimasTaskas);
    }

    [PunRPC]
    void RPCKurtiVanduoU3Saltini(Vector3 ZiurimasTaskas)
    {
        Vector3 AtsiradimoVieta = KulkuAtsiradimoVieta.position + (transform.forward * U3AtsiradimoNuotolis);
        GameObject VanduoU3Saltinis = Instantiate(U3SaltinioDaiktas, AtsiradimoVieta, KulkuAtsiradimoVieta.rotation);
        Destroy(VanduoU3Saltinis, U3Delsimas + 0.1f);
    }

    [PunRPC]
    void RPCKurtiVanduoU3(Vector3 ZiurimasTaskas)
    {
        Vector3 AtsiradimoVieta = KulkuAtsiradimoVieta.position + (transform.forward * U3AtsiradimoNuotolis);
        GameObject VanduoU3 = Instantiate(U3Daiktas, AtsiradimoVieta, KulkuAtsiradimoVieta.rotation);
        VanduoU3 KulkosKodas = VanduoU3.GetComponent<VanduoU3>();

        VanduoU3.transform.LookAt(ZiurimasTaskas);

        KulkosKodas.Zala = U3Zala;
        KulkosKodas.Autorius = gameObject;
        KulkosKodas.IlgejimoGreitis = U3IlgejimoGreitis;
        KulkosKodas.Jega = U3Jega;

        Destroy(VanduoU3, 10);
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