using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon;
using Photon.Pun;
using TMPro;

public class Zaidejas : MonoBehaviourPun, IPunObservable
{
    public Color[] VardoKomanduSpalvos;
    private List <Zaidejas> ZaidejuSarasas = new List<Zaidejas>();

    [HideInInspector]
    public string Vardas;
    [HideInInspector]
    public int ElementoNr;
    [HideInInspector]
    public int KomandosNr;

    [HideInInspector]
    public float Gyvybes;
    [HideInInspector]
    public float Skydas;
    [HideInInspector]
    public bool Gyvas;

    [HideInInspector]
    public float GyvybiuProcentas;
    [HideInInspector]
    public float SkydoProcentas;

    public int MaxGyvybes;

    public float PradinisGreitis;
    [HideInInspector]
    public float GreicioMod;
    [HideInInspector]
    public float Greitis;

    public float PradinisSoklumas;
    [HideInInspector]
    public float SoklumoMod;
    [HideInInspector]
    public float Soklumas;

    [HideInInspector]
    public float ZalosMod;

    public float PrisikelimoLaikas;
    [HideInInspector]
    public float LikesPrisikelimoLaikas;

    public Camera Kamera;
    private Camera BendraKamera;

    private SavarankiskoValdymas ZaidimoValdymoKodas; /// Negerai, jei atsiras daugiau zaidimo rezimu

    public Transform KulkosAtsiradimoVieta;

    public Oras OroKodas;
    public Vanduo VandensKodas;
    public Zeme ZemesKodas;
    public Ugnis UgniesKodas;

    [HideInInspector]
    public bool GaliSkraidyti;
    [HideInInspector]
    public bool GaliJudeti;
    [HideInInspector]
    public bool GaliPulti;

    [HideInInspector]
    public int SkraidymoLaikoIgnoravimas;
    [HideInInspector]
    public int JudejimoLaikoIgnoravimas;
    [HideInInspector]
    public int PuolimoLaikoIgnoravimas;

    [HideInInspector]
    public float SkraidymoCCLaikas;
    [HideInInspector]
    public float JudejimoCCLaikas;
    [HideInInspector]
    public float PuolimoCCLaikas;

    [HideInInspector]
    public int NuzudymuSk;
    [HideInInspector]
    public int MirciuSk;

    public float PaskutinioZalojusioZaidejoLaikas;
    [HideInInspector]
    public GameObject PaskutinisZalojesZaidejas;
    [HideInInspector]
    public float PaskutinioZalojusioZaidejoLaikmatis;

    public GameObject ZalosTekstoObjektas;
    public Color ZalosTekstoOroSpalva;
    public Color ZalosTekstoVandensSpalva;
    public Color ZalosTekstoZemesSpalva;
    public Color ZalosTekstoUgniesSpalva;

    //private bool DuomenysAtnaujinti;

    [Header("UI: ")]
    public TextMeshProUGUI BCDTekstas;
    public TextMeshProUGUI BBCDTekstas;
    public TextMeshProUGUI BBBCDTekstas;
    public TextMeshProUGUI UCDTekstas;
    public Image BCDFonas;
    public Image BBCDFonas;
    public Image BBBCDFonas;
    public Image UCDFonas;

    public Image GyvybiuJuostele;
    public Image SkydoJuostele;
    public TextMeshProUGUI GyvybiuTekstas;
    public TextMeshProUGUI SkydoTekstas;
    public Image MazojiGyvybiuJuostele;
    public Image MazojiSkydoJuostele;
    public TextMeshProUGUI MazasisGyvybiuTekstas;
    public TextMeshProUGUI MazasisSkydoTekstas;
    public TextMeshProUGUI MazasisVardoTekstas;

    public TextMeshProUGUI NuzudymuIrMirciuTekstas;
    public TextMeshProUGUI KomandosTekstas;
    public GameObject MirtiesPranesimuLentele;
    public GameObject MirtiesPranesimoObjektas;
    public GameObject ZaidejuIrKomanduInformacijosLentele;
    public Transform ZaidejuInformacijosLentele;
    public GameObject ZaidejoInformacijosObjektas;
    public GameObject[] KomanduInformacijosLaukeliai;

    public GameObject EkranoDrobe;
    public GameObject ZaidimoDrobe;

    void Start()
    {
        ZaidimoValdymoKodas = GameObject.FindGameObjectWithTag("GameController").GetComponent<SavarankiskoValdymas>();
        BendraKamera = ZaidimoValdymoKodas.BendraKamera;

        ZaidejoPasirinkimai();
        AtsiradimoDarbai();
    }
    
    void Update()
    {
        GyvybiuValdymas();
        LaikmaciuValdymas();
        ZaidejoDuomenuValdymas();
        UIValdymas();
        MygtukuValdymas();
    }

    void AtsiradimoDarbai()
    {
        Gyvybes = MaxGyvybes;
        Greitis = PradinisGreitis;
        Soklumas = PradinisSoklumas;
        GreicioMod = SoklumoMod = ZalosMod = 1f;
        SkraidymoCCLaikas = JudejimoCCLaikas = PuolimoCCLaikas = PaskutinioZalojusioZaidejoLaikmatis = 0f;
        SkraidymoLaikoIgnoravimas = JudejimoLaikoIgnoravimas = PuolimoLaikoIgnoravimas = NuzudymuSk = MirciuSk = 0;
        KomandosTekstas.text = (KomandosNr == 0) ? "SOLO" : "TEAM " + KomandosNr;
        NuzudymuSk = Duomenys.K;
        MirciuSk = Duomenys.D;
        //DuomenysAtnaujinti = false;
        Gyvas = true;

        if (!photonView.IsMine)
        {
            EkranoDrobe.SetActive(false);
            ZaidimoDrobe.SetActive(true);
            Kamera.gameObject.SetActive(false);
        }
        else
        {
            EkranoDrobe.SetActive(true);
            ZaidimoDrobe.SetActive(false);
        }
    }

    void GyvybiuValdymas()
    {
        if (photonView.IsMine)
        {
            Skydas -= Time.deltaTime;
            if (Skydas < 0)
            {
                Skydas = 0;
            }

            if (Gyvybes > MaxGyvybes)
            {
                Gyvybes = MaxGyvybes;
            }
            else if (Gyvas && (Gyvybes <= 0 || transform.position.y < -5))
            {
                Kamera.gameObject.SetActive(false);
                BendraKamera.gameObject.SetActive(true);
                Mirtis();
            }

            if (!Gyvas)
            {
                LikesPrisikelimoLaikas -= Time.deltaTime;
                if (LikesPrisikelimoLaikas <= 0)
                {
                    BendraKamera.gameObject.SetActive(false);
                    Kamera.gameObject.SetActive(true);
                    photonView.RPC("RPCPrisikelimas", RpcTarget.All);
                }
            }
        }
    }

    void LaikmaciuValdymas()
    {
        SkraidymoCCLaikas -= Time.deltaTime;
        JudejimoCCLaikas -= Time.deltaTime;
        PuolimoCCLaikas -= Time.deltaTime;
        PaskutinioZalojusioZaidejoLaikmatis -= Time.deltaTime;

        if (SkraidymoLaikoIgnoravimas > 0)
        {
            GaliSkraidyti = true;
        }
        else if (SkraidymoCCLaikas <= 0)
        {
            GaliSkraidyti = false;
            SkraidymoCCLaikas = 0;
        }
        else
        {
            GaliSkraidyti = true;
        }

        if (JudejimoLaikoIgnoravimas > 0)
        {
            GaliJudeti = false;
        }
        else if (JudejimoCCLaikas <= 0)
        {
            GaliJudeti = true;
            JudejimoCCLaikas = 0;
        }
        else
        {
            GaliJudeti = false;
        }

        if (PuolimoLaikoIgnoravimas > 0)
        {
            GaliPulti = false;
        }
        else if (PuolimoCCLaikas <= 0)
        {
            GaliPulti = true;
            PuolimoCCLaikas = 0;
        }
        else
        {
            GaliPulti = false;
        }

        if (PaskutinioZalojusioZaidejoLaikmatis < 0)
        {
            PaskutinisZalojesZaidejas = null;
            PaskutinioZalojusioZaidejoLaikmatis = 0;
        }
    }

    void ZaidejoDuomenuValdymas()
    {
        /*
        if (!DuomenysAtnaujinti)
        {
            return;
        }

        DuomenysAtnaujinti = true;
        */
        MazasisVardoTekstas.text = Vardas;
        MazasisVardoTekstas.color = VardoKomanduSpalvos[KomandosNr];
        /*
        switch (ElementoNr)
        {
            case 1:
                gameObject.GetComponent<MeshRenderer>().material.color = Color.grey;
                break;
            case 2:
                gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                break;
            case 3:
                gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
                break;
            case 4:
                gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                break;
        }
        */
    }

    void UIValdymas()
    {
        if (photonView.IsMine)
        {
            GyvybiuJuostele.fillAmount = Gyvybes / MaxGyvybes;
            SkydoJuostele.fillAmount = Mathf.Min(Skydas / MaxGyvybes, 1);
            GyvybiuTekstas.text = Mathf.Ceil(Gyvybes).ToString() + " / " + MaxGyvybes.ToString();
            SkydoTekstas.text = Mathf.Ceil(Skydas).ToString();

            NuzudymuIrMirciuTekstas.text = NuzudymuSk.ToString() + " / " + MirciuSk.ToString();
            //PrisikelimoLaikoTekstas.text = LikesPrisikelimoLaikas.ToString();
        }

        MazojiGyvybiuJuostele.fillAmount = Gyvybes / MaxGyvybes;
        MazojiSkydoJuostele.fillAmount = Mathf.Min(Skydas / MaxGyvybes, 1); ;
        MazasisGyvybiuTekstas.text = Mathf.Ceil(Gyvybes).ToString();
        MazasisSkydoTekstas.text = Mathf.Ceil(Skydas).ToString();
    }

    void MygtukuValdymas()
    {
        if(photonView.IsMine)
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                ZaidejuInformacijosLentelesValdymas(true);
            }
            else if (Input.GetKeyUp(KeyCode.Tab))
            {
                ZaidejuInformacijosLentelesValdymas(false);
            }

            if (Input.GetKey(KeyCode.LeftAlt))
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    PhotonNetwork.LeaveRoom();
                    SceneManager.LoadScene("Meniu");
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (Kamera.gameObject.activeSelf)
                    {
                        Kamera.gameObject.SetActive(false);
                        BendraKamera.gameObject.SetActive(true);
                    }
                    else
                    {
                        BendraKamera.gameObject.SetActive(false);
                        Kamera.gameObject.SetActive(true);
                    }
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    GautiZalos(10, 4);
                }
            }
        }
    }

    public void KeistiPaskutiniZalojusiZaideja (GameObject NaujasZaidejas)
    {
        PaskutinioZalojusioZaidejoLaikmatis = PaskutinioZalojusioZaidejoLaikas;
        PaskutinisZalojesZaidejas = NaujasZaidejas;
    }

    public void GautiZalos (float Zala, int ElementoNr)
    {
        if (photonView.IsMine)
        {
            float MofikuotaZala = Zala * ZalosMod;
            photonView.RPC("RPCGautiZalos", RpcTarget.All, MofikuotaZala, ElementoNr);
        }
    }

    [PunRPC]
    private void RPCGautiZalos(float Zala, int ElementoNr)
    {
        Gyvybes -= Mathf.Clamp(Zala - Skydas, 0, Mathf.Infinity);
        Skydas -= Zala;

        GameObject ZalosTekstas = Instantiate(ZalosTekstoObjektas, transform.position, transform.rotation);
        ZalosTekstas.GetComponent<TextMeshPro>().text = Zala.ToString();
        ZalosTekstas.GetComponent<ConstantForce>().force = new Vector3(Random.Range(-1, 1), Random.Range(4, 8), Random.Range(-1, 1));

        if (ElementoNr == 1)
        {
            ZalosTekstas.GetComponent<TextMeshPro>().color = ZalosTekstoOroSpalva;
        }
        else if (ElementoNr == 2)
        {
            ZalosTekstas.GetComponent<TextMeshPro>().color = ZalosTekstoVandensSpalva;
        }
        else if (ElementoNr == 3)
        {
            ZalosTekstas.GetComponent<TextMeshPro>().color = ZalosTekstoZemesSpalva;
        }
        else if (ElementoNr == 4)
        {
            ZalosTekstas.GetComponent<TextMeshPro>().color = ZalosTekstoUgniesSpalva;
        }

        Destroy(ZalosTekstas, 2);
    }

    public void NuzudeKitaZaideja()
    {
        NuzudymuSk++;
        Duomenys.K++;
    }

    private void Mirtis()
    {
        MirciuSk++;
        Duomenys.D++;
        if (PaskutinisZalojesZaidejas != null)
        {
            PaskutinisZalojesZaidejas.GetComponent<Zaidejas>().NuzudeKitaZaideja();
        }

        AtnaujintiZaidejuSarasa();
        foreach (Zaidejas z in ZaidejuSarasas)
        {
            if (PaskutinisZalojesZaidejas != null)
            {
                z.MirtiesPranesimas(PaskutinisZalojesZaidejas.GetComponent<Zaidejas>(), this);
            }
            else
            {
                z.MirtiesPranesimas(null, this);
            }
        }

        photonView.RPC("RPCMirtis", RpcTarget.All);
    }

    [PunRPC]
    public void RPCMirtis()
    {
        Gyvybes = 0;
        Gyvas = false;
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Judejimas>().enabled = false;
        ZaidimoDrobe.SetActive(false);
        PuolimoLaikoIgnoravimas++;

        LikesPrisikelimoLaikas = PrisikelimoLaikas;
    }

    public void MirtiesPranesimas(Zaidejas Zudikas, Zaidejas Mirusysis)
    {
        GameObject MirtiesPranesimas = Instantiate(MirtiesPranesimoObjektas);
        MirtiesPranesimas.transform.SetParent(MirtiesPranesimuLentele.transform);
        MirtiesPranesimas.transform.SetAsFirstSibling();
        TextMeshProUGUI PranesimoZudikoTekstas = MirtiesPranesimas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI PranesimoTarpinisTekstas = MirtiesPranesimas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI PranesimoMirusiojoTekstas = MirtiesPranesimas.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        if (Zudikas != null)
        {
            PranesimoZudikoTekstas.text = Zudikas.Vardas;
            PranesimoZudikoTekstas.color = VardoKomanduSpalvos[Zudikas.KomandosNr];
            PranesimoTarpinisTekstas.text = " has slain ";
            PranesimoMirusiojoTekstas.text = Mirusysis.Vardas;
            PranesimoMirusiojoTekstas.color = VardoKomanduSpalvos[Mirusysis.KomandosNr];
        }
        else
        {
            PranesimoZudikoTekstas.text = Mirusysis.Vardas;
            PranesimoZudikoTekstas.color = VardoKomanduSpalvos[Mirusysis.KomandosNr];
            PranesimoTarpinisTekstas.text = " has commited suicide";
            PranesimoMirusiojoTekstas.gameObject.SetActive(false);
        }

        Destroy(MirtiesPranesimas, 10);
    }
    /*
    [PunRPC]
    public void RPCMirtiesPranesimas (Zaidejas Zudikas, Zaidejas Mirusysis)
    {
        
    }
    */
    [PunRPC]
    public void RPCPrisikelimas()
    {
        //AtsiradimoDarbai();

        Gyvybes = MaxGyvybes;//
        Skydas = 0;//
        Gyvas = true;//
        PaskutinisZalojesZaidejas = null;
        int Nr = Random.Range(0, ZaidimoValdymoKodas.AtsiradimoVietos.Length);
        transform.position = ZaidimoValdymoKodas.AtsiradimoVietos[Nr].position;
        transform.rotation = ZaidimoValdymoKodas.AtsiradimoVietos[Nr].rotation;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<Judejimas>().enabled = true;
        PuolimoLaikoIgnoravimas = 0;//
        
        //
        if (!photonView.IsMine)
        {
            ZaidimoDrobe.SetActive(true);
        }
        else
        {
            ZaidimoDrobe.SetActive(false);
        }
        //
    }

    public void GreicioIrSoklumoPerskaiciavimas()
    {
        Greitis = PradinisGreitis * GreicioMod;
        Soklumas = PradinisSoklumas * SoklumoMod;
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

    private void ZaidejuInformacijosLentelesValdymas(bool ArAktyvinti)
    {
        if(ArAktyvinti)
        {
            ZaidejuIrKomanduInformacijosLentele.SetActive(true);
            AtnaujintiZaidejuSarasa();

            GameObject VirsutineEilute = Instantiate(ZaidejoInformacijosObjektas);
            VirsutineEilute.transform.SetParent(ZaidejuInformacijosLentele);
            VirsutineEilute.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.grey;
            VirsutineEilute.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.grey;
            VirsutineEilute.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.grey;
            VirsutineEilute.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = Color.grey;

            int[] KomanduNuzudymai = new int[5];
            int[] KomanduMirtys = new int[5];

            foreach (Zaidejas z in ZaidejuSarasas)
            {
                GameObject Eilute = Instantiate(ZaidejoInformacijosObjektas);
                Eilute.transform.SetParent(ZaidejuInformacijosLentele);
                Eilute.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = z.Vardas;
                Eilute.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (z.KomandosNr == 0) ? "SOLO" : z.KomandosNr.ToString();
                Eilute.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = z.NuzudymuSk.ToString();
                Eilute.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = z.MirciuSk.ToString();
                Eilute.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = VardoKomanduSpalvos[z.KomandosNr];
                Eilute.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = VardoKomanduSpalvos[z.KomandosNr];
                Eilute.transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = VardoKomanduSpalvos[z.KomandosNr];
                Eilute.transform.GetChild(3).GetComponent<TextMeshProUGUI>().color = VardoKomanduSpalvos[z.KomandosNr];

                KomanduNuzudymai[z.KomandosNr] += z.NuzudymuSk;
                KomanduMirtys[z.KomandosNr] += z.MirciuSk;
            }

            for (int i = 0; i < 5; i++)
            {
                KomanduInformacijosLaukeliai[i].GetComponent<Image>().color = VardoKomanduSpalvos[i];
                KomanduInformacijosLaukeliai[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = KomanduNuzudymai[i].ToString() + " / " + KomanduMirtys[i];
            }   
        }
        else
        {
            foreach (Transform t in ZaidejuInformacijosLentele)
            {
                Destroy(t.gameObject);
            }
            ZaidejuIrKomanduInformacijosLentele.SetActive(false);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Gyvybes);
            stream.SendNext(Skydas);
            stream.SendNext(Gyvas);
            stream.SendNext(Vardas);
            stream.SendNext(ElementoNr);
            stream.SendNext(NuzudymuSk);
            stream.SendNext(MirciuSk);
            stream.SendNext(KomandosNr);
        }
        else if (stream.IsReading)
        {
            Gyvybes = (float)stream.ReceiveNext();
            Skydas = (float)stream.ReceiveNext();
            Gyvas = (bool)stream.ReceiveNext();
            Vardas = (string)stream.ReceiveNext();
            ElementoNr = (int)stream.ReceiveNext();
            NuzudymuSk = (int)stream.ReceiveNext();
            MirciuSk = (int)stream.ReceiveNext();
            KomandosNr = (int)stream.ReceiveNext();
        }
    }

    void ZaidejoPasirinkimai ()
    {
        OroKodas.enabled = false;
        VandensKodas.enabled = false;
        ZemesKodas.enabled = false;
        UgniesKodas.enabled = false;

        if (photonView.IsMine)
        {
            switch (Duomenys.Elementas)
            {
                case 1:
                    OroKodas.enabled = true;
                    ElementoNr = 1;
                    break;
                case 2:
                    VandensKodas.enabled = true;
                    ElementoNr = 2;
                    break;
                case 3:
                    ZemesKodas.enabled = true;
                    ElementoNr = 3;
                    break;
                case 4:
                    UgniesKodas.enabled = true;
                    ElementoNr = 4;
                    break;
            }
            Vardas = Duomenys.Slapyvardis;
            KomandosNr = Duomenys.KomandosNr;
        }
    }
}
