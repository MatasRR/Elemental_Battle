using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;
using Photon.Pun;

public class Oras : Elementas
{
    [Header("B 1: ")]
	public float B1_CD;

	public float B1Zala;

	public GameObject B1Daiktas;
	public float B1DaiktoGreitis;

	[Header("B 2: ")]
	public int B2_CD;

	public float B2SkydoStiprumas;

	[Header("B 3: ")]
	public int B3_CD;

    public float B3GreicioMod;
    public float B3SoklumoMod;

    public float B3Trukme;

	[Header("B 4: ")]
	public float B4_CD;

    public int B4Trukme;
    public float SoklumoKompensacija;

    [Header("B 5: ")]
    public float B5_CD;

    public float B5Zala;

    public float B5Jega;
    public float B5DaiktoGreitis;
    public GameObject B5Daiktas;

    [Header("B 6: ")]
    public float B6_CD;

    public float B6SkydoGyvybes;
    public float B6MaxSugeriamaZala;
    public float B6Trukme;
    public float B6Nuotolis;
    public GameObject B6Daiktas;

    [Header("U 1: ")]
	public float U1_CD;

	public float U1Zala;
	public float U1Jega;
	public float U1Dydis;
	public GameObject U1Daiktas;

    [Header("U 2: ")]
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
        GameObject OrasB5 = Instantiate(B5Daiktas, KulkosAtsiradimoVieta.position, KulkosAtsiradimoVieta.rotation);
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

        SkydoKodas.Gyvybes = B6SkydoGyvybes;
        SkydoKodas.MaxSugeriamaZala = B6MaxSugeriamaZala;

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

        yield return new WaitForSeconds (2);

        photonView.RPC("RPCKurtiOrasU1", RpcTarget.All);

        ZaidejoKodas.JudejimoLaikoIgnoravimas--;
        ZaidejoKodas.PuolimoLaikoIgnoravimas--;
        RB.isKinematic = false;
	}

	[PunRPC]
	void RPCKurtiOrasU1 ()
	{
        GameObject OrasU1 = Instantiate (U1Daiktas, transform.position, transform.rotation);
        Destroy(OrasU1, 0.75f);

        Collider[] Kiti = Physics.OverlapSphere(transform.position, U1Dydis);
		foreach (Collider PataikeKitam in Kiti)
		{
            if (PataikeKitam.gameObject == gameObject)
            {
                return;
            }
            
            Rigidbody KituRB = PataikeKitam.GetComponent<Rigidbody> ();
			if (KituRB != null)
			{
                KituRB.AddExplosionForce (U1Jega, transform.position, U1Dydis, 0f, ForceMode.Impulse);
			}

            if (PataikeKitam.CompareTag("Player"))
            {
                PataikeKitam.GetComponent<Zaidejas>().GautiZalos(U1Zala);
            }
            else if (PataikeKitam.CompareTag("Skydas"))
            {
                PataikeKitam.GetComponent<Skydas>().GautiZalos(U1Zala);
            }
        }
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

    public override void CDNustatymas ()
    {
        switch (Duomenys.B1)
        {
            case 1: BCD = B1_CD; break;
            case 2: BCD = B2_CD; break;
            case 3: BCD = B3_CD; break;
            case 4: BCD = B4_CD; break;
            case 5: BCD = B5_CD; break;
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
            case 4: BBCD = B4_CD; break;
            case 5: BBCD = B5_CD; break;
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
            case 4: BBBCD = B4_CD; break;
            case 5: BBBCD = B5_CD; break;
            case 6: BBBCD = B6_CD; break;/*
            case 7: BBBCD = B7_CD; break;/*
            case 8: BBBCD = B8_CD; break;/*
            case 9: BBBCD = B9_CD; break;*/
        }
        switch (Duomenys.U)
        {
            case 1: UCD = U1_CD; break;
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