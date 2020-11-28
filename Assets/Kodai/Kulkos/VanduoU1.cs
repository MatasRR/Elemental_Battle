using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanduoU1 : Kulka
{
    public float GreitejimoProcentas;
    public float PlatejimoProcentas;
    public float NuotolisIkiZemes;
    public float NaikinimoRiba;
    public float KliutiesPerlipimoAukstis;
    public float YPokytis;
    public float PradziosYMod;

    public override void Start()
    {
        base.Start();

        GreitejimoProcentas += 1f;

        StartCoroutine(PradinisAukscioNustatymas());
        InvokeRepeating("Stiprejimas", 0, 0.1f);
    }

    public override void Update()
    {
        base.Update();
        
        if ((VirsAukstyn() && VirsZemyn()) || (!ToliAukstyn() && !ToliZemyn()))
        {
            Destroy(gameObject);
        }
        else if (VirsZemyn())
        {
            transform.position += Vector3.up * YPokytis;
        }
        else if (ToliZemyn() && !ArtiZemyn())
        {
            transform.position -= Vector3.up * YPokytis;
        }
    }
    
    void Stiprejimas()
    {
        GetComponent<Rigidbody>().velocity *= GreitejimoProcentas;
        float Dydis = transform.localScale.x * PlatejimoProcentas;
        transform.localScale += new Vector3(Dydis, 0, 0);
    }
    
    public bool ArtiZemyn()
    {
        return Physics.Raycast(transform.position, -Vector3.up, NuotolisIkiZemes);
    }

    public bool ToliZemyn()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out RaycastHit Info))
        {
            return !Info.collider.CompareTag("Išversta sfera") ? true : false;
        }
        else
        {
            return false;
        }
    }

    public bool ArtiAukstyn()
    {
        return Physics.Raycast(transform.position, Vector3.up, NaikinimoRiba);
    }

    public bool ToliAukstyn()
    {
        if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit Info))
        {
            return !Info.collider.CompareTag("Išversta sfera") ? true : false;
        }
        else
        {
            return false;
        }
    }

    public bool VirsZemyn()
    {
        if (Physics.Raycast(transform.position + (Vector3.up * NaikinimoRiba), -Vector3.up, out RaycastHit Info, NaikinimoRiba))
        {
            return !Info.collider.gameObject == gameObject ? true : false;
        }
        else
        {
            return false;
        }
    }

    public bool VirsAukstyn()
    {
        return Physics.Raycast(transform.position + (Vector3.up * NaikinimoRiba), Vector3.up, KliutiesPerlipimoAukstis);
    }


    IEnumerator PradinisAukscioNustatymas()
    {
        YPokytis *= PradziosYMod;
        yield return new WaitForSeconds(1);
        YPokytis /= PradziosYMod;
    }
}
