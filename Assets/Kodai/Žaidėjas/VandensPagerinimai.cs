using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VandensPagerinimai : MonoBehaviour
{
    private Vanduo VandensKodas;

    [Header("B1:")]
    public float B1SuletinimoLaikas;
    public float B1SuletinimoStipris;
    public float B1SuletinimoKaina;
    private bool B1SuletinimasPagerinta;

    [Space(10)]
    public float B1SustingdymoLaikas;
    public float B1IkalinimoKaina;
    private bool B1IkalinimasPagerinta;

    [Header("B2:")]
    public float B2AktyvinimoLaikas;
    public float B2GreitesnioGydymoKaina;
    private bool B2GreitesnisGydymasPagerinta;

    [Space(10)]
    public float B2Gydymas;
    public float B2StipresnioGydymoKaina;
    private bool B2StipresnisGydymasPagerinta;

    // Draugų gydymas

    [Header("B3:")]
    public float B3;

    // Judantis debesis, daugiau lašelių

    [Header("B4:")]
    public float B4IeciuSkaicius;
    public float B4KeliuIeciuKaina;
    private bool B4KeliosIetysPagerinta;

    // Zala/greitis

    [Header("U1:")]
    public float U1GreitejimoKoeficientas;
    public float U1GreitejimoKaina;
    private bool U1GreitejimasPagerinta;

    [Space(10)]
    public float U1PlatejimoKoeficientas;
    public float U1PlatejimoKaina;
    private bool U1PlatejimasPagerinta;

    // Knockback?

    [Header("U2:")]
    public float U2Jega;
    public float U2DidesnesJegosKaina;
    private bool U2JegaPagerinta;

    [Header("U3:")]
    public float U3IlgejimoGreitis;
    public float U3DelsimoLaikas;
    public float U3IlgejimoGreicioDidejimoKaina;
    private bool U3GreitejimasPagerinta;

    void Start()
    {
        VandensKodas = gameObject.GetComponent<Vanduo>();
    }

    void Update()
    {
        
    }

    // Reikės pridėti tikrinimą, ar exp nemažesnė už kainą
    // Ir atimti kainą iš exp

    public void B1Suletinimas()
    {
        if (VandensKodas.Patirtis >= B1SuletinimoKaina && !B1SuletinimasPagerinta)
        {
            VandensKodas.B1SuletinimoLaikas = B1SuletinimoLaikas;
            VandensKodas.B1SuletinimoStipris = B1SuletinimoStipris;
            VandensKodas.Patirtis -= B1SuletinimoKaina;
            B1SuletinimasPagerinta = true;
        }
    }

    public void B1Ikalinimas()
    {
        if (VandensKodas.Patirtis >= B1IkalinimoKaina && !B1IkalinimasPagerinta)
        {
            VandensKodas.B1SustingdymoLaikas = B1SustingdymoLaikas;
            VandensKodas.Patirtis -= B1IkalinimoKaina;
            B1IkalinimasPagerinta = true;
        }
    }

    public void B2GreitesnisGydymas()
    {
        if (VandensKodas.Patirtis >= B2GreitesnioGydymoKaina && !B2GreitesnisGydymasPagerinta)
        {
            VandensKodas.B2GydymoLaikas = B2GreitesnioGydymoKaina;
            VandensKodas.Patirtis -= B2GreitesnioGydymoKaina;
            B2GreitesnisGydymasPagerinta = true;
        }
    }

    public void B2StipresnisGydymas()
    {
        if (VandensKodas.Patirtis >= B2StipresnioGydymoKaina && !B2StipresnisGydymasPagerinta)
        {
            VandensKodas.B2Gydymas = B2Gydymas;
            VandensKodas.Patirtis -= B2StipresnioGydymoKaina;
            B2StipresnisGydymasPagerinta = true;
        }
    }

    public void B4KeliosIetys()
    {
        if (VandensKodas.Patirtis >= B4KeliuIeciuKaina && !B4KeliosIetysPagerinta)
        {
            VandensKodas.B4DaiktuSkaicius = B4IeciuSkaicius;
            VandensKodas.Patirtis -= B4KeliuIeciuKaina;
            B4KeliosIetysPagerinta = true;
        }
    }

    public void U1Greitejimas()
    {
        if (VandensKodas.Patirtis >= U1GreitejimoKaina && !U1GreitejimasPagerinta)
        {
            VandensKodas.U1GreitejimoKoeficientas = U1GreitejimoKoeficientas;
            VandensKodas.Patirtis -= U1GreitejimoKaina;
            U1GreitejimasPagerinta = true;
        }        
    }

    public void U1Platejimas()
    {
        if (VandensKodas.Patirtis >= U1PlatejimoKaina && !U1PlatejimasPagerinta)
        {
            VandensKodas.U1PlatejimoKoeficientas = U1PlatejimoKoeficientas;
            VandensKodas.Patirtis -= U1PlatejimoKaina;
            U1PlatejimasPagerinta = true;
        }        
    }

    public void U2StipresnisTraukimas()
    {
        if (VandensKodas.Patirtis >= U2DidesnesJegosKaina && !U2JegaPagerinta)
        {
            VandensKodas.U2Jega = U2Jega;
            VandensKodas.Patirtis -= U2DidesnesJegosKaina;
            U2JegaPagerinta = true;
        }        
    }

    public void U3Greitejimas()
    {
        if (VandensKodas.Patirtis >= U3IlgejimoGreicioDidejimoKaina && !U3GreitejimasPagerinta)
        {
            VandensKodas.U3IlgejimoGreitis = U3IlgejimoGreitis;
            VandensKodas.U3Delsimas = U3DelsimoLaikas;
            VandensKodas.Patirtis -= U3IlgejimoGreicioDidejimoKaina;
            U3GreitejimasPagerinta = true;
        }        
    }
}
