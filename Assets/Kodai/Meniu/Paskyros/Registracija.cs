using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;

public class Registracija : MonoBehaviour
{
    public InputField Slapyvardis;
    public InputField ElPastas;
    public InputField Slaptazodis;
    public InputField PakartotasSlaptazodis;
    public string SiScena;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            KurtiPaskyra();
        }
    }

    public void KurtiPaskyra()
    {
        if (Slaptazodis.text == PakartotasSlaptazodis.text) /// Tikrinti, ar kažkas įvesta, nereikia, tuo pasirūpina PlayFab
        {
            RegisterPlayFabUserRequest Uzklausa = new RegisterPlayFabUserRequest();

            Uzklausa.Username = Slapyvardis.text;
            Uzklausa.DisplayName = Slapyvardis.text;
            Uzklausa.Email = ElPastas.text;
            Uzklausa.Password = Slaptazodis.text;

            PlayFabClientAPI.RegisterPlayFabUser(Uzklausa,
                result =>
                {
                    SceneManager.UnloadSceneAsync(SiScena);
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
