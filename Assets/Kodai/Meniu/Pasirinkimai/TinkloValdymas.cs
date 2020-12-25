using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon;
using Photon.Pun;
using Photon.Realtime;

public class TinkloValdymas : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI JungimosiTekstas;
    public GameObject PagrindinisMeniu;
    public GameObject ZaidimoLangas;
    private bool DarNeprisijunge = true;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        JungimosiTekstas.gameObject.SetActive(true);
        PagrindinisMeniu.SetActive(false);
        
        if (Duomenys.AutomatinisPrisijungimas)
        {
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "";
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "eu";
            //PhotonNetwork.PhotonServerSettings.AppSettings.UseNameServer = true;
            //PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime = "EuropePUNAppId";
            //PhotonNetwork.PhotonServerSettings.AppSettings.Server = "ns.photonengine.cn"; CN - Kinija
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }

    void Update()
    {
        //Debug.Log(PhotonNetwork.IsConnected);
    }

    public override void OnConnectedToMaster()
    {
        JungimosiTekstas.gameObject.SetActive(false);
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
        PhotonNetwork.CreateRoom(Duomenys.Slapyvardis, RO, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("Savarankiškas");
    }
}
