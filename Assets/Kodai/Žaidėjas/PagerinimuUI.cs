using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PagerinimuUI : MonoBehaviour
{
    public Button[] Mygtukai;
    public GameObject[] ElementuLangai;
    public GameObject[] PaprastiOroLangai;
    public GameObject[] GalingiOroLangai;
    public GameObject[] PaprastiVandensLangai;
    public GameObject[] GalingiVandensLangai;
    public GameObject[] PaprastiZemesLangai;
    public GameObject[] GalingiZemesLangai;
    public GameObject[] PaprastiUgniesLangai;
    public GameObject[] GalingiUgniesLangai;

    private GameObject[] PaprastiLangai;
    private GameObject[] GalingiLangai;
    private GameObject DabartinisLangas;

    void Start()
    {
        switch(Duomenys.Elementas)
        {
            case 1:
                PaprastiLangai = PaprastiOroLangai;
                GalingiLangai = GalingiOroLangai;
                ElementuLangai[0].SetActive(true);
                break;
            case 2:
                PaprastiLangai = PaprastiVandensLangai;
                GalingiLangai = GalingiVandensLangai;
                ElementuLangai[1].SetActive(true);
                break;
            case 3:
                PaprastiLangai = PaprastiZemesLangai;
                GalingiLangai = GalingiZemesLangai;
                ElementuLangai[2].SetActive(true);
                break;
            case 4:
                PaprastiLangai = PaprastiUgniesLangai;
                GalingiLangai = GalingiUgniesLangai;
                ElementuLangai[3].SetActive(true);
                break;
        }

        DabartinisLangas = PaprastiLangai[0];
        AtidarytiLanga(1);
    }

    void Update()
    {

    }

    public void AtidarytiLanga(int Nr)
    {
        DabartinisLangas.SetActive(false);
        switch (Nr)
        {
            case 1:
                DabartinisLangas = PaprastiLangai[Duomenys.B1 - 1];
                break;
            case 2:
                DabartinisLangas = PaprastiLangai[Duomenys.B2 - 1];
                break;
            case 3:
                DabartinisLangas = PaprastiLangai[Duomenys.B3 - 1];
                break;
            case 4:
                DabartinisLangas = GalingiLangai[Duomenys.U - 1];
                break;
        }
        DabartinisLangas.SetActive(true);

        foreach (Button Mygtukas in Mygtukai)
        {
            Color Spalva = Mygtukas.image.color;
            Spalva.a = 0.6f;
            Mygtukas.image.color = Spalva;
        }
        Color RyskiSpalva = Mygtukai[Nr - 1].image.color;
        RyskiSpalva.a = 1f;
        Mygtukai[Nr - 1].image.color = RyskiSpalva;
    }
}
