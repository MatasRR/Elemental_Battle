using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class Skydas : MonoBehaviourPun
{
    [HideInInspector]
    public float Gyvybes;
    [HideInInspector]
    public float MaxSugeriamaZala;
    [HideInInspector]
    public GameObject Autorius;
    [HideInInspector]
    public int KomandosNr;

    public bool IgnoruojaSavoKulkas;

    private void Start()
    {
        KomandosNr = Autorius.GetComponent<Zaidejas>().KomandosNr;
    }

    void Update()
    {
        if (Gyvybes < 0)
        {
            photonView.RPC("NaikintiSkyda", RpcTarget.All);
        }
    }

    [PunRPC]
    private void NaikintiSkyda()
    {
        Destroy(gameObject);
    }

    public void GautiZalos(float Zala)
    {
        if (photonView.IsMine)
        {
            photonView.RPC("RPCGautiZalos", RpcTarget.All, Zala);
        }
    }

    [PunRPC]
    private void RPCGautiZalos(float Zala)
    {
        Gyvybes -= Zala;
    }
}
