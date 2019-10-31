using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class Judejimas : MonoBehaviourPun
{
    private Rigidbody RB;
    private Zaidejas ZaidejoKodas;
    private Camera Kamera;

    private float KamerosPersisukimas = 0f;
    public float PelesJautrumas;
    public float NuotolisIkiZemes;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        RB = gameObject.GetComponent<Rigidbody>();
        ZaidejoKodas = gameObject.GetComponent<Zaidejas>();
        Kamera = ZaidejoKodas.Kamera;
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (ZaidejoKodas.GaliJudeti)
        {
            ZaidejoJudejimas();
        }
        ZaidejoSukimasis();
        KursoriausValdymas();
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

    void KursoriausValdymas()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public bool AntZemes()
    {
        return Physics.Raycast(transform.position, -Vector3.up, NuotolisIkiZemes + 0.5f);
    }
}
