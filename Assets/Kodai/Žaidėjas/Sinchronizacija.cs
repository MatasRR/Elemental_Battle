using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class Sinchronizacija : MonoBehaviourPun, IPunObservable
{
    public float SinchronizacijosDaznis;
    [HideInInspector]
    public Vector3 Vieta;
    [HideInInspector]
    public Quaternion Posukis;
    [HideInInspector]
    public Vector3 Dydis;
    
    
    void Update()
    {
        if (!photonView.IsMine)
        {
            Sinchronizuoti();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(transform.localScale);
        }
        else if (stream.IsReading)
        {
            Vieta = (Vector3)stream.ReceiveNext();
            Posukis = (Quaternion)stream.ReceiveNext();
            Dydis = (Vector3)stream.ReceiveNext();
        }
    }

    public void Sinchronizuoti()
    {
        transform.position = Vector3.Lerp(transform.position, Vieta, SinchronizacijosDaznis);
        transform.rotation = Quaternion.Lerp(transform.rotation, Posukis, SinchronizacijosDaznis);
        transform.localScale = Vector3.Lerp(transform.localScale, Dydis, SinchronizacijosDaznis);
    }
}
