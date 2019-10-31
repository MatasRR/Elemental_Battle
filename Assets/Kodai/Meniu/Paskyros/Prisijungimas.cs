using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class Prisijungimas : MonoBehaviour
{
    public InputField Slapyvardis;
    public InputField Slaptazodis;
    public string KitaScena;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Prisijungti();
        }
    }

    public void Prisijungti()
    {
        LoginWithPlayFabRequest Uzklausa = new LoginWithPlayFabRequest();

        Uzklausa.Username = Slapyvardis.text;
        Uzklausa.Password = Slaptazodis.text;

        PlayFabClientAPI.LoginWithPlayFab(Uzklausa,
            result =>
            {
                SceneManager.LoadScene(KitaScena);
            },
            error =>
            {
                Pranesimai P = new Pranesimai();
                StartCoroutine(P.KurtiNaujaKlaida(error.ErrorMessage));
            }
        );
    }
}
