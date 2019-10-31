using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeruNumeriai : MonoBehaviour
{
    public int Nr = 0;
    public bool ArU = false;
    [HideInInspector]
    public Text Tekstas;

    private void Start()
    {
        Tekstas = gameObject.GetComponent<Text>();
    }

    void Update ()
    {
        if (!ArU && Nr == Duomenys.B1)
        {
            Tekstas.text = "1";
        }
        else if (!ArU && Nr == Duomenys.B2)
        {
            Tekstas.text = "2";
        }
        else if (!ArU && Nr == Duomenys.B3)
        {
            Tekstas.text = "3";
        }
        else if (ArU && Nr == Duomenys.U)
        {
            Tekstas.text = "4";
        }
        else
        {
            Tekstas.text = null;
        }
    }
}
