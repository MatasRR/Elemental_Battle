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
    public Material[] ZaidejoIsvaizdosMedziagos;
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
    public float AtakosMod;
    [HideInInspector]
    public float GynybosMod;

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

    public MeshRenderer ZaidejoGrafikosObjektas;
    [HideInInspector]
    public int ZaidejoIsvaizdosMedziagosNr;
    [HideInInspector]
    public int ZaidejoPaveiksleliuNr;

    public GameObject ZalosTekstoObjektas;
    public Color OroSpalva;
    public Color VandensSpalva;
    public Color ZemesSpalva;
    public Color UgniesSpalva;

    //private bool DuomenysAtnaujinti;

    [Header("UI: ")]
    public Image[] EkranoDrobesLaukeliai;
    public TextMeshProUGUI BCDTekstas;
    public TextMeshProUGUI BBCDTekstas;
    public TextMeshProUGUI BBBCDTekstas;
    public TextMeshProUGUI UCDTekstas;
    public Image BCDFonas;
    public Image BBCDFonas;
    public Image BBBCDFonas;
    public Image UCDFonas;
    public Image BPaveikslelis;
    public Image BBPaveikslelis;
    public Image BBBPaveikslelis;
    public Image UPaveikslelis;

    public Image GyvybiuJuostele;
    public Image SkydoJuostele;
    public TextMeshProUGUI GyvybiuTekstas;
    public TextMeshProUGUI SkydoTekstas;
    public Image MazojiGyvybiuJuostele;
    public Image MazojiSkydoJuostele;
    public TextMeshProUGUI MazasisGyvybiuTekstas;
    public TextMeshProUGUI MazasisSkydoTekstas;
    public TextMeshProUGUI MazasisVardoTekstas;

    public GameObject PaveiksliukasGreitisPlius;
    public GameObject PaveiksliukasGreitisMinus;
    public GameObject PaveiksliukasGreitisNulis;
    public GameObject PaveiksliukasAtakaPlius;
    public GameObject PaveiksliukasAtakaMinus;
    public GameObject PaveiksliukasAtakaNulis;
    public GameObject PaveiksliukasGynybaPlius;
    public GameObject PaveiksliukasGynybaMinus;
    public GameObject PaveiksliukasGynybaNulis;
    public GameObject PaveiksliukasSkraidymas;

    public GameObject MazasisPaveiksliukasGreitisPlius;
    public GameObject MazasisPaveiksliukasGreitisMinus;
    public GameObject MazasisPaveiksliukasGreitisNulis;
    public GameObject MazasisPaveiksliukasAtakaPlius;
    public GameObject MazasisPaveiksliukasAtakaMinus;
    public GameObject MazasisPaveiksliukasAtakaNulis;
    public GameObject MazasisPaveiksliukasGynybaPlius;
    public GameObject MazasisPaveiksliukasGynybaMinus;
    public GameObject MazasisPaveiksliukasGynybaNulis;
    public GameObject MazasisPaveiksliukasSkraidymas;

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

    public TextMeshProUGUI TestinisTekstas;

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
        GreicioMod = SoklumoMod = AtakosMod = GynybosMod = 1f;
        SkraidymoCCLaikas = JudejimoCCLaikas = PuolimoCCLaikas = PaskutinioZalojusioZaidejoLaikmatis = 0f;
        SkraidymoLaikoIgnoravimas = JudejimoLaikoIgnoravimas = PuolimoLaikoIgnoravimas = 0;
        KomandosTekstas.text = (KomandosNr == 0) ? "SOLO" : "TEAM " + KomandosNr;
        ZaidejoIsvaizdosMedziagosNr = Duomenys.IsvaizdosMedziagosNr;
        ZaidejoPaveiksleliuNr = Duomenys.GebejimuPaveiksleliuNr;
        NuzudymuSk = Duomenys.K;
        MirciuSk = Duomenys.D;
        //DuomenysAtnaujinti = false;
        Gyvas = true;

        foreach (Image i in EkranoDrobesLaukeliai)
        {
            switch (ElementoNr)
            {
                case 1:
                    i.color = OroSpalva;
                    break;
                case 2:
                    i.color = VandensSpalva;
                    break;
                case 3:
                    i.color = ZemesSpalva;
                    break;
                case 4:
                    i.color = UgniesSpalva;
                    break;
            }
            i.color = new Color(i.color.r, i.color.g, i.color.b, .75f);
        }

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

        // OnPlayerJoined() {DuomenysAtnaujinti = false};
        */
        MazasisVardoTekstas.text = Vardas;
        MazasisVardoTekstas.color = VardoKomanduSpalvos[KomandosNr];

        ZaidejoGrafikosObjektas.material = ZaidejoIsvaizdosMedziagos[ZaidejoIsvaizdosMedziagosNr];
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

            if (GaliJudeti)
            {
                PaveiksliukasGreitisNulis.SetActive(false);
                if (Greitis > PradinisGreitis)
                {
                    PaveiksliukasGreitisPlius.SetActive(true);
                    PaveiksliukasGreitisMinus.SetActive(false);
                }
                else if (Greitis < PradinisGreitis)
                {
                    PaveiksliukasGreitisPlius.SetActive(false);
                    PaveiksliukasGreitisMinus.SetActive(true);
                }
                else
                {
                    PaveiksliukasGreitisPlius.SetActive(false);
                    PaveiksliukasGreitisMinus.SetActive(false);
                }
            }
            else
            {
                PaveiksliukasGreitisMinus.SetActive(false);
                PaveiksliukasGreitisPlius.SetActive(false);
                PaveiksliukasGreitisNulis.SetActive(true);
            }

            if (GaliPulti)
            {
                PaveiksliukasAtakaNulis.SetActive(false);
                if (AtakosMod > 1f)
                {
                    PaveiksliukasAtakaPlius.SetActive(true);
                    PaveiksliukasAtakaMinus.SetActive(false);
                }
                else if (AtakosMod < 1f)
                {
                    PaveiksliukasAtakaPlius.SetActive(false);
                    PaveiksliukasAtakaMinus.SetActive(true);
                }
                else
                {
                    PaveiksliukasAtakaPlius.SetActive(false);
                    PaveiksliukasAtakaMinus.SetActive(false);
                }
            }
            else
            {
                PaveiksliukasAtakaPlius.SetActive(false);
                PaveiksliukasAtakaMinus.SetActive(false);
                PaveiksliukasAtakaNulis.SetActive(true);
            }

            if (true)
            {
                PaveiksliukasGynybaNulis.SetActive(false);
                if (GynybosMod < 1f)
                {
                    PaveiksliukasGynybaPlius.SetActive(true);
                    PaveiksliukasGynybaMinus.SetActive(false);
                }
                else if (GynybosMod > 1f)
                {
                    PaveiksliukasGynybaPlius.SetActive(false);
                    PaveiksliukasGynybaMinus.SetActive(true);
                }
                else
                {
                    PaveiksliukasGynybaPlius.SetActive(false);
                    PaveiksliukasGynybaMinus.SetActive(false);
                }
            }
            else
            {
                PaveiksliukasGynybaPlius.SetActive(false);
                PaveiksliukasGynybaMinus.SetActive(false);
                PaveiksliukasGynybaNulis.SetActive(true);
            }


            if (GaliSkraidyti)
            {
                PaveiksliukasSkraidymas.SetActive(true);
            }
            else
            {
                PaveiksliukasSkraidymas.SetActive(false);
            }
        }

        MazojiGyvybiuJuostele.fillAmount = Gyvybes / MaxGyvybes;
        MazojiSkydoJuostele.fillAmount = Mathf.Min(Skydas / MaxGyvybes, 1); ;
        MazasisGyvybiuTekstas.text = Mathf.Ceil(Gyvybes).ToString();
        MazasisSkydoTekstas.text = Mathf.Ceil(Skydas).ToString();

        if (GaliJudeti)
        {
            MazasisPaveiksliukasGreitisNulis.SetActive(false);
            if (Greitis > PradinisGreitis)
            {
                MazasisPaveiksliukasGreitisPlius.SetActive(true);
                MazasisPaveiksliukasGreitisMinus.SetActive(false);
            }
            else if (Greitis < PradinisGreitis)
            {
                MazasisPaveiksliukasGreitisPlius.SetActive(false);
                MazasisPaveiksliukasGreitisMinus.SetActive(true);
            }
            else
            {
                MazasisPaveiksliukasGreitisPlius.SetActive(false);
                MazasisPaveiksliukasGreitisMinus.SetActive(false);
            }
        }
        else
        {
            MazasisPaveiksliukasGreitisMinus.SetActive(false);
            MazasisPaveiksliukasGreitisPlius.SetActive(false);
            MazasisPaveiksliukasGreitisNulis.SetActive(true);
        }

        if (GaliPulti)
        {
            MazasisPaveiksliukasAtakaNulis.SetActive(false);
            if (AtakosMod > 1f)
            {
                MazasisPaveiksliukasAtakaPlius.SetActive(true);
                MazasisPaveiksliukasAtakaMinus.SetActive(false);
            }
            else if (AtakosMod < 1f)
            {
                MazasisPaveiksliukasAtakaPlius.SetActive(false);
                MazasisPaveiksliukasAtakaMinus.SetActive(true);
            }
            else
            {
                MazasisPaveiksliukasAtakaPlius.SetActive(false);
                MazasisPaveiksliukasAtakaMinus.SetActive(false);
            }
        }
        else
        {
            MazasisPaveiksliukasAtakaPlius.SetActive(false);
            MazasisPaveiksliukasAtakaMinus.SetActive(false);
            MazasisPaveiksliukasAtakaNulis.SetActive(true);
        }

        if (true)
        {
            MazasisPaveiksliukasGynybaNulis.SetActive(false);
            if (GynybosMod < 1f)
            {
                MazasisPaveiksliukasGynybaPlius.SetActive(true);
                MazasisPaveiksliukasGynybaMinus.SetActive(false);
            }
            else if (GynybosMod > 1f)
            {
                MazasisPaveiksliukasGynybaPlius.SetActive(false);
                MazasisPaveiksliukasGynybaMinus.SetActive(true);
            }
            else
            {
                MazasisPaveiksliukasGynybaPlius.SetActive(false);
                MazasisPaveiksliukasGynybaMinus.SetActive(false);
            }
        }
        else
        {
            MazasisPaveiksliukasGynybaPlius.SetActive(false);
            MazasisPaveiksliukasGynybaMinus.SetActive(false);
            MazasisPaveiksliukasGynybaNulis.SetActive(true);
        }


        if (GaliSkraidyti)
        {
            MazasisPaveiksliukasSkraidymas.SetActive(true);
        }
        else
        {
            MazasisPaveiksliukasSkraidymas.SetActive(false);
        }
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

    public void GautiZalos (float Zala, int ZalosElementoNr)
    {
        if (photonView.IsMine)
        {
            float ModifikuotaZala = Zala * GynybosMod;
            photonView.RPC("RPCGautiZalos", RpcTarget.All, ModifikuotaZala, ZalosElementoNr);
        }
    }

    public void GautiDOTZalos(float Zala, float Daznis, float Trukme, int ZalosElementoNr)
    {
        if (photonView.IsMine)
        {
            StartCoroutine(_GautiDOTZalos(Zala, Daznis, Trukme, ZalosElementoNr));
        }
    }

    private IEnumerator _GautiDOTZalos (float Zala, float Daznis, float Trukme, int ZalosElementoNr)
    {
        for (float i = 0; i < Trukme; i += Daznis)
        {
            float ModifikuotaZala = Zala * GynybosMod;
            photonView.RPC("RPCGautiZalos", RpcTarget.All, ModifikuotaZala, ZalosElementoNr);
            yield return new WaitForSeconds(Daznis);
        }        
    }

    [PunRPC]
    private void RPCGautiZalos(float Zala, int ZalosElementoNr)
    {
        if (Zala <= 0)
        {
            return;
        }

        Gyvybes -= Mathf.Clamp(Zala - Skydas, 0, Mathf.Infinity);
        Skydas -= Zala;

        GameObject ZalosTekstas = Instantiate(ZalosTekstoObjektas, transform.position, transform.rotation);
        ZalosTekstas.GetComponent<TextMeshPro>().text = Zala.ToString();
        ZalosTekstas.GetComponent<ConstantForce>().force = new Vector3(Random.Range(-1, 1), Random.Range(4, 8), Random.Range(-1, 1));

        if (ZalosElementoNr == 1)
        {
            ZalosTekstas.GetComponent<TextMeshPro>().color = OroSpalva;
        }
        else if (ZalosElementoNr == 2)
        {
            ZalosTekstas.GetComponent<TextMeshPro>().color = VandensSpalva;
        }
        else if (ZalosElementoNr == 3)
        {
            ZalosTekstas.GetComponent<TextMeshPro>().color = ZemesSpalva;
        }
        else if (ZalosElementoNr == 4)
        {
            ZalosTekstas.GetComponent<TextMeshPro>().color = UgniesSpalva;
        }

        Destroy(ZalosTekstas, 2);
    }

    public void Suletinti (float SuletinimoStipris, float SoklumoSilpninimoStipris, float SuletinimoLaikas)
    {
        if (photonView.IsMine)
        {
            StartCoroutine(_Suletinti(SuletinimoStipris, SoklumoSilpninimoStipris, SuletinimoLaikas));
        }
    }

    private IEnumerator _Suletinti(float SuletinimoStipris, float SoklumoSilpninimoStipris, float SuletinimoLaikas)
    {
        GreicioMod *= SuletinimoStipris;
        SoklumoMod *= SoklumoSilpninimoStipris;
        GreicioIrSoklumoPerskaiciavimas();
        yield return new WaitForSeconds(SuletinimoLaikas);
        GreicioMod /= SuletinimoStipris;
        SoklumoMod /= SoklumoSilpninimoStipris;
        GreicioIrSoklumoPerskaiciavimas();
    }

    public void KeistiAtakaIrGynyba(float AtakosPokytis, float GynybosPokytis, float Trukme)
    {
        StartCoroutine(_KeistiAtakaIrGynyba(AtakosPokytis, GynybosPokytis, Trukme));
    }

    private IEnumerator _KeistiAtakaIrGynyba(float AtakosPokytis, float GynybosPokytis, float Trukme)
    {
        AtakosMod *= (AtakosPokytis != 0 ? AtakosPokytis : 1);
        GynybosMod *= (GynybosPokytis != 0 ? GynybosPokytis : 1);
        yield return new WaitForSeconds(Trukme);
        AtakosMod /= (AtakosPokytis != 0 ? AtakosPokytis : 1);
        GynybosMod /= (GynybosPokytis != 0 ? GynybosPokytis : 1);
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
            stream.SendNext(ZaidejoIsvaizdosMedziagosNr);
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
            ZaidejoIsvaizdosMedziagosNr = (int)stream.ReceiveNext();
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
