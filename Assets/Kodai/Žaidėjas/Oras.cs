using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;
using Photon.Pun;

public class Oras : Elementas
{
    [Header("B 1: ")]
    public Sprite[] B1Paveiksleliai;
	public float B1_CD;

	public float B1Zala;

	public GameObject B1Daiktas;
	public float B1DaiktoGreitis;

	[Header("B 2: ")]
    public Sprite[] B2Paveiksleliai;
    public float B2_CD;

    public float B2SkydoStiprumas;

	[Header("B 3: ")]
    public Sprite[] B3Paveiksleliai;
    public float B3_CD;

    public float B3GreicioMod;
    public float B3SoklumoMod;

    public float B3Trukme;

	[Header("B 4: ")]
    public Sprite[] B4Paveiksleliai;
    public float B4_CD;

    public int B4Trukme;
    public float SoklumoKompensacija;

    [Header("B 5: ")]
    public Sprite[] B5Paveiksleliai;
    public float B5_CD;

    public float B5Zala;

    public float B5Jega;
    public float B5DaiktoGreitis;
    public GameObject B5Daiktas;

    [Header("B 6: ")]
    public Sprite[] B6Paveiksleliai;
    public float B6_CD;

    public float B6SkydoGyvybes;
    public float B6MaxSugeriamaZala;
    public float B6Trukme;
    public float B6Nuotolis;
    public GameObject B6Daiktas;

    [Header("U 1: ")]
    public Sprite[] U1Paveiksleliai;
    public float U1_CD;

    public float U1Zala;
    public float U1Skydas;
	public float U1Jega;
	public float U1Skersmuo;
    public float U1LaukimoLaikas;
    public float U1DidejimoLaikas;
	public GameObject U1Daiktas;

    [Header("U 2: ")]
    public Sprite[] U2Paveiksleliai;
    public float U2_CD;

    public float U2Zala;

    public float U2Jega;
    public float U2DaiktoGreitis;
    public float U2AtsiradimoAukstis;
    public GameObject U2Daiktas;


    public override void B1 ()
	{
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas))
        {
            photonView.RPC("RPCKurtiOrasB1", RpcTarget.All, PataikytasObjektas.point);
        }
    }

	[PunRPC]
	void RPCKurtiOrasB1 (Vector3 ZiurimasTaskas)
	{
		GameObject OrasB1 = Instantiate (B1Daiktas, KulkosAtsiradimoVieta.position, KulkosAtsiradimoVieta.rotation);
		Kulka KulkosKodas = OrasB1.GetComponent<Kulka> ();

        OrasB1.transform.LookAt (ZiurimasTaskas);

        KulkosKodas.Greitis = B1DaiktoGreitis;
        KulkosKodas.Zala = B1Zala;
        KulkosKodas.Autorius = gameObject;

        Destroy (OrasB1, 10);
	}

    public override void B2 ()
	{
        ZaidejoKodas.Skydas += B2SkydoStiprumas;
    }

    public override void B3()
    {
        StartCoroutine(_B3());
    }

	IEnumerator _B3 ()
	{
		ZaidejoKodas.GreicioMod *= B3GreicioMod;
		ZaidejoKodas.SoklumoMod *= B3SoklumoMod;
        ZaidejoKodas.GreicioIrSoklumoPerskaiciavimas();
		yield return new WaitForSeconds (B3Trukme);
		ZaidejoKodas.GreicioMod /= B3GreicioMod;
		ZaidejoKodas.SoklumoMod /= B3SoklumoMod;
        ZaidejoKodas.GreicioIrSoklumoPerskaiciavimas();
    }

    public override void B4 ()
    {
        StartCoroutine(_B4());
    }

	IEnumerator _B4 ()
	{
        ZaidejoKodas.SkraidymoCCLaikas += B4Trukme;
        ZaidejoKodas.SoklumoMod *= SoklumoKompensacija;
        ZaidejoKodas.GreicioIrSoklumoPerskaiciavimas();
        //gameObject.GetComponent<Rigidbody>().useGravity = false;
		yield return new WaitForSeconds (B4Trukme);
        ZaidejoKodas.SoklumoMod /= SoklumoKompensacija;
        ZaidejoKodas.GreicioIrSoklumoPerskaiciavimas();
        //gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    public override void B5()
    {
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas))
        {
            photonView.RPC("RPCKurtiOrasB5", RpcTarget.All, PataikytasObjektas.point);
        }
    }

    [PunRPC]
    void RPCKurtiOrasB5(Vector3 ZiurimasTaskas)
    {
        GameObject OrasB5 = Instantiate(B5Daiktas, transform.position, transform.rotation);
        OrasB5 KulkosKodas = OrasB5.GetComponent<OrasB5>();

        OrasB5.transform.LookAt(ZiurimasTaskas);

        KulkosKodas.Greitis = B5DaiktoGreitis;
        KulkosKodas.Zala = B5Zala;
        KulkosKodas.Autorius = gameObject;
        KulkosKodas.Jega = B5Jega;

        Destroy(OrasB5, 10);
    }

    public override void B6()
    {
        if (Physics.Raycast(Kamera.ScreenPointToRay(Input.mousePosition), out RaycastHit PataikytasObjektas))
        {
            photonView.RPC("RPCKurtiOrasB6", RpcTarget.All, PataikytasObjektas.point);
        }
    }

    [PunRPC]
    void RPCKurtiOrasB6(Vector3 ZiurimasTaskas)
    {
        Vector3 Kryptis = (transform.position - ZiurimasTaskas).normalized;
        Vector3 AtsiradimoVieta = transform.position + transform.forward * B6Nuotolis;
        GameObject OrasB6 = Instantiate(B6Daiktas, AtsiradimoVieta, KulkosAtsiradimoVieta.rotation);
        Skydas SkydoKodas = OrasB6.GetComponent<Skydas>();

        OrasB6.transform.SetParent(gameObject.transform);

        SkydoKodas.Gyvybes = B6SkydoGyvybes;
        SkydoKodas.MaxSugeriamaZala = B6MaxSugeriamaZala;
        SkydoKodas.Autorius = gameObject;

        Destroy(OrasB6, B6Trukme);
    }

    public override void U1()
    {
        StartCoroutine(_U1());
    }

    IEnumerator _U1 ()
	{
		Rigidbody RB = GetComponent<Rigidbody> ();
		RB.isKinematic = true;
		ZaidejoKodas.JudejimoLaikoIgnoravimas++;
        ZaidejoKodas.PuolimoLaikoIgnoravimas++;

        ZaidejoKodas.Skydas += U1Skydas;
        photonView.RPC("RPCKurtiOrasU1", RpcTarget.All);

        yield return new WaitForSeconds (U1LaukimoLaikas + U1DidejimoLaikas);

        ZaidejoKodas.JudejimoLaikoIgnoravimas--;
        ZaidejoKodas.PuolimoLaikoIgnoravimas--;
        RB.isKinematic = false;
	}

	[PunRPC]
	void RPCKurtiOrasU1 ()
	{
        GameObject OrasU1 = Instantiate (U1Daiktas, transform.position, transform.rotation);
        OrasU1 KulkosKodas = OrasU1.GetComponent<OrasU1>();
        
        KulkosKodas.Zala = U1Zala;
        KulkosKodas.Autorius = gameObject;
        KulkosKodas.Jega = U1Jega;
        KulkosKodas.Skersmuo = U1Skersmuo;
        KulkosKodas.LaukimoLaikas = U1LaukimoLaikas;
        KulkosKodas.DidejimoLaikas = U1DidejimoLaikas;

        Destroy(OrasU1, U1LaukimoLaikas + U1DidejimoLaikas);
    }

    public override void U2()
    {
        photonView.RPC("RPCKurtiOrasU2", RpcTarget.All);
    }

    [PunRPC]
    void RPCKurtiOrasU2()
    {
        Vector3 AtsiradimoVieta = KulkosAtsiradimoVieta.position;
        AtsiradimoVieta.y = U2AtsiradimoAukstis;
        Quaternion AtsiradimoPosukis = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        GameObject OrasU2 = Instantiate(U2Daiktas, AtsiradimoVieta, AtsiradimoPosukis);
        OrasU2 KulkosKodas = OrasU2.GetComponent<OrasU2>();

        KulkosKodas.Greitis = U2DaiktoGreitis;
        KulkosKodas.Zala = U2Zala;
        KulkosKodas.Autorius = gameObject;
        KulkosKodas.Jega = U2Jega;

        Destroy(OrasU2, 10);
    }

    public override void CDNustatymas()
    {
        switch (Duomenys.B1)
        {
            case 1: BCD = B1_CD; ZaidejoKodas.BPaveikslelis.sprite = B1Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 2: BCD = B2_CD; ZaidejoKodas.BPaveikslelis.sprite = B2Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 3: BCD = B3_CD; ZaidejoKodas.BPaveikslelis.sprite = B3Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 4: BCD = B4_CD; ZaidejoKodas.BPaveikslelis.sprite = B4Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
            case 5: BCD = B5_CD; ZaidejoKodas.BPaveikslelis.sprite = B5Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
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
            case 5: BBCD = B5_CD; ZaidejoKodas.BBPaveikslelis.sprite = B5Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
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
            case 5: BBBCD = B5_CD; ZaidejoKodas.BBBPaveikslelis.sprite = B5Paveiksleliai[ZaidejoKodas.ZaidejoPaveiksleliuNr]; break;
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