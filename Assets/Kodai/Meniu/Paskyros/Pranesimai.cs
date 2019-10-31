using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pranesimai : MonoBehaviour
{
    public IEnumerator KurtiNaujaKlaida(string PranesimoTurinys)
    {
        yield return SceneManager.LoadSceneAsync("Pranešimas", LoadSceneMode.Additive);
        FindObjectOfType<Pranesimas>().PranesimoTekstas.text = PranesimoTurinys;
    }
}
