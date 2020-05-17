using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class GreitasPrisijungimas : MonoBehaviour
{
    public void GreitaiPrisijungti(int Nr)
    {
        LoginWithPlayFabRequest Uzklausa = new LoginWithPlayFabRequest();

        string PrisijungimoDuomenys = "player" + Nr.ToString();

        Uzklausa.Username = Uzklausa.Password = PrisijungimoDuomenys;

        PlayFabClientAPI.LoginWithPlayFab(Uzklausa,
            result =>
            {
                SceneManager.LoadScene("Meniu");
            },
            error =>
            {
                Pranesimai P = new Pranesimai();
                StartCoroutine(P.KurtiNaujaKlaida(error.ErrorMessage));
            }
        );
    }
}
