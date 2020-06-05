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

    public virtual void Start()
    {
        KomandosNr = Autorius.GetComponent<Zaidejas>().KomandosNr;
    }

    public virtual void Update()
    {
        if (Gyvybes < 0)
        {
            photonView.RPC("RPCNaikintiSkyda", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPCNaikintiSkyda()
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

    public IEnumerator GautiDOTZalos(float Zala, float Daznis, float Trukme)
    {
        if (photonView.IsMine)
        {
            for (int i = 0; i < Trukme / Daznis; i++)
            {
                photonView.RPC("RPCGautiZalos", RpcTarget.All, Zala);
            }
            yield return new WaitForSeconds(Daznis);
        }
    }

    [PunRPC]
    private void RPCGautiZalos(float Zala)
    {
        Gyvybes -= Zala;
    }
}
