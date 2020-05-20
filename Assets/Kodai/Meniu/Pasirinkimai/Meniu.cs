﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class Meniu : MonoBehaviour
{
    [Header("Meniu")]
    public GameObject[] MeniuLangai;
    public Button ZaidimoMygtukas;

    [Header("Profilis")]
    public TextMeshProUGUI DabartinisSlapyvardis;
    public TextMeshProUGUI IvestasSlapyvardis;

    [Header ("Elementai")]
    public GameObject ElementuLentele;
    public GameObject KeruMygtukuLentele;
    public GameObject KeruMygtukuAntrojiLentele;
    public GameObject KeruLentele;
    public GameObject[] KeruLenteles;
    
    public Button[] PaprastujuKeruMygtukai;
    public Button[] GalingujuKeruMygtukai;

    public Color AktyvausMygtukoSpalva;
    public Color NeaktyvausMygtukoSpalva;
    public Color OroSpalva;
    public Color VandensSpalva;
    public Color ZemesSpalva;
    public Color UgniesSpalva;

    private int DabMygtukas = 0;

    //[Header("Įgūdžiai")]

    //[Header("Žaidimas")] 

    private void Start()
    {
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest()
        {
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowDisplayName = true
            }
        },
            result => Duomenys.Slapyvardis = result.PlayerProfile.DisplayName,
            error => Debug.LogError(error.GenerateErrorReport()));
    }

    private void Update()
    {
        if (Duomenys.Slapyvardis == null || Duomenys.B1 == 0 || Duomenys.B2 == 0 || Duomenys.B3 == 0 || Duomenys.U == 0)
        {
            ZaidimoMygtukas.interactable = false;
        }
        else
        {
            ZaidimoMygtukas.interactable = true;
        }

        DabartinisSlapyvardis.text = Duomenys.Slapyvardis;
    }

    public void Elementas (int Nr)
	{
        switch (Nr)
        {
            case 1:
                Duomenys.Elementas = 1;
                KeistiLenteliuSpalvas(OroSpalva);
                break;
            case 2:
                Duomenys.Elementas = 2;
                KeistiLenteliuSpalvas(VandensSpalva);
                break;
            case 3:
                Duomenys.Elementas = 3;
                KeistiLenteliuSpalvas(ZemesSpalva);
                break;
            case 4:
                Duomenys.Elementas = 4;
                KeistiLenteliuSpalvas(UgniesSpalva);
                break;
        }

        KeruMygtukuAntrojiLentele.SetActive(true);
        Duomenys.B1 = Duomenys.B2 = Duomenys.B3 = Duomenys.U = 0;

        if (DabMygtukas != 0)
        {
            RodytiKerus(Duomenys.Elementas);
        }
    }

    public void KeruMygtukas (int Nr)
    {
        DabMygtukas = Nr;

        RodytiKerus(Duomenys.Elementas);

        if (Nr < 4)
        {
            foreach (Button Mygtukas in PaprastujuKeruMygtukai)
            {
                Mygtukas.interactable = true;
            }
            foreach (Button Mygtukas in GalingujuKeruMygtukai)
            {
                Mygtukas.interactable = false;
            }
        }
        else if (Nr == 4)
        {
            foreach (Button Mygtukas in PaprastujuKeruMygtukai)
            {
                Mygtukas.interactable = false;
            }
            foreach (Button Mygtukas in GalingujuKeruMygtukai)
            {
                Mygtukas.interactable = true;
            }
        }
    }

    public void Kerai (int Nr)
    {
        switch (DabMygtukas)
        {
            case 1:
                Duomenys.B1 = Nr;
                break;
            case 2:
                Duomenys.B2 = Nr;
                break;
            case 3:
                Duomenys.B3 = Nr;
                break;
            case 4:
                Duomenys.U = Nr;
                break;
        }
    }

    public void RodytiPasirinktuKeruNr (Text Tekstas)
    {
        Tekstas.text = DabMygtukas.ToString();
    }

    void KeistiLenteliuSpalvas (Color Spalva)
    {
        ElementuLentele.GetComponent<Image>().color = Spalva;
        KeruMygtukuLentele.GetComponent<Image>().color = Spalva;
        KeruLentele.GetComponent<Image>().color = Spalva;

        foreach (GameObject Lentele in KeruLenteles)
        {
            Lentele.GetComponent<Image>().color = Spalva;
        }
    }

    void RodytiKerus (int Elementas)
    {
        foreach (GameObject Lentele in KeruLenteles)
        {
            Lentele.SetActive (false);
        }

        switch (Elementas)
        {
            case 1:
                KeruLenteles[0].SetActive(true);
                break;
            case 2:
                KeruLenteles[1].SetActive(true);
                break;
            case 3:
                KeruLenteles[2].SetActive(true);
                break;
            case 4:
                KeruLenteles[3].SetActive(true);
                break;
        }
    }

    public void PasirinktiKomanda(int Nr)
    {
        Duomenys.KomandosNr = Nr;
    }

    public void PasirinktiIsvaizdosMedziaga(int Nr)
    {
        Duomenys.IsvaizdosMedziagosNr = Nr;
    }

    public void PasirinktiPaveiksleliuTipa(int Nr)
    {
        Duomenys.GebejimuPaveiksleliuNr = Nr;
    }

    public void AtidarytiLanga(int Nr)
    {
        foreach (GameObject go in MeniuLangai)
        {
            go.SetActive(false);
        }

        MeniuLangai[Nr].SetActive(true);
    }

    public void KeistiSlapyvardi()
    {
        if (IvestasSlapyvardis.text != "")
        {
            var Uzklausa = new UpdateUserTitleDisplayNameRequest();
            Uzklausa.DisplayName = IvestasSlapyvardis.text;

            PlayFabClientAPI.UpdateUserTitleDisplayName(Uzklausa, OnPlayerNameResult, OnPlayFabError);
        }
    }

    void OnPlayerNameResult(UpdateUserTitleDisplayNameResult Rezultatas)
    {
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest()
        {
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowDisplayName = true
            }
        },
            result => DabartinisSlapyvardis.text = result.PlayerProfile.DisplayName,
            error => Debug.LogError(error.GenerateErrorReport()));        
    }

    void OnPlayFabError(PlayFabError Klaida)
    {
        Debug.LogError("Nepavyko atnaujinti slapyvardžio");
    }
}