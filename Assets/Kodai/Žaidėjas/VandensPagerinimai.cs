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

    [Space(10)]
    public float B1SustingdymoLaikas;
    public float B1IkalinimoKaina;

    [Header("B2:")]
    public float B2AktyvinimoLaikas;
    public float B2GreitesnioGydymoKaina;

    [Space(10)]
    public float B2Gydymas;
    public float B2StipresnioGydymoKaina;

    // Draugų gydymas

    [Header("B3:")]
    public float B3;

    // Judantis debesis, daugiau lašelių

    [Header("B4:")]
    public float B4IeciuSkaicius;
    public float B4KeliuIeciuKaina;

    // Zala/greitis

    [Header("U1:")]
    public float U1GreitejimoKoeficientas;
    public float U1GreitejimoKaina;

    [Space(10)]
    public float U1PlatejimoKoeficientas;
    public float U1PlatejimoKaina;

    // Knockback?

    [Header("U2:")]
    public float U2Jega;
    public float U2DidesnesJegosKaina;

    [Header("U3:")]
    public float U3IlgejimoGreitis;
    public float U3DelsimoLaikas;
    public float U3IlgejimoGreicioDidejimoKaina;

    void Start()
    {
        VandensKodas = gameObject.GetComponent<Vanduo>();
    }

    void Update()
    {
        
    }

    // Reikės pridėti tikrinimą, ar exp nemažesnė už kainą
    // Ir atimti kainą iš exp

    public void B1Suletinimas(bool Neatrakintas)
    {
        if (VandensKodas.Patirtis >= B1SuletinimoKaina)
        {
            VandensKodas.B1SuletinimoLaikas = B1SuletinimoLaikas;
            VandensKodas.B1SuletinimoStipris = B1SuletinimoStipris;
            VandensKodas.Patirtis -= B1SuletinimoKaina;
        }
    }

    public void B1Ikalinimas(bool Neatrakintas)
    {
        if (VandensKodas.Patirtis >= B1IkalinimoKaina)
        {
            VandensKodas.B1SustingdymoLaikas = B1SustingdymoLaikas;
            VandensKodas.Patirtis -= B1IkalinimoKaina;
        }
    }

    public void B2GreitesnisGydymas()
    {
        if (VandensKodas.Patirtis >= B2GreitesnioGydymoKaina)
        {
            VandensKodas.B2GydymoLaikas = B2GreitesnioGydymoKaina;
            VandensKodas.Patirtis -= B2GreitesnioGydymoKaina;
        }
    }

    public void B2StipresnisGydymas()
    {
        if (VandensKodas.Patirtis >= B2StipresnioGydymoKaina)
        {
            VandensKodas.B2Gydymas = B2Gydymas;
            VandensKodas.Patirtis -= B2StipresnioGydymoKaina;
        }
    }

    public void B4KeliosIetys()
    {
        if (VandensKodas.Patirtis >= B4KeliuIeciuKaina)
        {
            VandensKodas.B4IeciuSkaicius = B4IeciuSkaicius;
            VandensKodas.Patirtis -= B4KeliuIeciuKaina;
        }
    }

    public void U1Greitejimas(bool Neatrakintas)
    {
        if (VandensKodas.Patirtis >= U1GreitejimoKaina)
        {
            VandensKodas.U1GreitejimoKoeficientas = U1GreitejimoKoeficientas;
            VandensKodas.Patirtis -= U1GreitejimoKaina;
        }        
    }

    public void U1Platejimas(bool Neatrakintas)
    {
        if (VandensKodas.Patirtis >= U1PlatejimoKaina)
        {
            VandensKodas.U1PlatejimoKoeficientas = U1PlatejimoKoeficientas;
            VandensKodas.Patirtis -= U1PlatejimoKaina;
        }
    }

    public void U2StipresnisTraukimas()
    {
        if (VandensKodas.Patirtis >= U2DidesnesJegosKaina)
        {
            VandensKodas.U2Jega = U2Jega;
            VandensKodas.Patirtis -= U2DidesnesJegosKaina;
        }        
    }

    public void U3Greitejimas()
    {
        if (VandensKodas.Patirtis >= U3IlgejimoGreicioDidejimoKaina)
        {
            VandensKodas.U3IlgejimoGreitis = U3IlgejimoGreitis;
            VandensKodas.U3Delsimas = U3DelsimoLaikas;
            VandensKodas.Patirtis -= U3IlgejimoGreicioDidejimoKaina;
        }        
    }
}
