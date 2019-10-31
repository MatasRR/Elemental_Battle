using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;

public class SavarankiskoValdymas : MonoBehaviourPun, IPunObservable
{
    public Transform[] AtsiradimoVietos;

    public GameObject ZaidejoDaiktas;
    public Camera BendraKamera;
    public Text AtsiradimoTekstas;

    public float AtsiradimoLaikas;
    private float LikesLaikas;
    private bool ZaidejasAtsirado = false;
    
    void Start()
    {
        LikesLaikas = AtsiradimoLaikas;
    }

    void Update()
    {
        AtsiradimoTekstas.text = "Spawning in " + (Mathf.Round(LikesLaikas)).ToString();
        LikesLaikas -= Time.deltaTime;
        if (LikesLaikas <= 0f)
        {
            if (!ZaidejasAtsirado)
            {
                int Nr = Random.Range(0, AtsiradimoVietos.Length);
                PhotonNetwork.Instantiate(ZaidejoDaiktas.name, AtsiradimoVietos[Nr].position, AtsiradimoVietos[Nr].rotation, 0);
                BendraKamera.gameObject.SetActive(false);
                AtsiradimoTekstas.gameObject.SetActive(false);
                ZaidejasAtsirado = true;
            }

            LikesLaikas = AtsiradimoLaikas;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

        }
        else if (stream.IsReading)
        {

        }
    }
}
