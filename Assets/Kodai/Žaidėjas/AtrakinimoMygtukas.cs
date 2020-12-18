using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtrakinimoMygtukas : MonoBehaviour
{
    public int Nr;
    public bool ArGalingas;
    public GameObject AtrakintinasObjektas;

    void Start()
    {
        if (ArGalingas && Duomenys.U != Nr)
        {
            gameObject.SetActive(false);
        }
        if (!ArGalingas && Duomenys.B1 != Nr && Duomenys.B2 != Nr && Duomenys.B3 != Nr)
        {
            gameObject.SetActive(false);
        }
    }

    public void Atrakinti()
    {
        AtrakintinasObjektas.SetActive(true);
        gameObject.SetActive(false);
    }
}
