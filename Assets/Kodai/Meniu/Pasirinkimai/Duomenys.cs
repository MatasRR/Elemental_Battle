using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duomenys : MonoBehaviour
{
    public static string Slapyvardis;
    public static int Elementas = 0;
    public static int B1 = 0, B2 = 0, B3 = 0, U = 0; // Pasirinkti kerai
    public static int KomandosNr = 0;

    void Start ()
    {
        DontDestroyOnLoad(gameObject);
    }
}
