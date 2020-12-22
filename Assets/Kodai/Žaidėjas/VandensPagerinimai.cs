using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VandensPagerinimai : MonoBehaviour
{
    private Vanduo VandensKodas;

    [Header("B1:")]
    public float B1PradinisSuletinimoLaikas;
    public float B1PradinisSuletinimoStipris;
    public float B1SuletinimoStiprioPokytis;
    public float B1MaksimalusSuletinimoStipris;
    public float B1SuletinimoKaina;
    public TextMeshProUGUI B1SuletinimoTekstas;
    public GameObject B1SuletinimoLangas;
    public Button B1SuletinimoMygtukas;
    public Button B1SuletinimoAtrakinimoMygtukas;

    [Space(10)]
    public float B1PradinisSustingdymoLaikas;
    public float B1IkalinimoKaina;
    public float B1MaksimalusIkalinimoLaikas;
    public TextMeshProUGUI B1IkalinimoTekstas;
    public GameObject B1IkalinimoLangas;
    public Button B1IkalinimoMygtukas;
    public Button B1IkalinimoAtrakinimoMygtukas;

    [Header("B2:")]
    public float B2AktyvinimoLaikoPokytis;
    public float B2GreitesnioGydymoKaina;
    public float B2MinimalusGydymoLaikas;
    public TextMeshProUGUI B2GreitesnioGydymoTekstas;
    public Button B2GreitesnioGydymoMygtukas;

    [Space(10)]
    public float B2GydymoPokytis;
    public float B2StipresnioGydymoKaina;
    public TextMeshProUGUI B2StipresnioGydymoTekstas;
    public Button B2StipresnioGydymoMygtukas;

    // Draugų gydymas

    [Header("B3:")]
    public float B3ZalosPokytis;
    public float B3ZalosKaina;
    public TextMeshProUGUI B3ZalosTekstas;
    public Button B3ZalosMygtukas;

    // Judantis debesis, daugiau lašelių

    [Header("B4:")]
    public float B4IeciuSkaicius;
    public float B4KeliuIeciuKaina;
    public TextMeshProUGUI B4KeliuIeciuTekstas;
    public Button B4KeliuIeciuMygtukas;

    [Header("U1:")]
    public float U1GreitejimoKoeficientas;
    public float U1GreitejimoKoeficientoPokytis;
    public float U1GreitejimoKaina;
    public TextMeshProUGUI U1GreitejimoTekstas;
    public GameObject U1GreitejimoLangas;
    public Button U1GreitejimoMygtukas;
    public Button U1GreitejimoAtrakinimoMygtukas;

    [Space(10)]
    public float U1PlatejimoKoeficientas;
    public float U1PlatejimoKoeficientoPokytis;
    public float U1PlatejimoKaina;
    public TextMeshProUGUI U1PlatejimoTekstas;
    public GameObject U1PlatejimoLangas;
    public Button U1PlatejimoMygtukas;
    public Button U1PlatejimoAtrakinimoMygtukas;

    // Knockback?

    [Header("U2:")]
    public float U2JegosPokytis;
    public float U2MaksimaliJega;
    public float U2JegosKaina;
    public TextMeshProUGUI U2JegosTekstas;
    public Button U2JegosMygtukas;

    [Header("U3:")]
    public float U3AktyvavimoLaikoPokytis;
    public float U3MinimalusAktyvavimoLaikas;
    public float U3GreitesnioAktyvavimoKaina;
    public TextMeshProUGUI U3AktyvavimoLaikoTekstas;
    public Button U3AktyvavimoLaikoMygtukas;
    //public float U3IlgejimoGreitis;

    [Space(10)]
    public float U3LeidimoJudetiKaina;
    public TextMeshProUGUI U3LeidimoJudetiTekstas;
    public Button U3LeidimoJudetiMygtukas;

    void Start()
    {
        VandensKodas = gameObject.GetComponent<Vanduo>();
        PradziosAtrakinimoKaina(B1SuletinimoAtrakinimoMygtukas, B1SuletinimoKaina);
        PradziosAtrakinimoKaina(B1IkalinimoAtrakinimoMygtukas, B1IkalinimoKaina);
        PradziosAtrakinimoKaina(U1GreitejimoAtrakinimoMygtukas, U1GreitejimoKaina);
        PradziosAtrakinimoKaina(U1PlatejimoAtrakinimoMygtukas, U1PlatejimoKaina);
    }

    void Update()
    {
        if (Duomenys.B1 == 1 || Duomenys.B2 == 1 || Duomenys.B3 == 1)
        {
            PagerinimoEilutesUIValdymas(VandensKodas.B1SuletinimoStipris*100, B1SuletinimoStiprioPokytis*100, false, true, B1MaksimalusSuletinimoStipris,
                B1SuletinimoKaina, "%", B1SuletinimoTekstas, B1SuletinimoMygtukas, B1SuletinimoAtrakinimoMygtukas);

            PagerinimoEilutesUIValdymas(VandensKodas.B1SustingdymoLaikas, 0, false, true, B1MaksimalusIkalinimoLaikas, B1IkalinimoKaina, "s", B1IkalinimoTekstas,
                B1IkalinimoMygtukas, B1IkalinimoAtrakinimoMygtukas);
        }
        if (Duomenys.B1 == 2 || Duomenys.B2 == 2 || Duomenys.B3 == 2)
        {
            PagerinimoEilutesUIValdymas(VandensKodas.B2GydymoLaikas, -B2AktyvinimoLaikoPokytis, false, true, 0.5f, B2GreitesnioGydymoKaina, "s",
                B2GreitesnioGydymoTekstas, B2GreitesnioGydymoMygtukas, null);

            PagerinimoEilutesUIValdymas(VandensKodas.B2Gydymas, B2GydymoPokytis, false, false, 0, B2StipresnioGydymoKaina, "", B2StipresnioGydymoTekstas,
                B2StipresnioGydymoMygtukas, null);
        }
        if (Duomenys.B1 == 3 || Duomenys.B2 == 3 || Duomenys.B3 == 3)
        {
            PagerinimoEilutesUIValdymas(VandensKodas.B3Zala, B3ZalosPokytis, false, false, 0, B3ZalosKaina, "", B3ZalosTekstas, B3ZalosMygtukas, null);
        }
        if (Duomenys.B1 == 4 || Duomenys.B2 == 4 || Duomenys.B3 == 4)
        {
            PagerinimoEilutesUIValdymas(VandensKodas.B4IeciuSkaicius, 1, false, false, 0, B4KeliuIeciuKaina, "", B4KeliuIeciuTekstas, B4KeliuIeciuMygtukas, null);
        }
        if (Duomenys.U == 1)
        {
            PagerinimoEilutesUIValdymas(VandensKodas.U1GreitejimoKoeficientas * 100, U1GreitejimoKoeficientoPokytis * 100, false, false, 0, U1GreitejimoKaina,
                "%/s", U1GreitejimoTekstas, U1GreitejimoMygtukas, U1GreitejimoAtrakinimoMygtukas);

            PagerinimoEilutesUIValdymas(VandensKodas.U1PlatejimoKoeficientas * 100, U1PlatejimoKoeficientoPokytis * 100, false, false, 0, U1PlatejimoKaina,
                "%/s", U1PlatejimoTekstas, U1PlatejimoMygtukas, U1PlatejimoAtrakinimoMygtukas);
        }
        if (Duomenys.U == 2)
        {
            PagerinimoEilutesUIValdymas(VandensKodas.U2Jega, U2JegosPokytis, false, true, U2MaksimaliJega, U2JegosKaina, "", U2JegosTekstas,
                U2JegosMygtukas, null);
        }
        if (Duomenys.U == 3)
        {
            PagerinimoEilutesUIValdymas(VandensKodas.U3GalimaJudetiAktyvinant ? 1 : 0, 0, true, true, 1, U3LeidimoJudetiKaina, "", U3LeidimoJudetiTekstas,
                U3LeidimoJudetiMygtukas, null);

            PagerinimoEilutesUIValdymas(VandensKodas.U3Delsimas, -U3AktyvavimoLaikoPokytis, false, true, U3MinimalusAktyvavimoLaikas,
                U3GreitesnioAktyvavimoKaina, "s", U3AktyvavimoLaikoTekstas, U3AktyvavimoLaikoMygtukas, null);
        }
    }

    void PagerinimoEilutesUIValdymas(float Verte, float Pokytis, bool ArBool, bool YraRiba, float Riba, float Kaina, string Matas, TextMeshProUGUI PokycioTekstas, Button Mygtukas, Button AtrakinimoMygtukas)
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
            if (Mygtukas.interactable)
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
                    Mygtukas.interactable = VandensKodas.Patirtis >= Kaina;
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
            AtrakinimoMygtukas.interactable = VandensKodas.Patirtis >= Kaina;
        }
    }

    void PradziosAtrakinimoKaina(Button Mygtukas, float Kaina)
    {
        Mygtukas.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += " (" + Kaina + " XP)";
    }

    public void B1Suletinimas(bool Neatrakintas)
    {
        if (Neatrakintas)
        {
            VandensKodas.B1SuletinimoLaikas = B1PradinisSuletinimoLaikas;
            VandensKodas.B1SuletinimoStipris = B1PradinisSuletinimoStipris;
            B1SuletinimoAtrakinimoMygtukas.gameObject.SetActive(false);
            B1SuletinimoLangas.SetActive(true);
        }
        else
        {
            VandensKodas.B1SuletinimoStipris += B1SuletinimoStiprioPokytis;
        }
        VandensKodas.Patirtis -= B1SuletinimoKaina;
    }

    public void B1Ikalinimas(bool Neatrakintas)
    {
        if (Neatrakintas)
        {
            VandensKodas.B1SustingdymoLaikas = B1PradinisSustingdymoLaikas;
            B1IkalinimoAtrakinimoMygtukas.gameObject.SetActive(false);
            B1IkalinimoLangas.SetActive(true);
        }
        else
        {

        }
        VandensKodas.Patirtis -= B1IkalinimoKaina;
    }

    public void B2GreitesnisGydymas()
    {
        VandensKodas.B2GydymoLaikas -= B2AktyvinimoLaikoPokytis;
        VandensKodas.Patirtis -= B2GreitesnioGydymoKaina;
    }

    public void B2StipresnisGydymas()
    {
        VandensKodas.B2Gydymas += B2GydymoPokytis;
        VandensKodas.Patirtis -= B2StipresnioGydymoKaina;
    }

    public void B3Zala()
    {
        VandensKodas.B3Zala += B3ZalosPokytis;
        VandensKodas.Patirtis -= B3ZalosKaina;
    }

    public void B4KeliosIetys()
    {
        VandensKodas.B4IeciuSkaicius++;
        VandensKodas.Patirtis -= B4KeliuIeciuKaina;
    }

    public void U1Greitejimas(bool Neatrakintas)
    {
        if (Neatrakintas)
        {
            VandensKodas.U1GreitejimoKoeficientas = U1GreitejimoKoeficientas;
            U1GreitejimoAtrakinimoMygtukas.gameObject.SetActive(false);
            U1GreitejimoLangas.SetActive(true);
        }
        else
        {
            VandensKodas.U1GreitejimoKoeficientas += U1GreitejimoKoeficientoPokytis;
        }
        VandensKodas.Patirtis -= U1GreitejimoKaina;
    }

    public void U1Platejimas(bool Neatrakintas)
    {
        if (Neatrakintas)
        {
            VandensKodas.U1PlatejimoKoeficientas = U1PlatejimoKoeficientas;
            U1PlatejimoAtrakinimoMygtukas.gameObject.SetActive(false);
            U1PlatejimoLangas.SetActive(true);
        }
        else
        {
            VandensKodas.U1PlatejimoKoeficientas += U1PlatejimoKoeficientoPokytis;
        }
        VandensKodas.Patirtis -= U1PlatejimoKaina;
    }

    public void U2StipresnisTraukimas()
    {
        VandensKodas.U2Jega += U2JegosPokytis;
        VandensKodas.Patirtis -= U2JegosKaina;
    }

    public void U3LeidimasJudeti(bool Neatrakintas)
    {
        VandensKodas.U3GalimaJudetiAktyvinant = true;
        VandensKodas.Patirtis -= U3LeidimoJudetiKaina;
    }

    public void U3GreitesnisAktyvavimas()
    {
        VandensKodas.U3Delsimas -= U3AktyvavimoLaikoPokytis;
        VandensKodas.Patirtis -= U3GreitesnioAktyvavimoKaina;
    }
}
