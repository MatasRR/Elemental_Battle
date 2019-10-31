using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenuValdymas : MonoBehaviour
{
    public void PaleistiScena(string Scena)
    {
        SceneManager.LoadScene(Scena);
    }

    public void PaleistiScenaAsync(string Scena)
    {
        SceneManager.LoadSceneAsync(Scena, LoadSceneMode.Additive);
    }

    public void UzdarytiScenaAsync(string Scena)
    {
        SceneManager.UnloadSceneAsync(Scena);
    }
}
