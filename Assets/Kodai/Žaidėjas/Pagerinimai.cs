using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pagerinimai : MonoBehaviour
{
    [Header("Pagerinimai")]
    public float BaziniuPagerinimuKaina;
    public float ElementoNuolaida;
    public float GreicioPokytis;
    public float SoklumoPokytis;
    public float GyvybiuPokytis;
    public float RegeneracijosPokytis;
    public float GynybosPokytis;
    public float TvirtumoPokytis;
    public float AtakosPokytis;
    public float GebejimuAtsikrovimoPokytis;
    public TextMeshProUGUI[] PokyciuTekstas;
    public Button[] PagerinimuMygtukai;

    [Header("UI:")]
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
    public GameObject BaziniuGebejimuLangas;

    private GameObject[] PaprastiLangai;
    private GameObject[] GalingiLangai;
    private GameObject[] NaudojamiLangai = new GameObject[5];
    private GameObject DabartinisLangas;

    private Zaidejas ZaidejoKodas;

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

        ZaidejoKodas = gameObject.GetComponent<Zaidejas>();

        NaudojamiLangai[0] = BaziniuGebejimuLangas;
        NaudojamiLangai[1] = PaprastiLangai[Duomenys.B1 - 1];
        NaudojamiLangai[2] = PaprastiLangai[Duomenys.B2 - 1];
        NaudojamiLangai[3] = PaprastiLangai[Duomenys.B3 - 1];
        NaudojamiLangai[4] = GalingiLangai[Duomenys.U - 1];

        DabartinisLangas = BaziniuGebejimuLangas;
        AtidarytiLanga(0);
    }

    void Update()
    {
        PagerinimoEilutesUIValdymas(ZaidejoKodas.Greitis, GreicioPokytis, false, false, 0,
            BaziniuPagerinimuKaina * (Duomenys.Elementas == 1 ? ElementoNuolaida : 1), "", PokyciuTekstas[0], PagerinimuMygtukai[0], null);

        PagerinimoEilutesUIValdymas(ZaidejoKodas.Soklumas, SoklumoPokytis, false, false, 0,
            BaziniuPagerinimuKaina * (Duomenys.Elementas == 1 ? ElementoNuolaida : 1), "", PokyciuTekstas[1], PagerinimuMygtukai[1], null);

        PagerinimoEilutesUIValdymas(ZaidejoKodas.MaxGyvybes, GyvybiuPokytis, false, false, 0,
            BaziniuPagerinimuKaina * (Duomenys.Elementas == 2 ? ElementoNuolaida : 1), "", PokyciuTekstas[2], PagerinimuMygtukai[2], null);

        PagerinimoEilutesUIValdymas(ZaidejoKodas.Regeneracija, RegeneracijosPokytis, false, false, 0,
            BaziniuPagerinimuKaina * (Duomenys.Elementas == 2 ? ElementoNuolaida : 1), "", PokyciuTekstas[3], PagerinimuMygtukai[3], null);

        PagerinimoEilutesUIValdymas(ZaidejoKodas.GynybosMod, GynybosPokytis, false, false, 0,
            BaziniuPagerinimuKaina * (Duomenys.Elementas == 3 ? ElementoNuolaida : 1), "", PokyciuTekstas[4], PagerinimuMygtukai[4], null);

        PagerinimoEilutesUIValdymas(ZaidejoKodas.TvirtumoMod, TvirtumoPokytis, false, false, 0,
            BaziniuPagerinimuKaina * (Duomenys.Elementas == 3 ? ElementoNuolaida : 1), "", PokyciuTekstas[5], PagerinimuMygtukai[5], null);

        PagerinimoEilutesUIValdymas(ZaidejoKodas.AtakosMod, AtakosPokytis, false, false, 0,
            BaziniuPagerinimuKaina * (Duomenys.Elementas == 4 ? ElementoNuolaida : 1), "", PokyciuTekstas[6], PagerinimuMygtukai[6], null);

        PagerinimoEilutesUIValdymas(ZaidejoKodas.GebejimuAtsikrovimoMod, GebejimuAtsikrovimoPokytis, false, false, 0,
            BaziniuPagerinimuKaina * (Duomenys.Elementas == 4 ? ElementoNuolaida : 1), "", PokyciuTekstas[7], PagerinimuMygtukai[7], null);
    }

    public void AtidarytiLanga(int Nr)
    {
        DabartinisLangas.SetActive(false);
        DabartinisLangas = NaudojamiLangai[Nr];
        DabartinisLangas.SetActive(true);

        foreach (Button Mygtukas in Mygtukai)
        {
            Color Spalva = Mygtukas.image.color;
            Spalva.a = 0.6f;
            Mygtukas.image.color = Spalva;
        }
        Color RyskiSpalva = Mygtukai[Nr].image.color;
        RyskiSpalva.a = 1f;
        Mygtukai[Nr].image.color = RyskiSpalva;
    }

    public void PagerinimoEilutesUIValdymas(float Verte, float Pokytis, bool ArBool, bool YraRiba, float Riba, float Kaina, string Matas, TextMeshProUGUI PokycioTekstas, Button Mygtukas, Button AtrakinimoMygtukas)
    {
        bool Atrakinta = true;

        if (AtrakinimoMygtukas != null)
        {
            if (AtrakinimoMygtukas.gameObject.activeSelf)
            {
                Atrakinta = false;
            }
        }

        if (Atrakinta)
        {
            if (true /*Mygtukas.interactable*/)
            {
                string VertePries;
                string VertePo;

                if (ArBool)
                {
                    if (Verte == 0)
                    {
                        VertePries = "False";
                        VertePo = "True";
                    }
                    else
                    {
                        VertePries = "True";
                        VertePo = "False";
                    }
                }
                else
                {
                    VertePries = Verte.ToString() + Matas;
                    VertePo = (Verte + Pokytis).ToString() + Matas;
                }

                if ((Pokytis > 0 && Verte < Riba) || (Pokytis < 0 && Verte > Riba) || (ArBool && (Verte != Riba)) || !YraRiba)
                {
                    PokycioTekstas.text = VertePries + " –> " + VertePo;
                    Mygtukas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Kaina.ToString() + " XP";
                    Mygtukas.interactable = ZaidejoKodas.Patirtis >= Kaina;
                }
                else
                {
                    PokycioTekstas.text = VertePries;
                    Mygtukas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAX";
                    Mygtukas.interactable = false;
                }
            }
        }
        else
        {
            AtrakinimoMygtukas.interactable = ZaidejoKodas.Patirtis >= Kaina;
        }
    }

    public void PradziosAtrakinimoKaina(Button Mygtukas, float Kaina)
    {
        Mygtukas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += " (" + Kaina + " XP)";
    }

    public void Greitis()
    {
        ZaidejoKodas.Greitis += GreicioPokytis;
        ZaidejoKodas.Patirtis -= BaziniuPagerinimuKaina * (Duomenys.Elementas == 1 ? ElementoNuolaida : 1);
    }

    public void Soklumas()
    {
        ZaidejoKodas.Soklumas += SoklumoPokytis;
        ZaidejoKodas.Patirtis -= BaziniuPagerinimuKaina * (Duomenys.Elementas == 1 ? ElementoNuolaida : 1);
    }

    public void Gyvybes()
    {
        ZaidejoKodas.MaxGyvybes += GyvybiuPokytis;
        ZaidejoKodas.Gyvybes += GyvybiuPokytis;
        ZaidejoKodas.Patirtis -= BaziniuPagerinimuKaina * (Duomenys.Elementas == 2 ? ElementoNuolaida : 1);
    }

    public void Regeneracija()
    {
        ZaidejoKodas.Regeneracija += RegeneracijosPokytis;
        ZaidejoKodas.Patirtis -= BaziniuPagerinimuKaina * (Duomenys.Elementas == 2 ? ElementoNuolaida : 1);
    }

    public void Tvirtumas()
    {
        ZaidejoKodas.TvirtumoMod += TvirtumoPokytis;
        ZaidejoKodas.Patirtis -= BaziniuPagerinimuKaina * (Duomenys.Elementas == 3 ? ElementoNuolaida : 1);
    }

    public void Gynyba()
    {
        ZaidejoKodas.GynybosMod += GynybosPokytis;
        ZaidejoKodas.Patirtis -= BaziniuPagerinimuKaina * (Duomenys.Elementas == 3 ? ElementoNuolaida : 1);
    }

    public void Ataka()
    {
        ZaidejoKodas.AtakosMod += AtakosPokytis;
        ZaidejoKodas.Patirtis -= BaziniuPagerinimuKaina * (Duomenys.Elementas == 4 ? ElementoNuolaida : 1);
    }

    public void GebejimuAtsikrovimas()
    {
        ZaidejoKodas.GebejimuAtsikrovimoMod += GebejimuAtsikrovimoPokytis;
        ZaidejoKodas.Patirtis -= BaziniuPagerinimuKaina * (Duomenys.Elementas == 4 ? ElementoNuolaida : 1);
    }
}
