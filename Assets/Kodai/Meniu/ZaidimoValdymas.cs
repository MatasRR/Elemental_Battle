using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;

public class ZaidimoValdymas : MonoBehaviourPun, IPunObservable
{
    public Transform[] AtsiradimoVietos;

    public GameObject ZaidejoDaiktas;
    public Camera BendraKamera;
    public Text AtsiradimoTekstas;

    public float AtsiradimoLaikas;
    private float LikesLaikas;
    private bool ZaidejasAtsirado = false;
    
    public List<Zaidejas> ZaidejuSarasas = new List<Zaidejas>();

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

        //////////////////////////
        /// PAKEISTI
        //////////////////////////
        AtnaujintiZaidejuSarasa();
        //////////////////////////
    }

    public void Mirtis(Zaidejas Zudikas, Zaidejas Mirusysis)
    {
        foreach (Zaidejas z in ZaidejuSarasas)
        {
            z.MirtiesPranesimas(Zudikas, Mirusysis);
        }

        if (Zudikas != Mirusysis)
        {
            Zudikas.NuzudeArbaMire(true);
        }

        Mirusysis.NuzudeArbaMire(false);
    }

    private void AtnaujintiZaidejuSarasa()
    {
        GameObject[] VisuZaidejuObjektai = GameObject.FindGameObjectsWithTag("Player");
        ZaidejuSarasas.Clear();
        foreach (GameObject go in VisuZaidejuObjektai)
        {
            ZaidejuSarasas.Add(go.GetComponent<Zaidejas>());
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
