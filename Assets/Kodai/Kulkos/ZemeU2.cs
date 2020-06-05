using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZemeU2 : Kulka
{
    [HideInInspector]
    public Vector3 Tikslas;
    [HideInInspector]
    public float Aukstis;
    [HideInInspector]
    public float ZalaPoKontakto;
    [HideInInspector]
    public float SunaikinimoLaikasPoKontakto;

    private bool Atsitrenke = false;

    private float Gravitacija = Physics.gravity.y;

    public override void Start()
    {
        base.Start();

        float PokytisY = Tikslas.y - transform.position.y;
        Vector3 PokytisXZ = new Vector3(Tikslas.x - transform.position.x, 0, Tikslas.z - transform.position.z);
        float SkrydzioLaikas = Mathf.Sqrt(-2 * Aukstis / Gravitacija) + Mathf.Sqrt(2 * (PokytisY - Aukstis) / Gravitacija);
        Vector3 GreicioVectoriusY = Vector3.up * Mathf.Sqrt(-2 * Gravitacija * Aukstis);
        Vector3 GreicioVektoriusXZ = PokytisXZ / SkrydzioLaikas;

        gameObject.GetComponent<Rigidbody>().velocity = GreicioVectoriusY + GreicioVektoriusXZ;
    }

    public override void OnCollisionEnter(Collision c)
    {
        base.OnCollisionEnter(c);

        if (!Atsitrenke)
        {
            Zala = ZalaPoKontakto;
            Destroy(gameObject, SunaikinimoLaikasPoKontakto);
            Atsitrenke = true;
        }
    }
}