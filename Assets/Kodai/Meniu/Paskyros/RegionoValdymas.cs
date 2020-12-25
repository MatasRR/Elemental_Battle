using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegionoValdymas : MonoBehaviour
{
    public TextMeshProUGUI RegionoPasirinkimoTekstas;

    public void KeistiRegionoTipa()
    {
        Duomenys.AutomatinisPrisijungimas = !Duomenys.AutomatinisPrisijungimas;
        RegionoPasirinkimoTekstas.text = "Region: " + (Duomenys.AutomatinisPrisijungimas ? "Automatic" : "EU");
    }
}
