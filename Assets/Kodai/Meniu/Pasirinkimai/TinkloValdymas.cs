using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class TinkloValdymas : MonoBehaviourPunCallbacks
{
    public GameObject JungimosiTekstas;
    public GameObject PagrindinisMeniu;
    private bool DarNeprisijunge = true;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        JungimosiTekstas.SetActive(true);
        PagrindinisMeniu.SetActive(false);
        
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        JungimosiTekstas.SetActive(false);
        PagrindinisMeniu.SetActive(true);
    }

    public void PrisijungtiPrieSavarankiskoRezimoKambario()
    {
        if (DarNeprisijunge)
        {
            DarNeprisijunge = false;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        KurtiSavarankiskoRezimoKambari();
    }

    public void KurtiSavarankiskoRezimoKambari()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        RoomOptions RO = new RoomOptions { MaxPlayers = 20, IsOpen = true, IsVisible = true };
        PhotonNetwork.CreateRoom("S1", RO, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("Savarankiškas");
    }
}
