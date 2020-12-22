using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;

public class Judejimas : MonoBehaviourPun
{
    private Zaidejas ZaidejoKodas;
    private Camera Kamera;
    private Rigidbody RB;
    private float KamerosPersisukimas = 0f;

    public Slider PelesJautrumoSlankiklis;
    public float PelesJautrumas;

    public float NuotolisIkiZemes;
    public float KritimoDaugiklis;
    public float ZemoSuolioDaugiklis;

    [HideInInspector]
    public bool AtidarytasUILangas = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        RB = gameObject.GetComponent<Rigidbody>();
        ZaidejoKodas = gameObject.GetComponent<Zaidejas>();
        Kamera = ZaidejoKodas.Kamera;
        PelesJautrumoSlankiklis.value = PelesJautrumas;
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (ZaidejoKodas.GaliJudeti && (ZaidejoKodas.LikesGebejimuAktyvavimoLaikas <= 0 || !ZaidejoKodas.StiprusGebejimoAktyvavimas))
        {
            ZaidejoJudejimas();
        }
        
        if (!AtidarytasUILangas && (ZaidejoKodas.LikesGebejimuAktyvavimoLaikas <= 0 || !ZaidejoKodas.StiprusGebejimoAktyvavimas))
        {
            ZaidejoSukimasis();
        }
    }


    void ZaidejoJudejimas()
    {
        Vector3 JudX = Input.GetAxisRaw("Horizontal") * transform.right;
        Vector3 JudZ = Input.GetAxisRaw("Vertical") * transform.forward;

        Vector3 Jud = (JudX + JudZ).normalized * ZaidejoKodas.Greitis;
        RB.MovePosition(RB.position + Jud * Time.fixedDeltaTime);

        if (Input.GetKey(KeyCode.Space) && (ZaidejoKodas.GaliSkraidyti || AntZemes()))
        {
            RB.AddForce(Vector3.up * ZaidejoKodas.Soklumas, ForceMode.Impulse);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && ZaidejoKodas.GaliSkraidyti)
        {
            RB.AddForce(-Vector3.up * ZaidejoKodas.Soklumas * 0.5f, ForceMode.Impulse);
        }

        if (!ZaidejoKodas.GaliSkraidyti)
        {
            if (RB.velocity.y < 0)
            {
                RB.velocity += Vector3.up * Physics.gravity.y * (KritimoDaugiklis - 1) * Time.fixedDeltaTime;
            }
            else if (RB.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
            {
                RB.velocity += Vector3.up * Physics.gravity.y * (ZemoSuolioDaugiklis - 1) * Time.fixedDeltaTime;
            }
        }
    }

    void ZaidejoSukimasis()
    {
        float SukX = Input.GetAxisRaw("Mouse X") * PelesJautrumas;
        float SukY = Input.GetAxisRaw("Mouse Y") * PelesJautrumas;

        Vector3 Suk = new Vector3(0, SukX, 0);

        RB.MoveRotation(RB.rotation * Quaternion.Euler(Suk));
        Kamera.transform.Rotate(Vector3.left * SukY);

        // Kameros valdymas, kad nepersisuktu
        KamerosPersisukimas += SukY;

        if (KamerosPersisukimas > 90f)
        {
            KamerosPersisukimas = 90f;
            SukY = 0f;
            Vector3 Pasisukimas = Kamera.transform.eulerAngles;
            Pasisukimas.x = 270f;
            Kamera.transform.eulerAngles = Pasisukimas;
        }
        else if (KamerosPersisukimas < -90f)
        {
            KamerosPersisukimas = -90f;
            SukY = 0f;
            Vector3 Pasisukimas = Kamera.transform.eulerAngles;
            Pasisukimas.x = 90f;
            Kamera.transform.eulerAngles = Pasisukimas;
        }
    }

    public bool AntZemes()
    {
        return Physics.Raycast(transform.position, -Vector3.up, NuotolisIkiZemes + 0.5f);
    }

    public void KeistiPelesJautruma()
    {
        PelesJautrumas = PelesJautrumoSlankiklis.value;
    }

    public void KeistiUILanguAtidarymoBusena(bool Atidarytas)
    {
        if (Atidarytas)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            AtidarytasUILangas = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            AtidarytasUILangas = false;
        }
    }
}
