using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class GreitasPrisijungimas : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && Input.GetKey(KeyCode.LeftAlt))
        {
            LoginWithPlayFabRequest Uzklausa = new LoginWithPlayFabRequest();

            Uzklausa.Username = Uzklausa.Password = "player1";

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
}
