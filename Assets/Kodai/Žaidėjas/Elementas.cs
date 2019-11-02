using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Photon;
using Photon.Pun;
using TMPro;

[RequireComponent(typeof(Zaidejas))]
public class Elementas : MonoBehaviourPun
{
    [HideInInspector]
    public Zaidejas ZaidejoKodas;

    [HideInInspector]
    public Judejimas JudejimoKodas;

    [HideInInspector]
    public Camera Kamera;

    [HideInInspector]
    public Transform KulkosAtsiradimoVieta;

    [HideInInspector]
    public float BCD = 0;
    [HideInInspector]
    public float BBCD = 0;
    [HideInInspector]
    public float BBBCD = 0;
    [HideInInspector]
    public float UCD = 0;
    [HideInInspector]
    public float DabBCD = 0;
    [HideInInspector]
    public float DabBBCD = 0;
    [HideInInspector]
    public float DabBBBCD = 0;
    [HideInInspector]
    public float DabUCD = 0;
    
    [HideInInspector]
    public TextMeshProUGUI BCDTekstas;
    [HideInInspector]
    public TextMeshProUGUI BBCDTekstas;
    [HideInInspector]
    public TextMeshProUGUI BBBCDTekstas;
    [HideInInspector]
    public TextMeshProUGUI UCDTekstas;

    [HideInInspector]
    public Image BCDFonas;
    [HideInInspector]
    public Image BBCDFonas;
    [HideInInspector]
    public Image BBBCDFonas;
    [HideInInspector]
    public Image UCDFonas;


    public virtual void Start()
    {
        ZaidejoKodas = GetComponent<Zaidejas>();
        JudejimoKodas = GetComponent<Judejimas>();
        Kamera = ZaidejoKodas.Kamera;
        KulkosAtsiradimoVieta = ZaidejoKodas.KulkosAtsiradimoVieta;
        BCDTekstas = ZaidejoKodas.BCDTekstas;
        BBCDTekstas = ZaidejoKodas.BBCDTekstas;
        BBBCDTekstas = ZaidejoKodas.BBBCDTekstas;
        UCDTekstas = ZaidejoKodas.UCDTekstas;
        BCDFonas = ZaidejoKodas.BCDFonas;
        BBCDFonas = ZaidejoKodas.BBCDFonas;
        BBBCDFonas = ZaidejoKodas.BBBCDFonas;
        UCDFonas = ZaidejoKodas.UCDFonas;

        CDNustatymas();
    }

    public virtual void Update()
    {
        DabBCD -= Time.deltaTime;
        DabBBCD -= Time.deltaTime;
        DabBBBCD -= Time.deltaTime;
        DabUCD -= Time.deltaTime;

        DabBCD = Mathf.Max(DabBCD, 0);
        DabBBCD = Mathf.Max(DabBBCD, 0);
        DabBBBCD = Mathf.Max(DabBBBCD, 0);
        DabUCD = Mathf.Max(DabUCD, 0);

        BCDTekstas.text = Mathf.Ceil(DabBCD).ToString();
        BBCDTekstas.text = Mathf.Ceil(DabBBCD).ToString();
        BBBCDTekstas.text = Mathf.Ceil(DabBBBCD).ToString();
        UCDTekstas.text = Mathf.Ceil(DabUCD).ToString();

        BCDFonas.fillAmount = DabBCD / BCD;
        BBCDFonas.fillAmount = DabBBCD / BBCD;
        BBBCDFonas.fillAmount = DabBBBCD / BBBCD;
        UCDFonas.fillAmount = DabUCD / UCD;
    }

    public virtual void FixedUpdate()
    {
        if (!photonView.IsMine || !ZaidejoKodas.GaliPulti)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (DabBCD == 0)
            {
                DabBCD = BCD;

                switch (Duomenys.B1)
                {
                    case 1: B1(); break;
                    case 2: B2(); break;
                    case 3: B3(); break;
                    case 4: B4(); break;
                    case 5: B5(); break;
                    case 6: B6(); break;
                    case 7: B7(); break;
                    case 8: B8(); break;
                    case 9: B9(); break;
                }
            }
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            if (DabBBCD == 0)
            {
                DabBBCD = BBCD;

                switch (Duomenys.B2)
                {
                    case 1: B1(); break;
                    case 2: B2(); break;
                    case 3: B3(); break;
                    case 4: B4(); break;
                    case 5: B5(); break;
                    case 6: B6(); break;
                    case 7: B7(); break;
                    case 8: B8(); break;
                    case 9: B9(); break;
                }
            }
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            if (DabBBBCD == 0)
            {
                DabBBBCD = BBBCD;

                switch (Duomenys.B3)
                {
                    case 1: B1(); break;
                    case 2: B2(); break;
                    case 3: B3(); break;
                    case 4: B4(); break;
                    case 5: B5(); break;
                    case 6: B6(); break;
                    case 7: B7(); break;
                    case 8: B8(); break;
                    case 9: B9(); break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (DabUCD == 0)
            {
                DabUCD = UCD;

                switch (Duomenys.U)
                {
                    case 1: U1(); break;
                    case 2: U2(); break;
                    case 3: U3(); break;
                    case 4: U4(); break;
                    case 5: U5(); break;
                    case 6: U6(); break;
                    case 7: U7(); break;
                    case 8: U8(); break;
                    case 9: U9(); break;
                }
            }
        }
    }


    public virtual void B1() { }
    public virtual void B2() { }
    public virtual void B3() { }
    public virtual void B4() { }
    public virtual void B5() { }
    public virtual void B6() { }
    public virtual void B7() { }
    public virtual void B8() { }
    public virtual void B9() { }
    public virtual void U1() { }
    public virtual void U2() { }
    public virtual void U3() { }
    public virtual void U4() { }
    public virtual void U5() { }
    public virtual void U6() { }
    public virtual void U7() { }
    public virtual void U8() { }
    public virtual void U9() { }

    public virtual void CDNustatymas() { }
}